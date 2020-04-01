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
        private static object RuntimeTest(byte[] txid)
        {
            Runtime.Notify(Runtime.Trigger.Serialize());
            Runtime.Notify(Runtime.Platform);
            Runtime.Notify((long)Runtime.Time);
            Runtime.Notify(Runtime.InvocationCounter);

            var notifications0 = Runtime.GetNotifications(txid);

            var notifications = Runtime.GetNotifications();
            Runtime.Notify((uint)notifications.Length);
            //if (notifications.Length > 0)
            //{
            //    var notification = (object[])notifications[0].State;

            //    byte[] scriptHash = notifications[0].ScriptHash;
            //    bool isTransfer = (string)notification[0] == "Transfer";

            //    if ((byte[])notification[2] == Owner)
            //        Runtime.Notify((BigInteger)notification[3]);
            //}       


            if (Runtime.CheckWitness(addressHash))
            {
                Runtime.Notify(1);
            }
            else
            {
                Runtime.Notify(0);
            }

            Runtime.Log("end!");


            Runtime.Notify(Runtime.GasLeft);

            return Runtime.GasLeft;
        }

        private static object GasLeftTest()
        {
            Runtime.Notify((long)Runtime.Time);
            Runtime.Notify(Runtime.InvocationCounter);

            Runtime.Notify(Runtime.GasLeft);

            return Runtime.GasLeft;
        }
    }
}
