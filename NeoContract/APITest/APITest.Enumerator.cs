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
        public static bool EnumeratorTest(byte[] a, byte[] b)
        {
            int sum = 0;
            var enumerator = Enumerator<byte>.Create(a);

            while (enumerator.Next())
            {
                sum += enumerator.Value;
            }

            Notify("aa", sum);

            sum = 0;
            var enumeratorA = Enumerator<byte>.Create(a);
            var enumeratorB = Enumerator<byte>.Create(b);
            var enumeratorC = enumeratorA.Concat(enumeratorB);

            while (enumeratorC.Next())
            {
                sum += enumeratorC.Value;
            }
            Notify("aa", sum);

            return true;
        }

        //public static Enumerator<byte[]> GetEnumerator()
        //{
        //    var a = new byte[] { 0x22, 0x32, 0x24 };
        //    var b = new byte[] { 0x10, 0x11, 0x22 };
        //    var c = new byte[] { 0x33, 0x44, 0x55 };
        //    var enumerator = Enumerator<byte[]>.Create(new byte[3][] { a, b, c });
            
        //    return enumerator;
        //}

        public static byte[] getBuffer()
        {
            var a = new byte[] { 0x22, 0x32, 0x24 };
            var b = new byte[] { 0x10, 0x11, 0x22 };
            var c = new byte[] { 0x33, 0x44, 0x55 };

            return a.Concat(b).Concat(c);
        }


        public static Enumerator<BigInteger> GetEnumerator()
        {
            var enumerator = Enumerator<BigInteger>.Create(new BigInteger[4] { 12, 11, 1, 1 });
            return enumerator;
        }
    }
}