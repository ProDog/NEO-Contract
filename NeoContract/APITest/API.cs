using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace API
{
    [Features(ContractFeatures.HasStorage | ContractFeatures.Payable)]
    public partial class API : SmartContract
    {       
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Name() => "SDK API TEST CONTRACT";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        private static byte[] Owner() => new byte[] { 0xfa, 0x79, 0x76, 0x3b, 0x86, 0x76, 0x7b, 0x42, 0x68, 0x72, 0x34, 0x9f, 0xd2, 0xfd, 0xbc, 0xcf, 0x16, 0x2e, 0xe2, 0x20 };       
        [MethodImpl(MethodImplOptions.AggressiveInlining)]       
        public static byte[] NeoToken() => new byte[] { 0x89, 0x77, 0x20, 0xd8, 0xcd, 0x76, 0xf4, 0xf0, 0x0a, 0xbf, 0xa3, 0x7c, 0x0e, 0xdd, 0x88, 0x9c, 0x20, 0x8f, 0xde, 0x9b };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]       
        public static byte[] GasToken() => new byte[] { 0x3b, 0x7d, 0x37, 0x11, 0xc6, 0xf0, 0xcc, 0xf9, 0xb1, 0xdc, 0xa9, 0x03, 0xd1, 0xbf, 0xa1, 0xd8, 0x96, 0xf1, 0x23, 0x8c };

        #region Notifications
        [DisplayName("test0")]
        public static event Action<byte[], byte[], long, long> OnTest0;

        [DisplayName("test1")]
        public static event Action<BigInteger> OnTest1;

        [DisplayName("test2")]
        public static event Action<byte[]> OnTest2;

        [DisplayName("test3")]
        public static event Action<bool> OnTest3;

        [DisplayName("test4")]
        public static event Action<uint> OnTest4;

        [DisplayName("test5")]
        public static event Action<long> OnTest5;

        #endregion

        #region Storage key prefixes
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] GetStoragePrefixBalance() => new byte[] { 0x01, 0x01 };
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] GetStoragePrefixContract() => new byte[] { 0x02, 0x02 };
        #endregion

        public static object Main(string operation, object[] args)
        {
            if (Runtime.Trigger == TriggerType.Verification)
            {
                return Runtime.CheckWitness(Owner());
            }

            else if (Runtime.Trigger == TriggerType.Application)
            {                
                if (operation == "ExecutionEngineTest") return ExecutionEngineTest();

                if (operation == "ContractTest") return ContractTest();

                if (operation == "BlockchainTest") return BlockchainTest();
            }
            return false;
        }

        private static bool ExecutionEngineTest()
        {
            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
            OnTest0(tx.Hash, tx.Sender, tx.SystemFee, tx.NetworkFee);

            //当前合约hash 小端序
            var executingScriptHash = ExecutionEngine.ExecutingScriptHash;
            OnTest2(executingScriptHash);

            var callingScriptHash = ExecutionEngine.CallingScriptHash;
            OnTest2(callingScriptHash);

            var entryScriptHash = ExecutionEngine.EntryScriptHash;
            OnTest2(entryScriptHash);

            return true;
        }

        private static bool ContractTest()
        {
            var isStandard = Account.IsStandard(ExecutionEngine.ExecutingScriptHash);
            OnTest3(isStandard);

            var isStandard1 = Account.IsStandard(Owner());
            OnTest3(isStandard1);

            var isStandard2 = Account.IsStandard(GasToken());
            OnTest3(isStandard2);

            return true;
        }

        private static bool BlockchainTest()
        {
            OnTest4(Blockchain.GetHeight());

            OnTest4(Blockchain.GetBlock(Blockchain.GetHeight()).Index);
            OnTest4(Blockchain.GetBlock(Blockchain.GetHeight()).Version);
            OnTest4((uint)Blockchain.GetBlock(Blockchain.GetHeight()).TransactionsCount);
           
            OnTest2(Blockchain.GetBlock(Blockchain.GetHeight()).Hash);
            OnTest2(Blockchain.GetBlock(Blockchain.GetHeight()).MerkleRoot);
            OnTest2(Blockchain.GetBlock(Blockchain.GetHeight()).NextConsensus);
            OnTest2(Blockchain.GetBlock(Blockchain.GetHeight()).PrevHash);
            OnTest5((long)Blockchain.GetBlock(Blockchain.GetHeight()).Timestamp);

            return true;
        }

        private static bool IsPayable(byte[] address)
        {
            var c = Blockchain.GetContract(address);
            return c == null || c.IsPayable;
        }
    }
}
