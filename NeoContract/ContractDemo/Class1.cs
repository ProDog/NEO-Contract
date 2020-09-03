using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.ComponentModel;
using System.Numerics;

namespace NeoContract
{
    [Features(ContractFeatures.HasStorage | ContractFeatures.Payable)]
    public class Contract1 : SmartContract
    {
        [DisplayName("Test")]
        public static event Action<byte[]> OnTest;

        [DisplayName("TestEvent")]
        public static event Action<byte[]> OnEvent;

        static byte[] NeoToken = new byte[] { 0x89, 0x77, 0x20, 0xd8, 0xcd, 0x76, 0xf4, 0xf0, 0x0a, 0xbf, 0xa3, 0x7c, 0x0e, 0xdd, 0x88, 0x9c, 0x20, 0x8f, 0xde, 0x9b };
        public static byte[] Test1()
        {
            return NeoToken;
        }

        //[DisplayName("Test1")]
        //public static event Action<string> OnTest1;

        //public static bool Update(byte[] script, string manifest)
        //{            
        //    if (script.Length == 0 && manifest.Length == 0) return false;
        //    // Check equals
        //    var contract = Blockchain.GetContract(ExecutionEngine.ExecutingScriptHash);
        //    if (script != null && script.Equals(contract.Script) && manifest == contract.Manifest)
        //        return true;

        //    if (!contract.Script.Equals(script))
        //    {
        //        OnTest(script);
        //        OnTest(contract.Script);
        //    }

        //    if (manifest != contract.Manifest)
        //    {
        //        OnTest1("1");
        //    }

        //    Contract.Update(script, manifest);
        //    return true;
        //}

        private static byte[] Owner = "NNU67Fvdy3LEQTM374EJ9iMbCRxVExgM8Y".ToScriptHash();
        public static bool Verify()
        {
            return Runtime.CheckWitness(Owner);
        }
    }
}
