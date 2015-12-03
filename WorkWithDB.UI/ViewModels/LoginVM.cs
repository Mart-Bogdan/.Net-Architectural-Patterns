using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;
using System.Windows;
using System.Windows.Input;
using JetBrains.Annotations;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.UI.Views;

namespace WorkWithDB.UI.ViewModels
{
    public class LoginVM
    {
        public String Login { get; set; }


        public String Password {get;set;}

        public ICommand OkCommand
        {
            get
            {
                return RelayCommand.CreateVoid(
                    () =>
                    {
                        var login = Login;
                        var password = Password.ToString();

                        using (var uow = UnitOfWorkFactory.CreateInstance())
                        {
                            var userRepository = uow.BlogUserRepository;
                            var user = userRepository.GetByLoginPassword(login, password);
                            if (user == null)
                            {
                                MessageBox.Show("Incorrect credentials");
                                return;
                            }

                            StateHolder.CurrentUser = user;

                            MainWindow main = new MainWindow();
                            Application.Current.MainWindow.Close();
                            Application.Current.MainWindow = main;
                            main.Show();
                        }
                    },
                    ()=>
                        !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password)
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


        public ICommand RegisterCommand
        {
            get
            {
                return RelayCommand.CreateVoid(() =>
                {
                    
                });
            }
        }


    }
}