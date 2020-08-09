using Nedeljni_I_Milos_Peric.Command;
using Nedeljni_I_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_I_Milos_Peric.ViewModel
{
    class AddEmployeeViewModel : ViewModelBase
    {
        AddEmployeeView addEmployee;
        public AddEmployeeViewModel(AddEmployeeView addEmployeeOpen)
        {
            addEmployee = addEmployeeOpen;
        }

        private vwWorker worker;
        public vwWorker Worker
        {
            get { return worker; }
            set
            {
                worker = value;
                OnPropertyChanged("Worker");
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
                        addEmployee.Close();
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
