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
        public static bool AccountTest(byte[] scriptHash)
        {
            var isStandard = Account.IsStandard(scriptHash);

            return isStandard;

            return true;
        }
    }
}
