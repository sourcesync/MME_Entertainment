using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phidgets;
using Phidgets.Events;
using System.Threading;

namespace MME.Hercules
{
    public static class PhidgetUtility2
    {
        //private static InterfaceKit ifKit;

        private static InterfaceKit ifKit1;
        private static InterfaceKit ifKit2;

        private static bool open1 = false;
        private static bool open2 = false;

        /*
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
         * */

        public static bool InitAll()
        {
            if (!ConfigUtility.GetValue("UsePhidgetBoardScentomatic").Equals("1"))
                return false;

            /* CHI, first one */
            InitPhidgetBoard1(259243);
            InitPhidgetBoard2(259313);
           

            /* FLA, second one...*/
            /*
            InitPhidgetBoard1(259354);
            InitPhidgetBoard2(259314);
            */

            /* LA, third one */
            /*
            InitPhidgetBoard1(259261);
            InitPhidgetBoard2(259329);
            */
             
            return true;
        }

        public static bool InitPhidgetBoard1(int sid)
        {
            if (!ConfigUtility.GetValue("UsePhidgetBoardScentomatic").Equals("1"))
                return false;
            System.Console.WriteLine("before open");

            ifKit1 = new InterfaceKit();
            ifKit1.open(sid);
            ifKit1.waitForAttachment(5000);
            if (ifKit1.Attached) open1 = true;
            else System.Windows.Forms.MessageBox.Show("Could not open first phidget");

            System.Console.WriteLine("after open");

            return true;
        }

        public static bool InitPhidgetBoard2(int sid)
        {
            if (!ConfigUtility.GetValue("UsePhidgetBoardScentomatic").Equals("1"))
                return false;
            System.Console.WriteLine("before open");

            ifKit2 = new InterfaceKit();
            ifKit2.open(sid);
            ifKit2.waitForAttachment(5000);
            if (ifKit2.Attached) open2 = true;
            else System.Windows.Forms.MessageBox.Show("Could not open second phidget");

            System.Console.WriteLine("after open");

            return true;
        }

        public static void Shutdown()
        {
            if (!ConfigUtility.GetValue("UsePhidgetBoardScentomatic").Equals("1"))
                return;

            //ifKit.close();
            //ifKit = null;

            ifKit1.close();
            ifKit2.close();
        }

        /*
        public static void Relay(int index, bool enabled)
        {
            //if (!ConfigUtility.GetValue("UsePhidgetBoard").Equals("1"))
            //    return;

            System.Console.WriteLine("before relay" + index.ToString() + " " + enabled.ToString());

            if (index >= 0 && index <= ifKit.outputs.Count -1)
                ifKit.outputs[index] = enabled;
        }
         * */

        public static void RelayN(int which, int index, bool enabled)
        {
            if (!ConfigUtility.GetValue("UsePhidgetBoardScentomatic").Equals("1"))
                return;

            System.Console.WriteLine("before relay" + index.ToString() + " " + enabled.ToString());

            if (which==0)
            {
                if (!open1) return;

                if (index >= 0 && index <= ifKit1.outputs.Count - 1)
                    ifKit1.outputs[index] = enabled;
            }
            else if (which==1)
            {
                if (!open2) return;

                if (index >= 0 && index <= ifKit2.outputs.Count - 1)
                    ifKit2.outputs[index] = enabled;
            }
        }

        public static void Trigger(int which)
        {
            if (which < 2)
            {
                RelayN(which, 0, true);
                System.Threading.Thread.Sleep(500);
                RelayN(which, 0, false);

                RelayN(which, 1, true);
                System.Threading.Thread.Sleep(500);
                RelayN(which, 1, false);
            }
            else
            {
                RelayN(1, 2, true);
                System.Threading.Thread.Sleep(500);
                RelayN(1, 2, false);

                RelayN(1, 3, true);
                System.Threading.Thread.Sleep(500);
                RelayN(1, 3, false);
            }
        }
    }
}
