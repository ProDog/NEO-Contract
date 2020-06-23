using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace APITest
{
    public partial class APITest : SmartContract
    {
        public static object IteratorTest(byte[] a, byte[] b)
        {
            int sum = 0;
            var iterator = Iterator<byte, byte>.Create(a);

            while (iterator.Next())
            {
                sum += iterator.Value;
            }
            Runtime.Notify("aa", sum);


            sum = 1;
            var iteratorA = Iterator<byte, byte>.Create(a);
            var iteratorB = Iterator<byte, byte>.Create(b);
            var iteratorC = iteratorA.Concat(iteratorB);

            //while (iteratorC.Next())
            //{
            //    sum += iteratorC.Key;
            //    sum += iteratorC.Value;
            //}
            //Runtime.Notify(sum);

            //Map<byte, byte> map = new Map<byte, byte>();
            //map[2] = 12;
            //map[0] = 24;
            //map[1] = 10;
            //map[12] = 36;

            //Map<byte, byte> map1 = new Map<byte, byte>();
            //map1[2] = 12;
            //map1[0] = 24;
            //map1[1] = 10;
            //map1[12] = 36;

            //sum = 0;
            //var iteratorD = Iterator<byte, byte>.Create(map);
            //var iteratorE = Iterator<byte, byte>.Create(map1);
            //var iteratorF = iteratorD.Concat(iteratorE);
            //var enumerator = iteratorF.Keys;

            //while (enumerator.Next())
            //{
            //    sum += enumerator.Value;
            //}
            //Runtime.Notify(sum);//30

            //sum = 0;

            //var iteratorH = Iterator<byte, byte>.Create(map);
            //var iteratorI = Iterator<byte, byte>.Create(map1);
            //var iteratorG = iteratorH.Concat(iteratorI);
            //var enumeratorV = iteratorG.Values;
            //while (enumeratorV.Next())
            //{
            //    sum += enumeratorV.Value;
            //}
            //Runtime.Notify(sum);//164


            //FAULT

            //Map<byte[], byte[]> map = new Map<byte[], byte[]>();
            //map[new byte[] { 0x22, 0x23, 0x24 }] = new byte[] { 0x32, 0x33, 0x34 };
            //map[new byte[] { 0x42, 0x43, 0x44 }] = new byte[] { 0x52, 0x53, 0x54 };
            //map[new byte[] { 0x62, 0x63, 0x64 }] = new byte[] { 0x82, 0x83, 0x84 };

            //var iteratorW = Iterator<byte[], byte[]>.Create(map);

            //Runtime.Notify(iteratorW);

            //Runtime.Notify(iteratorW.Values);

            //Runtime.Notify(iteratorW.Keys);


            //Runtime.Notify(iteratorA);

            //Runtime.Notify(iteratorA.Values);

            //Runtime.Notify(iteratorA.Keys);

            return iteratorA.Keys;
        }

        public static object GetIterator()
        {
            var iteratorA = Iterator<BigInteger, BigInteger>.Create(new BigInteger[] {-10});
           
            return iteratorA;
        }
    }
}