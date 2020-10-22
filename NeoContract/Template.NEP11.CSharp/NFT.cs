//using Neo.SmartContract.Framework;
//using Neo.SmartContract.Framework.Services.Neo;
//using System;
//using System.ComponentModel;
//using System.Numerics;

//namespace Template.NEP11.CSharp
//{
//    [ManifestExtra("Author", "Neo")]
//    [ManifestExtra("Email", "dev@neo.org")]
//    [ManifestExtra("Description", "This is a NEP11 example")]
//    [Features(ContractFeatures.HasStorage | ContractFeatures.Payable)]
//    public partial class NEP11 : SmartContract
//    {
//        static readonly string Name = "Neo Non-Fungible Token Name";
//        static readonly string Symbol = "NFTSymbol";       
//        static readonly string[] SupportedStandards = new string[] { "NEP-11", "NEP-10" };
//        static readonly byte[] Admin = new byte[] { 0xfa, 0x79, 0x76, 0x3b, 0x86, 0x76, 0x7b, 0x42, 0x68, 0x72, 0x34, 0x9f, 0xd2, 0xfd, 0xbc, 0xcf, 0x16, 0x2e, 0xe2, 0x20 };
               
//        [DisplayName("Transfer")]
//        public static event Action<byte[], byte[], byte[]> OnTransfer;       //from to token_id                

//        public static object Main(string operation, object[] args)
//        {
//            if (Runtime.Trigger == TriggerType.Verification)
//            {
//                return Runtime.CheckWitness(Admin);
//            }

//            else if (Runtime.Trigger == TriggerType.Application)
//            {
//                if (operation == "name") return Name;
//                if (operation == "symbol") return Symbol;              
//                if (operation == "totalSupply") return TotalSupply();
//                if (operation == "supportedStandards") return SupportedStandards;
//                if (operation == "balanceOf") return BalanceOf((byte[])args[0]);
//                if (operation == "ownerOf") return Storage.Get(Context(), TokenOwnerKey((byte[])args[0]));                
//                if (operation == "properties") return Storage.Get(Context(), PropertiesKey((byte[])args[0]));

//                if (operation == "transfer") return Transfer((byte[])args[0], (byte[])args[1], (byte[])args[2]);

//                if (operation == "mint") return Mint((byte[])args[0], (byte[])args[1], (byte[])args[2]);                     
//                if (operation == "migrate") return Migrate((byte[])args[0], (string)args[1]);
//                if (operation == "destroy") return Destroy();
//            }
//            return false;
//        }

//        private static object TotalSupply() => Storage.Get(Context(), TotalSupplyKey);

//        private static object BalanceOf(byte[] owner) => Storage.Get(Context(), BalanceKey(owner));


//        private static object Mint(byte[] tokenId, byte[] owner, byte[] properties)
//        {            
//            if (!Runtime.CheckWitness(Admin)) return false;

//            if (owner.Length != 20) return false;
//            //if (properties.Length > 2048) return false;
           
//            var addr = Storage.Get(Context(), TokenOwnerKey(tokenId));
//            if (addr != null) return false;

//            Storage.Put(Context(), PropertiesKey(tokenId), properties);
//            Storage.Put(Context(), TokenOwnerKey(tokenId), owner);
            
//            BigInteger totalSupply = Storage.Get(Context(), TotalSupplyKey).ToBigInteger();
//            Storage.Put(Context(), TotalSupplyKey, totalSupply + 1);

//            var keyBalance = BalanceKey(owner);
//            BigInteger balance = Storage.Get(Context(), keyBalance).ToBigInteger();
//            Storage.Put(Context(), keyBalance, balance + 1);

//            //notify
//            OnTransfer(null, owner, tokenId);
//            return true;
//        }

//        private static object Transfer(byte[] from, byte[] to, byte[] tokenId)
//        {          
//            if (from.Length != 20 || to.Length != 20) return false;

//            if (!Runtime.CheckWitness(from)) return false;

//            if (from == to) return true;

//            var owner = Storage.Get(Context(), TokenOwnerKey(tokenId));
//            if (owner != from) return false;

//            Storage.Put(Context(), TokenOwnerKey(tokenId), to);

//            var formBalance = Storage.Get(Context(), BalanceKey(from)).ToBigInteger();
//            Storage.Put(Context(), BalanceKey(from), formBalance - 1);
//            var toBalance = Storage.Get(Context(), BalanceKey(to)).ToBigInteger();
//            Storage.Put(Context(), BalanceKey(to), toBalance + 1);

//            //notify
//            OnTransfer(from, to, tokenId);
//            return true;
//        }

//        public static bool Migrate(byte[] script, string manifest)
//        {
//            if (!Runtime.CheckWitness(Admin))
//            {
//                return false;
//            }
//            if (script.Length == 0 || manifest.Length == 0)
//            {
//                return false;
//            }
//            Contract.Update(script, manifest);
//            return true;
//        }

//        public static bool Destroy()
//        {
//            if (!Runtime.CheckWitness(Admin))
//            {
//                return false;
//            }

//            Contract.Destroy();
//            return true;
//        }

//        private static StorageContext Context() => Storage.CurrentContext;
//        private static byte[] TokenOwnerKey(byte[] tokenId) => new byte[] { 0x10 }.Concat(tokenId);
//        private static byte[] BalanceKey(byte[] owner) => new byte[] { 0x11 }.Concat(owner);  
//        private static byte[] PropertiesKey(byte[] tokenId) => new byte[] { 0x12 }.Concat(tokenId);
//        static string TotalSupplyKey = "totalSupply";
//    }
//}
