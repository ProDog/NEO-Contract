using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Neo3Contract
{
    public partial class Neo3Contract : SmartContract
    {
        public static object HelperTest()
        {
            byte[] bs = new byte[] { 216, 234, 162, 12, 34 };
            sbyte[] sbs = new sbyte[] { -123, 124, -16, 127, 28 };
            OnNotify("aa", bs.ToBigInteger());

            //var a = bs.AsString();
            var b = "gripzhang".ToByteArray();
            var c = 5.Within(10, 5);
            var d = 10.Within(5, 15);

            ////发布合约时报错 已修复
            var e = 120.AsSbyte();
            var f = 120.AsByte();
            var g = 55.ToByte();
            var h = 125.ToSbyte();

            //编译就报错 已修复
            var m = (new byte[] { 0x12, 0x23, 0x32 }).Reverse();

            string testStr = "test string";
            //return testStr.ToByteArray();
            OnNotify("aa", testStr.ToByteArray());
            OnNotify("aa", (new sbyte[] { -123, 124, -16, 127, 28 }));
            OnNotify("aa", (new byte[] { 123, 124, 16, 127, 28 }));
            OnNotify("aa", ((sbyte)90).ToByteArray());

            //return m;

            //return "zhanggrip".ToByteArray();

            // return 或 Runtime.Notify(a.Concat(b)) 都会报错
            var b1 = new byte[] { 0x12, 0x23, 0x32 };
            var b2 = new byte[] { 0x55, 0x23 };
            var b3 = b1.Concat(b2);
            //return b3;
            var nb = (new byte[] { 0x12, 0x23, 0x32 }).Concat(new byte[] { 0x55, 0x23 });
            //return nb;
            OnNotify("aa", (new byte[] { 0x12, 0x23, 0x32 }).Concat(new byte[] { 0x55, 0x23 }));
            //Notify("aa", bs.AsString());
            OnNotify("aa", "zhanggrip".ToByteArray());
            OnNotify("aa", 5.Within(10, 5));
            OnNotify("aa", 10.Within(5, 15));

            OnNotify("aa", (new byte[] { 0x12, 0x23, 0x32 }).Concat(new byte[] { 0x55, 0x23 }));
            OnNotify("aa", "zhang".ToByteArray().Concat("grip".ToByteArray()));

            //invoke 报错
            //return (new byte[] { 0x12, 0x23, 0x32 }).Range(0, 3);
            //return (new byte[] { 0x12, 0x23, 0x32 }).Take(1);
            //return (new byte[] { 0x12, 0x23, 0x32 }).Last(1);

            //invoke正常，同步区块后 Persist 报错
            OnNotify("aa", (new byte[] { 0x12, 0x23, 0x32 }).Range(0, 1));
            OnNotify("aa", (new byte[] { 0x12, 0x23, 0x32 }).Take(1));
            OnNotify("aa", (new byte[] { 0x12, 0x23, 0x32 }).Last(1));

            OnNotify("aa", "gripzhang".ToByteArray().Reverse());

            byte[] bt = (new byte[] { 0x12, 0x23, 0x32 }).Concat(new byte[] { 0x55, 0x23 });

            //Notify(a, b, c, d, e, f, g, h, m, b1, b2, b3, nb, bt);

            Map<UInt256, string> map = new Map<UInt256, string>();
            StorageMap whiteListMap = Storage.CurrentContext.CreateMap("whiteListMap");
            byte[] whiteListBytes = whiteListMap.Get("whiteList").Serialize();

            //whiteListBytes is null, so can't check it just using whiteListBytes.Length
            if (whiteListBytes != null && whiteListBytes.Length > 0)
                map = whiteListBytes.Deserialize() as Map<UInt256, string>;
            UInt256 key = (ExecutionEngine.ScriptContainer as Transaction).Hash;
            string value = "test_value";
            map[key] = value;
            whiteListMap.Put("whiteList", map.Serialize().ToByteString());

            //return h;

            return "gripzhang".ToByteArray().Reverse();
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

            return true;
        }
    }
}
