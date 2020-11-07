using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace APITest
{
    [Features(ContractFeatures.HasStorage | ContractFeatures.Payable)]
    public partial class APITest : SmartContract
    {
        private static byte[] Owner = "NNU67Fvdy3LEQTM374EJ9iMbCRxVExgM8Y".ToScriptHash();
        //public static bool Verify()
        //{
        //    return Runtime.CheckWitness(Owner);
        //}

        public delegate void Notify(params object[] arg);

        [DisplayName("event_name")]
        public static event Notify OnNotify;


        public static void _deploy(bool update)
        {
            if (!update)
            {
                
                Storage.Put(Storage.CurrentContext, "test", 11);
                OnNotify("test", 1);
            }
        }

        public static object getvalue()
        {
            return Storage.Get(Storage.CurrentContext, "test");
        }

        //static byte[] bytes = "9bde8f209c88dd0e7ca3bf0af0f476cdd8207789".HexToBytes();
        //static byte[] bytes1 = Neo.SmartContract.Framework.Helper.HexToBytes("0x9bde8f209c88dd0e7ca3bf0af0f476cdd8207789");
        //static byte[] addressHash = Neo.SmartContract.Framework.Helper.ToScriptHash("NZ6A2ZLxKQY8hQxDFvuZkecBx8fj6MihS7");
        //static byte[] addressHash2 = Neo.SmartContract.Framework.Helper.ToScriptHash("NLJNmdMBm5LR3J2gErmJzN3PF9qwAzFzCf");

        //public static byte[] GasToken = new byte[] { 0x3b, 0x7d, 0x37, 0x11, 0xc6, 0xf0, 0xcc, 0xf9, 0xb1, 0xdc, 0xa9, 0x03, 0xd1, 0xbf, 0xa1, 0xd8, 0x96, 0xf1, 0x23, 0x8c };

        //public static int num = 1;
        //public static string str = "test";


        // When this contract address is included in the transaction signature,
        // this method will be triggered as a VerificationTrigger to verify that the signature is correct.


        //[DisplayName("TestEvent")]
        //public static event Action<byte[]> OnEvent;

        //static byte[] NeoToken = new byte[] { 0x89, 0x77, 0x20, 0xd8, 0xcd, 0x76, 0xf4, 0xf0, 0x0a, 0xbf, 0xa3, 0x7c, 0x0e, 0xdd, 0x88, 0x9c, 0x20, 0x8f, 0xde, 0x9b };
        //public static byte[] Test1()
        //{
        //    return NeoToken;
        //}

        //static UInt160 addressHash = Neo.SmartContract.Framework.Helper.ToScriptHash("NNU67Fvdy3LEQTM374EJ9iMbCRxVExgM8Y");
        //public static object Test1()
        //{
        //    var a = "NNU67Fvdy3LEQTM374EJ9iMbCRxVExgM8Y".ToScriptHash();

        //    return a;
        //}

        //public static bool NotifyTest()
        //{
        //    OnEvent(new byte[] { 0x89, 0x77, 0x20, 0xd8}, 1);

        //    Notify("str", 11, 12);

        //    Runtime.Log("test");

        //    return true;
        //}

        //public static object Test(int c)
        //{            
        //    return c;
        //}

        //private static bool Test1(int a, int b)
        //{
        //    return a < b;
        //}

        //private static (int, int) Test2(int a, int b)
        //{
        //    return (a / b, a * b);
        //}

        public static bool BoolTest()
        {
            byte[] a1 = new byte[] { 0x01, 0x02 };
            byte[] a2 = new byte[] { 0x01, 0x02 };

            return a1.ToBigInteger() == a2.ToBigInteger();
        }

        public static bool BoolTest1()
        {
            byte[] a1 = new byte[] { 0x01, 0x02 };
            byte[] a2 = new byte[] { 0x01, 0x02 };

            return a1.ToByteString().Equals(a2.ToByteString());
        }

        //// true or false
        //public static bool BoolTest3()
        //{            
        //    return new Object() == null;
        //}

        //// true or false       
        //public static bool Exists(string message)
        //{
        //    return Storage.Get(message) == null;
        //}


        //public static object TestAbort()
        //{
        //    Runtime.Notify("test");
        //    Abort();
        //    Runtime.Notify(1);
        //    return true;
        //}

        //public static object TestAssert(BigInteger bigInteger)
        //{
        //    Runtime.Notify("test");
        //    if (bigInteger == 1) Assert(true);
        //    else Assert(false);

        //    Runtime.Notify(1);
        //    return true;
        //}

        //public static object Test2()
        //{
        //    MyObject myObject = new MyObject();
        //    //myObject.obj = "test";
        //    //myObject.callback = create();
        //    myObject.bol = true;
        //    myObject.it = 1223;
        //    myObject.byteString = "testqqwasdas";
        //    myObject.byteArray = new byte[] { 8, 12, 34, 53, 12 };
        //    myObject.array = new string[] { "aa", "bb", "cc0" };
        //    myObject.map = new Map<byte, byte>();

        //    Map<byte, byte> map1 = new Map<byte, byte>();
        //    map1[2] = 12;
        //    map1[0] = 24;

        //    myObject.map = map1;
        //    //myObject.iterator = Iterator<byte, byte>.Create(map1);

        //    OnNotify(myObject);

        //    return myObject;
        //}

        public static void ExceptionTest()
        {
            throw new Exception("this is a test exception!");
        }


    }

    public class MyObject
    {
        //public object obj;
        //public object callback;
        public bool bol;
        public int it;
        public string byteString;
        public byte[] byteArray;
        public string[] array;
        public Map<byte,byte> map;
        //public Iterator<byte, byte> iterator;
        //public Pointer pointer;
    }
}
