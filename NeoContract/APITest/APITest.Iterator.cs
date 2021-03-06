﻿using Neo.SmartContract.Framework;
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
            OnNotify("aa", sum);


            sum = 1;
            var iteratorA = Iterator<byte, byte>.Create(a);
            var iteratorB = Iterator<byte, byte>.Create(b);
            var iteratorC = iteratorA.Concat(iteratorB);

            while (iteratorC.Next())
            {
                sum += iteratorC.Key;
                sum += iteratorC.Value;
            }
            OnNotify(sum);

            Map<byte, byte> map = new Map<byte, byte>();
            map[2] = 12;
            map[0] = 24;
            map[1] = 10;
            map[12] = 36;

            Map<byte, byte> map1 = new Map<byte, byte>();
            map1[2] = 12;
            map1[0] = 24;
            map1[1] = 10;
            map1[12] = 36;

            sum = 0;
            var iteratorD = Iterator<byte, byte>.Create(map);
            var iteratorE = Iterator<byte, byte>.Create(map1);
            var iteratorF = iteratorD.Concat(iteratorE);
            var enumerator = iteratorF.Keys;

            while (enumerator.Next())
            {
                sum += enumerator.Value;
            }
            OnNotify(sum);//30

            sum = 0;

            var iteratorH = Iterator<byte, byte>.Create(map);
            var iteratorI = Iterator<byte, byte>.Create(map1);
            var iteratorG = iteratorH.Concat(iteratorI);
            var enumeratorV = iteratorG.Values;
            while (enumeratorV.Next())
            {
                sum += enumeratorV.Value;
            }
            OnNotify(sum);//164

            OnNotify(iteratorA);

            OnNotify(iteratorA.Values);

            OnNotify(iteratorA.Keys);

            return iteratorA.Keys;
        }

        public static object GetIterator()
        {
            var iteratorA = Iterator<BigInteger, BigInteger>.Create(new BigInteger[] { -10 });

            return iteratorA;
        }
    }
}