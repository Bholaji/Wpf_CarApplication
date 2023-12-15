using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Model;
using WPF.Utilities;

namespace WPF.ViewModel
{
    class SignUpVM: Utilities.ViewModelBase
    {
        /*Fields*/
        private string _firstName;
        private string _lastName;
        private string _email;
        private SecureString _password;
        private readonly UserModel _usermodel;

        /*Properties*/
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(nameof(LastName)); }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }
        public SecureString Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        /*Command*/
        public ICommand LoginCommand { get; }


        public SignUpVM()
        {
            _usermodel = new UserModel();
            /*FirstName = "Bolaji";*/
            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);

        }

        private bool CanExecuteLoginCommand(object arg)
        {
            throw new NotImplementedException();
        }

        private void ExecuteLoginCommand(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
