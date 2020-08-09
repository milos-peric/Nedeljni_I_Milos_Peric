using Nedeljni_I_Milos_Peric.Command;
using Nedeljni_I_Milos_Peric.Utility;
using Nedeljni_I_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nedeljni_I_Milos_Peric.ViewModel
{
    class ManagerPasswordEnterViewModel : ViewModelBase
    {
        private ManagerPasswordEnterView managerPasswordEnterView;
        private RegistrationView registration;
        static int retryCounter = 3;

        public ManagerPasswordEnterViewModel(ManagerPasswordEnterView managerPasswordEnterView, RegistrationView registrationView)
        {
            this.managerPasswordEnterView = managerPasswordEnterView;
            registration = registrationView;
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

        private ICommand confirmCommand;
        public ICommand ConfirmCommand
        {
            get
            {
                if (confirmCommand == null)
                {
                    confirmCommand = new RelayCommand(ConfirmCommandExecute);
                }
                return confirmCommand;
            }
        }

        private void ConfirmCommandExecute(object obj)
        {
            try
            {
                string password = (obj as PasswordBox).Password;
                string filePassword = RandomPasswordGenerator.ReadManagerPassword();
                if (password.Equals(filePassword))
                {
                    AddManagerView managerView = new AddManagerView();
                    managerPasswordEnterView.Close();
                    registration.Close();
                    managerView.Show();
                    return;
                }
                else
                {
                    if (retryCounter == 0)
                    {
                        MessageBox.Show("You have exeeded maximum number of tries.\nReturning to registration screen.");
                        RegistrationView registrationView = new RegistrationView(retryCounter);
                        managerPasswordEnterView.Close();
                        registration.Close();
                        registrationView.Show();
                    }
                    else
                    {
                        MessageBox.Show($"Wrong password. Remaining number of tries: {--retryCounter}");
                    }                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                        managerPasswordEnterView.Close();
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
