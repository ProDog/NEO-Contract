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
        //[DisplayName("Test")]
        //public static event Action<byte[]> OnTest;

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
