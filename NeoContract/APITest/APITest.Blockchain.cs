using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

namespace APITest
{
    public partial class APITest : SmartContract
    {
        //这个要用小端
        private static byte[] blockHash = "28339bb71514e9569782792da156e4345be90703fa5e0dbf2586b731987ca77d".HexToBytes();
        private static byte[] txHash = "eab061ab4bfde5c3b31b134031fe197ccd35da54e9a39d873d731f9642bd0aa1".HexToBytes();
        private static byte[] contractHash = "57636cdfb83c31699cc496cfd575edfc8f657e49".HexToBytes();
        public static bool BlockchainTest()
        {
            OnNotify(Blockchain.GetHeight());
            OnNotify(Blockchain.GetBlock(Blockchain.GetHeight()).Hash);

            OnNotify(Blockchain.GetBlock(Blockchain.GetHeight()).Index);
            OnNotify(Blockchain.GetBlock(Blockchain.GetHeight()).Version);
            OnNotify(Blockchain.GetBlock(Blockchain.GetHeight()).PrevHash);
            OnNotify(Blockchain.GetBlock(Blockchain.GetHeight()).MerkleRoot);
            OnNotify((long)Blockchain.GetBlock(Blockchain.GetHeight()).Timestamp);
            OnNotify(Blockchain.GetBlock(Blockchain.GetHeight()).NextConsensus);
            OnNotify((uint)Blockchain.GetBlock(Blockchain.GetHeight()).TransactionsCount);

            ////use little-endian
            ////byte[] blockHash = new byte[] { 221, 32, 9, 8, 77, 164, 207, 77, 104, 109, 114, 68, 218, 36, 109, 151, 224, 231, 107, 28, 89, 93, 147, 139, 38, 180, 36, 55, 220, 164, 94, 14 };

            Block block = Blockchain.GetBlock((UInt256)blockHash);
            OnNotify(block);
            OnNotify(Blockchain.GetBlock((UInt256)blockHash)?.Hash);
            OnNotify(Blockchain.GetBlock((UInt256)blockHash).Serialize());

            //////use little-endian
            //////byte[] contractHash = new byte[] { 179, 43, 195, 48, 182, 136, 177, 145, 44, 64, 49, 221, 13, 238, 186, 100, 21, 109, 217, 199 };
            Contract contract = Blockchain.GetContract((UInt160)contractHash);
            //OnNotify(contract);
            //OnNotify(contract?.IsPayable); 这种写法 加个 ? 就会报错
            OnNotify(contract.IsPayable);
            OnNotify(contract.HasStorage);
            //OnNotify(contract?.Script);

            //OnNotify(Blockchain.GetContract((UInt160)contractHash)?.IsPayable);
            OnNotify(Blockchain.GetContract((UInt160)contractHash).HasStorage);

            ////use little-endian
            ////byte[] txid = new byte[] { 174, 32, 38, 156, 164, 178, 143, 190, 234, 97, 150, 204, 107, 45, 190, 74, 153, 85, 146, 35, 171, 206, 125, 61, 253, 230, 22, 111, 212, 67, 170, 176 };
            OnNotify(Blockchain.GetTransaction((UInt256)txHash)?.Hash);
            OnNotify(Blockchain.GetTransaction((UInt256)txHash).Sender);
            OnNotify(Blockchain.GetTransaction((UInt256)txHash));

            OnNotify(Blockchain.GetTransactionFromBlock((UInt256)blockHash, 0)?.Hash);
            OnNotify(Blockchain.GetTransactionFromBlock(Blockchain.GetBlock((UInt256)blockHash).Index, 0)?.Hash);

            return true;
        }
    }
}
