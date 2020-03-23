using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Collections.Generic;
using System.Text;

namespace APITest
{
    public partial class APITest : SmartContract
    {
        private static bool HelperTest()
        {
            byte[] bs = new byte[] { 216, 234, 162, 12, 34 };
            Runtime.Notify(bs.ToBigInteger());

            //sbyte[] sbs = new sbyte[] { -123, 124, -16, 127, 28 };
            //Runtime.Notify(sbs.ToByteArray());
            //Runtime.Notify(bs.ToSbyteArray());

            //Runtime.Notify(bs.AsString());

            //Runtime.Notify("gripzhang".ToByteArray());

            //Runtime.Notify(5.Within(10, 5));
            //Runtime.Notify(10.Within(5, 15));

            ////Runtime.Notify(12.AsSbyte());
            ////Runtime.Notify(12.AsByte());

            //Runtime.Notify(58.ToByte());
            //Runtime.Notify(25.ToSbyte());

            //Runtime.Notify((new byte[] { 0x12, 0x23, 0x32 }).Concat(new byte[] { 0x55, 0x23 }));
            //Runtime.Notify((new byte[] { 0x12, 0x23, 0x32 }).Range(0, 1));

            //Runtime.Notify((new byte[] { 0x12, 0x23, 0x32 }).Take(1));
            //Runtime.Notify((new byte[] { 0x12, 0x23, 0x32 }).Last(1));

            //Runtime.Notify((new byte[] { 0x12, 0x23, 0x32 }).Reverse());

            return true;
        }
    }
}
