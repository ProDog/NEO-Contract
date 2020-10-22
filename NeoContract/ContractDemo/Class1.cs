using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.ComponentModel;
using System.Numerics;
using Neo;

namespace NeoContract
{
    [Features(ContractFeatures.HasStorage | ContractFeatures.Payable)]
    public class Contract1 : SmartContract
    {
        [DisplayName("Test")]
        public static event Action<byte[]> OnTest;

        [DisplayName("TestEvent")]
        public static event Action<object> OnEvent;

        static byte[] NeoToken = new byte[] { 0x89, 0x77, 0x20, 0xd8, 0xcd, 0x76, 0xf4, 0xf0, 0x0a, 0xbf, 0xa3, 0x7c, 0x0e, 0xdd, 0x88, 0x9c, 0x20, 0x8f, 0xde, 0x9b };

        private static UInt160 Owner = "NNU67Fvdy3LEQTM374EJ9iMbCRxVExgM8Y".ToScriptHash();
        private static byte[] ScriptHash = "445f844f002371c6ab31856b3135daa881d05b09".HexToBytes();

        public static bool Verify()
        {
            return Runtime.CheckWitness(Owner);
        }

        public static object Test()
        {
            OnTest(Owner);
            OnTest(ScriptHash);
            return Owner;
        }

        public static byte[] Test1()
        {
            return NeoToken;
        }

        public static object Test2()
        {
            //OnEvent(Account.IsStandard((UInt160)"0xaf1c0bf73ba05a11a6969a9e6eecbb8dc0adce93".HexToBytes()));

            OnEvent(Account.IsStandard((UInt160)ScriptHash));
            //OnEvent(Account.IsStandard(UInt160.Zero));

            return ScriptHash;
        }
    }
}
