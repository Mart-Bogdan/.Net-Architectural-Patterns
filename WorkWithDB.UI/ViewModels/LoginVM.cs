using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using JetBrains.Annotations;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.UI.Views;

namespace WorkWithDB.UI.ViewModels
{
    public class LoginVM
    {
        public String Login { get; set; }


        public PasswordBox Password
        {
            get;
            set;
        }

        public ICommand OkCommand
        {
            get
            {
                return RelayCommand.CreateVoid(
                    () =>
                    {
                        var login = Login;
                        var password = Password.Password;

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
                            App.Current.MainWindow.Close();
                            App.Current.MainWindow = main;
                            main.Show();
                        }
                    },
                    ()=>
                        !string.IsNullOrEmpty(Login) && Password!=null && Password.Password.Length>0
                    );
            }
        }



    }
}