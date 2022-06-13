using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HospitalManagement;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NorthernLightsHospital
{
    /// <summary>
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        // username and password hardcode for access to specific windows
        const string userMedecin = "doctor";
        const string passMedecin = "doctor";
        const string userAdmin = "admin";
        const string passAdmin = "admin";
        const string userPrepose = "employee";
        const string passPrepose = "employee";

        public WindowLogin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_username.Focus();
        }


        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            // fill temporary variables with login information
            string user = txt_username.Text;
            string pass = txt_password.Password;

            // if all the fields are filled, redirect to the window 
            if (String.IsNullOrEmpty(user) || String.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Please fill in all fields", "Warning!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (user.Equals(userMedecin) && pass.Equals(passMedecin))
                {
                    WindowMedecin windowMedecin = new WindowMedecin();
                    this.Close();
                    windowMedecin.Show();
                }
                else if (user.Equals(userAdmin) && pass.Equals(passAdmin))
                {
                    WindowAdmin windowAdmin = new WindowAdmin();
                    this.Close();
                    windowAdmin.Show();
                }
                else if (user.Equals(userPrepose) && pass.Equals(passPrepose))
                {
                    WindowPrepose windowPrepose = new WindowPrepose();
                    this.Close();
                    windowPrepose.Show();
                }
                else
                {
                    MessageBox.Show("The information entered is invalid!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Error);
                    txt_username.Clear();
                    txt_password.Clear();
                    txt_username.Focus();
                }
            }
        }
    }
}
