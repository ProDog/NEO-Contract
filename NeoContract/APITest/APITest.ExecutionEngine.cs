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
            Runtime.Notify(tx.Hash, tx.Sender, tx.SystemFee, tx.NetworkFee);

            //little-endian
            var executingScriptHash = ExecutionEngine.ExecutingScriptHash;
            Runtime.Notify(executingScriptHash);

            var callingScriptHash = ExecutionEngine.CallingScriptHash;
            Runtime.Notify(callingScriptHash);

            var entryScriptHash = ExecutionEngine.EntryScriptHash;
            Runtime.Notify(entryScriptHash);

            return true;
        }
    }
}
