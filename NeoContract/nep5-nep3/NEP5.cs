//using Neo.SmartContract.Framework;
//using Neo.SmartContract.Framework.Services.Neo;
//using System.Threading.Tasks;

//namespace Helloworld
//{
//    [ManifestExtra("Author", "Neo")]
//    [ManifestExtra("Email", "dev@neo.org")]
//    [ManifestExtra("Description", "This is a contract example")]
//    [SupportedStandards("NEP-5", "NEP-10")]
//    [Features(ContractFeatures.HasStorage)]
//    public class Contract1 : SmartContract
//    {
//        public static int Hello()
//        {
//            //A a = new A();
//            /*B b = a.b;
//            C c = b.c;*/
//            //a.b.c.value = 1;
//            D d = new D();
//            return d.a.b.c.value;
//        }
//    }

//    /*class A
//    {
//        public B b = new B();
//    }
//    class B
//    {
//        public C c = new C();
//    }
//    class C
//    {
//        public int value;
//    }*/

//    struct A
//    {
//        public B b;
//    }
//    struct B
//    {
//        public C c;
//    }
//    struct C
//    {
//        public int value;
//    }

//    class D
//    {
//        public A a = new A();
//    }
//}