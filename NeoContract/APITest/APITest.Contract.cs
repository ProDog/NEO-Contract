using Neo;
using Neo.Cryptography.ECC;
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
        // [{"type":"Hash160","value":"0x668e0c1f9d7b70a99dd9e06eadd4c784d641afbc"},{"type":"Hash160","value":"0xf621168b1fce3a89c33a5f6bcf7e774b4657031c"},{"type":"Hash160","value":"0x1e01f56dbb2a9799422512752b900a5a49ca5d99"},{"type":"Integer","value":"20000"}]

        public static bool ContractTest(byte[] scriptHash, byte[] from, byte[] to, BigInteger amount)
        {
            var result = Contract.Call((UInt160)scriptHash, "transfer", new object[] { from, to, amount });

            OnNotify(result);
            OnNotify(scriptHash);

            //Contract.CallEx(scriptHash, "transfer", new object[] { from, to, amount }, Neo.SmartContract.CallFlags.All);

            //var balance = Contract.CallEx(scriptHash, "balanceOf", new object[] { from }, Neo.SmartContract.CallFlags.AllowCall);

            //var balance1 = Contract.CallEx(scriptHash, "balanceOf", new object[] { to }, Neo.SmartContract.CallFlags.AllowModifyStates);

            //var balance2 = Contract.CallEx(scriptHash, "balanceOf", new object[] { from }, Neo.SmartContract.CallFlags.AllowNotify);

            //var balance3 = Contract.CallEx(scriptHash, "balanceOf", new object[] { to }, Neo.SmartContract.CallFlags.None);

            //var totalSupply = Contract.CallEx(scriptHash, "totalSupply", new object[] { }, Neo.SmartContract.CallFlags.ReadOnly);

            return true;
        }

        //[{"type":"Hash160","value":"0x668e0c1f9d7b70a99dd9e06eadd4c784d641afbc"}] 
        public static BigInteger ContractTest1(byte[] scriptHash)
        {
            var totalSupply = (BigInteger)Contract.Call((UInt160)scriptHash, "totalSupply", new object[] { });
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
