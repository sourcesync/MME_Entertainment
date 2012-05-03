using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
 

namespace CardAccess.Mapping.Module
{
    

    public partial class LoginView : System.Windows.Window
    {
        


        public LoginView()
        {
            InitializeComponent();
            btnLogin.Click += new RoutedEventHandler(btnLogin_Click);
            btnCancel.Click += new RoutedEventHandler(btnCancel_Click);


        }

        void btnCancel_Click(object sender, RoutedEventArgs e)
        {
           

            this.Close();
 
        }

        void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text == "A" && txtPassword.Password == "A")
            {
                this.Close();
                Application.Current.MainWindow.Show();
            }
            else
            {
                MessageBox.Show("Wrong UserName/PassWord");
            }
           
        }

        
    }
}