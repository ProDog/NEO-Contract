//using Neo.SmartContract.Framework;
//using Neo.SmartContract.Framework.Services.Neo;
//using Neo.SmartContract.Framework.Services.System;
//using System;
//using System.ComponentModel;
//using System.Numerics;

//namespace nep5
//{
//    public class nep5 : SmartContract
//    {
//        [DisplayName("transfer")]
//        public static event Action<byte[], byte[], BigInteger> Transferred;//(byte[] from, byte[] to, BigInteger value)

//        private static readonly byte[] superAdmin = "AGfMEihgzV5H5fbWekWmiv6DBjVPZhfp7H".ToScriptHash();

//        private const ulong factor = 100000000;//精度
//        private const ulong oneHundredMillion = 100000000 * 10;
//        private const ulong totalCoin = 1 * oneHundredMillion * factor;//总量

//        public static object Main(string method, object[] args)
//        {
//            var magicstr = "nep-test";
//            if (Runtime.Trigger == TriggerType.Application)
//            {
//                var callscript = ExecutionEngine.CallingScriptHash;
//                var entryscript = ExecutionEngine.EntryScriptHash;

//                if (method == "name") return "BTC";
//                if (method == "symbol") return "BTC";
//                if (method == "decimals") return 8;
//                if (method == "supportedStandards") return new string[] { "NEP-5", "NEP-7", "NEP-10" };
//                if (method == "totalSupply") return Storage.Get(Context(), "totalSupply").AsBigInteger();
//                if (method == "balanceOf") return Storage.Get(Context(), AddressKey((byte[])args[0])).AsBigInteger();

//                if (method == "deploy")
//                {
//                    if (!Runtime.CheckWitness(superAdmin)) return false;

//                    byte[] total_supply = Storage.Get(Context(), "totalSupply");
//                    if (total_supply.Length != 0) return false;

//                    var keySuperAdmin = AddressKey(superAdmin);
//                    Storage.Put(Context(), keySuperAdmin, totalCoin);
//                    Storage.Put(Context(), "totalSupply", totalCoin);

//                    //notify
//                    Transferred(null, superAdmin, totalCoin);
//                }

//                if (method == "transfer")
//                {
//                    if (args.Length != 3) return false;
//                    byte[] from = (byte[])args[0];
//                    byte[] to = (byte[])args[1];
//                    if (from.Length != 20 || to.Length != 20) return false;

//                    BigInteger value = (BigInteger)args[2];

//                    if (!(Runtime.CheckWitness(from) || from.Equals(callscript))) return false;

//                    return Transfer(from, to, value);
//                }
//            }

//            return false;

//        }

//        public static bool Transfer(byte[] from, byte[] to, BigInteger value)
//        {
//            if (value <= 0) return false;
//            if (from == to) return true;

//            var keyFrom = AddressKey(from);
//            BigInteger from_value = Storage.Get(Context(), keyFrom).AsBigInteger();
//            if (from_value < value) return false;
//            if (from_value == value)
//                Storage.Delete(Context(), keyFrom);
//            else
//                Storage.Put(Context(), keyFrom, from_value - value);

//            var keyTo = AddressKey(to);
//            BigInteger to_value = Storage.Get(Context(), keyTo).AsBigInteger();
//            Storage.Put(Context(), keyTo, to_value + value);

//            Transferred(from, to, value);
//            return true;
//        }

//        private static StorageContext Context() => Storage.CurrentContext;

//        private static byte[] AddressKey(byte[] address) => new byte[] { 0x11 }.Concat(address);
//    }
//}
