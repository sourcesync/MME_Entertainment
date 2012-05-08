using System;
using System.Windows.Forms;

namespace MME.Hercules
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
          

            fullKeyboard1.CurrentTextBox = textBox1;
        }

        private void textbox_Enter(object sender, EventArgs e)
        {
            fullKeyboard1.CurrentTextBox = (TextBox)sender;
        }
    }
}
