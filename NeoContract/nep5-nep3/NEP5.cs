using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.ComponentModel;
using System.Numerics;

namespace nep5_nep3
{
    [Features(ContractFeatures.HasStorage | ContractFeatures.Payable)]
    public class nep5: SmartContract
    {
        #region Token Settings
        static readonly ulong MaxSupply = 10_000_000_000_000_000;
        static readonly ulong InitialSupply = 2_000_000_000_000_000;
        static readonly byte[] Owner = new byte[] { 0xfa, 0x79, 0x76, 0x3b, 0x86, 0x76, 0x7b, 0x42, 0x68, 0x72, 0x34, 0x9f, 0xd2, 0xfd, 0xbc, 0xcf, 0x16, 0x2e, 0xe2, 0x20 };
        static readonly ulong TokensPerNEO = 1_000_000_000;
        static readonly ulong TokensPerGAS = 1;
        static readonly byte[] NeoToken = new byte[] { 0x89, 0x77, 0x20, 0xd8, 0xcd, 0x76, 0xf4, 0xf0, 0x0a, 0xbf, 0xa3, 0x7c, 0x0e, 0xdd, 0x88, 0x9c, 0x20, 0x8f, 0xde, 0x9b };
        static readonly byte[] GasToken = new byte[] { 0x3b, 0x7d, 0x37, 0x11, 0xc6, 0xf0, 0xcc, 0xf9, 0xb1, 0xdc, 0xa9, 0x03, 0xd1, 0xbf, 0xa1, 0xd8, 0x96, 0xf1, 0x23, 0x8c };
        #endregion

        #region Notifications
        [DisplayName("Transfer")]
        public static event Action<byte[], byte[], BigInteger> OnTransfer;
        #endregion

        #region Storage key prefixes
        static readonly byte[] StoragePrefixBalance = new byte[] { 0x01, 0x01 };
        static readonly byte[] StoragePrefixContract = new byte[] { 0x02, 0x02 };
        #endregion       

        public static string Name() => "Token Name Test01";
        public static string Symbol() => "TokenSymbol";
        public static ulong Decimals() => 8;
        public static string[] SupportedStandards() => new string[] { "NEP-5", "NEP-10" };

        public static bool Deploy()
        {
            if (!Runtime.CheckWitness(Owner))
            {
                return false;
            }

            StorageMap contract = Storage.CurrentContext.CreateMap(StoragePrefixContract);
            if (contract.Get("totalSupply") != null)
                throw new Exception("Contract already deployed！");

            StorageMap balances = Storage.CurrentContext.CreateMap(StoragePrefixBalance);
            balances.Put(Owner, InitialSupply);
            contract.Put("totalSupply", InitialSupply);

            OnTransfer(null, Owner, InitialSupply);
            return true;
        }

        public static bool Migrate(byte[] script, string manifest)
        {
            if (!Runtime.CheckWitness(Owner))
            {
                return false;
            }
            if (script.Length == 0 || manifest.Length == 0)
            {
                return false;
            }
            Contract.Update(script, manifest);
            return true;
        }

        public static bool Destroy()
        {
            if (!Runtime.CheckWitness(Owner))
            {
                return false;
            }

            Contract.Destroy();
            return true;
        }

        private static BigInteger GetTransactionAmount(object state)
        {
            var notification = (object[])state;
            // Checks notification format
            if (notification.Length != 4) return 0;
            // Only allow Transfer notifications
            if ((string)notification[0] != "Transfer") return 0;
            // Check dest
            if ((byte[])notification[2] != ExecutionEngine.ExecutingScriptHash) return 0;
            // Amount
            var amount = (BigInteger)notification[3];
            if (amount < 0) return 0;
            return amount;
        }

        public static bool Mint()
        {
            if (Runtime.InvocationCounter != 1)
                throw new Exception();

            var notifications = Runtime.GetNotifications();
            if (notifications.Length == 0)
                throw new Exception("Contribution transaction not found");

            BigInteger neo = 0;
            BigInteger gas = 0;

            for (int i = 0; i < notifications.Length; i++)
            {
                var notification = notifications[i];

                if (notification.ScriptHash == NeoToken)
                {
                    neo += GetTransactionAmount(notification.State);
                }
                else if (notification.ScriptHash == GasToken)
                {
                    gas += GetTransactionAmount(notification.State);
                }
            }

            StorageMap contract = Storage.CurrentContext.CreateMap(StoragePrefixContract);
            var supply = contract.Get("totalSupply");
            if (supply == null)
                throw new Exception("Contract not deployed");

            var current_supply = supply.ToBigInteger();
            var avaliable_supply = MaxSupply - current_supply;

            var contribution = (neo * TokensPerNEO) + (gas * TokensPerGAS);
            if (contribution <= 0)
                throw new Exception();
            if (contribution > avaliable_supply)
                throw new Exception();

            StorageMap balances = Storage.CurrentContext.CreateMap(StoragePrefixBalance);
            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
            var balance = balances.Get(tx.Sender)?.ToBigInteger() ?? 0;
            balances.Put(tx.Sender, balance + contribution);
            contract.Put("totalSupply", current_supply + contribution);

            OnTransfer(null, tx.Sender, balance + contribution);
            return true;
        }

        public static BigInteger TotalSupply()
        {
            StorageMap contract = Storage.CurrentContext.CreateMap(StoragePrefixContract);
            return contract.Get("totalSupply")?.ToBigInteger() ?? 0;
        }

        public static BigInteger BalanceOf(byte[] account)
        {
            if (!ValidateAddress(account)) throw new FormatException("The parameter 'account' SHOULD be 20-byte addresses.");

            Runtime.Log("test log");
            Runtime.Notify(0);

            StorageMap balances = Storage.CurrentContext.CreateMap(StoragePrefixBalance);
            return balances.Get(account)?.ToBigInteger() ?? 0;
        }

        public static bool Transfer(byte[] from, byte[] to, BigInteger amount)
        {
            if (!ValidateAddress(from)) throw new FormatException("The parameter 'from' SHOULD be 20-byte addresses.");
            if (!ValidateAddress(to)) throw new FormatException("The parameters 'to' SHOULD be 20-byte addresses.");
            if (!IsPayable(to)) return false;
            if (amount <= 0) throw new InvalidOperationException("The parameter amount MUST be greater than 0.");
            if (!Runtime.CheckWitness(from)) return false;

            StorageMap balances = Storage.CurrentContext.CreateMap(StoragePrefixBalance);
            BigInteger fromAmount = balances.Get(from).ToBigInteger();

            if (fromAmount < amount) return false;
            if (amount == 0 || from == to) return true;

            if (fromAmount == amount)
            {
                balances.Delete(from);
            }
            else
            {
                balances.Put(from, fromAmount - amount);
            }

            BigInteger toAmount = balances.Get(to)?.ToBigInteger() ?? 0;
            balances.Put(to, toAmount + amount);

            OnTransfer(from, to, amount);
            return true;
        }

        public static bool ValidateAddress(byte[] address)
        {
            if (address.Length != 20)
                return false;
            if (address.ToBigInteger() == 0)
                return false;
            return true;
        }

        public static bool IsPayable(byte[] address)
        {
            var c = Blockchain.GetContract(address);
            return c == null || c.IsPayable;
        }
    }
}
