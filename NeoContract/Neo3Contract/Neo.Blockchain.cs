using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

namespace Neo3Contract
{
    public partial class Neo3Contract : SmartContract
    {
        //小端
        private static byte[] blockHash = "97bf781ae29eec1096f7649e172ea7994f7b0c3055c0a60a4114f74942914ec0".HexToBytes();
        private static byte[] txHash = "353f9599b04c27224c9442cc126ff967de0adf624a7074472dcbfdb2dcbf44ac".HexToBytes();
        private static byte[] contractHash = "855c69df79591a3aa97594b2f6942e6d9ae82aa4".HexToBytes();
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

            Block block = Blockchain.GetBlock((UInt256)blockHash);
            //OnNotify(block);
            OnNotify(Blockchain.GetBlock((UInt256)blockHash)?.Hash);
            //OnNotify(Blockchain.GetBlock((UInt256)blockHash).Serialize());

            OnNotify(Blockchain.GetTransaction((UInt256)txHash)?.Hash);
            OnNotify(Blockchain.GetTransaction((UInt256)txHash).Sender);
            OnNotify(Blockchain.GetTransaction((UInt256)txHash).Version);
            OnNotify(Blockchain.GetTransaction((UInt256)txHash).Nonce);
            OnNotify(Blockchain.GetTransaction((UInt256)txHash).ValidUntilBlock);
            //OnNotify(Blockchain.GetTransaction((UInt256)txHash).Script);
            OnNotify(Blockchain.GetTransaction((UInt256)txHash).SystemFee);
            OnNotify(Blockchain.GetTransaction((UInt256)txHash).NetworkFee);
            //OnNotify(Blockchain.GetTransaction((UInt256)txHash));

            OnNotify(Blockchain.GetTransactionFromBlock((UInt256)blockHash, 0)?.Hash);
            OnNotify(Blockchain.GetTransactionFromBlock(Blockchain.GetBlock((UInt256)blockHash).Index, 0)?.Hash);

            OnNotify(Blockchain.GetTransactionHeight((UInt256)txHash));

            return true;
        }
    }
}
