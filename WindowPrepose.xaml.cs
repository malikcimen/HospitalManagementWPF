using HospitalManagement;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Interaction logic for WindowPrepose.xaml
    /// </summary>
    public partial class WindowPrepose : Window
    {
        // DB object
        HospitalDatabaseEntities database;


        // variables patient
        string name = "";
        string lastName = "";
        DateTime dateOfBirth;
        string address = "";
        string ville = "";
        string province = "";
        string codePostal = "";
        string numberTelephone = "";
        string assurance = "";

        // choice for patient comboBoxes
        string[] provinces = { "Istanbul", "Adana", "Bursa", "Kars" };
        string[] assurances = { "Global Insurance", "Local Insurance" };

        // admission variables
        string patient = "";
        string medecin = "";
        DateTime dateAdmission;
        bool surgery = false;
        DateTime dateChirurgie;
        string departement = "";
        string typeLit = "";
        bool television = false;
        bool telephone = false;
        string litDispo = "";
        static double prixJournalier = 0;
        static double prixLit = 0;

        // variables choix lits disponibles
        static Departement CBdepartement;
        static TypeBed CBtypeLit;
        static string deptID;
        static string typeLitID;

        public WindowPrepose()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // assign db
            database = new HospitalDatabaseEntities();
            

   

            // initialize the patient comboBoxes
            cb_province.DataContext = provinces;
            cb_assurance.DataContext = assurances;

            // initialize admission comboBoxes
            cb_patient.DataContext = database.Patients.ToList();
            cb_medecin.DataContext = database.Doctors.ToList();
            cb_departement.DataContext = database.Departements.ToList();
            cb_typeLit.DataContext = database.TypeBeds.ToList();

            // initialization of the indexes of the department and typeKit comboBoxes, also calls the selectionChanged of each
            cb_departement.SelectedIndex = 0;
            cb_typeLit.SelectedIndex = 2;

            // initialization of the daily price textBox with formatting
            txt_prix.Text = prixJournalier.ToString("C");

            // query that checks the total number of available beds
            var nbLitsDispoQuery = (from nbLits in database.Beds
                                    where nbLits.occupe == false
                                    select nbLits).Count();

            // nbBitsDispoQuery = 0; // for test no bed available
            if (nbLitsDispoQuery <= 0)
            {
                MessageBox.Show("No Beds Available, Please Exit Patients", "No Beds", MessageBoxButton.OK, MessageBoxImage.Warning);
                WindowMedecin windowMedecin = new WindowMedecin();
                this.Close();
                windowMedecin.Show();
            }
        }

        private void btn_enregistrerPatient_Click(object sender, RoutedEventArgs e)
        {
            // fill temporary variables with patient information
            name = txt_nom.Text;
            lastName = txt_prenom.Text;
            // if no date of birth is chosen, today's value will be chosen
            if (date_naissance.SelectedDate == null)
                dateOfBirth = DateTime.Today;
            else
                dateOfBirth = date_naissance.SelectedDate.Value;

            address = txt_adresse.Text;
            ville = txt_ville.Text;
            province = cb_province.Text;
            codePostal = txt_codePostal.Text;
            numberTelephone = txt_telephone.Text;

            if (cb_assurance.Text == "Global Insurance")
                assurance = "assPublique";
            else if (cb_assurance.Text == "Local Insurance")
                assurance = "assPrive";

            // variables for testing empty fields
            Object[] testPatient = { Name, lastName, date_naissance, address, ville, province, codePostal, numberTelephone, assurance };
            String[] nomTestPatient = { "Nom", "Prenom", "Naissance", "Adresse", "Ville", "Province", "Code Postal", "Telephone", "Assurance" };
            
            


            // if all the fields are filled, create an entry in the database
            if (ChampRemplis(testPatient, nomTestPatient))
            {
                Patient patient = new Patient();
                patient.NSS = name.Substring(0, 3) + lastName.Substring(0, 3);
                patient.name = name;
                patient.lastName = lastName;
                patient.dateNaissance = dateOfBirth;
                patient.address = address;
                patient.ville = ville;
                patient.province = province;
                patient.codePostal = codePostal;
                patient.telephone = numberTelephone;
                patient.idAssurance = assurance;

                database.Patients.Add(patient);

                try
                {
                    database.SaveChanges();
                    MessageBox.Show("Patient added successfully", "Add Patient", MessageBoxButton.OK, MessageBoxImage.Information);

                    // reset patient fields to default
                    txt_nom.Clear();
                    txt_prenom.Clear();
                    txt_adresse.Clear();
                    txt_ville.Clear();
                    cb_province.SelectedIndex = 0;
                    txt_codePostal.Clear();
                    txt_telephone.Clear();
                    cb_assurance.SelectedIndex = 0;

                    // refresh the patient comboBox in the admission section to display the new patient
                    cb_patient.DataContext = database.Patients.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btn_enregistrerAdmission_Click(object sender, RoutedEventArgs e)
        {
            // fill temporary variables with admission information
            patient = cb_patient.Text;
            medecin = cb_medecin.Text;

            // if no admission date is chosen, today's value will be chosen
            if (!date_admission.SelectedDate.HasValue)
                dateAdmission = DateTime.Today;
            else
                dateAdmission = date_admission.SelectedDate.Value;
            surgery = check_chirurgie.IsChecked.Value;

            // if the surgery checkBox is checked, you must choose a date otherwise the minimum value will be chosen
            if (check_chirurgie.IsChecked.Value == true && date_chirurgie.SelectedDate.HasValue)
                dateChirurgie = date_chirurgie.SelectedDate.Value;
            else if (check_chirurgie.IsChecked.Value == true && !date_chirurgie.SelectedDate.HasValue)
            {
                MessageBox.Show("Please enter a surgery date", "Date Surgery", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
                dateChirurgie = DateTime.MinValue;

            departement = cb_departement.Text;
            typeLit = cb_typeLit.Text;
            television = check_television.IsChecked.Value;
            telephone = check_telephone.IsChecked.Value;

            if (lb_litsDispo.SelectedItem == null)
                litDispo = "";
            else
                litDispo = lb_litsDispo.SelectedItem.ToString();


            // variables for testing empty fields
            Object[] testAdmission = { patient, medecin, dateAdmission, surgery, dateChirurgie, departement, typeLit, litDispo };
            String[] nomTestAdmission = { "Patient", "Médecin", "Date Admission", "Surgery", "Date Chirurgie", "Département", "Type Lit", "Lits Disponibles" };

            // if all the fields are filled, create an entry in the database
            if (ChampRemplis(testAdmission, nomTestAdmission))
            {
                Patient patient = cb_patient.SelectedItem as Patient;
                Doctor medecin = cb_medecin.SelectedItem as Doctor;
                Bed lit = lb_litsDispo.SelectedItem as Bed;

                Admission admission = new Admission();
                admission.idAdmission = patient.NSS + dateAdmission.Date.ToString("d");
                admission.surgery = surgery;
                admission.dateAdmission = dateAdmission;
                admission.dateSurgery = dateChirurgie;
                admission.dateLeave = DateTime.MinValue;
                admission.television = television;
                admission.telephone = telephone;
                admission.NSS = patient.NSS;
                admission.numeroLit = lit.numeroLit;
                admission.idMedecin = medecin.idMedecin;
                lit.occupe = true;

                database.Admissions.Add(admission);

                try
                {
                    database.SaveChanges();
                    MessageBox.Show("Admission added successfully", "New Admission", MessageBoxButton.OK, MessageBoxImage.Information);

                    // reset admission fields to default
                    cb_patient.SelectedIndex = -1;
                    cb_medecin.SelectedIndex = -1;
                    check_chirurgie.IsChecked = false;
                    cb_departement.SelectedIndex = 0;
                    cb_typeLit.SelectedIndex = 2;
                    check_television.IsChecked = false;
                    check_telephone.IsChecked = false;
                    lb_litsDispo.DataContext = ListBoxLitsUpdate(deptID, typeLitID);
                    txt_prix.Text = String.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void check_chirurgie_Checked(object sender, RoutedEventArgs e)
        {
            // allows to put the date of surgery if necessary
            date_chirurgie.IsEnabled = true;
            // patients who will undergo surgery are automatically assigned a room in the surgery department
            cb_departement.SelectedIndex = 1;
        }

        private void check_chirurgie_Unchecked(object sender, RoutedEventArgs e)
        {
            // prevent putting a date if there is no surgery
            date_chirurgie.IsEnabled = false;
        }

        private void cb_departement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // return the index to -1 to prevent an error
            lb_litsDispo.SelectedIndex = -1;

            // update of the chosen department
            CBdepartement = cb_departement.SelectedItem as Departement;
            deptID = CBdepartement.idDepartement;

            // update of the litDispo data Context listBox with a query
            lb_litsDispo.DataContext = ListBoxLitsUpdate(deptID, typeLitID);

            // remove the price of the bed that was applied in the listBoxSelectionChanged and reset the price of the bed to 0
            prixJournalier -= prixLit;
            txt_prix.Text = prixJournalier.ToString("C");
            prixLit = 0;
        }

        private void cb_typeLit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // return the index to -1 to prevent an error
            lb_litsDispo.SelectedIndex = -1;

            // update the type of bed chosen
            CBtypeLit = cb_typeLit.SelectedItem as TypeBed;
            typeLitID = CBtypeLit.idType;

            // update of the litDispo data Context listBox with a query
            lb_litsDispo.DataContext = ListBoxLitsUpdate(deptID, typeLitID);

            // remove the price of the bed that was applied in the listBoxSelectionChanged and reset the price of the bed to 0
            prixJournalier -= prixLit;
            txt_prix.Text = prixJournalier.ToString("C");
            prixLit = 0;
        }

        private void cb_patient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // if the index is -1, returns
            if (cb_patient.SelectedIndex == -1)
            {
                return;
            }
            else
            {
                // patients who are under 16 and not admitted to surgery
                // are automatically assigned a room from the pediatrics department
                Patient patient = cb_patient.SelectedItem as Patient;
                DateTime maintenant = DateTime.Today;
                DateTime anneeNaissance = patient.dateNaissance.Value;
                var age = maintenant.Year - anneeNaissance.Year;
                if (age <= 16 && !check_chirurgie.IsChecked.Value)
                {
                    cb_departement.SelectedIndex = 2;
                }
            }
        }

        private void check_television_Checked(object sender, RoutedEventArgs e)
        {
            prixJournalier += 42.5;
            txt_prix.Text = prixJournalier.ToString("C");
        }

        private void check_television_Unchecked(object sender, RoutedEventArgs e)
        {
            prixJournalier -= 42.5;
            txt_prix.Text = prixJournalier.ToString("C");
        }

        private void check_telephone_Checked(object sender, RoutedEventArgs e)
        {
            prixJournalier += 7.5;
            txt_prix.Text = prixJournalier.ToString("C");
        }

        private void check_telephone_Unchecked(object sender, RoutedEventArgs e)
        {
            prixJournalier -= 7.5;
            txt_prix.Text = prixJournalier.ToString("C");
        }

        private void lb_litsDispo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // if no bed is chosen
            if (lb_litsDispo.SelectedIndex == -1 || cb_patient.SelectedIndex == -1)
            {
                return;
            }
            else
            {
                // put the chosen patient and bed in the objects
                Patient patient = cb_patient.SelectedItem as Patient;
                Bed litChoisi = lb_litsDispo.SelectedItem as Bed;

                // patient insurance and queries to see how many beds of each category are available
                string assurancePatient = patient.idAssurance;
                var nbLitsStandardDispo = (from nbLits in database.Beds
                                           where nbLits.occupe == false && nbLits.idType == "standard"
                                           select nbLits).Count();
                var nbLitsSemiPriveDispo = (from nbLits in database.Beds
                                            where nbLits.occupe == false && nbLits.idType == "semi-private"
                                            select nbLits).Count();

                // if the patient does not have private insurance and standard beds are available
                if (assurancePatient == "assPublique" && nbLitsStandardDispo > 0 && litChoisi.idType == "semi-privee")
                {
                    prixLit = 267;
                    prixJournalier += prixLit;
                    txt_prix.Text = prixJournalier.ToString("C");
                }
                // if the patient does not have private insurance and semi-private beds are available
                else if (assurancePatient == "assPublique" && nbLitsSemiPriveDispo > 0 && litChoisi.idType == "privee")
                {
                    prixLit = 571;
                    prixJournalier += prixLit;
                    txt_prix.Text = prixJournalier.ToString("C");
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

        // check each of the fields and display an error message if it is empty
        private static bool ChampRemplis(object[] test, string[] nomTest)
        {
            bool testOK = false;

            for (int i = 0; i < test.Length; i++)
            {
                string testVide = test[i].ToString();

                
                // if the last field is not empty, the test is OK
                 if (i == test.Length - 1)
                    testOK = true;
            }
            return testOK;
        }

        // returns the list of available beds with the chosen department and type of bed
        private List<Bed> ListBoxLitsUpdate(string departement, string type)
        {
            var litdispoQuery = (from lit in database.Beds
                                 where (lit.idDepartement == departement
                                 && lit.idType == type && lit.occupe == false)
                                 select lit).ToList();

            return litdispoQuery;
        }
    }
}
