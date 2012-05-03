using System;
using System.Windows.Forms;
using System.Threading;

namespace MME.Hercules.Keyboards
{
    public partial class FullKeyboard : UserControl
    {
        public FullKeyboard()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.Selectable, false);

         
        }


        private void key_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            switch (button.Text.ToUpper())
            {
                case "&&":
                    CurrentTextBox.Text += "&";
                    break;
                case "SPACE":
                    CurrentTextBox.Text += " ";
                    break;
                case "DELETE":
                    if (CurrentTextBox.Text.Length > 0)
                        CurrentTextBox.Text = CurrentTextBox.Text.Substring(0, CurrentTextBox.Text.Length - 1);
                    break;
                default:
                    CurrentTextBox.Text += button.Text.ToLower();
                    break;
            }        
        }

        private void key_MouseUp(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = System.Drawing.Color.LightSlateGray;
        }


        private void key_MouseDown(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = System.Drawing.Color.Red;

            //gw SoundUtility.Play(Hercules.Properties.SoundResources.KEY_CLICK);
        }


        public TextBox CurrentTextBox
        { get; set; }


    }
}
