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
        private static bool EnumeratorTest(byte[] a, byte[] b)
        {
            int sum = 0;
            var enumerator = Enumerator<byte>.Create(a);

            while (enumerator.Next())
            {
                sum += enumerator.Value;
            }

            Runtime.Notify(sum);

            sum = 0;
            var enumeratorA = Enumerator<byte>.Create(a);
            var enumeratorB = Enumerator<byte>.Create(b);
            var enumeratorC = enumeratorA.Concat(enumeratorB);

            while (enumeratorC.Next())
            {
                sum += enumeratorC.Value;
            }
            Runtime.Notify(sum);

            return true;
        }
    }
}