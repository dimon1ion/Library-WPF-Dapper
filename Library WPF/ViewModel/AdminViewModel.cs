using Library_WPF.Model;
using Library_WPF.Model.ShowModel;
using Library_WPF.Service;
using Library_WPF.Service.Command;
using Library_WPF.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Library_WPF.ViewModel
{
    public enum ShowTable
    {
        ShowCustomers = 1,
        ShowManagers,
        ShowAdmins,
        ShowAuthors,
        ShowPublishers,
        ShowGenres,
        ShowBooks,
        ShowSales,
        ShowShelvedBooks,
        ShowStocks,
        ShowStockBooks,
        None
    }
    public class AdminViewModel : INotifyPropertyChanged
    {
        private DataTable _data;
        private DapperExecutor _dapper;

        private ShowTable _showAction;
        private IEnumerable<Customer> customers;
        private IEnumerable<Salesman> salesmen;
        private IEnumerable<Admin> admins;
        private IEnumerable<Author> authors;
        private IEnumerable<Publisher> publishers;
        private IEnumerable<Genre> genres;
        private IEnumerable<ShowBook> showBooks;
        private IEnumerable<ShowSale> showSales;
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
        private Command _update;
        private Command _delete;

        private string _deleteIsEnable;

        public bool DeleteIsEnable
        {
            get => Boolean.Parse(_deleteIsEnable);
            set
            {
                if (!Equals(Boolean.Parse(_deleteIsEnable), value))
                {
                    _deleteIsEnable = value.ToString();
                    OnPropertyChanged(nameof(DeleteIsEnable));
                }
            }
        }




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
        public ICommand Update => _update;
        public ICommand Delete => _delete;

        public AdminViewModel(AdminWindow adminWindow, DataGrid dataGrid)
        {
            //_data = ((DataView)dataGrid.ItemsSource).ToTable();
            //MessageBox.Show(dataGrid.SelectedItem.ToString());
            //MessageBox.Show(((DataRowView)dataGrid.SelectedItem).Row["Id"].ToString());
            _data = new DataTable();
            _dapper = new DapperExecutor();
            _showAction = ShowTable.None;

            _showCustomers = new Command(obj =>
            {
                _data = new DataTable();
                _showAction = ShowTable.ShowCustomers;

                _data.Columns.Add("Id");
                _data.Columns.Add("FirstName");
                _data.Columns.Add("LastName");
                _data.Columns.Add("Login");
                _data.Columns.Add("Password");

                customers = _dapper.Get<Customer>("Select * FROM Customers");

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
                _showAction = ShowTable.ShowManagers;

                _data.Columns.Add("Id");
                _data.Columns.Add("FirstName");
                _data.Columns.Add("LastName");
                _data.Columns.Add("Login");
                _data.Columns.Add("Password");

                salesmen = _dapper.Get<Salesman>("Select * FROM Salesmen");

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
                _showAction = ShowTable.ShowAdmins;

                _data.Columns.Add("Id");
                _data.Columns.Add("Login");
                _data.Columns.Add("Password");

                admins = _dapper.Get<Admin>("Select * FROM Admins");

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
                _showAction = ShowTable.ShowAuthors;

                _data.Columns.Add("Id");
                _data.Columns.Add("Firstname");
                _data.Columns.Add("Lastname");

                authors = _dapper.Get<Author>("Select * FROM Authors");

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
                _showAction = ShowTable.ShowPublishers;

                _data.Columns.Add("Id");
                _data.Columns.Add("Name");

                publishers = _dapper.Get<Publisher>("Select * FROM Publishers");

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
                _showAction = ShowTable.ShowGenres;

                _data.Columns.Add("Id");
                _data.Columns.Add("Name");

                genres = _dapper.Get<Genre>("Select * FROM Genres");

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
                _showAction = ShowTable.ShowBooks;

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

                showBooks = _dapper.Get<ShowBook>("SELECT Books.Id, Books.Name[Name], Authors.FirstName + ' ' + Authors.LastName[Author], " +
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
                _showAction = ShowTable.ShowSales;

                _data.Columns.Add("Id");
                _data.Columns.Add("Book");
                _data.Columns.Add("Customer");
                _data.Columns.Add("Purchase date");
                _data.Columns.Add("Selling price");

                showSales = _dapper.Get<ShowSale>("SELECT BookSales.Id, '\"' + Books.Name + '\" ' + Authors.FirstName + ' ' + Authors.LastName[Book], Customers.FirstName + ' ' + Customers.LastName[Customer], " +
                    "BookSales.Purchase_date[PurchaseDate], BookSales.Selling_price[SellingPrice] " +
                    "FROM BookSales, Books, Customers, Authors " +
                    "WHERE BookSales.BookId = Books.Id AND BookSales.CustomerId = Customers.Id " +
                    "AND Books.AuthorId = Authors.Id");

                DataRow row;
                foreach (var item in showSales)
                {
                    row = _data.NewRow();
                    row["Id"] = item.Id;
                    row["Book"] = item.Book;
                    row["Customer"] = item.Customer;
                    row["Purchase date"] = item.PurchaseDate.ToString();
                    row["Selling price"] = item.SellingPrice;
                    _data.Rows.Add(row);
                }

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

            _showShelvedBooks = new Command(obj =>
            {
                _data = new DataTable();
                _showAction = ShowTable.ShowShelvedBooks;

                _data.Columns.Add("Id");
                _data.Columns.Add("Book");
                _data.Columns.Add("Customer");

                showShelvedBooks = _dapper.Get<ShowShelvedBook>("SELECT ShelvedBooks.Id, '\"' + Books.Name + '\" ' + Authors.FirstName + ' ' + Authors.LastName[BookName], Customers.FirstName + ' ' + Customers.LastName[CustomerName] " +
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
                _showAction = ShowTable.ShowStocks;

                _data.Columns.Add("Id");
                _data.Columns.Add("Name");

                stocks = _dapper.Get<Stock>("SELECT * FROM Stocks");

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
                _showAction = ShowTable.ShowStockBooks;

                _data.Columns.Add("Id");
                _data.Columns.Add("Stock");
                _data.Columns.Add("Book");
                _data.Columns.Add("Stock percent");

                showStockBooks = _dapper.Get<ShowStockBook>("SELECT StocksBooks.Id, Stocks.Name[StockName], '\"' + Books.Name + '\" ' + Authors.FirstName + ' ' + Authors.LastName[BookName], StocksBooks.StockPercent " +
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
                    row["Stock percent"] = item.StockPercent;
                    _data.Rows.Add(row);
                }

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

            _update = new Command(obj =>
            {
                if (_showAction != ShowTable.None)
                {
                    _data = ((DataView)dataGrid.ItemsSource).ToTable();
                }
                try
                {
                    switch (_showAction)
                    {
                        case ShowTable.ShowCustomers:
                            {
                                string updateQuery = "Update Customers SET FirstName = @FirstName, LastName = @LastName, Login = @Login," +
                                " Password = @Password WHERE Id = @Id";
                                string firstName, lastName, login, password;
                                bool insert;
                                int id;

                                foreach (DataRow row in _data.Rows)
                                {
                                    foreach (Customer customer in customers)
                                    {
                                        if (row["Id"].ToString() == customer.Id.ToString())
                                        {
                                            insert = false;
                                            firstName = customer.FirstName;
                                            lastName = customer.LastName;
                                            login = customer.Login;
                                            password = customer.Password;
                                            id = customer.Id;

                                            if (row["FirstName"].ToString() != customer.FirstName) { firstName = row["FirstName"].ToString(); insert = true; }
                                            if (row["LastName"].ToString() != customer.LastName) { lastName = row["LastName"].ToString(); insert = true; }
                                            if (row["Login"].ToString() != customer.LastName) { login = row["Login"].ToString(); insert = true; }
                                            if (row["Password"].ToString() != customer.LastName) { password = row["Password"].ToString(); insert = true; }

                                            if (insert)
                                            {
                                                _dapper.InsertUpdate(updateQuery, new { FirstName = firstName, LastName = lastName, Login = login, Password = password, Id = id });
                                            }

                                            break;
                                        }
                                    }
                                }
                            }
                            _showCustomers.Execute(null);
                            break;
                        case ShowTable.ShowManagers:
                            {
                                string updateQuery = "Update Salesmen SET FirstName = @FirstName, LastName = @LastName, Login = @Login," +
                                " Password = @Password WHERE Id = @Id";
                                string firstName, lastName, login, password;
                                bool insert;
                                int id;

                                foreach (DataRow row in _data.Rows)
                                {
                                    foreach (Salesman salesman in salesmen)
                                    {
                                        if (row["Id"].ToString() == salesman.Id.ToString())
                                        {
                                            insert = false;
                                            firstName = salesman.FirstName;
                                            lastName = salesman.LastName;
                                            login = salesman.Login;
                                            password = salesman.Password;
                                            id = salesman.Id;

                                            if (row["FirstName"].ToString() != salesman.FirstName) { firstName = row["FirstName"].ToString(); insert = true; }
                                            if (row["LastName"].ToString() != salesman.LastName) { lastName = row["LastName"].ToString(); insert = true; }
                                            if (row["Login"].ToString() != salesman.LastName) { login = row["Login"].ToString(); insert = true; }
                                            if (row["Password"].ToString() != salesman.LastName) { password = row["Password"].ToString(); insert = true; }

                                            if (insert)
                                            {
                                                _dapper.InsertUpdate(updateQuery, new { FirstName = firstName, LastName = lastName, Login = login, Password = password, Id = id });
                                            }

                                            break;
                                        }
                                    }
                                }
                            }
                            _showManagers.Execute(null);
                            break;
                        case ShowTable.ShowAdmins:
                            {
                                string updateQuery = "Update Admins SET Login = @Login," +
                                " Password = @Password WHERE Id = @Id";
                                string login, password;
                                bool insert;
                                int id;

                                foreach (DataRow row in _data.Rows)
                                {
                                    foreach (Salesman salesman in salesmen)
                                    {
                                        if (row["Id"].ToString() == salesman.Id.ToString())
                                        {
                                            insert = false;
                                            login = salesman.Login;
                                            password = salesman.Password;
                                            id = salesman.Id;

                                            if (row["Login"].ToString() != salesman.LastName) { login = row["Login"].ToString(); insert = true; }
                                            if (row["Password"].ToString() != salesman.LastName) { password = row["Password"].ToString(); insert = true; }

                                            if (insert)
                                            {
                                                _dapper.InsertUpdate(updateQuery, new { Login = login, Password = password, Id = id });
                                            }

                                            break;
                                        }
                                    }
                                }
                            }
                            _showAdmins.Execute(null);
                            break;
                        case ShowTable.ShowAuthors:
                            {
                                string updateQuery = "Update Authors SET FirstName = @FirstName, LastName = @LastName " +
                                "WHERE Id = @Id";
                                string firstName, lastName;
                                bool insert;
                                int id;

                                foreach (DataRow row in _data.Rows)
                                {
                                    foreach (Author author in authors)
                                    {
                                        if (row["Id"].ToString() == author.Id.ToString())
                                        {
                                            insert = false;
                                            firstName = author.FirstName;
                                            lastName = author.LastName;
                                            id = author.Id;

                                            if (row["FirstName"].ToString() != author.FirstName) { firstName = row["FirstName"].ToString(); insert = true; }
                                            if (row["LastName"].ToString() != author.LastName) { lastName = row["LastName"].ToString(); insert = true; }

                                            if (insert)
                                            {
                                                _dapper.InsertUpdate(updateQuery, new { FirstName = firstName, LastName = lastName, Id = id });
                                            }

                                            break;
                                        }
                                    }
                                }
                            }
                            _showAuthors.Execute(null);
                            break;
                        case ShowTable.ShowPublishers:
                            {
                                string updateQuery = "Update Publishers SET Name = @Name " +
                                "WHERE Id = @Id";
                                string name;
                                bool insert;
                                int id;

                                foreach (DataRow row in _data.Rows)
                                {
                                    foreach (Publisher publisher in publishers)
                                    {
                                        if (row["Id"].ToString() == publisher.Id.ToString())
                                        {
                                            insert = false;
                                            name = publisher.Name;
                                            id = publisher.Id;

                                            if (row["Name"].ToString() != publisher.Name) { name = row["Name"].ToString(); insert = true; }

                                            if (insert)
                                            {
                                                _dapper.InsertUpdate(updateQuery, new { Name = name, Id = id });
                                            }

                                            break;
                                        }
                                    }
                                }
                            }
                            _showPublishers.Execute(null);
                            break;
                        case ShowTable.ShowGenres:
                            {
                                string updateQuery = "Update Genres SET Name = @Name " +
                                "WHERE Id = @Id";
                                string name;
                                bool insert;
                                int id;

                                foreach (DataRow row in _data.Rows)
                                {
                                    foreach (Genre genre in genres)
                                    {
                                        if (row["Id"].ToString() == genre.Id.ToString())
                                        {
                                            insert = false;
                                            name = genre.Name;
                                            id = genre.Id;

                                            if (row["Name"].ToString() != genre.Name) { name = row["Name"].ToString(); insert = true; }

                                            if (insert)
                                            {
                                                _dapper.InsertUpdate(updateQuery, new { Name = name, Id = id });
                                            }

                                            break;
                                        }
                                    }
                                }
                            }
                            _showGenres.Execute(null);
                            break;
                        case ShowTable.ShowBooks:
                            {
                                IEnumerable<Book> books = from b in showBooks
                                                          select new Book()
                                                          {
                                                              Id = b.Id,
                                                              Name = b.Name,
                                                              Continuation = b.Continuation,
                                                              Cost_price = b.Cost_price,
                                                              Number_of_pages = b.Number_of_pages,
                                                              Quantity = b.Quantity,
                                                              Selling_price = b.Selling_price,
                                                              Year_of_publishing = b.Year_of_publishing
                                                          };
                                string updateQuery = "UPDATE Books " +
                                "Set Name = @Name, Number_of_pages = @Number_of_pages, Year_of_publishing = @Year_of_publishing, Cost_price = @Cost_price, " +
                                "Selling_price = @Selling_price, Continuation = @Continuation, Quantity = @Quantity " +
                                "Where Id = @Id";
                                string name;
                                bool insert, continuation;
                                int id, number_of_pages, year_of_publishing, quantity;
                                decimal cost_price, selling_price;

                                foreach (DataRow row in _data.Rows)
                                {
                                    foreach (Book book in books)
                                    {
                                        if (row["Id"].ToString() == book.Id.ToString())
                                        {
                                            insert = false;
                                            name = book.Name;
                                            number_of_pages = book.Number_of_pages;
                                            year_of_publishing = book.Year_of_publishing;
                                            cost_price = book.Cost_price;
                                            selling_price = book.Selling_price;
                                            continuation = book.Continuation;
                                            quantity = book.Quantity;
                                            id = book.Id;


                                            if (row["Name"].ToString() != book.Name) { name = row["Name"].ToString(); insert = true; }
                                            if (row["Number of pages"].ToString() != book.Number_of_pages.ToString()) { number_of_pages = Int32.Parse(row["Number of pages"].ToString()); insert = true; }
                                            if (row["Year of publishing"].ToString() != book.Year_of_publishing.ToString()) { year_of_publishing = Int32.Parse(row["Year of publishing"].ToString()); insert = true; }
                                            if (row["Cost price"].ToString() != book.Cost_price.ToString()) { cost_price = decimal.Parse(row["Cost price"].ToString()); insert = true; }
                                            if (row["Selling price"].ToString() != book.Selling_price.ToString()) { selling_price = Decimal.Parse(row["Selling price"].ToString()); insert = true; }
                                            if (row["Continuation"].ToString() != book.Continuation.ToString()) { continuation = Boolean.Parse(row["Continuation"].ToString()); insert = true; }
                                            if (row["Quantity"].ToString() != book.Quantity.ToString()) { quantity = Int32.Parse(row["Quantity"].ToString()); insert = true; }

                                            if (insert)
                                            {
                                                _dapper.InsertUpdate(updateQuery, new
                                                {
                                                    Name = name,
                                                    Number_of_pages = number_of_pages,
                                                    Year_of_publishing = year_of_publishing,
                                                    Cost_price = cost_price,
                                                    Selling_price = selling_price,
                                                    Continuation = continuation,
                                                    Quantity = quantity,
                                                    Id = id
                                                });
                                            }

                                            break;
                                        }
                                    }
                                }
                            }
                            _showBooks.Execute(null);
                            break;
                        case ShowTable.ShowSales:
                            {
                                IEnumerable<Sale> sales = from s in showSales
                                                          select new Sale()
                                                          {
                                                              Id = s.Id,
                                                              PurchaseDate = s.PurchaseDate,
                                                              SellingPrice = s.SellingPrice
                                                          };
                                string updateQuery = "UPDATE BookSales " +
                                "SET Purchase_date = @Purchase_date, Selling_price = @Selling_price " +
                                "Where Id = @Id";
                                bool insert;
                                int id;
                                decimal selling_price;
                                DateTime purchase_date;

                                foreach (DataRow row in _data.Rows)
                                {
                                    foreach (Sale sale in sales)
                                    {
                                        if (row["Id"].ToString() == sale.Id.ToString())
                                        {
                                            insert = false;
                                            selling_price = sale.SellingPrice;
                                            purchase_date = sale.PurchaseDate;
                                            id = sale.Id;


                                            if (row["Purchase date"].ToString() != sale.PurchaseDate.ToString()) { purchase_date = DateTime.Parse(row["Purchase date"].ToString()); insert = true; }
                                            if (row["Selling price"].ToString() != sale.SellingPrice.ToString()) { selling_price = Decimal.Parse(row["Selling price"].ToString()); insert = true; }

                                            if (insert)
                                            {
                                                _dapper.InsertUpdate(updateQuery, new
                                                {
                                                    Purchase_date = purchase_date,
                                                    Selling_price = selling_price,
                                                    Id = id
                                                });
                                            }

                                            break;
                                        }
                                    }
                                }
                            }
                            _showSales.Execute(null);
                            break;
                        case ShowTable.ShowShelvedBooks:
                            return;
                        case ShowTable.ShowStocks:
                            {
                                string updateQuery = "UPDATE Stocks SET Name = @Name " +
                                "WHERE Id = @Id";
                                string name;
                                bool insert;
                                int id;

                                foreach (DataRow row in _data.Rows)
                                {
                                    foreach (Stock stock in stocks)
                                    {
                                        if (row["Id"].ToString() == stock.Id.ToString())
                                        {
                                            insert = false;
                                            name = stock.Name;
                                            id = stock.Id;

                                            if (row["Name"].ToString() != stock.Name) { name = row["Name"].ToString(); insert = true; }

                                            if (insert)
                                            {
                                                _dapper.InsertUpdate(updateQuery, new { Name = name, Id = id });
                                            }

                                            break;
                                        }
                                    }
                                }
                            }
                            _showStocks.Execute(null);
                            break;
                        case ShowTable.ShowStockBooks:
                            {
                                IEnumerable<StockBook> stockBooks = from sb in showStockBooks select new StockBook() { Id = sb.Id, StockPercent = sb.StockPercent };
                                string updateQuery = "UPDATE StocksBooks SET StockPercent = @StockPercent " +
                                "WHERE Id = @Id";
                                bool insert;
                                int id, stockPercent;

                                foreach (DataRow row in _data.Rows)
                                {
                                    foreach (StockBook stockBook in stockBooks)
                                    {
                                        if (row["Id"].ToString() == stockBook.Id.ToString())
                                        {
                                            insert = false;
                                            stockPercent = stockBook.StockPercent;
                                            id = stockBook.Id;

                                            if (row["Stock percent"].ToString() != stockBook.StockPercent.ToString()) { stockPercent = Int32.Parse(row["Stock percent"].ToString()); insert = true; }

                                            if (insert)
                                            {
                                                if (stockPercent > 100) { stockPercent = 100; }
                                                else if (stockPercent < 1) { stockPercent = 1; }
                                                _dapper.InsertUpdate(updateQuery, new { StockPercent = stockPercent, Id = id });
                                            }

                                            break;
                                        }
                                    }
                                }
                            }
                            _showStockBooks.Execute(null);
                            break;
                        case ShowTable.None:
                            return;
                    }
                    MessageBox.Show("Update successful");
                }
                catch (Exception)
                {
                    MessageBox.Show("Update error, check the entered data", "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }, obj =>
            {
                return _showAction == ShowTable.ShowShelvedBooks;
            });

            _delete = new Command(obj =>
            {

            }, obj =>
            {
                return true;
            });

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string properyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }
    }
}
