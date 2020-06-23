using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;

namespace APITest
{
    public partial class APITest : SmartContract
    {
        public static bool ExecutionEngineTest()
        {
            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
            Runtime.Notify("aa", tx.Hash, tx.Sender, tx.SystemFee, tx.NetworkFee);

            //little-endian
            var executingScriptHash = ExecutionEngine.ExecutingScriptHash;
            Runtime.Notify("aa", executingScriptHash);

            var callingScriptHash = ExecutionEngine.CallingScriptHash;
            Runtime.Notify("aa", callingScriptHash);

            var entryScriptHash = ExecutionEngine.EntryScriptHash;
            Runtime.Notify("aa", entryScriptHash);

            return true;
        }
    }
}
