using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
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
            var notifications = Runtime.GetNotifications();

            OnNotify(notifications);
            OnNotify((uint)notifications.Length);


            var notifications1 = Runtime.GetNotifications(ExecutionEngine.ExecutingScriptHash);
            OnNotify(notifications1);
            OnNotify((uint)notifications1.Length);

            var notifications2 = Runtime.GetNotifications((UInt160)contractHash);
            OnNotify(notifications2);
            OnNotify((uint)notifications2.Length);

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
                        OnNotify("error ");
                    }

                    throw new Exception();
                }
                catch
                {
                    OnNotify(1);

                    try
                    {
                        throw new Exception();
                    }
                    catch
                    {
                        OnNotify(2);
                    }

                    finally
                    {
                        OnNotify(3);
                    }
                }

                finally
                {
                    OnNotify(4);
                }
            }
            catch
            {
                OnNotify(5);

                try
                {
                    OnNotify(6);
                    throw new Exception();
                }
                catch
                {
                    OnNotify(7);
                }

                finally
                {
                    OnNotify(8);
                }
            }
            finally
            {
                OnNotify(9);
            }

            return Runtime.GasLeft;
        }

        public static object GasLeftTest()
        {
            OnNotify((long)Runtime.Time);
            OnNotify(Runtime.InvocationCounter);

            OnNotify(Runtime.GasLeft);

            return Runtime.GasLeft;
        }
    }
}
