using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace APITest
{
    [Features(ContractFeatures.HasStorage | ContractFeatures.Payable)]
    public partial class APITest : SmartContract
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Name() => "SDK API TEST CONTRACT";

        private static byte[] Owner = "NikMd2j2bgVr8HzYgoJjbnwUPyXWnzjDCM".ToScriptHash();

        public static byte[] NeoToken = new byte[] { 0x89, 0x77, 0x20, 0xd8, 0xcd, 0x76, 0xf4, 0xf0, 0x0a, 0xbf, 0xa3, 0x7c, 0x0e, 0xdd, 0x88, 0x9c, 0x20, 0x8f, 0xde, 0x9b };

        public static byte[] GasToken = new byte[] { 0x3b, 0x7d, 0x37, 0x11, 0xc6, 0xf0, 0xcc, 0xf9, 0xb1, 0xdc, 0xa9, 0x03, 0xd1, 0xbf, 0xa1, 0xd8, 0x96, 0xf1, 0x23, 0x8c };

        static byte[] bytes = "9bde8f209c88dd0e7ca3bf0af0f476cdd8207789".HexToBytes();
        static byte[] bytes1 = Neo.SmartContract.Framework.Helper.HexToBytes("0x9bde8f209c88dd0e7ca3bf0af0f476cdd8207789");
        static byte[] addressHash = Neo.SmartContract.Framework.Helper.ToScriptHash("NZ6A2ZLxKQY8hQxDFvuZkecBx8fj6MihS7");
        static byte[] addressHash1 = Neo.SmartContract.Framework.Helper.ToScriptHash("NSzwm3ZZQNt7puaij6aq7hQ8EBD8r66XgF");
        static byte[] addressHash2 = Neo.SmartContract.Framework.Helper.ToScriptHash("NLJNmdMBm5LR3J2gErmJzN3PF9qwAzFzCf");

        //public static object Main(string operation, object[] args)
        //{

        //    if (Runtime.Trigger == TriggerType.Verification)
        //    {
        //        return Runtime.CheckWitness(Owner);
        //    }

        //    else if (Runtime.Trigger == TriggerType.Application)
        //    {
        //        if (operation == "executionEngine") return ExecutionEngineTest();

        //        if (operation == "account") return AccountTest((byte[])args[0]);

        //        if (operation == "blockchain") return BlockchainTest((byte[])args[0]);

        //        if (operation == "runtime") return RuntimeTest((byte[])args[0]);

        //        if (operation == "gasleft") return GasLeftTest();

        //        if (operation == "contract") return ContractTest((byte[])args[0], (byte[])args[1], (byte[])args[2], (BigInteger)args[3]);

        //        if (operation == "helper") return HelperTest();

        //        if (operation == "json") return JsonTest();

        //        if (operation == "native") return NativeTest((byte[])args[0], (byte[])args[1], (BigInteger)args[2]);

        //        if (operation == "storage") return StorageTest();

        //        if (operation == "storageContext") return StorageContextTest((byte[])args[0], (byte[])args[1]);

        //        if (operation == "crypto") return CryptoTest((byte[])args[0], (byte[])args[1], (byte[])args[2]);

        //        if (operation == "multiCrypto") return MultiCryptoTest((byte[])args[0], (byte[][])args[1], (byte[][])args[2]);

        //        if (operation == "iterator") return IteratorTest((byte[])args[0], (byte[])args[1]);

        //        if (operation == "enumerator") return EnumeratorTest((byte[])args[0], (byte[])args[1]);

        //        if (operation == "assert") return TestAssert((BigInteger)args[0]);

        //        if (operation == "abort") return TestAbort();

        //        if (operation == "migrate") return Migrate((byte[])args[0], (string)args[1]);

        //        //if (operation == "return") return Return(6, 2);

        //        //if (operation == "test") return Test();

        //    }
        //    return false;
        //}

        public static object Test()
        {
            if (Runtime.CheckWitness(addressHash) && Runtime.CheckWitness(addressHash1) && Runtime.CheckWitness(addressHash2))
            {
                Runtime.Notify(1);
                //return 0;
            }
            else
            {
                Runtime.Notify(0);
                //return 1;
            }

            return 1;
        }

        //public static (int, int) Return(int a, int b)
        //{
        //    return (a / b, a % b);
        //}

        // 1 or 0
        public static bool BoolTest()
        {            
            return true;
        }

        // 1 or 0
        public static bool BoolTest1()
        {            
            return "aa" != "aa";
        }

        // true or false
        public static bool BoolTest2()
        {           
            return new byte[] { 0x12 } != null;
        }

        // true or false
        public static bool BoolTest3()
        {            
            return new Object() == null;
        }

        // true or false       
        public static bool Exists(string message)
        {
            return Storage.Get(message) == null;
        }


        public static object TestAbort()
        {
            Runtime.Notify("test");
            Abort();
            Runtime.Notify(1);
            return true;
        }

        public static object TestAssert(BigInteger bigInteger)
        {
            Runtime.Notify("test");
            if (bigInteger == 1) Assert(true);
            else Assert(false);

            Runtime.Notify(1);
            return true;
        }
    }
}
