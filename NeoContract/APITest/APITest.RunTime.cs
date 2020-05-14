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

                    throw new Exception();
                }
                catch
                {
                    Runtime.Notify(1);

                    try
                    {
                        throw new Exception();
                    }
                    catch
                    {
                        Runtime.Notify(2);
                    }

                    finally
                    {
                        Runtime.Notify(3);
                    }
                }

                finally
                {
                    Runtime.Notify(4);
                }
            }
            catch
            {
                Runtime.Notify(5);

                try
                {
                    Runtime.Notify(6);
                    throw new Exception();
                }
                catch
                {
                    Runtime.Notify(7);
                }

                finally
                {
                    Runtime.Notify(8);
                }
            }
            finally
            {
                Runtime.Notify(9);
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
