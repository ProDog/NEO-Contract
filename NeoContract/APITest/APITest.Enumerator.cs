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

            OnNotify("aa", sum);

            sum = 0;
            var enumeratorA = Enumerator<byte>.Create(a);
            var enumeratorB = Enumerator<byte>.Create(b);
            var enumeratorC = enumeratorA.Concat(enumeratorB);

            while (enumeratorC.Next())
            {
                sum += enumeratorC.Value;
            }
            OnNotify("aa", sum);

            return true;
        }       

        public static byte[] getBuffer()
        {
            var a = new byte[] { 0x22, 0x32, 0x24 };
            var b = new byte[] { 0x10, 0x11, 0x22 };
            var c = new byte[] { 0x33, 0x44, 0x55 };

            return a.Concat(b).Concat(c);
        }
    }
}