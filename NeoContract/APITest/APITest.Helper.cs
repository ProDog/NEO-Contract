using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace APITest
{
    public partial class APITest : SmartContract
    {
        private static object HelperTest()
        {
            byte[] bs = new byte[] { 216, 234, 162, 12, 34 };
            //sbyte[] sbs = new sbyte[] { -123, 124, -16, 127, 28 };
            //Runtime.Notify(bs.ToBigInteger());

            //var a = bs.AsString();
            //var b = "gripzhang".ToByteArray();
            //var c = 5.Within(10, 5);
            //var d = 10.Within(5, 15);

            //发布合约时报错
            //var e = 12.AsSbyte();
            //var f = 12.AsByte();
            //var g = 58.ToByte();
            //var h = 25.ToSbyte();            

            //编译就报错
            //var m = (new byte[] { 0x12, 0x23, 0x32 }).Reverse();

            //通过
            //Runtime.Notify(sbs.ToByteArray());
            //Runtime.Notify(bs.ToSbyteArray());
            //Runtime.Notify(bs.AsString());
            //Runtime.Notify("zhanggrip".ToByteArray());
            //Runtime.Notify(5.Within(10, 5));
            //Runtime.Notify(10.Within(5, 15));

            // return 或 Runtime.Notify(a.Concat(b)) 都会报错
            //var b1 = new byte[] { 0x12, 0x23, 0x32 };
            //var b2 = new byte[] { 0x55, 0x23 };
            //var b3 = b1.Concat(b2);
            //return b3;
            //var nb = (new byte[] { 0x12, 0x23, 0x32 }).Concat(new byte[] { 0x55, 0x23 });
            //return nb;
            //Runtime.Notify((new byte[] { 0x12, 0x23, 0x32 }).Concat(new byte[] { 0x55, 0x23 }));

            //Runtime.Notify((new byte[] { 0x12, 0x23, 0x32 }).Concat(new byte[] { 0x55, 0x23 }));
            //Runtime.Notify("zhang".ToByteArray().Concat("grip".ToByteArray()));

            //invoke 报错
            //return (new byte[] { 0x12, 0x23, 0x32 }).Range(0, 3);
            //return (new byte[] { 0x12, 0x23, 0x32 }).Take(1);
            //return (new byte[] { 0x12, 0x23, 0x32 }).Last(1);

            //invoke正常，同步区块后 Persist 报错
            //Runtime.Notify((new byte[] { 0x12, 0x23, 0x32 }).Range(0, 1));
            //Runtime.Notify((new byte[] { 0x12, 0x23, 0x32 }).Take(1));
            //Runtime.Notify((new byte[] { 0x12, 0x23, 0x32 }).Last(1));

            //neo2 中 Reverse() 没有效果，待验证，by longfei
            //Runtime.Notify("gripzhang".ToByteArray().Reverse());


            //byte[] bt = (new byte[] { 0x12, 0x23, 0x32 }).Concat(new byte[] { 0x55, 0x23 });

            //Runtime.Notify(bt);

            //return bt;


            Map<byte[], string> map = new Map<byte[], string>();
            StorageMap whiteListMap = Storage.CurrentContext.CreateMap("whiteListMap");
            byte[] whiteListBytes = whiteListMap.Get("whiteList");

            //whiteListBytes is null, so can't check it just using whiteListBytes.Length
            if (whiteListBytes != null && whiteListBytes.Length > 0)
                map = whiteListBytes.Deserialize() as Map<byte[], string>;
            byte[] key = (ExecutionEngine.ScriptContainer as Transaction).Hash;
            string value = "test_value";
            map[key] = value;
            whiteListMap.Put("whiteList", map.Serialize());

            return map;
        }

        public static bool Migrate(byte[] script, string manifest)
        {

            if (!Runtime.CheckWitness(Owner))
            {
                return false;
            }
            if (script.Length == 0 || manifest.Length == 0)
            {
                return false;
            }

            Contract.Update(script, manifest);
            return true;
        }
    }
}
