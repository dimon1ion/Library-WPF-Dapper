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
    }
}
