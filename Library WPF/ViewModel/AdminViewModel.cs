using Library_WPF.Model;
using Library_WPF.Model.ShowModelView;
using Library_WPF.Service;
using Library_WPF.Service.Command;
using Library_WPF.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace Library_WPF.ViewModel
{
    public enum ShowTable
    {
        ShowCustomers = 1,
        ShowSalesmen,
        ShowAdmins,
        ShowBookViews
    }
    public class AdminViewModel
    {
        private DataTable _data;
        private DapperExecutor dapper;
        private IEnumerable<Customer> customers;
        private IEnumerable<Salesman> salesmen;
        private IEnumerable<Admin> admins;
        private IEnumerable<ShowBookView> showBookViews;
        private IEnumerable<Author> authors;
        private IEnumerable<Publisher> publishers;
        private IEnumerable<ShowSalesView> showSalesViews;
        
        private Command _showCustomers;
        private Command _showManagers;
        private Command _showAdmins;
        private Command _showBooks;
        private Command _showAuthors;
        private Command _showPublishers;
        private Command _showSales;

        public ICommand ShowCustomers => _showCustomers;
        public ICommand ShowManagers => _showManagers;
        public ICommand ShowAdmins => _showAdmins;
        public ICommand ShowBooks => _showBooks;
        public ICommand ShowAuthors => _showAuthors;
        public ICommand ShowPublishers => _showPublishers;
        public ICommand ShowSales => _showSales;

        public AdminViewModel(AdminWindow adminWindow, DataGrid dataGrid)
        {
            _data = new DataTable();
            dapper = new DapperExecutor();

            _showCustomers = new Command(obj =>
            {
                _data = new DataTable();

                _data.Columns.Add("Id");
                _data.Columns.Add("FirstName");
                _data.Columns.Add("LastName");
                _data.Columns.Add("Login");
                _data.Columns.Add("Password");

                customers = dapper.Get<Customer>("Select * FROM Customers");

                DataRow row;
                foreach (var item in customers)
                {
                    row = _data.NewRow();
                    row["Id"] = item.Id;
                    row["FirstName"] = item.FirstName;
                    row["LastName"] = item.LastName;
                    row["Login"] = item.Login;
                    row["Password"] = item.Password;
                    _data.Rows.Add(row);
                }

                dataGrid.ItemsSource = _data.AsDataView();
            });

            _showManagers = new Command(obj =>
            {
                _data = new DataTable();

                _data.Columns.Add("Id");
                _data.Columns.Add("FirstName");
                _data.Columns.Add("LastName");
                _data.Columns.Add("Login");
                _data.Columns.Add("Password");

                salesmen = dapper.Get<Salesman>("Select * FROM Salesmen");

                DataRow row;
                foreach (var item in salesmen)
                {
                    row = _data.NewRow();
                    row["Id"] = item.Id;
                    row["FirstName"] = item.FirstName;
                    row["LastName"] = item.LastName;
                    row["Login"] = item.Login;
                    row["Password"] = item.Password;
                    _data.Rows.Add(row);
                }

                dataGrid.ItemsSource = _data.AsDataView();
            });

            _showAdmins = new Command(obj =>
            {
                _data = new DataTable();

                _data.Columns.Add("Id");
                _data.Columns.Add("Login");
                _data.Columns.Add("Password");

                admins = dapper.Get<Admin>("Select * FROM Admins");

                DataRow row;
                foreach (var item in admins)
                {
                    row = _data.NewRow();
                    row["Id"] = item.Id;
                    row["Login"] = item.Login;
                    row["Password"] = item.Password;
                    _data.Rows.Add(row);
                }

                dataGrid.ItemsSource = _data.AsDataView();
            });

            _showBooks = new Command(obj =>
            {
                _data = new DataTable();

                _data.Columns.Add("Id");
                _data.Columns.Add("Name");
                _data.Columns.Add("Author First Name");
                _data.Columns.Add("Author Last Name");
                _data.Columns.Add("Genre");
                _data.Columns.Add("Publisher Name");
                _data.Columns.Add("Number of pages");
                _data.Columns.Add("Year of publishing");
                _data.Columns.Add("Cost price");
                _data.Columns.Add("Selling price");
                _data.Columns.Add("Continuation");
                _data.Columns.Add("Quantity");

                showBookViews = dapper.Get<ShowBookView>("SELECT Books.Id, Authors.FirstName, Authors.LastName, " +
                    "Books.Genre, Publishers.Name, Books.Number_of_pages, " +
                    "Books.Year_of_publishing, Books.Cost_price, " +
                    "Books.Selling_price, Books.Continuation, Books.Quantity " +
                    "FROM Books, Authors, Publishers " +
                    "WHERE Books.AuthorId = Authors.Id AND Books.PublisherId = Publishers.Id");

                DataRow row;
                foreach (var item in showBookViews)
                {
                    row = _data.NewRow();
                    row["Id"] = item.Id;
                    row["Name"] = item.AuthorFirstName;
                    row["Author First Name"] = item.AuthorFirstName;
                    row["Author Last Name"] = item.AuthorLastName;
                    row["Publisher Name"] = item.PublisherName;
                    row["Number of pages"] = item.Number_of_pages;
                    row["Year of publishing"] = item.Year_of_publishing;
                    row["Cost price"] = item.Cost_price;
                    row["Selling price"] = item.Selling_price;
                    row["Continuation"] = item.Continuation;
                    row["Quantity"] = item.Quantity;
                    _data.Rows.Add(row);
                }

                dataGrid.ItemsSource = _data.AsDataView();
            });

            _showAuthors = new Command(obj =>
            {
                _data = new DataTable();

                _data.Columns.Add("Id");
                _data.Columns.Add("Firstname");
                _data.Columns.Add("Lastname");

                authors = dapper.Get<Author>("Select * FROM Authors");

                DataRow row;
                foreach (var item in authors)
                {
                    row = _data.NewRow();
                    row["Id"] = item.Id;
                    row["Firstname"] = item.FirstName;
                    row["Lastname"] = item.LastName;
                    _data.Rows.Add(row);
                }

                dataGrid.ItemsSource = _data.AsDataView();
            });

            _showPublishers = new Command(obj =>
            {
                _data = new DataTable();

                _data.Columns.Add("Id");
                _data.Columns.Add("Name");

                publishers = dapper.Get<Publisher>("Select * FROM Publishers");

                DataRow row;
                foreach (var item in publishers)
                {
                    row = _data.NewRow();
                    row["Id"] = item.Id;
                    row["Name"] = item.Name;
                    _data.Rows.Add(row);
                }

                dataGrid.ItemsSource = _data.AsDataView();
            });

        }
    }
}
