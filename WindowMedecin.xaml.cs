using System;
using HospitalManagement;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for WindowMedecin.xaml
    /// </summary>
    public partial class WindowMedecin : Window
    {
        // DB object
        HospitalDatabaseEntities database;

        public WindowMedecin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // assign db
            database = new HospitalDatabaseEntities();

            // query to display patients who are still in the hospital
            var admissionsValides = (from admissions in database.Admissions
                                     where admissions.dateLeave == DateTime.MinValue
                                     select admissions);
            list_admissions.DataContext = admissionsValides.ToList();
        }

        private void btn_conge_Click(object sender, RoutedEventArgs e)
        {
            // the selected patient will be discharged and their bed will be available again
            if (list_admissions.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please choose an admission", "Warning!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Admission admission = list_admissions.SelectedItem as Admission;
                Bed bed = (from l in database.Beds
                           where l.numeroLit == admission.numeroLit
                           select l).FirstOrDefault() as Bed;

                admission.dateLeave = DateTime.Today;
                bed.occupe = false;

                try
                {
                    database.SaveChanges();
                    MessageBox.Show("Leave successfully given", "Leave", MessageBoxButton.OK, MessageBoxImage.Information);

                    list_admissions.SelectedIndex = -1;

                    list_admissions.DataContext = (from admissions in database.Admissions
                                                   where admissions.dateLeave == DateTime.MinValue
                                                   select admissions).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            // return to the login page
            WindowLogin windowLogin = new WindowLogin();
            this.Close();
            windowLogin.Show();
        }

        private void list_admissions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // to clear the field when the index returns to -1 after a holiday
            if (list_admissions.SelectedIndex == -1)
            {
                txt_nomPatient.DataContext = null;
            }
            // refresh the listBox if there are other patients
            else
            {
                Admission admission = list_admissions.SelectedItem as Admission;
                txt_nomPatient.DataContext = (from patient in database.Patients
                                              where patient.NSS == admission.NSS
                                              select patient).FirstOrDefault();
            }
        }
    }
}
