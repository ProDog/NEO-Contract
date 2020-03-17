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

                if (operation == "AccountTest") return AccountTest();

                if (operation == "BlockchainTest") return BlockchainTest();

                if (operation == "RunTimeTest") return RunTimeTest();

                if (operation == "ContractTest") return ContractTest((byte[])args[0], (byte[])args[1], (BigInteger)args[2]);

                if (operation == "HelperTest") return HelperTest();

                if (operation == "JsonTest") return JsonTest();

                if (operation == "NativeTest") return NativeTest((byte[])args[0], (byte[])args[1], (BigInteger)args[2]);

                if (operation == "StorageTest") return StorageTest();
            }
            return false;
        }

        private static object NativeTest(byte[] from, byte[] to, BigInteger amount)
        {
            Native.NEO("transfer", new object[] { from, to, amount });

            Native.GAS("transfer", new object[] { from, to, amount });

            var res = Native.Policy("supportedStandards", new object[] { });
            Runtime.Notify(res);

            return true;
        }

        private static bool JsonTest()
        {
            Block block = Blockchain.GetBlock(Blockchain.GetHeight());
            Runtime.Notify(block);

            var stringBlock = Json.Serialize(block);
            Runtime.Notify(stringBlock);

            var vBlock = Json.Deserialize(stringBlock);
            Runtime.Notify(vBlock);

            Block cBlock = Json.Deserialize(stringBlock) as Block;
            Runtime.Notify(cBlock);

            return true;
        }

        private static bool HelperTest()
        {           
            StorageMap storage = Storage.CurrentContext.CreateMap("test");
            storage.Put("test1", "value");
            storage.Put("test1", "value");
            Runtime.Notify(storage.Get("test1"));

            StorageMap storage1 = Storage.CurrentContext.CreateMap(new byte[] { 0x01 });
            BigInteger aa = (BigInteger)200;
            storage1.Put("test1", aa);
            Runtime.Notify(storage1.Get("test1"));

            StorageMap storage2 = Storage.CurrentContext.CreateMap(0x02);
            storage2.Put(new byte[] { 0x01 }, new byte[] { 0x3b, 0x7d, 0x37, 0x11, 0xc6, 0xf0, 0xcc, 0xf9, 0xb1, 0xdc, 0xa9, 0x03, 0xd1, 0xbf, 0xd1, 0xd1 });
            Runtime.Notify(storage2.Get(new byte[] { 0x01 }));
            storage2.Delete(new byte[] { 0x01 });
            Runtime.Notify(storage2.Get(new byte[] { 0x01 }));

            return true;
        }

        private static bool ExecutionEngineTest()
        {
            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
            Runtime.Notify(tx.Hash, tx.Sender, tx.SystemFee, tx.NetworkFee);

            //当前合约hash 小端序
            var executingScriptHash = ExecutionEngine.ExecutingScriptHash;
            Runtime.Notify(executingScriptHash);

            var callingScriptHash = ExecutionEngine.CallingScriptHash;
            Runtime.Notify(callingScriptHash);

            var entryScriptHash = ExecutionEngine.EntryScriptHash;
            Runtime.Notify(entryScriptHash);

            return true;
        }

        private static bool AccountTest()
        {
            var isStandard = Account.IsStandard(ExecutionEngine.ExecutingScriptHash);
            Runtime.Notify(isStandard);

            var isStandard1 = Account.IsStandard(Owner());
            Runtime.Notify(isStandard1);

            var isStandard2 = Account.IsStandard(GasToken());
            Runtime.Notify(isStandard2);

            return true;
        }

        private static bool ContractTest(byte[] from, byte[] to, BigInteger amount)
        {
            var scriptHash = new byte[] { 162, 210, 135, 131, 161, 45, 171, 207, 225, 70, 39, 213, 236, 229, 148, 229, 63, 247, 220, 163 };

            var result = Contract.Call(scriptHash, "transfer", new object[] { from, to, amount });

            Runtime.Notify(result);

            Contract.CallEx(scriptHash, "transfer", new object[] { from, to, amount }, Neo.SmartContract.CallFlags.All);

            var balance = Contract.CallEx(scriptHash, "balanceOf", new object[] { from }, Neo.SmartContract.CallFlags.AllowCall);
            Runtime.Notify(balance);

            var balance1 = Contract.CallEx(scriptHash, "balanceOf", new object[] { to }, Neo.SmartContract.CallFlags.AllowModifyStates);
            Runtime.Notify(balance1);

            var balance2 = Contract.CallEx(scriptHash, "balanceOf", new object[] { from }, Neo.SmartContract.CallFlags.AllowNotify);
            Runtime.Notify(balance2);

            var balance3 = Contract.CallEx(scriptHash, "balanceOf", new object[] { to }, Neo.SmartContract.CallFlags.None);
            Runtime.Notify(balance3);

            var totalSupply = Contract.CallEx(scriptHash, "totalSupply", new object[] { }, Neo.SmartContract.CallFlags.ReadOnly);
            Runtime.Notify(totalSupply);

            return true;
        }

        private static bool BlockchainTest()
        {
            //Runtime.Notify(Blockchain.GetHeight());
            //Runtime.Notify(Blockchain.GetBlock(Blockchain.GetHeight()).Hash);
            //Runtime.Notify(Blockchain.GetBlock(Blockchain.GetHeight()).Index);
            //Runtime.Notify(Blockchain.GetBlock(Blockchain.GetHeight()).Version);
            //Runtime.Notify(Blockchain.GetBlock(Blockchain.GetHeight()).PrevHash);
            //Runtime.Notify(Blockchain.GetBlock(Blockchain.GetHeight()).MerkleRoot);
            //Runtime.Notify((long)Blockchain.GetBlock(Blockchain.GetHeight()).Timestamp);
            //Runtime.Notify(Blockchain.GetBlock(Blockchain.GetHeight()).NextConsensus);
            //Runtime.Notify((uint)Blockchain.GetBlock(Blockchain.GetHeight()).TransactionsCount);

            ////使用小端序
            ////byte[] blockHash = new byte[] { 143, 40, 169, 28, 212, 240, 46, 255, 58, 27, 122, 170, 234, 119, 58, 158, 217, 232, 55, 120, 75, 221, 32, 58, 207, 245, 135, 22, 12, 47, 136, 243 };
            byte[] blockHash = new byte[] { 243, 136, 47, 12, 22, 135, 245, 207, 58, 32, 221, 75, 120, 55, 232, 217, 158, 58, 119, 234, 170, 122, 27, 58, 255, 46, 240, 212, 28, 169, 40, 143 };
            //Block block = Blockchain.GetBlock(blockHash);
            //Runtime.Notify(block);
            //Runtime.Notify(Blockchain.GetBlock(blockHash).Hash);

            //Runtime.Notify(Json.Serialize(Blockchain.GetBlock(Blockchain.GetHeight())));

            //byte[] contractHash = new byte[] { 163, 220, 247, 63, 229, 148, 229, 236, 213, 39, 70, 225, 207, 171, 45, 161, 131, 135, 210, 162 };//大端
            byte[] contractHash = new byte[] { 162, 210, 135, 131, 161, 45, 171, 207, 225, 70, 39, 213, 236, 229, 148, 229, 63, 247, 220, 163 };

            Contract contract = Blockchain.GetContract(contractHash);
            //Runtime.Notify(contract);
            Runtime.Notify(Blockchain.GetContract(contractHash).IsPayable);
            Runtime.Notify(Blockchain.GetContract(contractHash).HasStorage);

            byte[] txid = new byte[] { 197, 91, 110, 0, 95, 50, 123, 78, 96, 217, 166, 94, 143, 223, 29, 148, 157, 232, 212, 191, 161, 96, 175, 90, 69, 232, 29, 221, 46, 19, 187, 11 };
            Runtime.Notify(Blockchain.GetTransaction(txid).Hash);
            Runtime.Notify(Blockchain.GetTransaction(txid));

            Runtime.Notify(Blockchain.GetTransactionFromBlock(blockHash, 0).Hash);
            Runtime.Notify(Blockchain.GetTransactionFromBlock(Blockchain.GetBlock(blockHash).Index, 0).Hash);
            Runtime.Notify(Blockchain.GetTransaction(txid));

            return true;
        }

        private static bool RunTimeTest()
        {
            Runtime.Notify(Runtime.Trigger.Serialize());
            Runtime.Notify(Runtime.Platform);
            Runtime.Notify((long)Runtime.Time);
            Runtime.Notify(Runtime.InvocationCounter);

            var notifications = Runtime.GetNotifications();
            //if (notifications.Length == 0)
            //    throw new Exception();
            //var notification = (object[])notifications[0].State;

            //byte[] scriptHash = notifications[0].ScriptHash;
            //bool isTransfer = (string)notification[0] == "Transfer";

            Runtime.Notify((uint)notifications.Length);

            if (Runtime.CheckWitness(Owner()))
            {
                Runtime.Notify(1);
            }
            else
            {
                Runtime.Notify(0);
            }

            Runtime.Notify("1111", 5, 77, ExecutionEngine.EntryScriptHash);
            Runtime.Log("end!");

            return true;
        }

        private static bool StorageTest()
        {
            StorageMap storageMap = Storage.CurrentContext.CreateMap("test");

            storageMap.Put("12313", 123);
            storageMap.Put("12314", 123);

            Storage.Put(Storage.CurrentContext, "12315", "hello");
            Runtime.Notify(Storage.Get(Storage.CurrentContext, "12315"));

            Storage.Put(Storage.CurrentContext, "12315", 2);
            Runtime.Notify(Storage.Get(Storage.CurrentContext, "12315"));

            Storage.PutEx("12318", "hello", StorageFlags.None);
            Runtime.Notify(Storage.Get(Storage.CurrentContext, "12318"));

            Storage.Put(Storage.CurrentContext, "12318", 2);
            Runtime.Notify(Storage.Get(Storage.CurrentContext, "12318"));

            //Storage.PutEx("12317", "hello", StorageFlags.Constant);
            //Runtime.Notify(Storage.Get(Storage.CurrentContext, "12317"));

            //Storage.Put(Storage.CurrentContext, "12317", 2);
            //Runtime.Notify(Storage.Get(Storage.CurrentContext, "12317"));


            Runtime.Notify(Storage.Find("12315"));

            return true;
        }

        private static bool IsPayable(byte[] address)
        {
            var c = Blockchain.GetContract(address);
            return c == null || c.IsPayable;
        }
    }
}
