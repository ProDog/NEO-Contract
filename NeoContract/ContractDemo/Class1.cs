using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.ComponentModel;
using System.Numerics;
using Neo;

namespace NeoContract
{
    public class Contract : SmartContract
    {
        static UInt160 owner = "NNU67Fvdy3LEQTM374EJ9iMbCRxVExgM8Y".ToScriptHash();

        public static bool main()
        {
            var a = "aa";
            if (!Runtime.CheckWitness(owner))
            {
                return false;
            }
            return true;
        }
    }
}
