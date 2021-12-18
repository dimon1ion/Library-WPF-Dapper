using Library_WPF.Model;
using Library_WPF.Service;
using Library_WPF.Service.Command;
using Library_WPF.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Library_WPF.ViewModel
{
    public class SignInViewModel : INotifyPropertyChanged
    {

        private string _login;
        private string _errorLogin;
        private string _password;
        private string _errorPassword;
        private DapperExecutor dapper;
        private MainWindow _mainWindow;

        #region Properties

        public string Login
        {
            get => _login;
            set
            {
                if (!Equals(_login, value))
                {
                    _login = value;
                    OnPropertyChanged(nameof(Login));
                }
            }
        }

        public string ErrorLogin
        {
            get => _errorLogin;
            set
            {
                if (!Equals(_errorLogin, value))
                {
                    _errorLogin = value;
                    OnPropertyChanged(nameof(ErrorLogin));
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (!Equals(_password, value))
                {
                    _password = value;
                }
            }
        }

        public string ErrorPassword
        {
            get => _errorPassword;
            set
            {
                if (!Equals(_errorPassword, value))
                {
                    _errorPassword = value;
                    OnPropertyChanged(nameof(ErrorPassword));
                }
            }
        }

        #endregion

        private Command _signIn;

        public ICommand SignIn => _signIn;

        #region Constructor

        public SignInViewModel(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            dapper = new DapperExecutor();
            _login = String.Empty;
            _errorLogin = String.Empty;
            _password = String.Empty;
            _errorPassword = String.Empty;

            _signIn = new Command(obj =>
            {
                bool error = false;

                if (Login == String.Empty)
                {
                    ErrorLogin = "* Enter login";
                    error = true;
                }
                else { ErrorLogin = String.Empty; }

                if (Password == String.Empty)
                {
                    ErrorPassword = "* Enter password";
                    error = true;
                }
                else { ErrorPassword = String.Empty; }

                if (!error)
                {
                    bool loginfind = false;
                    Admin admin;
                    Salesman salesman;
                    Customer customer;
                    if ((admin = dapper.GetFirst<Admin>($"SELECT * FROM Admins WHERE Admins.Login = @Login", new { Login = Login })) != null)
                    {
                        loginfind = true;
                        if (admin.Password == Password)
                        {
                            mainWindow.Visibility = Visibility.Hidden;
                            AdminWindow adminWindow = new AdminWindow();
                            adminWindow.ShowDialog();
                            mainWindow.Close();
                            return;
                        }
                    }
                    if ((salesman = dapper.GetFirst<Salesman>($"SELECT * FROM Salesmen WHERE Salesmen.Login = @Login", new { Login = Login })) != null)
                    {
                        loginfind = true;
                        if (salesman.Password == Password)
                        {
                            mainWindow.Visibility = Visibility.Hidden;
                            ManagerWindow managerWindow = new ManagerWindow(salesman);
                            managerWindow.ShowDialog();
                            mainWindow.Close();
                            return;
                        }
                    }
                    if ((customer = dapper.GetFirst<Customer>($"SELECT * FROM Customers WHERE Customers.Login = @Login", new { Login = Login })) != null)
                    {
                        loginfind = true;
                        if (customer.Password == Password)
                        {
                            mainWindow.Visibility = Visibility.Hidden;
                            CustomerWindow customerWindow = new CustomerWindow(customer);
                            customerWindow.ShowDialog();
                            mainWindow.Close();
                            return;
                        }
                    }
                    if (!loginfind)
                    {
                        ErrorLogin = "* We couldn't find an account with that login";
                        return;
                    }
                    else
                    {
                        ErrorPassword = "* password is incorrect";
                        return;
                    }
                }

            });
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string properyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }
    }
}
