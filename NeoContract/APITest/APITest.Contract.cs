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
        public static bool ContractTest(byte[] scriptHash, byte[] from, byte[] to, BigInteger amount)
        {
            //var scriptHash = new byte[] { 162, 210, 135, 131, 161, 45, 171, 207, 225, 70, 39, 213, 236, 229, 148, 229, 63, 247, 220, 163 };

            var result = Contract.Call(scriptHash, "transfer", new object[] { from, to, amount });

            Runtime.Notify(result);

            Contract.CallEx(scriptHash, "transfer", new object[] { from, to, amount }, Neo.SmartContract.CallFlags.All);

            var balance = Contract.CallEx(scriptHash, "balanceOf", new object[] { from }, Neo.SmartContract.CallFlags.AllowCall);
            Runtime.Notify(balance);

            var balance1 = Contract.CallEx(scriptHash, "balanceOf", new object[] { to }, Neo.SmartContract.CallFlags.AllowModifyStates);
            Runtime.Notify(balance1);

            var balance2 = Contract.CallEx(scriptHash, "balanceOf", new object[] { from }, Neo.SmartContract.CallFlags.AllowNotify);
            Runtime.Notify(balance2);

            var balance3 = Contract.CallEx(scriptHash, "balanceOf", new object[] { to }, Neo.SmartContract.CallFlags.None);
            Runtime.Notify(balance3);

            var totalSupply = Contract.CallEx(scriptHash, "totalSupply", new object[] { }, Neo.SmartContract.CallFlags.ReadOnly);
            Runtime.Notify(totalSupply);

            return true;
        }
    }
}
