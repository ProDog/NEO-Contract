using Neo;
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
        //scriptHash use little-endian
        //[{"type":"Hash160","value":"0x497e658ffced75d5cf96c49c69313cb8df6c6357"}] 
        public static bool AccountTest(byte[] scriptHash)
        {
            var isStandard = Account.IsStandard((UInt160)scriptHash);

            return isStandard;
        }
    }
}
