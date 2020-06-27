using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;

namespace APITest
{
    public partial class APITest : SmartContract
    {
        //public static bool ExecutionEngineTest()
        //{
        //    Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
        //    Notify("aa", tx.Hash, tx.Sender, tx.SystemFee, tx.NetworkFee);

        //    //little-endian
        //    var executingScriptHash = ExecutionEngine.ExecutingScriptHash;
        //    Notify("aa", executingScriptHash);

        //    var callingScriptHash = ExecutionEngine.CallingScriptHash;
        //    Notify("aa", callingScriptHash);

        //    var entryScriptHash = ExecutionEngine.EntryScriptHash;
        //    Notify("aa", entryScriptHash);

        //    return true;
        //}
    }
}
