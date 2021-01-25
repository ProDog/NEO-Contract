using Neo.Cryptography.ECC;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Neo3Contract
{
    //public partial class Neo3Contract : SmartContract
    //{
        //public static bool CryptoTest(byte[] message, byte[] pubkey, byte[] signature)
        //{
        //    Crypto.ECDsa.Secp256k1.Verify(null, (ECPoint)pubkey, signature);
        //    Crypto.ECDsa.Secp256k1.Verify(message, (ECPoint)pubkey, signature);

        //    Crypto.ECDsa.Secp256r1.Verify(null, (ECPoint)pubkey, signature);
        //    Crypto.ECDsa.Secp256r1.Verify(message, (ECPoint)pubkey, signature);
        //    return true;
        //}

        //public static bool MultiCryptoTest(byte[] message, ECPoint[] pubkey, byte[][] signature)
        //{
        //    Crypto.ECDsa.Secp256k1.CheckMultiSig(null, pubkey, signature);
        //    Crypto.ECDsa.Secp256k1.CheckMultiSig(message, pubkey, signature);

        //    Crypto.ECDsa.Secp256r1.CheckMultiSig(null, pubkey, signature);
        //    Crypto.ECDsa.Secp256r1.CheckMultiSig(message, pubkey, signature);
        //    return true;
        //}

        ////[{"type":"Hash160","value":"0xe19de267a37a71734478f512b3e92c79fc3695fa"}] 
        //public static object Hash160Test(byte[] msg)
        //{
        //    OnNotify("aa", Crypto.RIPEMD160(msg));
        //    OnNotify("aa", Crypto.Hash160(msg));
        //    return Crypto.Hash160(msg);
        //}

        //public static object Hash256Test(byte[] msg)
        //{
        //    OnNotify("aa", Crypto.Hash256(msg));
        //    OnNotify("aa", Crypto.SHA256(msg));
        //    return Crypto.Hash256(msg);
        //}
    //}
}