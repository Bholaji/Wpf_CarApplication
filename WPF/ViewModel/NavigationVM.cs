using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Utilities;

namespace WPF.ViewModel
{
    class NavigationVM:ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand LoggedHomeCommand { get; set; }
        public ICommand ProductCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand TransactionCommand { get; set; }

        private void LoggedHome(object obj) => CurrentView = new HomeVM();
        private void Product(object obj) => CurrentView = new ProductVM();
        private void LogOut(object obj) => CurrentView = new LogOutVM();
        private void Transaction(object obj) => CurrentView = new TransactionVM();

        public NavigationVM()
        {
            LoggedHomeCommand = new RelayCommand(LoggedHome);
            ProductCommand = new RelayCommand(Product);
            LogoutCommand = new RelayCommand(LogOut);
            TransactionCommand = new RelayCommand(Transaction);


            //Startup page
            CurrentView = new HomeVM();
        }
    }
}
