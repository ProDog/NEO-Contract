using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Neo3Contract
{
    public partial class Neo3Contract : SmartContract
    {
        public static bool JsonTest()
        {
            Block block = Blockchain.GetBlock(Blockchain.GetHeight());
            OnNotify("aa", block);

            var stringBlock = Json.Serialize(block);
            OnNotify("aa", stringBlock);

            var vBlock = Json.Deserialize(stringBlock);
            OnNotify("aa", vBlock);

            Block cBlock = Json.Deserialize(stringBlock) as Block;
            OnNotify("aa", cBlock);

            return true;
        }
    }
}