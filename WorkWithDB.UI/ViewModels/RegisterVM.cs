using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using JetBrains.Annotations;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.Entity;
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
                        //var login = Login;
                        //var password = Password.Password;

                        //using (var uow = UnitOfWorkFactory.CreateInstance())
                        //{
                        //    var userRepository = uow.BlogUserRepository;
                        //    var user = userRepository.GetByLoginPassword(login, password);
                        //    if (user == null)
                        //    {
                        //        MessageBox.Show("Incorrect credentials");
                        //        return;
                        //    }

                        //    StateHolder.CurrentUser = user;

                        //    MainWindow main = new MainWindow();
                        //    Application.Current.MainWindow.Close();
                        //    Application.Current.MainWindow = main;
                        //    main.Show();
                        //}
                    },
                    w =>
                        !string.IsNullOrEmpty(UserPassword) && UserPassword != PasswordConfirmation
                    );
            }
        }


        public ICommand CancelCommand
        {
            get
            {
                return RelayCommand.CreateVoid(Application.Current.Shutdown);
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
                }

                return null;
            }
        }



        public string Error { get; private set; }
    }
}