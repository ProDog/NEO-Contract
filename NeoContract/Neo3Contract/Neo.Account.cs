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
        //[{"type":"Hash160","value":"0xb9ac0300bec226885a09e61f83666372fd1e523e"}] 
        public static bool AccountTest(byte[] scriptHash)
        {
            var isStandard = Account.IsStandard((UInt160)scriptHash);

            return isStandard;
        }
    }
}
