using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HospitalManagement;
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
    /// Interaction logic for WindowAdmin.xaml
    /// </summary>
    public partial class WindowAdmin : Window
    {
        // objet BDD

        HospitalDatabaseEntities database;

        // variables for doctor
        string name = "";
        string lastname = "";

        public WindowAdmin()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // assign db
            database = new HospitalDatabaseEntities();

            // initialize the doctor listView with the existing doctors
            listView_medecin.DataContext = database.Doctors.ToList();
        }

        private void btn_ajouter_Click(object sender, RoutedEventArgs e) // add button operation
        {
            // fill temporary variables with doctor information
            name = txt_nom.Text;
            lastname = txt_prenom.Text;

            // if all the fields are filled, create an entry in the database
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(lastname))
            {
                MessageBox.Show("Please fill in all fields", "Warning!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Doctor medecin = new Doctor();
                medecin.idMedecin = name.Substring(0, 3) + lastname.Substring(0, 3); // creates doc Id :)
                medecin.name = name;
                medecin.lastName = lastname;

                database.Doctors.Add(medecin);

                try
                {
                    database.SaveChanges();
                    MessageBox.Show("Doctor added successfully", "Add Doctor", MessageBoxButton.OK, MessageBoxImage.Information);
                    txt_nom.Clear();
                    txt_prenom.Clear();
                    listView_medecin.DataContext = database.Doctors.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btn_effacer_Click(object sender, RoutedEventArgs e)
        {
            // allows you to delete the fields
            txt_nom.Clear();
            txt_prenom.Clear();
            listView_medecin.SelectedIndex = -1;    // reset the selected item to -1 for deselection
        }

        private void btn_modifier_Click(object sender, RoutedEventArgs e)
        {
            // fill temporary variables with doctor information
            name = txt_nom.Text;
            lastname = txt_prenom.Text;

            // if all the fields are filled, modification of the entry in the database
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(lastname))
            {
                MessageBox.Show("Please fill in all fields", "Warning!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Doctor temp = listView_medecin.SelectedItem as Doctor;
                temp.name = name;
                temp.lastName = lastname;

                try
                {
                    database.SaveChanges();
                    MessageBox.Show("Change(s) made successfully!", "Doctor change", MessageBoxButton.OK, MessageBoxImage.Information);
                    txt_nom.Clear();
                    txt_prenom.Clear();
                    listView_medecin.SelectedIndex = -1;
                    listView_medecin.DataContext = database.Doctors.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }



        private void btn_supprimer_Click(object sender, RoutedEventArgs e)
        {
            Doctor temp = listView_medecin.SelectedItem as Doctor;

            database.Doctors.Remove(temp);

            try
            {
                database.SaveChanges();
                MessageBox.Show("Delete successfully!", "Delete Doctor", MessageBoxButton.OK, MessageBoxImage.Information);
                txt_nom.Clear();
                txt_prenom.Clear();
                listView_medecin.SelectedIndex = -1;                        // reset the selected item to -1 to prevent an error and deselection
                listView_medecin.DataContext = database.Doctors.ToList();  // refresh of the listView
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                MessageBox.Show("You cannot delete this doctor because he is assigned to one or more patients", "Cannot delete", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listView_medecin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // check if an existing item is selected and fill in the fields for modification
            if (listView_medecin.SelectedIndex >= 0)
            {
                // cast the selected item into a Doctor variable to be able to extract its properties
                Doctor temp = listView_medecin.SelectedItem as Doctor;

                txt_nom.Text = temp.name;
                txt_prenom.Text = temp.lastName;
            }
        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            // return to the login page
            WindowLogin windowLogin = new WindowLogin();
            this.Close();
            windowLogin.Show();
        }
    }
}
