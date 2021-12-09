using Library_WPF.Model;
using Library_WPF.Model.ShowModel;
using Library_WPF.Service;
using Library_WPF.Service.Command;
using Library_WPF.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
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
        private IEnumerable<Author> authors;
        private IEnumerable<Publisher> publishers;
        private IEnumerable<Genre> genres;
        private IEnumerable<ShowBook> showBooks;
        private IEnumerable<ShowSale> showSalesViews;
        private IEnumerable<ShowShelvedBook> showShelvedBooks;
        private IEnumerable<Stock> stocks;
        private IEnumerable<ShowStockBook> showStockBooks;
        
        private Command _showCustomers;
        private Command _showManagers;
        private Command _showAdmins;
        private Command _showAuthors;
        private Command _showPublishers;
        private Command _showGenres;
        private Command _showBooks;
        private Command _showSales;
        private Command _showShelvedBooks;
        private Command _showStocks;
        private Command _showStockBooks;

        public ICommand ShowCustomers => _showCustomers;
        public ICommand ShowManagers => _showManagers;
        public ICommand ShowAdmins => _showAdmins;
        public ICommand ShowAuthors => _showAuthors;
        public ICommand ShowPublishers => _showPublishers;
        public ICommand ShowGenres => _showGenres;
        public ICommand ShowBooks => _showBooks;
        public ICommand ShowSales => _showSales;
        public ICommand ShowShelvedBooks => _showShelvedBooks;
        public ICommand ShowStocks => _showStocks;
        public ICommand ShowStockBooks => _showStockBooks;

        public AdminViewModel(AdminWindow adminWindow, DataGrid dataGrid)
        {
            //_data = ((DataView)dataGrid.ItemsSource).ToTable();
            //MessageBox.Show(dataGrid.SelectedItem.ToString());
            //MessageBox.Show(((DataRowView)dataGrid.SelectedItem).Row["Id"].ToString());
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
            }); // Ready

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
            }); // Ready

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
            }); // Ready

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
            }); // Ready

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
            }); // Ready

            _showGenres = new Command(obj =>
            {
                _data = new DataTable();

                _data.Columns.Add("Id");
                _data.Columns.Add("Name");

                genres = dapper.Get<Genre>("Select * FROM Genres");

                DataRow row;
                foreach (var item in genres)
                {
                    row = _data.NewRow();
                    row["Id"] = item.Id;
                    row["Name"] = item.Name;
                    _data.Rows.Add(row);
                }

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

            _showBooks = new Command(obj =>
            {
                _data = new DataTable();

                _data.Columns.Add("Id");
                _data.Columns.Add("Name");
                _data.Columns.Add("Author");
                _data.Columns.Add("Genre");
                _data.Columns.Add("Publisher");
                _data.Columns.Add("Number of pages");
                _data.Columns.Add("Year of publishing");
                _data.Columns.Add("Cost price");
                _data.Columns.Add("Selling price");
                _data.Columns.Add("Continuation");
                _data.Columns.Add("Quantity");

                showBooks = dapper.Get<ShowBook>("SELECT Books.Id, Books.Name[Name], Authors.FirstName + ' ' + Authors.LastName[Author], " +
                    "Genres.Name[Genre], Publishers.Name[Publisher], Books.Number_of_pages, " +
                    "Books.Year_of_publishing, Books.Cost_price, " +
                    "Books.Selling_price, Books.Continuation, Books.Quantity " +
                    "FROM Books, Authors, Publishers, Genres " +
                    "WHERE Books.AuthorId = Authors.Id AND Books.PublisherId = Publishers.Id " +
                    "AND Books.GenreId = Genres.Id");

                DataRow row;
                foreach (var item in showBooks)
                {
                    row = _data.NewRow();
                    row["Id"] = item.Id;
                    row["Name"] = item.Name;
                    row["Author"] = item.Author;
                    row["Publisher"] = item.Publisher;
                    row["Genre"] = item.Genre;
                    row["Number of pages"] = item.Number_of_pages;
                    row["Year of publishing"] = item.Year_of_publishing;
                    row["Cost price"] = item.Cost_price;
                    row["Selling price"] = item.Selling_price;
                    row["Continuation"] = item.Continuation;
                    row["Quantity"] = item.Quantity;
                    _data.Rows.Add(row);
                }

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

            _showSales = new Command(obj =>
            {
                _data = new DataTable();

                _data.Columns.Add("Id");
                _data.Columns.Add("Book");
                _data.Columns.Add("Customer");
                _data.Columns.Add("Purchase date");
                _data.Columns.Add("Selling price");

                showSalesViews = dapper.Get<ShowSale>("SELECT BookSales.Id, '\"' + Books.Name + '\" ' + Authors.FirstName + ' ' + Authors.LastName[Book], Customers.FirstName + ' ' + Customers.LastName[Customer], " +
                    "BookSales.Purchase_date[PurchaseDate], BookSales.Selling_price[SellingPrice] " +
                    "FROM BookSales, Books, Customers, Authors " +
                    "WHERE BookSales.BookId = Books.Id AND BookSales.CustomerId = Customers.Id " +
                    "AND Books.AuthorId = Authors.Id");

                DataRow row;
                foreach (var item in showSalesViews)
                {
                    row = _data.NewRow();
                    row["Id"] = item.Id;
                    row["Book"] = item.Book;
                    row["Customer"] = item.Customer;
                    row["Purchase date"] = item.PurchaseDate;
                    row["Selling price"] = item.SellingPrice;
                    _data.Rows.Add(row);
                }

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

            _showShelvedBooks = new Command(obj =>
            {
                _data = new DataTable();

                _data.Columns.Add("Id");
                _data.Columns.Add("Book");
                _data.Columns.Add("Customer");

                showShelvedBooks = dapper.Get<ShowShelvedBook>("SELECT ShelvedBooks.Id, '\"' + Books.Name + '\" ' + Authors.FirstName + ' ' + Authors.LastName[BookName], Customers.FirstName + ' ' + Customers.LastName[CustomerName] " +
                    "FROM ShelvedBooks, Books, Customers, Authors " +
                    "WHERE ShelvedBooks.BookId = Books.Id AND ShelvedBooks.CustomerId = Customers.Id " +
                    "AND Books.AuthorId = Authors.Id");

                DataRow row;
                foreach (var item in showShelvedBooks)
                {
                    row = _data.NewRow();
                    row["Id"] = item.Id;
                    row["Book"] = item.BookName;
                    row["Customer"] = item.CustomerName;
                    _data.Rows.Add(row);
                }

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

            _showStocks = new Command(obj =>
            {
                _data = new DataTable();

                _data.Columns.Add("Id");
                _data.Columns.Add("Name");

                stocks = dapper.Get<Stock>("SELECT * FROM Stocks");

                DataRow row;
                foreach (var item in stocks)
                {
                    row = _data.NewRow();
                    row["Id"] = item.Id;
                    row["Name"] = item.Name;
                    _data.Rows.Add(row);
                }

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

            _showStockBooks = new Command(obj =>
            {
                _data = new DataTable();

                _data.Columns.Add("Id");
                _data.Columns.Add("Stock");
                _data.Columns.Add("Book");
                _data.Columns.Add("Stock precent");

                showStockBooks = dapper.Get<ShowStockBook>("SELECT StocksBooks.Id, Stocks.Name[StockName], '\"' + Books.Name + '\" ' + Authors.FirstName + ' ' + Authors.LastName[BookName], StocksBooks.StockPercent " +
                    "FROM StocksBooks, Stocks, Books, Authors " +
                    "WHERE StocksBooks.StockId = Stocks.Id AND StocksBooks.BookId = Books.Id " +
                    "AND Books.AuthorId = Authors.Id");

                DataRow row;
                foreach (var item in showStockBooks)
                {
                    row = _data.NewRow();
                    row["Id"] = item.Id;
                    row["Stock"] = item.StockName;
                    row["Book"] = item.BookName;
                    row["Stock precent"] = item.StockPercent;
                    _data.Rows.Add(row);
                }

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

        }
    }
}
