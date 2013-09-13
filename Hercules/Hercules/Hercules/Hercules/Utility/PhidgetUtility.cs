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

        public static void InitPhidgetBoard()
        {
            if (!ConfigUtility.GetValue("UsePhidgetBoard").Equals("1"))
                return;

            if (ConfigUtility.GetValue("PhidgetRelay_VanityLight").Equals("0"))
                return;

            int sid = int.Parse(ConfigUtility.GetValue(ConfigUtility.GetValue("PhidgetRelay_VanityLight")));

              ifKit = new InterfaceKit();
              ifKit.open(sid);
              ifKit.waitForAttachment();
        }

        public static void Shutdown()
        {
            if (!ConfigUtility.GetValue("UsePhidgetBoard").Equals("1"))
                return;
            if (ConfigUtility.GetValue("PhidgetRelay_VanityLight").Equals("0"))
                return;

            ifKit.close();
            ifKit = null;
        }

        public static void Relay(int index, bool enabled)
        {
            if (!ConfigUtility.GetValue("UsePhidgetBoard").Equals("1"))
                return;
            if (ConfigUtility.GetValue("PhidgetRelay_VanityLight").Equals("0"))
                return;

            if (index >= 0 && index <= ifKit.outputs.Count -1)
                ifKit.outputs[index] = enabled;
        }
    }
}
