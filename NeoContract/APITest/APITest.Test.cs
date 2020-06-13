using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace APITest
{    
    public partial class APITest : SmartContract
    {
        private static Func<int> CreateFuncPointer()
        {
            return new Func<int>(MyMethod);
        }

        public static int MyMethod()
        {
            return 123;
        }

        public static int CallFuncPointer()
        {
            var pointer = CreateFuncPointer();
            return pointer.Invoke();
        }

        private static Func<byte[], BigInteger> CreateFuncPointerWithArg()
        {
            return new Func<byte[], BigInteger>(MyMethodWithArg);
        }

        public static BigInteger MyMethodWithArg(byte[] num)
        {
            return num.ToBigInteger();
        }

        public static BigInteger CallFuncPointerWithArg()
        {
            var pointer = CreateFuncPointerWithArg();

            Runtime.Notify(pointer.Invoke(new byte[] { 11, 22, 33 }));

            return pointer.Invoke(new byte[] { 11, 22, 33 });
        }
    }
}
