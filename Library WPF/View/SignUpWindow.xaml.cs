using Dapper;
using Library_WPF.Model;
using Library_WPF.Service;
using Library_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Library_WPF.View
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        public DapperExecutor dapper { get; set; }
        public SignUpWindow()
        {
            InitializeComponent();
            dapper = new DapperExecutor();
            this.DataContext = new SignUpViewModel(this);
        }

        private void PasswordBoxPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            switch ((sender as PasswordBox).Name)
            {
                case "PasswordBoxPass":
                    ((SignUpViewModel)this.DataContext).Password = (sender as PasswordBox).Password;
                    break;
                case "PasswordBoxSecondPass":
                    ((SignUpViewModel)this.DataContext).SecondPassword = (sender as PasswordBox).Password;
                    break;
                default:
                    break;
            }
            
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    bool error = false;
        //    if (TextBoxFirstName.Text == String.Empty)
        //    {
        //        ErrorFirstNameText.Text = "* Enter first name";
        //        error = true;
        //    }
        //    else { ErrorFirstNameText.Text = String.Empty; }

        //    if (TextBoxLastName.Text == String.Empty)
        //    {
        //        ErrorLastNameText.Text = "* Enter last name";
        //        error = true;
        //    }
        //    else { ErrorLastNameText.Text = String.Empty; }

        //    if (TextBoxLogin.Text == String.Empty)
        //    {
        //        ErrorLoginText.Text = "* Enter login";
        //        error = true;
        //    }
        //    else { ErrorLoginText.Text = String.Empty; }

        //    if (PasswordBoxPass.Password == String.Empty)
        //    {
        //        ErrorPasswordText.Text = "* Enter password";
        //        error = true;
        //    }
        //    else if (PasswordBoxPass.Password.Length < 7)
        //    {
        //        ErrorPasswordText.Text = "* Password must be more than 8 characters";
        //        error = true;
        //    }
        //    else { ErrorPasswordText.Text = String.Empty; }

        //    if (PasswordBoxSecondPass.Password == String.Empty)
        //    {
        //        ErrorPasswordAgainText.Text = "* Enter password";
        //        error = true;
        //    }
        //    else if (PasswordBoxSecondPass.Password != PasswordBoxPass.Password)
        //    {
        //        ErrorPasswordAgainText.Text = "* Password entered incorrectly";
        //        error = true;
        //    }
        //    else { ErrorPasswordAgainText.Text = String.Empty; }

        //    if (!error)
        //    {
        //        //var obj = Get<Customer>("SELECT * FROM Customers");
        //        //string res = "";
        //        //foreach (var item in obj)
        //        //{
        //        //    res += $"{item.FirstName} {item.LastName} {item.Login} {item.Password}\n";
        //        //}
        //        //MessageBox.Show(res);
        //        //Insert("INSERT INTO Customers(FirstName, LastName, Login, Password) VALUES ('Di1', 'Ma', 'kaka', '1234567')");
        //        //MessageBox.Show("Test");
        //        if (dapper.GetFirst<int>($"SELECT COUNT(Id) FROM Customers WHERE Customers.Login = @Login", new { Login = TextBoxLogin.Text }) != 0)
        //        {
        //            ErrorLoginText.Text = "* Another user is using this login";
        //        }
        //        else
        //        {
        //            dapper.Insert("INSERT INTO Customers(FirstName, LastName, Login, Password) VALUES (@FirstName, @LastName, @Login, @Password)",
        //                new { FirstName = TextBoxFirstName.Text, LastName = TextBoxLastName.Text, Login = TextBoxLogin.Text, Password = PasswordBoxPass.Password });
        //            this.Close();
        //        }

        //    }
        //}

        //private IEnumerable<T> Get<T>(string query)
        //{
        //    using (SqlConnection connection = DapperExecutor.GetConnection())
        //    {
        //        connection.Open();
        //        return connection.Query<T>(query);
        //    }
        //}

        //private async void Insert(string query)
        //{
        //    using (SqlConnection connection = DapperExecutor.GetConnection())
        //    {
        //        connection.Open();
        //        await connection.ExecuteAsync(query);
        //    }
        //}
    }
}
