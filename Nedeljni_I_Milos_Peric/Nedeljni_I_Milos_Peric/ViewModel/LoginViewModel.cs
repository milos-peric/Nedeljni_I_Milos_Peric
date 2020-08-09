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
    class LoginViewModel : ViewModelBase
    {
        LoginView login;
        DataBaseService dataBaseService = new DataBaseService();
        public LoginViewModel(LoginView viewLogin)
        {
            login = viewLogin;
            RandomPasswordGenerator.WriteRandomPasswordToFile();
            admin = new vwAdmin();
            manager = new vwManager();
        }

        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
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

        private ICommand signIn;
        public ICommand SignIn
        {
            get
            {
                if (signIn == null)
                {
                    signIn = new RelayCommand(SignInCommandExecute, param => CanSignInCommandExecute());
                }
                return signIn;
            }
        }

        private void SignInCommandExecute(object obj)
        {
            try
            {
                string password = (obj as PasswordBox).Password;
                admin = dataBaseService.FindAdminCredentials(UserName, password);
                manager = dataBaseService.FindManagerCredentials(UserName, password);               
                if (UserName.Equals("WPFMaster") && password.Equals("WPFAccess"))
                {
                    MasterView masterView = new MasterView();
                    login.Close();
                    masterView.Show();
                    return;
                }
                if (admin != null)
                {
                    AdminView adminView = new AdminView(admin);
                    login.Close();
                    adminView.Show();
                    return;
                }
                else if (manager != null)
                {
                    ManagerView managerView = new ManagerView(manager);
                    login.Close();
                    managerView.Show();
                    return;
                }
                else
                {
                    MessageBox.Show("Wrong usename or password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanSignInCommandExecute()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ICommand registerCommand;
        public ICommand RegisterCommand
        {
            get
            {
                if (registerCommand == null)
                {
                    registerCommand = new RelayCommand(param => RegisterExecute());
                }
                return registerCommand;
            }
        }

        private void RegisterExecute()
        {
            try
            {
                RegistrationView registerView = new RegistrationView();
                login.Close();
                registerView.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
