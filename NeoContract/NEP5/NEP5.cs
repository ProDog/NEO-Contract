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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Name() => "Token Name Migrate Test";
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Symbol() => "TokenSymbol";
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Decimals() => 8;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong MaxSupply() => 1_000_000_000;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong InitialSupply() => 20_000_000;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string[] SupportedStandards() => new string[] { "NEP-5", "NEP-10" };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        //fa 79 76 3b 86 76 7b 42 68 72 34 9f d2 fd bc cf 16 2e e2 20 合约中此处用小端序
        private static byte[] Owner() => new byte[] { 0xfa, 0x79, 0x76, 0x3b, 0x86, 0x76, 0x7b, 0x42, 0x68, 0x72, 0x34, 0x9f, 0xd2, 0xfd, 0xbc, 0xcf, 0x16, 0x2e, 0xe2, 0x20 };

        //0x20e22e16cfbcfdd29f347268427b76863b7679fa
        //private static byte[] Owner() => new byte[] { 0x20, 0xe2, 0x2e, 0x16, 0xcf, 0xbc, 0xfd, 0xd2, 0x9f, 0x34, 0x72, 0x68, 0x42, 0x7b, 0x76, 0x86, 0x3b, 0x76, 0x79, 0xfa };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong TokensPerNEO() => 1;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong TokensPerGAS() => 1;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static byte[] NeoToken() => new byte[] { 0x9b, 0xde, 0x8f, 0x20, 0x9c, 0x88, 0xdd, 0x0e, 0x7c, 0xa3, 0xbf, 0x0a, 0xf0, 0xf4, 0x76, 0xcd, 0xd8, 0x20, 0x77, 0x89 };
        public static byte[] NeoToken() => new byte[] { 0x89, 0x77, 0x20, 0xd8, 0xcd, 0x76, 0xf4, 0xf0, 0x0a, 0xbf, 0xa3, 0x7c, 0x0e, 0xdd, 0x88, 0x9c, 0x20, 0x8f, 0xde, 0x9b };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static byte[] GasToken() => new byte[] { 0x8c, 0x23, 0xf1, 0x96, 0xd8, 0xa1, 0xbf, 0xd1, 0x03, 0xa9, 0xdc, 0xb1, 0xf9, 0xcc, 0xf0, 0xc6, 0x11, 0x37, 0x7d, 0x3b };
        public static byte[] GasToken() => new byte[] { 0x3b, 0x7d, 0x37, 0x11, 0xc6, 0xf0, 0xcc, 0xf9, 0xb1, 0xdc, 0xa9, 0x03, 0xd1, 0xbf, 0xa1, 0xd8, 0x96, 0xf1, 0x23, 0x8c };
        //public static byte[] GasToken() => "0x8c23f196d8a1bfd103a9dcb1f9ccf0c611377d3b".HexToBytes();
        #endregion

        #region Notifications
        [DisplayName("Transfer")]
        public static event Action<byte[], byte[], BigInteger> OnTransfer;

        [DisplayName("test1")]
        public static event Action<BigInteger> OnTest1;

        [DisplayName("test2")]
        public static event Action<byte[]> OnTest2;

        #endregion

        #region Storage key prefixes
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] GetStoragePrefixBalance() => new byte[] { 0x01, 0x01 };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] GetStoragePrefixContract() => new byte[] { 0x02, 0x02 };
        #endregion

        public static object Main(string operation, object[] args)
        {
            if (Runtime.Trigger == TriggerType.Verification)
            {
                return Runtime.CheckWitness(Owner());
            }



            else if (Runtime.Trigger == TriggerType.Application)
            {
                #region NEP5 METHODS
                if (operation == "name") return Name();
                if (operation == "symbol") return Symbol();
                if (operation == "decimals") return Decimals();
                if (operation == "totalSupply") return TotalSupply();
                if (operation == "balanceOf") return BalanceOf((byte[])args[0]);
                if (operation == "transfer") return Transfer((byte[])args[0], (byte[])args[1], (BigInteger)args[2]);
                #endregion

                #region NEP10 METHODS
                if (operation == "supportedStandards") return SupportedStandards();
                #endregion

                #region CROWDSALE METHODS
                if (operation == "mint") return Mint();
                #endregion

                #region ADMIN METHODS
                if (operation == "deploy") return Deploy();
                if (operation == "migrate") return Migrate((byte[])args[0], (string)args[1]);
                if (operation == "destroy") return Destroy();
                #endregion
            }
            return false;
        }

        private static BigInteger TotalSupply()
        {
            StorageMap contract = Storage.CurrentContext.CreateMap(GetStoragePrefixContract());
            return contract.Get("totalSupply").ToBigInteger();
        }

        private static BigInteger BalanceOf(byte[] account)
        {
            if (!ValidateAddress(account)) throw new FormatException("The parameter 'account' SHOULD be 20-byte addresses.");

            StorageMap balances = Storage.CurrentContext.CreateMap(GetStoragePrefixBalance());
            return balances.Get(account)?.ToBigInteger() ?? 0;
        }

        private static bool Transfer(byte[] from, byte[] to, BigInteger amount)
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

        private static BigInteger GetTransactionAmount(object state)
        {
            var notification = (object[])state;

            OnTest1(3);

            // Checks notification format
            if (notification.Length != 4) return 0;

            OnTest1(4);

            // Only allow Transfer notifications
            if ((string)notification[0] != "Transfer") return 0;

            OnTest1(5);
            // Check dest
            if ((byte[])notification[2] != ExecutionEngine.ExecutingScriptHash) return 0;

            OnTest1(6);
            // Amount
            return (BigInteger)notification[3];
        }

        private static bool Mint()
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

                OnTest1(0);
                OnTest2(notification.ScriptHash);
                OnTest2(NeoToken());
                OnTest2(GasToken());

                //此处ScriptHash是tokenhash的小端格式
                if (notification.ScriptHash == NeoToken())
                {
                    OnTest1(1);

                    neo += GetTransactionAmount(notification.State);
                }
                else if (notification.ScriptHash == GasToken())
                {
                    OnTest1(2);

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

        private static bool Deploy()
        {
            if (!Runtime.CheckWitness(Owner()))
            {
                return false;
            }

            StorageMap contract = Storage.CurrentContext.CreateMap(GetStoragePrefixContract());
            if (contract.Get("totalSupply") != null)
                throw new Exception("Contract already deployed");

            StorageMap balances = Storage.CurrentContext.CreateMap(GetStoragePrefixBalance());
            balances.Put(Owner(), InitialSupply());
            contract.Put("totalSupply", InitialSupply());

            OnTransfer(null, Owner(), InitialSupply());
            return true;
        }

        public static bool Migrate(byte[] script, string manifest)
        {
            if (!Runtime.CheckWitness(Owner()))
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
            if (!Runtime.CheckWitness(Owner()))
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
