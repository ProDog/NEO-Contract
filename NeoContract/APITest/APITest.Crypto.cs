using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace APITest
{
    public partial class APITest : SmartContract
    {
        public static bool CryptoTest(byte[] message, byte[] pubkey, byte[] signature)
        {
            Crypto.ECDsa.Secp256k1.Verify(null, pubkey, signature);
            Crypto.ECDsa.Secp256k1.Verify(message, pubkey, signature);

            Crypto.ECDsa.Secp256r1.Verify(null, pubkey, signature);
            Crypto.ECDsa.Secp256r1.Verify(message, pubkey, signature);
            return true;
        }

        public static bool MultiCryptoTest(byte[] message, byte[][] pubkey, byte[][] signature)
        {
            Crypto.ECDsa.Secp256k1.CheckMultiSig(null, pubkey, signature);
            Crypto.ECDsa.Secp256k1.CheckMultiSig(message, pubkey, signature);

            Crypto.ECDsa.Secp256r1.CheckMultiSig(null, pubkey, signature);
            Crypto.ECDsa.Secp256r1.CheckMultiSig(message, pubkey, signature);
            return true;
        }

        public static object Hash160Test(byte[] msg)
        {
            Runtime.Notify(Crypto.RIPEMD160(msg));
            Runtime.Notify(Crypto.HASH160(msg));
            return Crypto.HASH160(msg);
        }

        public static object Hash256Test(byte[] msg)
        {
            Runtime.Notify(Crypto.HASH256(msg));
            Runtime.Notify(Crypto.SHA256(msg));
            return Crypto.HASH256(msg);
        }
    }
}