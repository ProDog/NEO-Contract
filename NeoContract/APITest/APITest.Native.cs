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
        public static object NativeTest(byte[] from, byte[] to, BigInteger amount)
        {
            Native.NEO("transfer", new object[] { from, to, amount });

            Native.GAS("transfer", new object[] { from, to, amount });

            var res = Native.Policy("supportedStandards", new object[] { });
            Runtime.Notify("aa", res);

            return true;
        }
    }
}