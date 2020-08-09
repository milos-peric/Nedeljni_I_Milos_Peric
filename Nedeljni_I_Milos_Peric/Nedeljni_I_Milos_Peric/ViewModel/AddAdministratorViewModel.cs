using Nedeljni_I_Milos_Peric.Command;
using Nedeljni_I_Milos_Peric.Utility;
using Nedeljni_I_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nedeljni_I_Milos_Peric.ViewModel
{
    class AddAdministratorViewModel : ViewModelBase
    {
        AddAdministratorView addAdmin;
        DataBaseService dataBaseService = new DataBaseService();

        public AddAdministratorViewModel(AddAdministratorView addAdminOpen)
        {
            addAdmin = addAdminOpen;
            admin = new vwAdmin();
        }

        private vwAdmin admin;
        public vwAdmin Admin
        {
            get { return admin; }
            set
            {
                admin = value;
                OnPropertyChanged("Admin");
            }
        }

        private ICommand addAdminCommand;
        public ICommand AddAdminCommand
        {
            get
            {
                if (addAdminCommand == null)
                {
                    addAdminCommand = new RelayCommand(AddAdminCommandExecute, param => CanAddAdminCommandExecute());
                }
                return addAdminCommand;
            }
        }

        private void AddAdminCommandExecute(object obj)
        {
            try
            {
                //if (EntryValidation.ValidateName(Patient.FirstName) == false)
                //{
                //    MessageBox.Show("First Name can only contain letters. Please try again", "Invalid input");
                //    return;
                //}
                //if (EntryValidation.ValidateName(Patient.LastName) == false)
                //{
                //    MessageBox.Show("Last Name can only contain letters. Please try again", "Invalid input");
                //    return;
                //}
                //if (EntryValidation.ValidateJmbg(Patient.JMBG) == false)
                //{
                //    MessageBox.Show("JMBG you entered is not valid. Please try again", "Invalid input");
                //    return;
                //}
                //if (EntryValidation.ValidateMedicalInsuranceNumber(Patient.MedicalInsuranceNumber) == false)
                //{
                //    MessageBox.Show("Medical insurance number entered is not valid, must contain exactly 11 numbers. Please try again", "Invalid input");
                //    return;
                //}
                string password = (obj as PasswordBox).Password;
                string encryptPassword = EncryptionHelper.Encrypt(password);
                char userGender = admin.Gender.ElementAt(0);
                admin.Gender = userGender.ToString();
                admin.UserPassword = encryptPassword;
                DateTime expiryDate = DateTime.Today;
                admin.ExpirationDate = expiryDate.AddDays(7);
                Debug.WriteLine(admin.FirstName);
                Debug.WriteLine(admin.LastName);
                Debug.WriteLine(admin.JMBG);
                Debug.WriteLine(admin.Gender);
                Debug.WriteLine(admin.Residence);
                Debug.WriteLine(admin.MarriageStatus);
                Debug.WriteLine(admin.Username);
                Debug.WriteLine(admin.UserPassword);
                Debug.WriteLine(admin.ExpirationDate);
                Debug.WriteLine(admin.AdminType);
                dataBaseService.AddAdministrator(admin);
                MessageBox.Show("New administrator registered successfully!", "Info");
                addAdmin.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddAdminCommandExecute()
        {
            if (string.IsNullOrEmpty(admin.FirstName) || string.IsNullOrEmpty(admin.LastName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new RelayCommand(param => CancelCommandExecute());
                }
                return cancelCommand;
            }
        }

        private void CancelCommandExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to close window?", "Close Window", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        addAdmin.Close();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
