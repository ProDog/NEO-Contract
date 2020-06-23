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
            Runtime.Notify("aa", block);

            var stringBlock = Json.Serialize(block);
            Runtime.Notify("aa", stringBlock);

            var vBlock = Json.Deserialize(stringBlock);
            Runtime.Notify("aa", vBlock);

            Block cBlock = Json.Deserialize(stringBlock) as Block;
            Runtime.Notify("aa", cBlock);

            return true;
        }
    }
}