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
        public static object RuntimeTest()
        {
            //var notifications = Runtime.GetNotifications();
            //Runtime.Notify(notifications);
            //Runtime.Notify((uint)notifications.Length);

            try
            {
                Runtime.Notify(0);                           
                try
                {
                    try
                    {
                        throw new Exception();
                    }
                    catch
                    {
                        Runtime.Notify("error ");
                    }
                }
                catch
                {
                    Runtime.Notify(1);
                }

                finally
                {
                    Runtime.Notify(2);
                }
            }
            catch
            {
                Runtime.Notify(3);                
            }
            finally
            {
                Runtime.Notify(4);
            }

            return Runtime.GasLeft;
        }

        public static object GasLeftTest()
        {
            Runtime.Notify((long)Runtime.Time);
            Runtime.Notify(Runtime.InvocationCounter);

            Runtime.Notify(Runtime.GasLeft);

            return Runtime.GasLeft;
        }
    }
}
