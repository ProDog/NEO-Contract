using Neo.SmartContract.Framework;
using System;

namespace ContractDemo
{
    [Features(ContractFeatures.HasStorage | ContractFeatures.Payable)]
    public class Class1 : SmartContract
    {
        public static bool Test()
        {
            return true;
        }
    }
}
