using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace APITest
{
    public partial class APITest : SmartContract
    {
        ////[{"type":"Hash160","value":"0xf621168b1fce3a89c33a5f6bcf7e774b4657031c"}] 
        //public static void GasCall(UInt160 account)
        //{
        //    OnNotify(account);

        //    OnNotify(GAS.Name);

        //    OnNotify(GAS.Symbol);

        //    OnNotify(GAS.Decimals);

        //    OnNotify(GAS.TotalSupply());

        //    OnNotify(GAS.BalanceOf(account));

        //    //OnNotify(GAS.Transfer((UInt160)account, Owner, 1000));

        //    OnNotify(Owner);
        //}

        ////[{"type":"Hash160","value":"0xf621168b1fce3a89c33a5f6bcf7e774b4657031c"},{"type":"PublicKey","value":"0222d8515184c7d62ffa99b829aeb4938c4704ecb0dd7e340e842e9df121826343"}]
        //public static void NeoCall(UInt160 account, ECPoint publicKey)
        //{
        //    OnNotify(NEO.Name);

        //    OnNotify(NEO.Symbol);

        //    OnNotify(NEO.Decimals);

        //    OnNotify(NEO.TotalSupply());

        //    OnNotify(NEO.BalanceOf(account));

        //    OnNotify(NEO.UnclaimedGas(account, Blockchain.GetHeight()));

        //    OnNotify(NEO.GetCandidates());

        //    //OnNotify(NEO.GetValidators());

        //    OnNotify(NEO.GetCommittee());

        //    OnNotify(NEO.GetNextBlockValidators());

        //    OnNotify(NEO.RegisterCandidate(publicKey));

        //    OnNotify(NEO.UnregisterCandidate(publicKey));

        //    OnNotify(NEO.Vote(account, publicKey));

        //    //OnNotify(NEO.Transfer((UInt160)account, Owner, 1000));

        //    //OnNotify(NEO.GetGasPerBlock());

        //    //OnNotify(NEO.SetGasPerBlock(300000000));

        //    //OnNotify(NEO.GetGasPerBlock());
        //}

        //public static void PolicyCall()
        //{
        //    OnNotify(Policy.Name());

        //    OnNotify(Policy.GetMaxTransactionsPerBlock());

        //    OnNotify(Policy.GetMaxBlockSize());

        //    OnNotify(Policy.GetMaxBlockSystemFee());

        //    OnNotify(Policy.GetFeePerByte());

        //    OnNotify(Policy.Hash);

        //    //OnNotify(Policy.IsBlocked(Owner));

        //    OnNotify(Policy.BlockAccount((UInt160)Owner));

        //    OnNotify(Policy.UnblockAccount((UInt160)Owner));
        //}

        //public static void DesignationCall()
        //{
        //    OnNotify(Designation.Name);

        //    OnNotify(Designation.Hash);

        //    OnNotify(Designation.GetDesignatedByRole((DesignationRole)8));
        //}

        //public static void OracleCall()
        //{
        //    OnNotify(Oracle.Name);

        //    OnNotify(Oracle.Hash);

        //}
    }
}
