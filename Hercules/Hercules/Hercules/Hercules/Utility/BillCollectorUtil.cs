using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMEBillCollector;

namespace MME.Hercules.Utility
{
    public class BillCollector
    {
        public static MMEBillCollector.MMEBillCollector bc = null;

        public static bool Initialize( System.Windows.Forms.Control sync, System.EventHandler cb)
        {
            String port = "COM3";

            String val = ConfigUtility.GetValue("BillCollectorPort");
            if ((val != null) && (val != ""))
                port = val;

            bc = new MMEBillCollector.MMEBillCollector(port);
            if (!bc.init(sync, cb))
            {
                System.Windows.Forms.MessageBox.Show("ERROR: Cannot initialize bill collector.");
                bc = null;
                return false;
            }

            return true;
        }

        public static bool Finish()
        {
            if (bc != null) bc.finish();
            return true;
        }
    }
}
