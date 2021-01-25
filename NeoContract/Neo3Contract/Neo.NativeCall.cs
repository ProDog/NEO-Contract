using Neo;
using Neo.Cryptography.ECC;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Neo3Contract
{
    public partial class Neo3Contract : SmartContract
    {
        //[{"type":"Hash160","value":"0xf621168b1fce3a89c33a5f6bcf7e774b4657031c"}] 
        public static void GasCall(UInt160 account)
        {
            OnNotify(account);

            OnNotify(GAS.Hash);

            OnNotify(GAS.Symbol);

            OnNotify(GAS.Decimals);

            OnNotify(GAS.TotalSupply());

            OnNotify(GAS.BalanceOf(account));

            OnNotify(GAS.Transfer((UInt160)account, Owner, 123456789, null));

            OnNotify(Owner);
        }

        //[{"type":"Hash160","value":"0xf621168b1fce3a89c33a5f6bcf7e774b4657031c"},{"type":"PublicKey","value":"0222d8515184c7d62ffa99b829aeb4938c4704ecb0dd7e340e842e9df121826343"}]
        public static void NeoCall(UInt160 account, ECPoint publicKey)
        {
            OnNotify(NEO.Hash);

            OnNotify(NEO.Symbol);

            OnNotify(NEO.Decimals);

            OnNotify(NEO.TotalSupply());

            OnNotify(NEO.BalanceOf(account));

            OnNotify(NEO.UnclaimedGas(account, Blockchain.GetHeight()));

            OnNotify(NEO.GetCandidates());

            OnNotify(NEO.GetCommittee());

            OnNotify(NEO.GetNextBlockValidators());

            OnNotify(NEO.RegisterCandidate(publicKey));

            OnNotify(NEO.UnRegisterCandidate(publicKey));

            OnNotify(NEO.Vote(account, publicKey));

            OnNotify(NEO.Transfer((UInt160)account, Owner, 11, null));

            OnNotify(NEO.GetGasPerBlock());

            OnNotify(NEO.SetGasPerBlock(300000000));

            OnNotify(NEO.GetGasPerBlock());
        }

        public static void PolicyCall()
        {
            OnNotify(Policy.Hash);

            OnNotify(Policy.GetMaxTransactionsPerBlock());

            OnNotify(Policy.GetMaxBlockSize());

            OnNotify(Policy.GetMaxBlockSystemFee());

            OnNotify(Policy.GetFeePerByte());

            OnNotify(Policy.Hash);

            OnNotify(Policy.IsBlocked(Owner));

            OnNotify(Policy.BlockAccount((UInt160)Owner));

            OnNotify(Policy.UnblockAccount((UInt160)Owner));
        }

        public static void Update(byte[] nefFile, string manifest)
        {
            if (!Verify()) throw new Exception("No authorization.");
            ContractManagement.Update(nefFile.ToByteString(), manifest);
        }

        public static void Destroy()
        {
            if (!Verify()) throw new Exception("No authorization.");
            ContractManagement.Destroy();
        }

        public static object ManagementTest(UInt160 hash)
        {
            OnNotify(ContractManagement.Hash);
            OnNotify(ContractManagement.Name);
            OnNotify(ContractManagement.GetContract(hash).Hash);

            OnNotify(ContractManagement.GetContract(hash).Id);

            OnNotify(ContractManagement.GetContract(hash).Nef);

            OnNotify(ContractManagement.GetContract(hash).UpdateCounter);

            return ContractManagement.GetContract(hash).Manifest;
        }
    }
}
