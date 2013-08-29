using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;

namespace MMEBillCollector
{
    public class MMEBillCollector
    {
        public static SerialPort _serialPort = null;
        public String port = null;
        public String initstr = null;
        public bool _continue = false;
        public const int BUFMAX = 1000;
        byte[] buf = new byte[BUFMAX];
        int bufptr = 0;
        Thread readThread = null;
        public System.Windows.Forms.Control sync = null;
        public System.EventHandler cb = null;

        public  MMEBillCollector(String port, String init)
        {  
            this.port = port;
            this.initstr = init;
        }

        public Boolean init(System.Windows.Forms.Control sync, System.EventHandler cb)
        {
            this.sync = sync;
            this.cb = cb;

            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort();

            // Allow the user to set the appropriate properties.
            _serialPort.PortName = this.port; //"COM3";

            //_serialPort.PortName = SetPortName(_serialPort.PortName);
            _serialPort.BaudRate = 115200; // SetPortBaudRate(_serialPort.BaudRate);
            _serialPort.Parity = Parity.None; // SetPortParity(_serialPort.Parity);
            _serialPort.DataBits = 8; //_serialPort.DataBits = SetPortDataBits(_serialPort.DataBits);
            _serialPort.StopBits = StopBits.One; //_serialPort.StopBits = SetPortStopBits(_serialPort.StopBits);
            _serialPort.Handshake = Handshake.None; //_serialPort.Handshake = SetPortHandshake(_serialPort.Handshake);

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;

            _serialPort.Open();

            _continue = true;

            /*
            if (!send_clear_command())
            {
                return false;
            }
             * */

            if (this.initstr != null)
            {
                this.send_command(this.initstr);
            }

            if (!send_status_command())
            {
                return false;
            }


            readThread = new Thread(Read);
            readThread.Start();

            return true;
        }

        public bool finish()
        {
            _continue = false;

            readThread.Join();

            _serialPort.Close();

            return true;
        }

        public void Read()
        {
            while (this._continue)
            {
                try
                {
                    int rd = _serialPort.Read(buf, bufptr, 1);
                    if (rd > 0)
                    {
                        if (buf[bufptr] == 13)
                        {
                            buf[bufptr] = 0;
                            String str = Encoding.ASCII.GetString(buf, 0, bufptr);
                            Console.WriteLine("{0}", str);

                            if (this.sync != null && this.cb != null)
                            {
                                this.sync.Invoke(this.cb, new object[] { str });
                            }

                            bufptr = 0;
                            continue;
                        }


                        bufptr++;
                        if (bufptr == (BUFMAX - 1))
                        {
                            System.Windows.Forms.MessageBox.Show("ERROR: Exceeded Read Buffer");
                            System.Threading.Thread.Sleep(1000);
                            bufptr = 0;
                            continue;
                        }

                        
                    }
                    else if (rd == 0)
                    {
                        System.Threading.Thread.Sleep(10);
                        continue;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("ERROR: Read Device Failed.");
                        break;
                    }
                }
                catch (TimeoutException)
                {
                    continue;
                }
            }
        }

        public bool send_command(String str)
        {
            char[] chrs = str.ToCharArray();

            bool use_raw_num = false;
            //if (str.StartsWith("M")) use_raw_num = true;
            //_serialPort.Write(chrs, 0, chrs.Length);

            
            for ( int i=0;i<chrs.Length;i++)
            {
                char c = chrs[i];
                byte[] b = new byte[] { (byte)c };
                if ((use_raw_num) && (char.IsDigit(c)))
                    b = new byte[] { byte.Parse(c.ToString()) };

                _serialPort.Write(b, 0, 1);

            }

            byte[] cr = new byte[] { 13 };
            _serialPort.Write(cr,0,1);

            return true;

        }

        public bool send_clear_command()
        {
            try
            {
                char[] c = new char[1];
                byte[] bt = new byte[1];
                bt[0] = 52;
                _serialPort.Write(bt, 0, 1);
                bt[0] = 13;
                _serialPort.Write(bt, 0, 1);
                return true;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool send_status_command()
        {
            try
            {
                char[] c = new char[1];
                byte[] bt = new byte[1];
                bt[0] = 63;
                _serialPort.Write(bt, 0, 1);
                bt[0] = 13;
                _serialPort.Write(bt, 0, 1);
                return true;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
    }
}
