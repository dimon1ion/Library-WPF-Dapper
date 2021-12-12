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
    public class SignUpViewModel : INotifyPropertyChanged
    {

        #region Private fields

        private string _login;
        private string _errorLogin;
        private string _firstName;
        private string _errorFirstName;
        private string _lastName;
        private string _errorLastName;
        private string _password;
        private string _errorPassword;
        private string _secondPassword;
        private string _errorSecondPassword;
        private DapperExecutor dapper;
        private SignUpWindow _signUpWindow;

        #endregion

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


        public string FirstName
        {
            get => _firstName;
            set
            {
                if (!Equals(_firstName, value))
                {
                    _firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        public string ErrorFirstName
        {
            get => _errorFirstName;
            set
            {
                if (!Equals(_errorFirstName, value))
                {
                    _errorFirstName = value;
                    OnPropertyChanged(nameof(ErrorFirstName));
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (!Equals(_lastName, value))
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        public string ErrorLastName
        {
            get => _errorLastName;
            set
            {
                if (!Equals(_errorLastName, value))
                {
                    _errorLastName = value;
                    OnPropertyChanged(nameof(ErrorLastName));
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

        public string SecondPassword
        {
            get => _secondPassword;
            set
            {
                if (!Equals(_secondPassword, value))
                {
                    _secondPassword = value;
                }
            }
        }

        public string ErrorSecondPassword
        {
            get => _errorSecondPassword;
            set
            {
                if (!Equals(_errorSecondPassword, value))
                {
                    _errorSecondPassword = value;
                    OnPropertyChanged(nameof(ErrorSecondPassword));
                }
            }
        }

        #endregion

        #region Commands

        private Command _signUp;
        public ICommand SignUp => _signUp;
        
        #endregion

        #region Constructor

        public SignUpViewModel(SignUpWindow signUpWindow)
        {
            _signUpWindow = signUpWindow;
            _login = String.Empty;
            _firstName = String.Empty;
            _lastName = String.Empty;
            _password = String.Empty;
            _secondPassword = String.Empty;
            dapper = new DapperExecutor();

            _signUp = new Command(async obj =>
            {

                bool error = false;
                if (FirstName == String.Empty)
                {
                    ErrorFirstName = "* Enter first name";
                    error = true;
                }
                else { ErrorFirstName = String.Empty; }

                if (LastName == String.Empty)
                {
                    ErrorLastName = "* Enter last name";
                    error = true;
                }
                else { ErrorLastName = String.Empty; }

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
                else if (Password.Length < 7)
                {
                    ErrorPassword = "* Password must be more than 8 characters";
                    error = true;
                }
                else { ErrorPassword = String.Empty; }

                if (SecondPassword == String.Empty)
                {
                    ErrorSecondPassword = "* Enter password";
                    error = true;
                }
                else if (SecondPassword != Password)
                {
                    ErrorSecondPassword = "* Password entered incorrectly";
                    error = true;
                }
                else { ErrorSecondPassword = String.Empty; }

                if (!error)
                {
                    if (dapper.GetFirst<int>($"SELECT COUNT(Id) FROM Customers WHERE Customers.Login = @Login", new { Login = Login }) != 0)
                    {
                        ErrorLogin = "* Another user is using this login";
                    }
                    else
                    {
                        await dapper.InsertUpdateDelete("INSERT INTO Customers(FirstName, LastName, Login, Password) VALUES (@FirstName, @LastName, @Login, @Password)",
                            new { FirstName = FirstName, LastName = LastName, Login = Login, Password = Password });
                        _signUpWindow.Close();
                    }

                }

            });
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
