using Neo;
using Neo.Cryptography.ECC;
using Neo.SmartContract;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Neo3Contract
{
    public partial class Neo3Contract : SmartContract
    {
        // [{"type":"Hash160","value":"0x9ac04cf223f646de5f7faccafe34e30e5d4382a2"},{"type":"Hash160","value":"0xf621168b1fce3a89c33a5f6bcf7e774b4657031c"},{"type":"Hash160","value":"0x1e01f56dbb2a9799422512752b900a5a49ca5d99"},{"type":"Integer","value":"20000"}]

        public static bool ContractTest(byte[] scriptHash, byte[] from, byte[] to, BigInteger amount)
        {
            var result = Contract.Call((UInt160)scriptHash, "transfer", CallFlags.All, new object[] { from, to, amount, null });

            OnNotify(result);
            OnNotify(scriptHash);

            var balance = Contract.Call((UInt160)scriptHash, "balanceOf", CallFlags.All, new object[] { from });
            OnNotify(balance);

            Contract.Call((UInt160)scriptHash, "transfer", CallFlags.All, new object[] { from, to, amount, null });

            var balance1 = Contract.Call((UInt160)scriptHash, "balanceOf", CallFlags.All, new object[] { from });

            //var balance1 = Contract.CallEx((UInt160)scriptHash, "balanceOf", new object[] { to }, Neo.SmartContract.CallFlags.AllowModifyStates);

            //var balance2 = Contract.CallEx((UInt160)scriptHash, "balanceOf", new object[] { from }, Neo.SmartContract.CallFlags.AllowNotify);

            //var balance3 = Contract.CallEx((UInt160)scriptHash, "balanceOf", new object[] { to }, Neo.SmartContract.CallFlags.None);

            //var totalSupply = Contract.CallEx((UInt160)scriptHash, "totalSupply", new object[] { }, Neo.SmartContract.CallFlags.ReadOnly);

            OnNotify(balance1);

            return true;
        }

        //[{"type":"Hash160","value":"0x9ac04cf223f646de5f7faccafe34e30e5d4382a2"}] 
        public static BigInteger ContractTest1(byte[] scriptHash)
        {
            var totalSupply = (BigInteger)Contract.Call((UInt160)scriptHash, "totalSupply", CallFlags.All, new object[] { });
            OnNotify(totalSupply);

            return totalSupply;
        }

        //[{"type":"PublicKey","value":"0222d8515184c7d62ffa99b829aeb4938c4704ecb0dd7e340e842e9df121826343"}] 
        public static object ContractTest2(byte[] publicKey)
        {
            var account = Contract.CreateStandardAccount((ECPoint)publicKey);
            OnNotify(account);
            return Contract.CreateStandardAccount((ECPoint)publicKey);
        }
    }
}
