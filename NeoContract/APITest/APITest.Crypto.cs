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
        private static bool CryptoTest(byte[] message, byte[] pubkey, byte[] signature)
        {
            Runtime.Notify(Crypto.ECDsaVerify(null, pubkey, signature));
            Runtime.Notify(Crypto.ECDsaVerify(message, pubkey, signature));
            return true;
        }

        private static bool MultiCryptoTest(byte[] message, byte[][] pubkey, byte[][] signature)
        {
            Runtime.Notify(Crypto.ECDsaCheckMultiSig(null, pubkey, signature));
            Runtime.Notify(Crypto.ECDsaCheckMultiSig(message, pubkey, signature));
            return true;
        }
    }
}