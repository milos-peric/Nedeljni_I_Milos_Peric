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
    class AdminViewModel : ViewModelBase
    {
        private AdminView adminView;

        public AdminViewModel(AdminView adminView, vwAdmin adminInstance)
        {
            this.adminView = adminView;
            admin = adminInstance;
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
                        adminView.Close();
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
