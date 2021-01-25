using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;

namespace Neo3Contract
{
    [DisplayName("Contract Name")]
    [ManifestExtra("Author", "Neo")]
    [ManifestExtra("Email", "dev@neo.org")]
    [ManifestExtra("Description", "This is a Neo3 contract example")]
    [SupportedStandards("NEP", "null")]
    public partial class Neo3Contract : SmartContract
    {
        private static UInt160 Owner = "NNU67Fvdy3LEQTM374EJ9iMbCRxVExgM8Y".ToScriptHash();

        public static bool Verify()
        {
            return Runtime.CheckWitness(Owner);
        }

        public delegate void Notify(params object[] arg);

        [DisplayName("event_name")]
        public static event Notify OnNotify;

        public static void _deploy(bool update)
        {
            if (!update)
            {
                Storage.Put(Storage.CurrentContext, "deploy", 11);
                OnNotify("deploy", 1);
            }

            else
            {
                Storage.Put(Storage.CurrentContext, "update", 11);
                OnNotify("update", 1);
            }
        }

        public static object getvalue()
        {
            return Storage.Get(Storage.CurrentContext, "test");
        }
    }
}
