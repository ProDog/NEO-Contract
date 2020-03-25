using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace APITest
{
    [Features(ContractFeatures.HasStorage | ContractFeatures.Payable)]
    public partial class APITest : SmartContract
    {       
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Name() => "SDK API TEST CONTRACT";
       
        private static byte[] Owner = new byte[] { 0xfa, 0x79, 0x76, 0x3b, 0x86, 0x76, 0x7b, 0x42, 0x68, 0x72, 0x34, 0x9f, 0xd2, 0xfd, 0xbc, 0xcf, 0x16, 0x2e, 0xe2, 0x20 };       
   
        public static byte[] NeoToken = new byte[] { 0x89, 0x77, 0x20, 0xd8, 0xcd, 0x76, 0xf4, 0xf0, 0x0a, 0xbf, 0xa3, 0x7c, 0x0e, 0xdd, 0x88, 0x9c, 0x20, 0x8f, 0xde, 0x9b };
     
        public static byte[] GasToken = new byte[] { 0x3b, 0x7d, 0x37, 0x11, 0xc6, 0xf0, 0xcc, 0xf9, 0xb1, 0xdc, 0xa9, 0x03, 0xd1, 0xbf, 0xa1, 0xd8, 0x96, 0xf1, 0x23, 0x8c };
        
        static byte[] bytes = "9bde8f209c88dd0e7ca3bf0af0f476cdd8207789".HexToBytes();
        static byte[] bytes1 = Neo.SmartContract.Framework.Helper.HexToBytes("0x9bde8f209c88dd0e7ca3bf0af0f476cdd8207789");
        static byte[] script = Neo.SmartContract.Framework.Helper.ToScriptHash("NikMd2j2bgVr8HzYgoJjbnwUPyXWnzjDCM");               

        public static object Main(string operation, object[] args)
        {

            if (Runtime.Trigger == TriggerType.Verification)
            {
                return Runtime.CheckWitness(Owner);
            }

            else if (Runtime.Trigger == TriggerType.Application)
            {
                if (operation == "executionEngine") return ExecutionEngineTest();

                if (operation == "account") return AccountTest((byte[])args[0]);

                if (operation == "blockchain") return BlockchainTest();

                if (operation == "runtime") return RuntimeTest((byte[])args[0]);

                if (operation == "contract") return ContractTest((byte[])args[0], (byte[])args[1], (byte[])args[2], (BigInteger)args[3]);
                              
                if (operation == "helper") return HelperTest();

                if (operation == "json") return JsonTest();

                if (operation == "native") return NativeTest((byte[])args[0], (byte[])args[1], (BigInteger)args[2]);

                if (operation == "storage") return StorageTest();

                if (operation == "storageContext") return StorageContextTest((byte[])args[0], (byte[])args[1]);

                if (operation == "crypto") return CryptoTest((byte[])args[0], (byte[])args[1], (byte[])args[2]);

                if (operation == "multiCrypto") return MultiCryptoTest((byte[])args[0], (byte[][])args[1], (byte[][])args[2]);

                if (operation == "iterator") return IteratorTest((byte[]) args[0], (byte[])args[1]);

                if (operation == "enumerator") return EnumeratorTest((byte[])args[0], (byte[])args[1]);

            }
            return false;
        }
    }
}
