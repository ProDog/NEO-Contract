using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace ContractDemo
{
    [Features(ContractFeatures.HasStorage | ContractFeatures.Payable)]
    public class Class1 : SmartContract
    {
        public delegate void mydelegate(params object[] arg);

        [DisplayName("event")]
        public static event mydelegate Notify;

        private static byte[] myAddr = "NNB8GKS7mdMXXGsAwvXYyEGonkEjDbqNkG".ToScriptHash();

        public static byte[] test()
        {
            return myAddr;
        }


        //public static object iteratorTest()
        //{
        //    //var a = new byte[] { 0x22, 0x23, 0x24 };
        //    //var b = new byte[] { 0x22, 0x23, 0x24 };

        //    //int sum = 0;
        //    //var iterator = Iterator<byte, byte>.Create(a);

        //    //while (iterator.Next())
        //    //{
        //    //    sum += iterator.Value;
        //    //}

        //    //sum = 1;
        //    //var iteratorA = Iterator<byte, byte>.Create(a);
        //    //var iteratorB = Iterator<byte, byte>.Create(b);
        //    //var iteratorC = iteratorA.Concat(iteratorB);

        //    //while (iteratorC.Next())
        //    //{
        //    //    sum += iteratorC.Key;
        //    //    sum += iteratorC.Value;
        //    //}

        //    //FAULT

        //    //Map<byte[], byte[]> map = new Map<byte[], byte[]>();
        //    //map[new byte[] { 0x22, 0x23, 0x24 }] = new byte[] { 0x32, 0x33, 0x34 };
        //    //map[new byte[] { 0x42, 0x43, 0x44 }] = new byte[] { 0x52, 0x53, 0x54 };
        //    //map[new byte[] { 0x62, 0x63, 0x64 }] = new byte[] { 0x82, 0x83, 0x84 };


        //    Map<byte[], byte> map = new Map<byte[], byte>();
        //    map[new byte[] { 0x22, 0x23, 0x24 }] = 0x34;
        //    map[new byte[] { 0x42, 0x43, 0x44 }] = 0x54;
        //    map[new byte[] { 0x62, 0x63, 0x64 }] = 0x84;

        //    //var iteratorW = Iterator<byte[], byte[]>.Create(map);
        //    //iterator.Next();
        //    //return iterator.Value;

        //    return map;
        //}

        //public static object getIterator()
        //{
        //    var iteratorA = Iterator<BigInteger, BigInteger>.Create(new BigInteger[] { -10 });

        //    return iteratorA;
        //}
    }
}
