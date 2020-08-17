using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace APITest
{
    public partial class APITest : SmartContract
    {
        public static object Test(object args)
        {
            return 123;
        }

        //public static object createFromMethod(byte[] hash, string method)
        //{
        //    return Callback.CreateFromMethod(hash, method);
        //}

        //public static object callMethod(byte[] hash, byte[] from, byte[] to, BigInteger value)
        //{
        //    return Callback.CreateFromMethod(hash, "transfer").Invoke(new object[] { from, to, value });
        //}

        public static object create()
        {
            var action = new Func<object, object>(Test);
            return Callback.Create(action);
        }

        //public static object createAndCall()
        //{
        //    var action = new Func<object, object>(Test);
        //    var callback = Callback.Create(action);

        //    return callback.Invoke(new object[] { null });
        //}

        //public static object callSyscall(byte hash)
        //{
        //    return Callback.CreateFromSyscall(SyscallCallback.System_Blockchain_GetContract).Invoke(new object[] { hash});
        //}

        //public static object createAndCallFromSyscall(uint method)
        //{
        //    return Callback.CreateFromSyscall((SyscallCallback)method).Invoke(new object[0]);
        //}

        //public static ValueTuple<int, int> valueTuple()
        //{
        //    return new ValueTuple<int, int>(2, 3);
        //}

        //public static ValueTuple<int, int>[] valueTupleArray()
        //{
        //    return new ValueTuple<int, int>[] { (2, 3), (1, 2) };
        //}

        //public static Tuple<int, int> tuple()
        //{
        //    return new Tuple<int, int>(1, 2);
        //}

        //public static Tuple<int, int>[] tupleArray()
        //{
        //    return new Tuple<int, int>[] { new Tuple<int, int>(1, 2) };
        //}

        //private static Func<int> CreateFuncPointer()
        //{
        //    return new Func<int>(MyMethod);
        //}

        //public static int CallFuncPointer()
        //{
        //    var pointer = CreateFuncPointer();
        //    return pointer.Invoke();
        //}

        //public static int MyMethod()
        //{
        //    return 33;
        //}

        //private static Func<byte[][], string[][]> CreateFuncPointerWithArg()
        //{
        //    return new Func<byte[][], string[][]>(MyMethodWithArg);
        //}

        //public static string[][] MyMethodWithArg(byte[][] num)
        //{
        //    return new string[][] { new string[] { num[0].ToByteString() }, new string[] { num[0].ToByteString() } };
        //}

        //public static string[][] CallFuncPointerWithArg()
        //{
        //    var pointer = CreateFuncPointerWithArg();

        //    return pointer.Invoke(new byte[][] { new byte[] { 11, 11 }, new byte[] { 11, 11 } });
        //}
    }
}
