using Nedeljni_I_Milos_Peric.Command;
using Nedeljni_I_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_I_Milos_Peric.ViewModel
{
    class MasterViewModel : ViewModelBase
    {
        MasterView masterView;

        public MasterViewModel(MasterView viewMaster)
        {
            masterView = viewMaster;
        }

        private ICommand addAdministratorCommand;
        public ICommand AddAdministratorCommand
        {
            get
            {
                if (addAdministratorCommand == null)
                {
                    addAdministratorCommand = new RelayCommand(param => AddNewAdministratorExecute());
                }
                return addAdministratorCommand;
            }
        }

        private void AddNewAdministratorExecute()
        {
            try
            {
                AddAdministratorView addAdministrator = new AddAdministratorView();
                addAdministrator.ShowDialog();
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
                        masterView.Close();
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
