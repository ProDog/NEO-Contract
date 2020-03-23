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
        private static bool StorageTest()
        {
            StorageMap storage = Storage.CurrentContext.CreateMap("test");
            storage.Put("test1", "value");
            storage.Put("test1", "value");
            Runtime.Notify(storage.Get("test1"));

            StorageMap storage1 = Storage.CurrentContext.CreateMap(new byte[] { 0x01 });
            BigInteger aa = 200;
            storage1.Put("test1", aa);
            Runtime.Notify(storage1.Get("test1"));

            StorageMap storageMap = Storage.CurrentContext.CreateMap("test_map");
            // Contract compilations report errors when byte[] length exceeds 16
            storageMap.Put(new byte[] { 0x01 }, new byte[] { 0x3b, 0x7d, 0x37, 0x11, 0xc6, 0xf0, 0xcc, 0xf9, 0xb1, 0xdc, 0xa9, 0x03, 0xd1, 0xbf, 0xd1, 0xd1 });
                       

            Runtime.Notify(storageMap.Get(new byte[] { 0x01 }));
            storageMap.Delete(new byte[] { 0x01 });
            Runtime.Notify(storageMap.Get(new byte[] { 0x01 }));

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

            //var findRes = Storage.Find("12315");
            //if (findRes != null)
            //{
            //    Runtime.Notify(0);
            //    Runtime.Notify(findRes.Keys);
            //}
            //else
            //{
            //    Runtime.Notify(1);
            //}

            return true;
        }

        private static bool StorageContextTest()
        {
            Storage.Put(Storage.CurrentContext, "test", 11);
            Runtime.Notify(Storage.Get(Storage.CurrentReadOnlyContext, "test"));

            StorageContext storageContext = Storage.CurrentContext.AsReadOnly;

            Runtime.Notify(Storage.Get(storageContext, "test"));

            Storage.PutEx("test", 33, StorageFlags.Constant);

            Runtime.Notify(Storage.Get(storageContext, "test"));

            Runtime.Notify(1);

            //如果是ReadOnly 这里Put会报错
            //Storage.Put(storageContext, "test", 22);

            Runtime.Notify(2);

            Runtime.Notify(Storage.Get(storageContext, "test"));

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