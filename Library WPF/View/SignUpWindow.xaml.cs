using Library_WPF.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public SignUpWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            if (TextBoxFirstName.Text == String.Empty)
            {
                ErrorFirstNameText.Text = "* Enter first name";
                error = true;
            }
            else { ErrorFirstNameText.Text = String.Empty; }

            if (TextBoxLastName.Text == String.Empty)
            {
                ErrorLastNameText.Text = "* Enter last name";
                error = true;
            }
            else { ErrorLastNameText.Text = String.Empty; }

            if (TextBoxLogin.Text == String.Empty)
            {
                ErrorLoginText.Text = "* Enter login";
                error = true;
            }
            else { ErrorLoginText.Text = String.Empty; }

            if (PasswordBoxPass.Password == String.Empty)
            {
                ErrorPasswordText.Text = "* Enter password";
                error = true;
            }
            else { ErrorPasswordText.Text = String.Empty; }

            if (PasswordBoxSecondPass.Password == String.Empty)
            {
                ErrorPasswordAgainText.Text = "* Enter password";
                error = true;
            }
            else if(PasswordBoxSecondPass.Password != PasswordBoxPass.Password)
            {
                ErrorPasswordAgainText.Text = "* Password entered incorrectly";
                error = true;
            }
            else { ErrorPasswordAgainText.Text = String.Empty; }

            if (!error)
            {
                using (SqlConnection connection = DapperExecutor.GetConnection())
                {
                    connection.Open();
                    MessageBox.Show("Test");
                }
            }
        }
    }
}
