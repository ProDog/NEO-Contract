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
        //[{"type":"Hash160","value":"0xe19de267a37a71734478f512b3e92c79fc3695fa"}] 
        public static void GasCall(byte[] account)
        {
            OnNotify(account);

            OnNotify(GAS.Name);

            OnNotify(GAS.Symbol);

            OnNotify(GAS.Decimals);

            OnNotify(GAS.TotalSupply());

            OnNotify(GAS.BalanceOf(account));
        }


        public static void NeoCall(byte[] account)
        {
            OnNotify(NEO.Name);

            OnNotify(NEO.Symbol);

            OnNotify(NEO.Decimals);

            OnNotify(NEO.TotalSupply());

            OnNotify(NEO.BalanceOf(account));

            OnNotify(NEO.UnclaimedGas(account, Blockchain.GetHeight()));

            OnNotify(NEO.GetCandidates());

            //OnNotify(NEO.GetValidators());

            OnNotify(NEO.GetCommittee());

            OnNotify(NEO.GetNextBlockValidators());
        }

        public static void PolicyCall()
        {
            OnNotify(Policy.Name());

            OnNotify(Policy.GetMaxTransactionsPerBlock());

            OnNotify(Policy.GetMaxBlockSize());

            OnNotify(Policy.GetMaxBlockSystemFee());

            OnNotify(Policy.GetFeePerByte());

            OnNotify(Policy.Hash);

            //OnNotify(Policy.GetBlockedAccounts());

        }

        public static void DesignationCall()
        {
            OnNotify(Designation.Name);

            OnNotify(Designation.Hash);

            OnNotify(Designation.GetDesignatedByRole((DesignationRole)8));
        }

        public static void OracleCall()
        {
            OnNotify(Oracle.Name);

            OnNotify(Oracle.Hash);

        }
    }
}
