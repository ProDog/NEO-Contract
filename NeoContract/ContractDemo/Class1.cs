using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace ContractDemo
{
    [Features(ContractFeatures.HasStorage | ContractFeatures.Payable)]
    public class Class1 : SmartContract
    {
        static byte[] NeoToken = new byte[] { 0x89, 0x77, 0x20, 0xd8, 0xcd, 0x76, 0xf4, 0xf0, 0x0a, 0xbf};

        public static byte[] Test()
        {
            return NeoToken;
        }
    }
}
