using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_CarApplication.Utilities;

namespace WPF_CarApplication.ViewModel
{
    class NavigationVM: ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView=value; OnPropertyChanged(); }
        }

        public ICommand LoggedHomeCommand { get; set; }
        public ICommand ProductCommand { get; set; }
        public ICommand SettingCommand { get; set; }
        public ICommand TransactionCommand { get; set; }

        private void LoggedHome(object obj) => CurrentView = new LoggedHomeVM();
        private void Product(object obj) => CurrentView = new ProductVM();
        private void Setting(object obj) => CurrentView = new SettingsVM();
        private void Transaction(object obj) => CurrentView = new TransactionVM();

        public NavigationVM()
        {
            LoggedHomeCommand = new RelayCommand(LoggedHome);
            ProductCommand = new RelayCommand(Product);
            SettingCommand = new RelayCommand(Setting);
            TransactionCommand = new RelayCommand(Transaction);


            //Startup page
            CurrentView = new HomeVM();
        }
    }
}
