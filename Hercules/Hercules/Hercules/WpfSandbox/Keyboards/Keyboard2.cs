using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MME.Hercules.Keyboards
{
    public partial class Keyboard2 : UserControl
    {

        public bool StartedTyping = false;
        public bool IsEnabled = true;

        
        public KeyboardLayout CurrentLayout;
        private bool CapsLock = false;
        private bool Shift = false;


        public Keyboard2()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.Selectable, false);
        }


        private void SwitchLayout()
        {

            shiftleft.ForeColor = shiftright.ForeColor = Color.Black;
            shiftleft.BackColor = shiftright.BackColor = Color.Gainsboro;
            
            numberleft.ForeColor = Color.Black;
            numberleft.BackColor = Color.Gainsboro;


            switch (CurrentLayout)
            {
                case KeyboardLayout.Other:
                    bq.Text = "[";
                    bw.Text = "]";
                    be.Text = "{";
                    br.Text = "}";
                    bt.Text = "#";
                    by.Text = "%";
                    bu.Text = "^";
                    bi.Text = "*";
                    bo.Text = "+";
                    bp.Text = "=";
                    bdel.Text = "Delete";
                    ba.Text = "_";
                    bs.Text = "\\";
                    bd.Text = "|";
                    bf.Text = "~";
                    bg.Text = "<";
                    bh.Text = ">";
                    bj.Text = " ";
                    bk.Text = " ";
                    bl.Text = " ";
                    shiftleft.Text = shiftright.Text = "123";
                    bz.Text = ".";
                    bx.Text = ",";
                    bc.Text = "?";
                    bv.Text = "!";
                    bb.Text = "'";
                    bn.Text = "\"";
                    bm.Text = "";
                    bcomma.Text = "";
                    bperiod.Text = "";
                    numberleft.Text = numberright.Text = "ABC";
                    break;
                case KeyboardLayout.Numeric:
                    bq.Text = "1";
                    bw.Text = "2";
                    be.Text = "3";
                    br.Text = "4";
                    bt.Text = "5";
                    by.Text = "6";
                    bu.Text = "7";
                    bi.Text = "8";
                    bo.Text = "9";
                    bp.Text = "0";
                    bdel.Text = "Delete";
                    ba.Text = "-";
                    bs.Text = "/";
                    bd.Text = ":";
                    bf.Text = ";";
                    bg.Text = "(";
                    bh.Text = ")";
                    bj.Text = "$";
                    bk.Text = "&&";
                    bl.Text = "@";
                    
                    shiftleft.Text = shiftright.Text = "#+=";
                    bz.Text = ".";
                    bx.Text = ",";
                    bc.Text = "?";
                    bv.Text = "!";
                    bb.Text = "'";
                    bn.Text = "\"";
                    bm.Text = "";
                    bcomma.Text = "";
                    bperiod.Text = "";
                    numberleft.Text = numberright.Text = "ABC";
                    break;
                default:
                    bq.Text = "Q";
                    bw.Text = "W";
                    be.Text = "E";
                    br.Text = "R";
                    bt.Text = "T";
                    by.Text = "Y";
                    bu.Text = "U";
                    bi.Text = "I";
                    bo.Text = "O";
                    bp.Text = "P";
                    bdel.Text = "Delete";
                    ba.Text = "A";
                    bs.Text = "S";
                    bd.Text = "D";
                    bf.Text = "F";
                    bg.Text = "G";
                    bh.Text = "H";
                    bj.Text = "J";
                    bk.Text = "K";
                    bl.Text = "L";

                    shiftleft.Text = shiftright.Text = "Shift";
                    bz.Text = "Z";
                    bx.Text = "X";
                    bc.Text = "C";
                    bv.Text = "V";
                    bb.Text = "B";
                    bn.Text = "N";
                    bm.Text = "M";
                    bcomma.Text = "@";
                    bperiod.Text = ".";
                    
                    numberleft.Text = "Caps Lock";
                    numberright.Text = ".?123";

                    break;
            }

            if (CurrentLayout == KeyboardLayout.ABC)
            {
                if (Shift)
                {
                    shiftleft.ForeColor = shiftright.ForeColor = Color.White;
                    shiftleft.BackColor = shiftright.BackColor = Color.Blue;
                }

                if (CapsLock)
                {
                    numberleft.ForeColor = Color.White;
                    numberleft.BackColor = Color.Blue;
                }
            }
        }

        public void SetKeyboard(KeyboardLayout layout)
        {
            CurrentLayout = layout;
            SwitchLayout();
        }

        public void SetKeyboard(bool enabled)
        {
            IsEnabled = enabled;         
        }



        private void key_Click(object sender, EventArgs e)
        {
            // don't do anything if disabled
            if (!IsEnabled) return;

            Button button = (Button)sender;

            StartedTyping = true;

            if (button.Text.ToUpper() == "SHIFT")
            {
                Shift = !Shift;

                shiftleft.ForeColor = shiftright.ForeColor = (Shift) ? Color.White : Color.Black;
                shiftleft.BackColor = shiftright.BackColor = (Shift) ? Color.Blue : Color.Gainsboro;
                
                return;
            }

            switch (button.Text.ToUpper())
            {
                case "CLEAR":
                    CurrentTextBox.Text = "";
                    break;
                case "CAPS LOCK":
                    CapsLock = !CapsLock;
                    numberleft.ForeColor = (CapsLock) ? Color.White : Color.Black;
                    numberleft.BackColor = (CapsLock) ? Color.Blue : Color.Gainsboro;
                    break;
                case ".?123":
                case "123":
                    CurrentLayout = KeyboardLayout.Numeric;
                    SwitchLayout();
                    break;
                case "ABC":
                     CurrentLayout = KeyboardLayout.ABC;
                     SwitchLayout();
                    break;
                case "#+=":
                    CurrentLayout = KeyboardLayout.Other;
                    SwitchLayout();
                    break;
                case "&&":
                    CurrentTextBox.Text += "&";
                    break;
                case "DELETE":
                    if (CurrentTextBox.Text.Length > 0)
                        CurrentTextBox.Text = CurrentTextBox.Text.Substring(0, CurrentTextBox.Text.Length - 1);
                    break;
                default:
                    if (!string.IsNullOrEmpty(button.Text) && CurrentTextBox != null)
                        CurrentTextBox.Text += (Shift || CapsLock) ? button.Text.ToUpper() : button.Text.ToLower();
                    break;
            }

            Shift = false;
            shiftleft.ForeColor = shiftright.ForeColor = Color.Black;
            shiftleft.BackColor = shiftright.BackColor = Color.Gainsboro;
        }        

        private void key_MouseDown(object sender, MouseEventArgs e)
        {            
            //gw SoundUtility.StopSpeaking();
            //gw SoundUtility.Play(Hercules.Properties.SoundResources.KEY_CLICK);
        }



        public TextBox CurrentTextBox
        { get; set; }

        private void Keyboard2_Load(object sender, EventArgs e)
        {

        }
    }

}
