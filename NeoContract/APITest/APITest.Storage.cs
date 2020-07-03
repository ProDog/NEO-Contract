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
        //public static object StorageTest()
        //{
        //    StorageMap storage = Storage.CurrentContext.CreateMap("test");
        //    storage.Put("test1", "value");
        //    storage.Put("test1", "value");
        //    Notify("aa", storage.Get("test1"));

        //    StorageMap storage1 = Storage.CurrentContext.CreateMap(new byte[] { 0x01 });
        //    BigInteger aa = 200;
        //    storage1.Put("test1", aa);
        //    Notify("aa", storage1.Get("test1"));

        //    StorageMap storageMap = Storage.CurrentContext.CreateMap("test_map");
        //    // Contract compilations report errors when byte[] length exceeds 16
        //    storageMap.Put(new byte[] { 0x01 }, new byte[] { 0x89, 0x77, 0x20, 0xd8, 0xcd, 0x76, 0xf4, 0xf0, 0x0a, 0xbf, 0xa3, 0x7c, 0x0e, 0xdd, 0x88, 0x9c, 0x20, 0x8f, 0xde, 0x9b, 0x3b, 0x7d, 0x37, 0x11, 0xc6, 0xf0, 0xcc, 0xf9, 0xb1, 0xdc, 0xa9, 0x03, 0xd1, 0xbf, 0xa1, 0xd8, 0x96, 0xf1, 0x23, 0x8c, 0xfa, 0x79, 0x76, 0x3b, 0x86, 0x76, 0x7b, 0x42, 0x68, 0x72, 0x34, 0x9f, 0xd2, 0xfd, 0xbc, 0xcf, 0x16, 0x2e, 0xe2, 0x20 });

        //    Notify("aa", storageMap.Get(new byte[] { 0x01 }));
        //    //return storageMap.Get(new byte[] { 0x01 });

        //    Notify("aa", storageMap.Get(new byte[] { 0x01 }));
        //    storageMap.Delete(new byte[] { 0x01 });
        //    Notify("aa", storageMap.Get(new byte[] { 0x01 }));

        //    storageMap.Put("12313", 123);
        //    storageMap.Put("12314", 123);

        //    Storage.Put(Storage.CurrentContext, "12315", "hello");
        //    Notify("aa", Storage.Get(Storage.CurrentContext, "12315"));

        //    Storage.Put(Storage.CurrentContext, "12315", 2);
        //    Notify("aa", Storage.Get(Storage.CurrentContext, "12315"));

        //    //Storage.PutEx("12318", "hello", StorageFlags.None);
        //    //Runtime.Notify(Storage.Get(Storage.CurrentContext, "12318"));

        //    Storage.Put(Storage.CurrentContext, "12318", 2);
        //    Notify("aa", Storage.Get(Storage.CurrentContext, "12318"));




        //    //Storage.PutEx("12317", "hello", StorageFlags.Constant);
        //    //Runtime.Notify(Storage.Get(Storage.CurrentContext, "12317"));
        //    Storage.Put(Storage.CurrentContext, "12317", 2);
        //    Notify("aa", Storage.Get(Storage.CurrentContext, "12317"));

        //    var findRes = Storage.Find("12315");
        //    if (findRes != null)
        //    {
        //        Notify("aa", 0);
        //        //Runtime.Notify(findRes.Keys);
        //    }
        //    else
        //    {
        //        Notify("aa", 1);
        //    }

        //    return storageMap.Get(new byte[] { 0x01 });
        //}

        //public static bool StorageContextTest(byte[] key, byte[] value)
        //{
        //    Storage.Put(Storage.CurrentContext, "test", 11);
        //    Notify("aa", Storage.Get(Storage.CurrentReadOnlyContext, "test"));

        //    StorageContext storageContext = Storage.CurrentContext.AsReadOnly;

        //    Notify("aa", Storage.Get(storageContext, "test"));

        //    //Storage.PutEx("test", 33, StorageFlags.Constant);

        //    Notify("aa", Storage.Get(storageContext, "test"));

        //    Notify("aa", 1);

        //    //如果是ReadOnly 这里Put会报错
        //    //Storage.Put(storageContext, "test", 22);

        //    Notify("aa", 2);

        //    Notify("aa", Storage.Get(storageContext, "test"));

        //    StorageMap whiteListMap = Storage.CurrentContext.CreateMap("whiteListMap");
        //    byte[] whiteListBytes = whiteListMap.Get("whiteList");
        
        //    Map<byte[], byte[]> map = new Map<byte[], byte[]>();
     
        //    if (whiteListBytes != null && whiteListBytes.Length > 0)
        //    {               
        //        map = whiteListBytes.Deserialize() as Map<byte[], byte[]>;
        //    }

        //    map[key] = value;
        //    whiteListMap.Put("whiteList", map.Serialize());
        //    Notify("aa", map);
        //    Notify("aa", map.Keys.Length);

        //    return true;
        //}
    }
}