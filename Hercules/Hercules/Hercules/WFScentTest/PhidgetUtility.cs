using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phidgets;
using Phidgets.Events;
using System.Threading;

namespace MME.Hercules
{
    public static class PhidgetUtility
    {
        private static InterfaceKit ifKit;

        private static InterfaceKit ifKit1;
        private static InterfaceKit ifKit2;

        public static bool InitPhidgetBoard()
        {
            //if (!ConfigUtility.GetValue("UsePhidgetBoard").Equals("1"))
            //    return;
            System.Console.WriteLine("before open");

              ifKit = new InterfaceKit();
              ifKit.open();
              ifKit.waitForAttachment();


              System.Console.WriteLine("after open");

              return true;
        }

        public static bool InitPhidgetBoard1(int sid)
        {
            //if (!ConfigUtility.GetValue("UsePhidgetBoard").Equals("1"))
            //    return;
            System.Console.WriteLine("before open");

            ifKit1 = new InterfaceKit();
            ifKit1.open(sid);
            ifKit1.waitForAttachment();

            System.Console.WriteLine("after open");

            return true;
        }

        public static bool InitPhidgetBoard2(int sid)
        {
            //if (!ConfigUtility.GetValue("UsePhidgetBoard").Equals("1"))
            //    return;
            System.Console.WriteLine("before open");

            ifKit2 = new InterfaceKit();
            ifKit2.open(sid);
            ifKit2.waitForAttachment();

            System.Console.WriteLine("after open");

            return true;
        }

        public static void Shutdown()
        {
            //if (!ConfigUtility.GetValue("UsePhidgetBoard").Equals("1"))
            //    return;

            ifKit.close();
            ifKit = null;
        }

        public static void Relay(int index, bool enabled)
        {
            //if (!ConfigUtility.GetValue("UsePhidgetBoard").Equals("1"))
            //    return;

            System.Console.WriteLine("before relay" + index.ToString() + " " + enabled.ToString());

            if (index >= 0 && index <= ifKit.outputs.Count -1)
                ifKit.outputs[index] = enabled;
        }

        public static void RelayN(int which, int index, bool enabled)
        {
            //if (!ConfigUtility.GetValue("UsePhidgetBoard").Equals("1"))
            //    return;

            System.Console.WriteLine("before relay" + index.ToString() + " " + enabled.ToString());

            if (which==0)
            {
                if (index >= 0 && index <= ifKit1.outputs.Count - 1)
                    ifKit1.outputs[index] = enabled;
            }
            else
            {
                if (index >= 0 && index <= ifKit2.outputs.Count - 1)
                    ifKit2.outputs[index] = enabled;
            }

        }
    }
}
