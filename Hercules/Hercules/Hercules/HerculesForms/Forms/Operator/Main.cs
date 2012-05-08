using System;
using System.Windows.Forms;

namespace MME.Hercules.Forms.Operator
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }


        private void Main_Load(object sender, EventArgs e)
        {
            if (ConfigUtility.IsDeveloperMode)
                this.WindowState = FormWindowState.Normal;


            DateTime laststart = Convert.ToDateTime(ConfigUtility.GetValue("LastStartUpDate"));

            TimeSpan span = DateTime.Now - laststart;

            since.Text = laststart.ToString() + " (" + span.Minutes.ToString() + " minutes)";

            timeout.Text = ConfigUtility.GetValue("Timeout") + " seconds";
            sound.Text = (ConfigUtility.GetValue("SoundEnabled").Equals("1") ? "ON" : "OFF");
            record.Text = (ConfigUtility.GetValue("StoreImages").Equals("1") ? "ENABLED" : "DISABLED");
            photoscount.Text = ConfigUtility.GetConfig(ConfigUtility.Config, "PhotoCount");
            backgrounds.Text = ConfigUtility.GetConfig(ConfigUtility.Config, "PhotoBackgrounds");
            useemail.Text = (ConfigUtility.GetConfig(ConfigUtility.Config, "AllowEmailPublish").Equals("1") ? "YES" : "NO");
            requireemail.Text = (ConfigUtility.GetConfig(ConfigUtility.Config, "ForceEmail").Equals("1") ? "YES" : "NO");
            mms.Text = (ConfigUtility.GetConfig(ConfigUtility.Config, "AllowMobilePublish").Equals("1") ? "YES" : "NO");
            facebook.Text = (ConfigUtility.GetConfig(ConfigUtility.Config, "AllowFacebookPublish").Equals("1") ? "YES" : "NO");
            maxprints.Text = ConfigUtility.GetConfig(ConfigUtility.Config, "MaxCopies");
            colorcount.Text = ConfigUtility.GetValue("ColorCount");
            bwcount.Text = ConfigUtility.GetValue("BWCount");

            printer.Text = (ConfigUtility.GetValue("PrinterEnabled").Equals("1") ? "ENABLED" : "DISABLED");
            camera.Text = (ConfigUtility.GetValue("CameraEnabled").Equals("1") ? "ENABLED" : "DISABLED");
            // skin name
            if (string.IsNullOrEmpty(ConfigUtility.GetValue("Skin")))
              skin.Text = "DEFAULT";
            else            
                skin.Text = ConfigUtility.GetValue("skin").ToUpper();

            switch (ConfigUtility.GetValue("BoothType"))
            {
                default:
                    booth.Text = "BOOTH";
                    break;
            }

            switch (ConfigUtility.GetValue("ColorTypes"))
            {
                case "1":
                    colors.Text = "B&&W ONLY";
                    break;
                case "2":
                    colors.Text = "COLOR ONLY";
                    break;
                default:
                    colors.Text = "B&&W, COLOR";
                    break;
            }

            version.Text = "Hercules program version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." +
                    System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString() + "." +
                    System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
        }

        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                case Keys.F10:
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    break;
            }
        }

        private void printer_Click(object sender, EventArgs e)
        {
            SoundUtility.PlaySync(Hercules.Properties.SoundResources.SELECTION_BUTTON);


        }

    }
}
