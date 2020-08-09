using Nedeljni_I_Milos_Peric.Command;
using Nedeljni_I_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_I_Milos_Peric.ViewModel
{
    class RegistrationViewModel : ViewModelBase
    {
        RegistrationView registration;
        public RegistrationViewModel(RegistrationView registrationViewOpen)
        {
            registration = registrationViewOpen;
            userTypeList = new List<string>
            {
                "Employee",
                "Manager"
            };
        }

        public RegistrationViewModel(RegistrationView registrationViewOpen, int retryCounter)
        {
            registration = registrationViewOpen;
            userTypeList = new List<string>
            {
                "Employee",
                "Manager"
            };
            if (retryCounter == 0)
            {
                userTypeList.Remove("Manager");
            }
        }

        private string userType;
        public string UserType
        {
            get { return userType; }
            set
            {
                userType = value;
                OnPropertyChanged("UserType");
            }
        }

        private List<string> userTypeList;
        public List<string> UserTypeList
        {
            get { return userTypeList; }
            set
            {
                userTypeList = value;
                OnPropertyChanged("UserTypeList");
            }
        }

        private ICommand addUserCommand;
        public ICommand AddUserCommand
        {
            get
            {
                if (addUserCommand == null)
                {
                    addUserCommand = new RelayCommand(param => AddNewUserExecute());
                }
                return addUserCommand;
            }
        }

        private void AddNewUserExecute()
        {
            try
            {
                if (userType == null)
                {
                    MessageBox.Show("User not selected.\nPlease select a user from Combo Box.", "Error");
                }
                else if (userType == "Employee")
                {
                    AddEmployeeView addEmployee = new AddEmployeeView();
                    addEmployee.ShowDialog();
                }
                else if (userType == "Manager")
                {
                    try
                    {
                        MessageBoxResult result = MessageBox.Show("Are you sure you want to register as Manager?", "Confirmation", MessageBoxButton.YesNo);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                ManagerPasswordEnterView enterPasswordView = new ManagerPasswordEnterView(registration);
                                Thread.Sleep(500);
                                enterPasswordView.Show();
                                return;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private ICommand logoutCommand;
        public ICommand LogoutCommand
        {
            get
            {
                if (logoutCommand == null)
                {
                    logoutCommand = new RelayCommand(param => Logout());
                    return logoutCommand;
                }
                return logoutCommand;
            }
        }

        public void Logout()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to logout?", "Confirmation", MessageBoxButton.OKCancel);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        LoginView loginView = new LoginView();
                        Thread.Sleep(1000);
                        registration.Close();
                        loginView.Show();
                        return;
                    case MessageBoxResult.Cancel:
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
