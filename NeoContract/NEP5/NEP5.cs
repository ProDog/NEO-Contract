using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NEP5
{
    [Features(ContractFeatures.HasStorage | ContractFeatures.Payable)]
    public partial class NEP5 : SmartContract
    {
        #region Token Settings
        public static string Name() => "Token Name Migrate Test";

        public static string Symbol() => "TokenSymbol";

        public static byte Decimals() => 8;
   
        public static ulong MaxSupply() => 1_000_000_000;

        public static ulong InitialSupply() => 20_000_000; 

        //fa 79 76 3b 86 76 7b 42 68 72 34 9f d2 fd bc cf 16 2e e2 20 合约中此处用小端序
        static byte[] Owner = "NNU67Fvdy3LEQTM374EJ9iMbCRxVExgM8Y".ToScriptHash();

        public static ulong TokensPerNEO() => 1;

        public static ulong TokensPerGAS() => 1;

        static readonly byte[] NeoToken = "0xde5f57d430d3dece511cf975a8d37848cb9e0525".HexToBytes();
        static readonly byte[] GasToken = "0x668e0c1f9d7b70a99dd9e06eadd4c784d641afbc".HexToBytes();
        #endregion

        #region Notifications

        public delegate void mydelegate(params object[] arg);

        [DisplayName("event")]
        public static event mydelegate Notify;
       
        [DisplayName("Transfer")]
        public static event Action<byte[], byte[], BigInteger> OnTransfer;       

        #endregion

        #region Storage key prefixes
        private static byte[] GetStoragePrefixBalance() => new byte[] { 0x01, 0x01 };
        private static byte[] GetStoragePrefixContract() => new byte[] { 0x02, 0x02 };
        #endregion

        public static string[] SupportedStandards()
        {
            return new string[] { "NEP-5", "NEP-10" };
        }

        public static BigInteger TotalSupply()
        {
            StorageMap contract = Storage.CurrentContext.CreateMap(GetStoragePrefixContract());
            return contract.Get("totalSupply").ToBigInteger();
        }

        public static BigInteger BalanceOf(byte[] account)
        {
            if (!ValidateAddress(account)) throw new FormatException("The parameter 'account' SHOULD be 20-byte addresses.");

            StorageMap balances = Storage.CurrentContext.CreateMap(GetStoragePrefixBalance());
            return balances.Get(account)?.ToBigInteger() ?? 0;
        }

        public static bool Transfer(byte[] from, byte[] to, BigInteger amount)
        {
            if (!ValidateAddress(from)) throw new FormatException("The parameter 'from' SHOULD be 20-byte addresses.");
            if (!ValidateAddress(to)) throw new FormatException("The parameters 'to' SHOULD be 20-byte addresses.");
            if (!IsPayable(to)) return false;
            if (amount <= 0) throw new InvalidOperationException("The parameter amount MUST be greater than 0.");
            if (!Runtime.CheckWitness(from)) return false;

            StorageMap balances = Storage.CurrentContext.CreateMap(GetStoragePrefixBalance());
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

        public static BigInteger GetTransactionAmount(object state)
        {
            var notification = (object[])state;

            // Checks notification format
            if (notification.Length != 4) return 0;

            // Only allow Transfer notifications
            if ((string)notification[0] != "Transfer") return 0;

            // Check dest
            if ((byte[])notification[2] != ExecutionEngine.ExecutingScriptHash) return 0;

            // Amount
            return (BigInteger)notification[3];
        }

        public static bool Mint()
        {
            if (Runtime.InvocationCounter != 1)
                throw new Exception();

            var notifications = Runtime.GetNotifications();

            if (notifications.Length == 0)
                throw new Exception();

            BigInteger neo = 0;
            BigInteger gas = 0;

            for (int i = 0; i < notifications.Length; i++)
            {
                var notification = notifications[i];

                //此处ScriptHash是tokenhash的小端格式
                if (notification.ScriptHash == NeoToken)
                {
                    neo += GetTransactionAmount(notification.State);
                }
                else if (notification.ScriptHash == GasToken)
                {
                    gas += GetTransactionAmount(notification.State);
                }
            }

            StorageMap contract = Storage.CurrentContext.CreateMap(GetStoragePrefixContract());
            byte[] totalSupplyData = contract.Get("totalSupply");

            BigInteger current_supply = 0;
            if (totalSupplyData != null && totalSupplyData.Length > 0)
            {
                current_supply = totalSupplyData.ToBigInteger();
            }

            var avaliable_supply = MaxSupply() - current_supply;

            var contribution = (neo * TokensPerNEO()) + (gas * TokensPerGAS());

            //if (contribution <= 0)
            //    throw new Exception();
            //if (contribution > avaliable_supply)
            //    throw new Exception();

            StorageMap balances = Storage.CurrentContext.CreateMap(GetStoragePrefixBalance());
            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;

            var balanceData = balances.Get(tx.Sender);

            BigInteger balance = 0;
            if (balanceData != null && balanceData.Length > 0)
            {
                balance = balanceData.ToBigInteger();
            }
            balances.Put(tx.Sender, balance + contribution);
            contract.Put("totalSupply", current_supply + contribution);

            OnTransfer(null, tx.Sender, balance + contribution);
            return true;
        }

        public static bool Deploy()
        {
            if (!Runtime.CheckWitness(Owner))
            {
                return false;
            }

            StorageMap contract = Storage.CurrentContext.CreateMap(GetStoragePrefixContract());
            if (contract.Get("totalSupply") != null)
                throw new Exception("Contract already deployed");

            StorageMap balances = Storage.CurrentContext.CreateMap(GetStoragePrefixBalance());
            balances.Put(Owner, InitialSupply());
            contract.Put("totalSupply", InitialSupply());

            OnTransfer(null, Owner, InitialSupply());
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

        private static bool ValidateAddress(byte[] address)
        {
            if (address.Length != 20)
                return false;
            if (address.ToBigInteger() == 0)
                return false;
            return true;
        }

        private static bool IsPayable(byte[] address)
        {
            var c = Blockchain.GetContract(address);
            return c == null || c.IsPayable;
        }
    }
}
