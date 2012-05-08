using System;
using System.Windows.Forms;


namespace MME.Hercules
{
    public partial class ConsoleForm : Form
    {
        const string wiaFormatJPEG = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";
        private int cnt = 0;

        public ConsoleForm()
        {
            InitializeComponent();
        }

        private void ConsoleForm_Load(object sender, EventArgs e)
        {
            // Perform application initialization

            // Enter user mode
            Forms.User.Start sf = new Forms.User.Start();
            DialogResult dr = sf.ShowDialog();
        }
        
       

        private void button3_Click(object sender, EventArgs e)
        {
            SoundUtility.Play(Hercules.Properties.SoundResources.PHOTOS_DEVELOPED_QUICK);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
       
    }
}
