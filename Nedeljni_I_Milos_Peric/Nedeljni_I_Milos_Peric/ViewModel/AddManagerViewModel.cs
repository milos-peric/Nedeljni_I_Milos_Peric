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
    class AddManagerViewModel : ViewModelBase
    {
        AddManagerView addManager;
        DataBaseService dataBaseService = new DataBaseService();

        public AddManagerViewModel(AddManagerView addManagerOpen)
        {
            addManager = addManagerOpen;
            manager = new vwManager();
        }

        private vwManager manager;
        public vwManager Manager
        {
            get { return manager; }
            set
            {
                manager = value;
                OnPropertyChanged("Manager");
            }
        }

        private ICommand addManagerCommand;
        public ICommand AddManagerCommand
        {
            get
            {
                if (addManagerCommand == null)
                {
                    addManagerCommand = new RelayCommand(AddManagerCommandExecute, param => CanAddManagerCommandExecute());
                }
                return addManagerCommand;
            }
        }

        private void AddManagerCommandExecute(object obj)
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
                manager.UserPassword = encryptPassword;
                string encryptedReservePassword = manager.ReservedPassword + "WPF";
                manager.ReservedPassword = EncryptionHelper.Encrypt(encryptedReservePassword);
                char userGender = manager.Gender.ElementAt(0);
                manager.Gender = userGender.ToString();
                Debug.WriteLine(manager.FirstName);
                Debug.WriteLine(manager.LastName);
                Debug.WriteLine(manager.JMBG);
                Debug.WriteLine(manager.Gender);
                Debug.WriteLine(manager.Residence);
                Debug.WriteLine(manager.MarriageStatus);
                Debug.WriteLine(manager.Username);
                Debug.WriteLine(manager.UserPassword);
                Debug.WriteLine(manager.Email);
                Debug.WriteLine(manager.ReservedPassword);
                Debug.WriteLine(manager.SuccessfulProjects);
                Debug.WriteLine(manager.OfficeNumber);
                dataBaseService.AddManager(manager);
                MessageBox.Show("New manager registered successfully!", "Info");
                LoginView login = new LoginView();                
                addManager.Close();
                login.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddManagerCommandExecute()
        {
            if (string.IsNullOrEmpty(manager.FirstName) || string.IsNullOrEmpty(manager.LastName))
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
                        addManager.Close();
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
