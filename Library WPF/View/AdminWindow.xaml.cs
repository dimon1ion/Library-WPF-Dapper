using Library_WPF.Model;
using Library_WPF.Service;
using Library_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        //DataTable data = new DataTable();
        public AdminWindow()
        {
            InitializeComponent();
            this.DataContext = new AdminViewModel(this, grid);
            
            //DapperExecutor dapper = new DapperExecutor();
            //data.Columns.Add("Id");
            //data.Columns.Add("FirstName");
            //data.Columns.Add("LastName");
            //data.Columns.Add("Login");
            //data.Columns.Add("Password");

            //var customer = dapper.Get<Customer>("Select * FROM Customers");

            //foreach (var item in customer)
            //{
            //    DataRow row = data.NewRow();
            //    row["Id"] = item.Id;
            //    row["FirstName"] = item.FirstName;
            //    row["LastName"] = item.LastName;
            //    row["Login"] = item.Login;
            //    row["Password"] = item.Password;
            //    data.Rows.Add(row);

            //}

            //grid.ItemsSource = data.AsDataView();
        }

        private void but_Click(object sender, RoutedEventArgs e)
        {
            //DataRow row = data.NewRow();
            //row["Id"] = 54;
            //row["FirstName"] = "kak";
            //row["LastName"] = "dela";
            //row["Login"] = "a?";
            //row["Password"] = "ответь";
            //data.Rows.Add(row);

            //grid.ItemsSource = data.AsDataView();
        }

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }
    }
}
