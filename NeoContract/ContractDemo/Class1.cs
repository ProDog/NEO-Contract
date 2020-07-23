﻿using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace NeoContract
{
    [Features(ContractFeatures.HasStorage)]
    public class Contract1 : SmartContract
    {
        private static readonly StorageMap Data = Storage.CurrentContext.CreateMap("data");

        public static void Put(string message)
        {
            Data.Put(message, Blockchain.GetHeight());
        }

        public static BigInteger Get(string msg)
        {
            return Data.Get(msg)?.ToBigInteger() ?? 0;
        }

        public static void Put2(string message)
        {
            var Data2 = Storage.CurrentContext.CreateMap("data");
            Data2.Put(message, Blockchain.GetHeight());
        }

        public static BigInteger Get2(string msg)
        {
            var Data2 = Storage.CurrentContext.CreateMap("data");
            return Data2.Get(msg)?.ToBigInteger() ?? 0;
        }
    }
}
