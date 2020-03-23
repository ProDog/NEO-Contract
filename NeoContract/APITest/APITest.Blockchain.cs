using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

namespace APITest
{
    public partial class APITest : SmartContract
    {
        private static bool BlockchainTest()
        {
            Runtime.Notify(Blockchain.GetHeight());
            Runtime.Notify(Blockchain.GetBlock(Blockchain.GetHeight()).Hash);
            Runtime.Notify(Blockchain.GetBlock(Blockchain.GetHeight()).Index);
            Runtime.Notify(Blockchain.GetBlock(Blockchain.GetHeight()).Version);
            Runtime.Notify(Blockchain.GetBlock(Blockchain.GetHeight()).PrevHash);
            Runtime.Notify(Blockchain.GetBlock(Blockchain.GetHeight()).MerkleRoot);
            Runtime.Notify((long)Blockchain.GetBlock(Blockchain.GetHeight()).Timestamp);
            Runtime.Notify(Blockchain.GetBlock(Blockchain.GetHeight()).NextConsensus);
            Runtime.Notify((uint)Blockchain.GetBlock(Blockchain.GetHeight()).TransactionsCount);

            //use little-endian
            byte[] blockHash = new byte[] { 243, 136, 47, 12, 22, 135, 245, 207, 58, 32, 221, 75, 120, 55, 232, 217, 158, 58, 119, 234, 170, 122, 27, 58, 255, 46, 240, 212, 28, 169, 40, 143 };

            Block block = Blockchain.GetBlock(blockHash);
            Runtime.Notify(block);
            Runtime.Notify(Blockchain.GetBlock(blockHash).Hash);
            Runtime.Notify(Json.Serialize(Blockchain.GetBlock(Blockchain.GetHeight())));

            //use little-endian
            byte[] contractHash = new byte[] { 162, 210, 135, 131, 161, 45, 171, 207, 225, 70, 39, 213, 236, 229, 148, 229, 63, 247, 220, 163 };
            Contract contract = Blockchain.GetContract(contractHash);
            Runtime.Notify(contract.IsPayable);
            Runtime.Notify(contract.HasStorage);
            Runtime.Notify(contract.Script);

            Runtime.Notify(Blockchain.GetContract(contractHash).IsPayable);
            Runtime.Notify(Blockchain.GetContract(contractHash).HasStorage);

            //use little-endian
            byte[] txid = new byte[] { 197, 91, 110, 0, 95, 50, 123, 78, 96, 217, 166, 94, 143, 223, 29, 148, 157, 232, 212, 191, 161, 96, 175, 90, 69, 232, 29, 221, 46, 19, 187, 11 };
            Runtime.Notify(Blockchain.GetTransaction(txid).Hash);
            Runtime.Notify(Blockchain.GetTransaction(txid));

            Runtime.Notify(Blockchain.GetTransactionFromBlock(blockHash, 0).Hash);
            Runtime.Notify(Blockchain.GetTransactionFromBlock(Blockchain.GetBlock(blockHash).Index, 0).Hash);
            Runtime.Notify(Blockchain.GetTransaction(txid));

            return true;
        }
    }
}
