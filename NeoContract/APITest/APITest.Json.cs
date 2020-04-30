using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace APITest
{
    public partial class APITest : SmartContract
    {
        public static bool JsonTest()
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
    }
}