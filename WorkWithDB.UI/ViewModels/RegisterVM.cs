using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using JetBrains.Annotations;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.Entity;
using WorkWithDB.UI.MVVM;
using WorkWithDB.UI.Views;

namespace WorkWithDB.UI.ViewModels
{
    public class RegisterVM : BlogUser, IDataErrorInfo 
    {
        public string PasswordConfirmation { get; set; }

        public ICommand OkCommand
        {
            get
            {
                return RelayCommand.Create<Window>(
                    w =>
                    {
                        using (var uow = UnitOfWorkFactory.CreateInstance())
                        {
                            var userRepository = uow.BlogUserRepository;

                            var userId = userRepository.Insert(this);
                            uow.Commit();

                            StateHolder.CurrentUser = userRepository.GetById(userId);

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


        public string this[ [InvokerParameterName] string columnName]
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
    }
}