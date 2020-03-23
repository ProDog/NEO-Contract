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


            Map<byte[], string> map = new Map<byte[], string>();
            StorageMap whiteListMap = Storage.CurrentContext.CreateMap("whiteListMap");
            byte[] whiteListBytes = whiteListMap.Get("whiteList");
            if (whiteListBytes.Length > 0)
                map = whiteListBytes.Deserialize() as Map<byte[], string>;
            byte[] key = new byte[] { 0x11, 0x12 };
            string value = "test_value";
            map[key] = value;
            whiteListMap.Put("whiteList", map.Serialize());

            return true;
        }
    }
}