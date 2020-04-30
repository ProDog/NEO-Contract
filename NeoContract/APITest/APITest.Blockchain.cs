using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

namespace APITest
{
    public partial class APITest : SmartContract
    {
        public static bool BlockchainTest(byte[] hash)
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
            //byte[] blockHash = new byte[] { 221, 32, 9, 8, 77, 164, 207, 77, 104, 109, 114, 68, 218, 36, 109, 151, 224, 231, 107, 28, 89, 93, 147, 139, 38, 180, 36, 55, 220, 164, 94, 14 };

            Block block = Blockchain.GetBlock(hash);
            Runtime.Notify(block);
            Runtime.Notify(Blockchain.GetBlock(hash)?.Hash);
            Runtime.Notify(Json.Serialize(Blockchain.GetBlock(Blockchain.GetHeight())));

            //use little-endian
            //byte[] contractHash = new byte[] { 179, 43, 195, 48, 182, 136, 177, 145, 44, 64, 49, 221, 13, 238, 186, 100, 21, 109, 217, 199 };
            Contract contract = Blockchain.GetContract(hash);
            Runtime.Notify(contract?.IsPayable);
            Runtime.Notify(contract?.HasStorage);
            Runtime.Notify(contract?.Script);

            Runtime.Notify(Blockchain.GetContract(hash)?.IsPayable);
            Runtime.Notify(Blockchain.GetContract(hash)?.HasStorage);

            //use little-endian
            //byte[] txid = new byte[] { 174, 32, 38, 156, 164, 178, 143, 190, 234, 97, 150, 204, 107, 45, 190, 74, 153, 85, 146, 35, 171, 206, 125, 61, 253, 230, 22, 111, 212, 67, 170, 176 };
            Runtime.Notify(Blockchain.GetTransaction(hash)?.Hash);
            Runtime.Notify(Blockchain.GetTransaction(hash));

            Runtime.Notify(Blockchain.GetTransactionFromBlock(hash, 0)?.Hash);
            Runtime.Notify(Blockchain.GetTransactionFromBlock(Blockchain.GetBlock(hash).Index, 0)?.Hash);
            Runtime.Notify(Blockchain.GetTransaction(hash));

            return true;
        }
    }
}
