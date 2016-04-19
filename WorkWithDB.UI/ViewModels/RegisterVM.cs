using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using JetBrains.Annotations;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.Entity;
using WorkWithDB.UI.MVVM;
using WorkWithDB.UI.Views;

namespace WorkWithDB.UI.ViewModels
{
    public class RegisterVM : BlogUser, IDataErrorInfo,INotifyPropertyChanged 
    {
        
        private string _passwordConfirmation;

        public string PasswordConfirmation
        {
            get { return _passwordConfirmation; }
            set
            {
                if (value == _passwordConfirmation) return;
                _passwordConfirmation = value;
                OnPropertyChanged();
                OnPropertyChanged("UserPassword");
            }
        }

        public new string UserPassword
        {
            get { return base.UserPassword; }
            set
            {
                if (value == base.UserPassword) return;
                base.UserPassword = value;
                OnPropertyChanged("PasswordConfirmation");
                OnPropertyChanged();
            }
        }

        public ICommand OkCommand
        {
            get
            {
                return RelayCommand.Create<Window>(
                    w =>
                    {
                        using (var uow = UnitOfWorkFactory.CreateInstance())
                        {
                            var userRepository = uow.AuthRepository;

                            var user = userRepository.Register(this.Clone());

                            StateHolder.CurrentUser = user;

                            w.Close();

                            MainWindow main = new MainWindow();
                            Application.Current.MainWindow.Close();
                            Application.Current.MainWindow = main;
                            main.Show();
                        }
                    },
                    w =>
                        !string.IsNullOrEmpty(Nick) &&
                        !string.IsNullOrEmpty(UserPassword) && 
                        UserPassword == PasswordConfirmation
                    );
            }
        }


        public ICommand CancelCommand
        {
            get
            {
                return RelayCommand.Create<Window>(w=>w.Close());
            }
        }


        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "PasswordConfirmation":
                    case "UserPassword":
                        if (String.IsNullOrEmpty(UserPassword) && columnName == "UserPassword")
                            return "Password can't be empty";
                        if (UserPassword != PasswordConfirmation)
                            return "Passwords do not match";

                        break;
                    case "Nick":
                        return String.IsNullOrWhiteSpace(Nick) ? "Login must not be empty" : null;
                    case "Name":
                        return String.IsNullOrWhiteSpace(Name) ? "Name must be specifyed" : null;
                }

                return null;
            }
        }



        public string Error { get; private set; }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}