using Library_WPF.Model;
using Library_WPF.Model.ShowModel;
using Library_WPF.Service.Interface;
using Library_WPF.View;
using Library_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Library_WPF.Service
{
    internal class LibraryAction
    {
        public IRepository executor { get; set; }

        public IEnumerable<Customer> customers { get; set; }
        public IEnumerable<Salesman> salesmen { get; set; }
        public IEnumerable<Admin> admins { get; set; }
        public IEnumerable<Author> authors { get; set; }
        public IEnumerable<Publisher> publishers { get; set; }
        public IEnumerable<Genre> genres { get; set; }
        public IEnumerable<ShowBook> showBooks { get; set; }
        public IEnumerable<ShowSale> showSales { get; set; }
        public IEnumerable<ShowShelvedBook> showShelvedBooks { get; set; }
        public IEnumerable<Stock> stocks { get; set; }
        public IEnumerable<ShowStockBook> showStockBooks { get; set; }

        public LibraryAction(IRepository executor)
        {
            this.executor = executor;
        }

        public void GetDataTable(TableAction tableAction, out DataTable data)
        {
            data = new DataTable();
            DataRow row;
            switch (tableAction)
            {
                #region Show with return IEnumerable value

                case TableAction.ShowCustomers:

                    data.Columns.Add("Id");
                    data.Columns.Add("FirstName");
                    data.Columns.Add("LastName");
                    data.Columns.Add("Login");
                    data.Columns.Add("Password");

                    customers = executor.Get<Customer>("Select * FROM Customers");

                    foreach (var item in customers)
                    {
                        row = data.NewRow();
                        row["Id"] = item.Id;
                        row["FirstName"] = item.FirstName;
                        row["LastName"] = item.LastName;
                        row["Login"] = item.Login;
                        row["Password"] = item.Password;
                        data.Rows.Add(row);
                    }
                    break;
                case TableAction.ShowManagers:

                    data.Columns.Add("Id");
                    data.Columns.Add("FirstName");
                    data.Columns.Add("LastName");
                    data.Columns.Add("Login");
                    data.Columns.Add("Password");

                    salesmen = executor.Get<Salesman>("Select * FROM Salesmen");

                    foreach (var item in salesmen)
                    {
                        row = data.NewRow();
                        row["Id"] = item.Id;
                        row["FirstName"] = item.FirstName;
                        row["LastName"] = item.LastName;
                        row["Login"] = item.Login;
                        row["Password"] = item.Password;
                        data.Rows.Add(row);
                    }
                    break;
                case TableAction.ShowAdmins:

                    data.Columns.Add("Id");
                    data.Columns.Add("Login");
                    data.Columns.Add("Password");

                    admins = executor.Get<Admin>("Select * FROM Admins");

                    foreach (var item in admins)
                    {
                        row = data.NewRow();
                        row["Id"] = item.Id;
                        row["Login"] = item.Login;
                        row["Password"] = item.Password;
                        data.Rows.Add(row);
                    }
                    break;
                case TableAction.ShowAuthors:

                    data.Columns.Add("Id");
                    data.Columns.Add("Firstname");
                    data.Columns.Add("Lastname");

                    authors = executor.Get<Author>("Select * FROM Authors");

                    foreach (var item in authors)
                    {
                        row = data.NewRow();
                        row["Id"] = item.Id;
                        row["Firstname"] = item.FirstName;
                        row["Lastname"] = item.LastName;
                        data.Rows.Add(row);
                    }
                    break;

                case TableAction.ShowPublishers:

                    data.Columns.Add("Id");
                    data.Columns.Add("Name");

                    publishers = executor.Get<Publisher>("Select * FROM Publishers");

                    foreach (var item in publishers)
                    {
                        row = data.NewRow();
                        row["Id"] = item.Id;
                        row["Name"] = item.Name;
                        data.Rows.Add(row);
                    }
                    break;

                case TableAction.ShowGenres:

                    data.Columns.Add("Id");
                    data.Columns.Add("Name");

                    genres = executor.Get<Genre>("Select * FROM Genres");

                    foreach (var item in genres)
                    {
                        row = data.NewRow();
                        row["Id"] = item.Id;
                        row["Name"] = item.Name;
                        data.Rows.Add(row);
                    }
                    break;

                case TableAction.ShowBooks:

                    data.Columns.Add("Id");
                    data.Columns.Add("Name");
                    data.Columns.Add("Author");
                    data.Columns.Add("Genre");
                    data.Columns.Add("Publisher");
                    data.Columns.Add("Number of pages");
                    data.Columns.Add("Year of publishing");
                    data.Columns.Add("Cost price");
                    data.Columns.Add("Selling price");
                    data.Columns.Add("Continuation");
                    data.Columns.Add("Quantity");

                    showBooks = executor.Get<ShowBook>("SELECT Books.Id, Books.Name[Name], Authors.FirstName + ' ' + Authors.LastName[Author], " +
                        "Genres.Name[Genre], Publishers.Name[Publisher], Books.Number_of_pages, " +
                        "Books.Year_of_publishing, Books.Cost_price, " +
                        "Books.Selling_price, Books.Continuation, Books.Quantity " +
                        "FROM Books, Authors, Publishers, Genres " +
                        "WHERE Books.AuthorId = Authors.Id AND Books.PublisherId = Publishers.Id " +
                        "AND Books.GenreId = Genres.Id");

                    foreach (var item in showBooks)
                    {
                        row = data.NewRow();
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
                        data.Rows.Add(row);
                    }
                    break;

                case TableAction.ShowSales:

                    data.Columns.Add("Id");
                    data.Columns.Add("Book");
                    data.Columns.Add("Customer");
                    data.Columns.Add("Purchase date");
                    data.Columns.Add("Selling price");

                    showSales = executor.Get<ShowSale>("SELECT BookSales.Id, '\"' + Books.Name + '\" ' + Authors.FirstName + ' ' + Authors.LastName[Book], Customers.FirstName + ' ' + Customers.LastName[Customer], " +
                        "BookSales.Purchase_date[PurchaseDate], BookSales.Selling_price[SellingPrice] " +
                        "FROM BookSales, Books, Customers, Authors " +
                        "WHERE BookSales.BookId = Books.Id AND BookSales.CustomerId = Customers.Id " +
                        "AND Books.AuthorId = Authors.Id");

                    foreach (var item in showSales)
                    {
                        row = data.NewRow();
                        row["Id"] = item.Id;
                        row["Book"] = item.Book;
                        row["Customer"] = item.Customer;
                        row["Purchase date"] = item.PurchaseDate.ToString();
                        row["Selling price"] = item.SellingPrice;
                        data.Rows.Add(row);
                    }
                    break;

                case TableAction.ShowShelvedBooks:

                    data.Columns.Add("Id");
                    data.Columns.Add("Book");
                    data.Columns.Add("Customer");

                    showShelvedBooks = executor.Get<ShowShelvedBook>("SELECT ShelvedBooks.Id, '\"' + Books.Name + '\" ' + Authors.FirstName + ' ' + Authors.LastName[BookName], Customers.FirstName + ' ' + Customers.LastName[CustomerName] " +
                        "FROM ShelvedBooks, Books, Customers, Authors " +
                        "WHERE ShelvedBooks.BookId = Books.Id AND ShelvedBooks.CustomerId = Customers.Id " +
                        "AND Books.AuthorId = Authors.Id");

                    foreach (var item in showShelvedBooks)
                    {
                        row = data.NewRow();
                        row["Id"] = item.Id;
                        row["Book"] = item.BookName;
                        row["Customer"] = item.CustomerName;
                        data.Rows.Add(row);
                    }
                    break;

                case TableAction.ShowStocks:

                    data.Columns.Add("Id");
                    data.Columns.Add("Name");

                    stocks = executor.Get<Stock>("SELECT * FROM Stocks");

                    foreach (var item in stocks)
                    {
                        row = data.NewRow();
                        row["Id"] = item.Id;
                        row["Name"] = item.Name;
                        data.Rows.Add(row);
                    }
                    break;

                case TableAction.ShowStockBooks:

                    data.Columns.Add("Id");
                    data.Columns.Add("Stock");
                    data.Columns.Add("Book");
                    data.Columns.Add("Stock percent");

                    showStockBooks = executor.Get<ShowStockBook>("SELECT StocksBooks.Id, Stocks.Name[StockName], '\"' + Books.Name + '\" ' + Authors.FirstName + ' ' + Authors.LastName[BookName], StocksBooks.StockPercent " +
                        "FROM StocksBooks, Stocks, Books, Authors " +
                        "WHERE StocksBooks.StockId = Stocks.Id AND StocksBooks.BookId = Books.Id " +
                        "AND Books.AuthorId = Authors.Id");

                    foreach (var item in showStockBooks)
                    {
                        row = data.NewRow();
                        row["Id"] = item.Id;
                        row["Stock"] = item.StockName;
                        row["Book"] = item.BookName;
                        row["Stock percent"] = item.StockPercent;
                        data.Rows.Add(row);
                    }
                    break;

                #endregion

                case TableAction.None:
                    break;
                default:
                    break;
            }

        }
        public void UpdateGrid(TableAction tableAction, out DataTable _data, ref DataGrid dataGrid)
        {

            if (tableAction != TableAction.None)
            {
                _data = ((DataView)dataGrid.ItemsSource).ToTable();
            }
            else
            {
                _data = null;
                return;
            }
            try
            {
                switch (tableAction)
                {
                    #region UpdateShow

                    case TableAction.ShowCustomers:
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
                                            executor.InsertUpdateDelete(updateQuery, new { FirstName = firstName, LastName = lastName, Login = login, Password = password, Id = id });
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case TableAction.ShowManagers:
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
                                            executor.InsertUpdateDelete(updateQuery, new { FirstName = firstName, LastName = lastName, Login = login, Password = password, Id = id });
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case TableAction.ShowAdmins:
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
                                            executor.InsertUpdateDelete(updateQuery, new { Login = login, Password = password, Id = id });
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case TableAction.ShowAuthors:
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
                                            executor.InsertUpdateDelete(updateQuery, new { FirstName = firstName, LastName = lastName, Id = id });
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case TableAction.ShowPublishers:
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
                                            executor.InsertUpdateDelete(updateQuery, new { Name = name, Id = id });
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case TableAction.ShowGenres:
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
                                            executor.InsertUpdateDelete(updateQuery, new { Name = name, Id = id });
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case TableAction.ShowBooks:
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
                                            executor.InsertUpdateDelete(updateQuery, new
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
                        break;
                    case TableAction.ShowSales:
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
                                            executor.InsertUpdateDelete(updateQuery, new
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
                        break;
                    case TableAction.ShowStocks:
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
                                            executor.InsertUpdateDelete(updateQuery, new { Name = name, Id = id });
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case TableAction.ShowStockBooks:
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
                                            executor.InsertUpdateDelete(updateQuery, new { StockPercent = stockPercent, Id = id });
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        break;

                    #endregion

                    case TableAction.ShowShelvedBooks:
                        return;
                    case TableAction.None:
                        return;
                }
                MessageBox.Show("Update successful!");
            }
            catch (Exception)
            {
                MessageBox.Show("Update error, check the entered data", "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteFromGrid(TableAction tableAction, ref DataGrid dataGrid)
        {
            string deleteQuery = null;
            int id = Int32.Parse(((DataRowView)dataGrid.SelectedItem).Row["Id"].ToString());
            switch (tableAction)
            {
                case TableAction.ShowCustomers:
                    deleteQuery = "DELETE FROM Customers WHERE Customers.Id = @Id";
                    break;
                case TableAction.ShowManagers:
                    deleteQuery = "DELETE FROM Salesmen WHERE Salesmen.Id = @Id";
                    break;
                case TableAction.ShowAdmins:
                    deleteQuery = "DELETE FROM Admins WHERE Admins.Id = @Id";
                    break;
                case TableAction.ShowAuthors:
                    deleteQuery = "DELETE FROM Authors WHERE Authors.Id = @Id";
                    break;
                case TableAction.ShowPublishers:
                    deleteQuery = "DELETE FROM Publishers WHERE Publishers.Id = @Id";
                    break;
                case TableAction.ShowGenres:
                    deleteQuery = "DELETE FROM Genres WHERE Genres.Id = @Id";
                    break;
                case TableAction.ShowBooks:
                    deleteQuery = "DELETE FROM Books WHERE Books.Id = @Id";
                    break;
                case TableAction.ShowSales:
                    deleteQuery = "DELETE FROM BookSales WHERE BookSales.Id = @Id";
                    break;
                case TableAction.ShowShelvedBooks:
                    deleteQuery = "DELETE FROM ShelvedBooks WHERE ShelvedBooks.Id = @Id";
                    break;
                case TableAction.ShowStocks:
                    deleteQuery = "DELETE FROM Stocks WHERE Stocks.Id = @Id";
                    break;
                case TableAction.ShowStockBooks:
                    deleteQuery = "DELETE FROM StocksBooks WHERE StocksBooks.Id = @Id";
                    break;
                case TableAction.None:
                    return;
            }
            executor.InsertUpdateDelete(deleteQuery, new { Id = id });

        }
        public void AddNewValue(TableAction tableAction, ref DataGrid dataGrid)
        {
            string addQuery = null;
            switch (tableAction)
            {
                case TableAction.ShowCustomers:
                    {
                        addQuery = "INSERT INTO Customers(FirstName, LastName, Login, Password) " +
                        "VALUES(@FirstName, @LastName, @Login, @Password)";
                        string firstName = ((DataRowView)dataGrid.SelectedItem).Row["FirstName"].ToString();
                        string lastName = ((DataRowView)dataGrid.SelectedItem).Row["LastName"].ToString();
                        string login = ((DataRowView)dataGrid.SelectedItem).Row["Login"].ToString();
                        string password = ((DataRowView)dataGrid.SelectedItem).Row["Password"].ToString();
                        executor.InsertUpdateDelete(addQuery, new { FirstName = firstName, LastName = lastName, Login = login, Password = password });
                    }
                    break;
                case TableAction.ShowManagers:
                    {
                        addQuery = "INSERT INTO Salesmen(FirstName, LastName, Login, Password) " +
                        "VALUES(@FirstName, @LastName, @Login, @Password)";
                        string firstName = ((DataRowView)dataGrid.SelectedItem).Row["FirstName"].ToString();
                        string lastName = ((DataRowView)dataGrid.SelectedItem).Row["LastName"].ToString();
                        string login = ((DataRowView)dataGrid.SelectedItem).Row["Login"].ToString();
                        string password = ((DataRowView)dataGrid.SelectedItem).Row["Password"].ToString();
                        executor.InsertUpdateDelete(addQuery, new { FirstName = firstName, LastName = lastName, Login = login, Password = password });
                    }
                    break;
                case TableAction.ShowAdmins:
                    {
                        addQuery = "INSERT INTO Admins(Login, Password) " +
                        "VALUES(@Login, @Password)";
                        string login = ((DataRowView)dataGrid.SelectedItem).Row["Login"].ToString();
                        string password = ((DataRowView)dataGrid.SelectedItem).Row["Password"].ToString();
                        executor.InsertUpdateDelete(addQuery, new { Login = login, Password = password });
                    }
                    break;
                case TableAction.ShowAuthors:
                    {
                        addQuery = "INSERT INTO Authors(FirstName, LastName) " +
                        "VALUES(@FirstName, @LastName)";
                        string firstName = ((DataRowView)dataGrid.SelectedItem).Row["FirstName"].ToString();
                        string lastName = ((DataRowView)dataGrid.SelectedItem).Row["LastName"].ToString();
                        executor.InsertUpdateDelete(addQuery, new { FirstName = firstName, LastName = lastName });
                    }
                    break;
                case TableAction.ShowPublishers:
                    {
                        addQuery = "INSERT INTO Publishers(Name) " +
                        "VALUES(@Name)";
                        string name = ((DataRowView)dataGrid.SelectedItem).Row["Name"].ToString();
                        executor.InsertUpdateDelete(addQuery, new { Name = name });
                    }
                    break;
                case TableAction.ShowGenres:
                    {
                        addQuery = "INSERT INTO Genres(Name) " +
                        "VALUES(@Name)";
                        string name = ((DataRowView)dataGrid.SelectedItem).Row["Name"].ToString();
                        executor.InsertUpdateDelete(addQuery, new { Name = name });
                    }
                    break;
                case TableAction.ShowStocks:
                    {
                        addQuery = "INSERT INTO Stocks(Name) " +
                            "VALUES(@Name)";
                        string name = ((DataRowView)dataGrid.SelectedItem).Row["Name"].ToString();
                        executor.InsertUpdateDelete(addQuery, new { Name = name });
                    }
                    break;
                case TableAction.ShowBooks:
                case TableAction.ShowSales:
                case TableAction.ShowShelvedBooks:
                case TableAction.ShowStockBooks:
                    AdditionWindow window = new AdditionWindow(tableAction, executor);
                    window.ShowDialog();
                    break;
                case TableAction.None:
                    break;
                default:
                    break;
            }
        }
    }
}
