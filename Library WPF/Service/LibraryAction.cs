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

        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Salesman> Salesmen { get; set; }
        public IEnumerable<Admin> Admins { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<ShowPopularAuthor> PopularAuthors { get; set; }
        public IEnumerable<Publisher> Publishers { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<ShowPopularGenre> PopularGenres { get; set; }
        public IEnumerable<ShowBook> ShowBooks { get; set; }
        public IEnumerable<ShowSale> ShowSales { get; set; }
        public IEnumerable<ShowShelvedBook> ShowShelvedBooks { get; set; }
        public IEnumerable<Stock> Stocks { get; set; }
        public IEnumerable<ShowStockBook> ShowStockBooks { get; set; }
        public IEnumerable<ShowBookForCustomer> ShowBookForCustomers { get; set; }

        public LibraryAction(IRepository executor)
        {
            this.executor = executor;
        }

        public void GetDataTableWithParams(TableActionWithParam tableAction, out DataTable data, object param1)
        {
            data = new DataTable();
            DataRow row;
            switch (tableAction)
            {
                case TableActionWithParam.FindInCustBookbyName:
                case TableActionWithParam.FindInCustBookbyAuthor:
                case TableActionWithParam.FindInCustBookbyGenre:

                    data.Columns.Add("Name");
                    data.Columns.Add("Author");
                    data.Columns.Add("Genre");
                    data.Columns.Add("Publisher");
                    data.Columns.Add("Number of pages");
                    data.Columns.Add("Year of publishing");
                    data.Columns.Add("Continuation");
                    data.Columns.Add("Stock name");
                    data.Columns.Add("Selling price");
                    data.Columns.Add("Total");

                    switch (tableAction)
                    {
                        case TableActionWithParam.FindInCustBookbyName:
                            ShowBookForCustomers = executor.Get<ShowBookForCustomer>("SELECT Books.Id, Books.Name, Authors.FirstName + ' ' + Authors.LastName[Author], " +
                                "Genres.Name[Genre], Publishers.Name[Publisher], Books.Number_of_pages, Books.Year_of_publishing, " +
                                "Books.Continuation, " +
                                "(SELECT TOP 1 Stocks.Name FROM Stocks, StocksBooks WHERE StocksBooks.BookId = Books.Id AND StocksBooks.StockId = Stocks.Id)[StockName], " +
                                "Books.Selling_price, " +
                                "(Books.Selling_price * (CAST((100 - (SELECT TOP 1 StocksBooks.StockPercent FROM StocksBooks WHERE StocksBooks.BookId = Books.Id)) as float) / 100))[Total] " +
                                "FROM Books, Authors, Genres, Publishers " +
                                "WHERE Books.AuthorId = Authors.Id AND Books.GenreId = Genres.Id AND Books.Name LIKE(@Name) " +
                                "AND Quantity > 0 AND Books.PublisherId = Publishers.Id", new { Name = '%' + param1.ToString() + '%' });
                            break;
                        case TableActionWithParam.FindInCustBookbyAuthor:
                            ShowBookForCustomers = executor.Get<ShowBookForCustomer>("SELECT Books.Id, Books.Name, Authors.FirstName + ' ' + Authors.LastName[Author], " +
                                "Genres.Name[Genre], Publishers.Name[Publisher], Books.Number_of_pages, Books.Year_of_publishing, " +
                                "Books.Continuation, " +
                                "(SELECT TOP 1 Stocks.Name FROM Stocks, StocksBooks WHERE StocksBooks.BookId = Books.Id AND StocksBooks.StockId = Stocks.Id)[StockName], " +
                                "Books.Selling_price, " +
                                "(Books.Selling_price * (CAST((100 - (SELECT TOP 1 StocksBooks.StockPercent FROM StocksBooks WHERE StocksBooks.BookId = Books.Id)) as float) / 100))[Total] " +
                                "FROM Books, Authors, Genres, Publishers " +
                                "WHERE Books.AuthorId = Authors.Id AND Books.GenreId = Genres.Id AND (Authors.FirstName LIKE(@Name) OR Authors.LastName LIKE(@Name)) " +
                                "AND Quantity > 0 AND Books.PublisherId = Publishers.Id", new { Name = '%' + param1.ToString() + '%' });
                            break;
                        case TableActionWithParam.FindInCustBookbyGenre:
                            ShowBookForCustomers = executor.Get<ShowBookForCustomer>("SELECT Books.Id, Books.Name, Authors.FirstName + ' ' + Authors.LastName[Author], " +
                                "Genres.Name[Genre], Publishers.Name[Publisher], Books.Number_of_pages, Books.Year_of_publishing, " +
                                "Books.Continuation, " +
                                "(SELECT TOP 1 Stocks.Name FROM Stocks, StocksBooks WHERE StocksBooks.BookId = Books.Id AND StocksBooks.StockId = Stocks.Id)[StockName], " +
                                "Books.Selling_price, " +
                                "(Books.Selling_price * (CAST((100 - (SELECT TOP 1 StocksBooks.StockPercent FROM StocksBooks WHERE StocksBooks.BookId = Books.Id)) as float) / 100))[Total] " +
                                "FROM Books, Authors, Genres, Publishers " +
                                "WHERE Books.AuthorId = Authors.Id AND Books.GenreId = Genres.Id AND Genres.Name LIKE(@Name) " +
                                "AND Quantity > 0 AND Books.PublisherId = Publishers.Id", new { Name = '%' + param1.ToString() + '%' });
                            break;
                        default:
                            break;
                    }

                    foreach (var item in ShowBookForCustomers)
                    {
                        row = data.NewRow();
                        row["Name"] = item.Name;
                        row["Author"] = item.Author;
                        row["Genre"] = item.Genre;
                        row["Publisher"] = item.Publisher;
                        row["Number of pages"] = item.Number_of_pages;
                        row["Year of publishing"] = item.Year_of_publishing;
                        row["Continuation"] = item.Continuation;
                        row["Stock name"] = item.StockName ?? "empty";
                        row["Selling price"] = item.Selling_price;
                        row["Total"] = item.Total.ToString() ?? item.Selling_price.ToString();
                        data.Rows.Add(row);
                    }

                    break;
                case TableActionWithParam.ShowShelvedBooksbyCustId:

                    data.Columns.Add("Book");

                    ShowShelvedBooks = executor.Get<ShowShelvedBook>("SELECT ShelvedBooks.Id, '\"' + Books.Name + '\" ' + Authors.FirstName + ' ' + Authors.LastName[BookName]," +
                        " Customers.FirstName + ' ' + Customers.LastName[CustomerName], Books.Id[BookId], Customers.Id[CustomerId] " +
                        "FROM ShelvedBooks, Books, Customers, Authors " +
                        "WHERE ShelvedBooks.BookId = Books.Id AND ShelvedBooks.CustomerId = Customers.Id AND Customers.Id = @CustId " +
                        "AND Books.AuthorId = Authors.Id", new { CustId = Convert.ToInt32(param1) });

                    foreach (var item in ShowShelvedBooks)
                    {
                        row = data.NewRow();
                        row["Book"] = item.BookName;
                        data.Rows.Add(row);
                    }
                    break;
                case TableActionWithParam.ShowSalesbyCustId:
                    data.Columns.Add("Book");
                    data.Columns.Add("Purchase date");
                    data.Columns.Add("Selling price");

                    ShowSales = executor.Get<ShowSale>("SELECT BookSales.Id, '\"' + Books.Name + '\" ' + Authors.FirstName + ' ' + Authors.LastName[Book], Customers.FirstName + ' ' + Customers.LastName[Customer], " +
                        "BookSales.Purchase_date[PurchaseDate], BookSales.Selling_price[SellingPrice] " +
                        "FROM BookSales, Books, Customers, Authors " +
                        "WHERE BookSales.BookId = Books.Id AND BookSales.CustomerId = Customers.Id AND Customers.Id = @CustId " +
                        "AND Books.AuthorId = Authors.Id", new { CustId = Convert.ToInt32(param1) });

                    foreach (var item in ShowSales)
                    {
                        row = data.NewRow();
                        row["Book"] = item.Book;
                        row["Purchase date"] = item.PurchaseDate.ToString();
                        row["Selling price"] = item.SellingPrice;
                        data.Rows.Add(row);
                    }
                    break;
                default:
                    break;
            }

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

                    Customers = executor.Get<Customer>("Select * FROM Customers");

                    foreach (var item in Customers)
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

                    Salesmen = executor.Get<Salesman>("Select * FROM Salesmen");

                    foreach (var item in Salesmen)
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

                    Admins = executor.Get<Admin>("Select * FROM Admins");

                    foreach (var item in Admins)
                    {
                        row = data.NewRow();
                        row["Id"] = item.Id;
                        row["Login"] = item.Login;
                        row["Password"] = item.Password;
                        data.Rows.Add(row);
                    }
                    break;
                case TableAction.ShowPopularAuthors:
                    data.Columns.Add("Firstname");
                    data.Columns.Add("Lastname");
                    data.Columns.Add("Number of sold books");

                    PopularAuthors = executor.Get<ShowPopularAuthor>("SELECT Authors.Id, Authors.FirstName, Authors.LastName, COUNT(Authors.Id)[NumOfSoldBooks] FROM BookSales, Authors, Books " +
                        "WHERE BookSales.BookId = Books.Id AND Books.AuthorId = Authors.Id " +
                        "GROUP BY Authors.Id, Authors.FirstName, Authors.LastName " +
                        "ORDER BY COUNT(Authors.Id) DESC");

                    foreach (var item in PopularAuthors)
                    {
                        row = data.NewRow();
                        row["Firstname"] = item.FirstName;
                        row["Lastname"] = item.LastName;
                        row["Number of sold books"] = item.NumOfSoldBooks;
                        data.Rows.Add(row);
                    }
                    break;
                case TableAction.ShowAuthors:

                    data.Columns.Add("Id");
                    data.Columns.Add("Firstname");
                    data.Columns.Add("Lastname");

                    Authors = executor.Get<Author>("Select * FROM Authors");

                    foreach (var item in Authors)
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

                    Publishers = executor.Get<Publisher>("Select * FROM Publishers");

                    foreach (var item in Publishers)
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

                    Genres = executor.Get<Genre>("Select * FROM Genres");

                    foreach (var item in Genres)
                    {
                        row = data.NewRow();
                        row["Id"] = item.Id;
                        row["Name"] = item.Name;
                        data.Rows.Add(row);
                    }
                    break;

                case TableAction.ShowPopularGenresForDay:
                case TableAction.ShowPopularGenresForMonth:
                case TableAction.ShowPopularGenresForYear:
                    data.Columns.Add("Name");
                    data.Columns.Add("Number of sold books");

                    string filter;

                    switch (tableAction)
                    {
                        case TableAction.ShowPopularGenresForDay:
                            filter = "day";
                            break;
                        case TableAction.ShowPopularGenresForMonth:
                            filter = "month";
                            break;
                        case TableAction.ShowPopularGenresForYear:
                            filter = "year";
                            break;
                        default:
                            filter = "day";
                            break;
                    }

                    PopularGenres = new DapperExecutor().Get<ShowPopularGenre>("SELECT Genres.Id, Genres.Name, COUNT(Genres.Id)[NumOfSoldGenres] FROM BookSales, Genres, Books " +
                    $"WHERE BookSales.BookId = Books.Id AND Books.GenreId = Genres.Id AND DATEDIFF({filter}, BookSales.Purchase_date, GETDATE()) < 1 " +
                    "GROUP BY Genres.Id, Genres.Name " +
                    "ORDER BY COUNT(Genres.Id) DESC");

                    foreach (var item in PopularGenres)
                    {
                        row = data.NewRow();
                        row["Name"] = item.Name;
                        row["Number of sold books"] = item.NumOfSoldGenres;
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

                    ShowBooks = executor.Get<ShowBook>("SELECT Books.Id, Books.Name[Name], Authors.FirstName + ' ' + Authors.LastName[Author], " +
                        "Genres.Name[Genre], Publishers.Name[Publisher], Books.Number_of_pages, " +
                        "Books.Year_of_publishing, Books.Cost_price, " +
                        "Books.Selling_price, Books.Continuation, Books.Quantity " +
                        "FROM Books, Authors, Publishers, Genres " +
                        "WHERE Books.AuthorId = Authors.Id AND Books.PublisherId = Publishers.Id " +
                        "AND Books.GenreId = Genres.Id");

                    foreach (var item in ShowBooks)
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

                    ShowSales = executor.Get<ShowSale>("SELECT BookSales.Id, '\"' + Books.Name + '\" ' + Authors.FirstName + ' ' + Authors.LastName[Book], Customers.FirstName + ' ' + Customers.LastName[Customer], " +
                        "BookSales.Purchase_date[PurchaseDate], BookSales.Selling_price[SellingPrice] " +
                        "FROM BookSales, Books, Customers, Authors " +
                        "WHERE BookSales.BookId = Books.Id AND BookSales.CustomerId = Customers.Id " +
                        "AND Books.AuthorId = Authors.Id");

                    foreach (var item in ShowSales)
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

                    ShowShelvedBooks = executor.Get<ShowShelvedBook>("SELECT ShelvedBooks.Id, '\"' + Books.Name + '\" ' + Authors.FirstName + ' ' + Authors.LastName[BookName]," +
                        " Customers.FirstName + ' ' + Customers.LastName[CustomerName], Books.Id[BookId], Customers.Id[CustomerId] " +
                        "FROM ShelvedBooks, Books, Customers, Authors " +
                        "WHERE ShelvedBooks.BookId = Books.Id AND ShelvedBooks.CustomerId = Customers.Id " +
                        "AND Books.AuthorId = Authors.Id");

                    foreach (var item in ShowShelvedBooks)
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

                    Stocks = executor.Get<Stock>("SELECT * FROM Stocks");

                    foreach (var item in Stocks)
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

                    ShowStockBooks = executor.Get<ShowStockBook>("SELECT StocksBooks.Id, Stocks.Name[StockName], '\"' + Books.Name + '\" ' + Authors.FirstName + ' ' + Authors.LastName[BookName], StocksBooks.StockPercent " +
                        "FROM StocksBooks, Stocks, Books, Authors " +
                        "WHERE StocksBooks.StockId = Stocks.Id AND StocksBooks.BookId = Books.Id " +
                        "AND Books.AuthorId = Authors.Id");

                    foreach (var item in ShowStockBooks)
                    {
                        row = data.NewRow();
                        row["Id"] = item.Id;
                        row["Stock"] = item.StockName;
                        row["Book"] = item.BookName;
                        row["Stock percent"] = item.StockPercent;
                        data.Rows.Add(row);
                    }
                    break;
                case TableAction.ShowNewBookForCustomer:
                case TableAction.ShowBookForCustomer:
                case TableAction.ShowPopularBooksForCustomer:
                    data.Columns.Add("Name");
                    data.Columns.Add("Author");
                    data.Columns.Add("Genre");
                    data.Columns.Add("Publisher");
                    data.Columns.Add("Number of pages");
                    data.Columns.Add("Year of publishing");
                    data.Columns.Add("Continuation");
                    data.Columns.Add("Stock name");
                    data.Columns.Add("Selling price");
                    data.Columns.Add("Total");

                    switch (tableAction)
                    {
                        case TableAction.ShowNewBookForCustomer:
                            ShowBookForCustomers = executor.Get<ShowBookForCustomer>("SELECT Books.Id, Books.Name, Authors.FirstName + ' ' + Authors.LastName[Author], " +
                                "Genres.Name[Genre], Publishers.Name[Publisher], Books.Number_of_pages, Books.Year_of_publishing, " +
                                "Books.Continuation, " +
                                "(SELECT TOP 1 Stocks.Name FROM Stocks, StocksBooks WHERE StocksBooks.BookId = Books.Id AND StocksBooks.StockId = Stocks.Id)[StockName], " +
                                "Books.Selling_price, " +
                                "(Books.Selling_price * (CAST((100 - (SELECT TOP 1 StocksBooks.StockPercent FROM StocksBooks WHERE StocksBooks.BookId = Books.Id)) as float) / 100))[Total] " +
                                "FROM Books, Authors, Genres, Publishers " +
                                "WHERE Books.AuthorId = Authors.Id AND Books.GenreId = Genres.Id AND Quantity > 0 " +
                                "AND Books.PublisherId = Publishers.Id " +
                                "ORDER BY Books.Id DESC");
                            break;
                        case TableAction.ShowBookForCustomer:
                            ShowBookForCustomers = executor.Get<ShowBookForCustomer>("SELECT Books.Id, Books.Name, Authors.FirstName + ' ' + Authors.LastName[Author], " +
                                "Genres.Name[Genre], Publishers.Name[Publisher], Books.Number_of_pages, Books.Year_of_publishing, " +
                                "Books.Continuation, " +
                                "(SELECT TOP 1 Stocks.Name FROM Stocks, StocksBooks WHERE StocksBooks.BookId = Books.Id AND StocksBooks.StockId = Stocks.Id)[StockName], " +
                                "Books.Selling_price, " +
                                "(Books.Selling_price * (CAST((100 - (SELECT TOP 1 StocksBooks.StockPercent FROM StocksBooks WHERE StocksBooks.BookId = Books.Id)) as float) / 100))[Total] " +
                                "FROM Books, Authors, Genres, Publishers " +
                                "WHERE Books.AuthorId = Authors.Id AND Books.GenreId = Genres.Id AND Quantity > 0 " +
                                "AND Books.PublisherId = Publishers.Id");
                            break;
                        case TableAction.ShowPopularBooksForCustomer:
                            ShowBookForCustomers = executor.Get<ShowBookForCustomer>("SELECT Books.Id, Books.Name, Authors.FirstName + ' ' + Authors.LastName[Author], " +
                                "Genres.Name[Genre], Publishers.Name[Publisher], Books.Number_of_pages, Books.Year_of_publishing, " +
                                "Books.Continuation, " +
                                "(SELECT TOP 1 Stocks.Name FROM Stocks, StocksBooks WHERE StocksBooks.BookId = Books.Id AND StocksBooks.StockId = Stocks.Id)[StockName], " +
                                "Books.Selling_price, " +
                                "(Books.Selling_price * (CAST((100 - (SELECT TOP 1 StocksBooks.StockPercent FROM StocksBooks WHERE StocksBooks.BookId = Books.Id)) as float) / 100))[Total] " +
                                "FROM Books, Authors, Genres, Publishers, (SELECT BookSales.BookId, COUNT(BookSales.BookId)[Count] " +
                                "FROM BookSales, Books " +
                                "WHERE BookSales.BookId = Books.Id " +
                                "GROUP BY BookSales.BookId) as Sales " +
                                "WHERE Sales.BookId = Books.Id AND Books.AuthorId = Authors.Id AND Books.GenreId = Genres.Id " +
                                "AND Quantity > 0 AND Books.PublisherId = Publishers.Id " +
                                "ORDER BY Sales.Count DESC");
                            break;
                        default:
                            break;
                    }

                    foreach (var item in ShowBookForCustomers)
                    {
                        row = data.NewRow();
                        row["Name"] = item.Name;
                        row["Author"] = item.Author;
                        row["Genre"] = item.Genre;
                        row["Publisher"] = item.Publisher;
                        row["Number of pages"] = item.Number_of_pages;
                        row["Year of publishing"] = item.Year_of_publishing;
                        row["Continuation"] = item.Continuation;
                        row["Stock name"] = item.StockName ?? "empty";
                        row["Selling price"] = item.Selling_price;
                        row["Total"] = item.Total.ToString() ?? item.Selling_price.ToString();
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
                            string firstName, lastName, login, password;
                            bool insert;
                            int id;

                            foreach (DataRow row in _data.Rows)
                            {
                                foreach (Customer customer in Customers)
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
                                            UpdateCustomer(id, firstName, lastName, login, password);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case TableAction.ShowManagers:
                        {
                            string firstName, lastName, login, password;
                            bool insert;
                            int id;

                            foreach (DataRow row in _data.Rows)
                            {
                                foreach (Salesman salesman in Salesmen)
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
                                            UpdateSalesman(id, firstName, lastName, login, password);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case TableAction.ShowAdmins:
                        {
                            string login, password;
                            bool insert;
                            int id;

                            foreach (DataRow row in _data.Rows)
                            {
                                foreach (Salesman salesman in Salesmen)
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
                                            UpdateAdmin(id, login, password);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case TableAction.ShowAuthors:
                        {
                            string firstName, lastName;
                            bool insert;
                            int id;

                            foreach (DataRow row in _data.Rows)
                            {
                                foreach (Author author in Authors)
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
                                            UpdateAuthor(id, firstName, lastName);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case TableAction.ShowPublishers:
                        {
                            string name;
                            bool insert;
                            int id;

                            foreach (DataRow row in _data.Rows)
                            {
                                foreach (Publisher publisher in Publishers)
                                {
                                    if (row["Id"].ToString() == publisher.Id.ToString())
                                    {
                                        insert = false;
                                        name = publisher.Name;
                                        id = publisher.Id;

                                        if (row["Name"].ToString() != publisher.Name) { name = row["Name"].ToString(); insert = true; }

                                        if (insert)
                                        {
                                            UpdatePublisher(id, name);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case TableAction.ShowGenres:
                        {
                            string name;
                            bool insert;
                            int id;

                            foreach (DataRow row in _data.Rows)
                            {
                                foreach (Genre genre in Genres)
                                {
                                    if (row["Id"].ToString() == genre.Id.ToString())
                                    {
                                        insert = false;
                                        name = genre.Name;
                                        id = genre.Id;

                                        if (row["Name"].ToString() != genre.Name) { name = row["Name"].ToString(); insert = true; }

                                        if (insert)
                                        {
                                            UpdateGenre(id, name);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case TableAction.ShowBooks:
                        {
                            IEnumerable<Book> books = from b in ShowBooks
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
                                            UpdateBook(id, name, number_of_pages, year_of_publishing, cost_price, selling_price, continuation, quantity);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case TableAction.ShowSales:
                        {
                            IEnumerable<Sale> sales = from s in ShowSales
                                                      select new Sale()
                                                      {
                                                          Id = s.Id,
                                                          PurchaseDate = s.PurchaseDate,
                                                          SellingPrice = s.SellingPrice
                                                      };
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
                                            UpdateSale(id, purchase_date, selling_price);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case TableAction.ShowStocks:
                        {
                            string name;
                            bool insert;
                            int id;

                            foreach (DataRow row in _data.Rows)
                            {
                                foreach (Stock stock in Stocks)
                                {
                                    if (row["Id"].ToString() == stock.Id.ToString())
                                    {
                                        insert = false;
                                        name = stock.Name;
                                        id = stock.Id;

                                        if (row["Name"].ToString() != stock.Name) { name = row["Name"].ToString(); insert = true; }

                                        if (insert)
                                        {
                                            UpdateStock(id, name);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case TableAction.ShowStockBooks:
                        {
                            IEnumerable<StockBook> stockBooks = from sb in ShowStockBooks select new StockBook() { Id = sb.Id, StockPercent = sb.StockPercent };
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
                                            UpdateStockBook(id, stockPercent);
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
            int id = Int32.Parse(((DataRowView)dataGrid.SelectedItem).Row["Id"].ToString());
            switch (tableAction)
            {
                case TableAction.ShowCustomers:
                    DeleteCustomer(id);
                    break;
                case TableAction.ShowManagers:
                    DeleteSalesman(id);
                    break;
                case TableAction.ShowAdmins:
                    DeleteAdmin(id);
                    break;
                case TableAction.ShowAuthors:
                    DeleteAuthor(id);
                    break;
                case TableAction.ShowPublishers:
                    DeletePublisher(id);
                    break;
                case TableAction.ShowGenres:
                    DeleteGenre(id);
                    break;
                case TableAction.ShowBooks:
                    DeleteBook(id);
                    break;
                case TableAction.ShowSales:
                    DeleteSale(id);
                    break;
                case TableAction.ShowShelvedBooks:
                    DeleteShelvedBook(id);
                    break;
                case TableAction.ShowStocks:
                    DeleteStock(id);
                    break;
                case TableAction.ShowStockBooks:
                    DeleteStockBook(id);
                    break;
                case TableAction.None:
                    return;
            }

        }
        public void AddNewValueFromGrid(TableAction tableAction, ref DataGrid dataGrid)
        {
            try
            {
                switch (tableAction)
                {
                    case TableAction.ShowCustomers:
                        {
                            string firstName = ((DataRowView)dataGrid.SelectedItem).Row["FirstName"].ToString();
                            string lastName = ((DataRowView)dataGrid.SelectedItem).Row["LastName"].ToString();
                            string login = ((DataRowView)dataGrid.SelectedItem).Row["Login"].ToString();
                            string password = ((DataRowView)dataGrid.SelectedItem).Row["Password"].ToString();
                            AddCustomer(firstName, lastName, login, password);
                        }
                        break;
                    case TableAction.ShowManagers:
                        {
                            string firstName = ((DataRowView)dataGrid.SelectedItem).Row["FirstName"].ToString();
                            string lastName = ((DataRowView)dataGrid.SelectedItem).Row["LastName"].ToString();
                            string login = ((DataRowView)dataGrid.SelectedItem).Row["Login"].ToString();
                            string password = ((DataRowView)dataGrid.SelectedItem).Row["Password"].ToString();
                            AddSalesman(firstName, lastName, login, password);
                        }
                        break;
                    case TableAction.ShowAdmins:
                        {
                            string login = ((DataRowView)dataGrid.SelectedItem).Row["Login"].ToString();
                            string password = ((DataRowView)dataGrid.SelectedItem).Row["Password"].ToString();
                            AddAdmin(login, password);
                        }
                        break;
                    case TableAction.ShowAuthors:
                        {
                            string firstName = ((DataRowView)dataGrid.SelectedItem).Row["FirstName"].ToString();
                            string lastName = ((DataRowView)dataGrid.SelectedItem).Row["LastName"].ToString();
                            AddAuthor(firstName, lastName);
                        }
                        break;
                    case TableAction.ShowPublishers:
                        {
                            string name = ((DataRowView)dataGrid.SelectedItem).Row["Name"].ToString();
                            AddPublisher(name);
                        }
                        break;
                    case TableAction.ShowGenres:
                        {
                            string name = ((DataRowView)dataGrid.SelectedItem).Row["Name"].ToString();
                            AddGenre(name);
                        }
                        break;
                    case TableAction.ShowStocks:
                        {
                            string name = ((DataRowView)dataGrid.SelectedItem).Row["Name"].ToString();
                            AddStock(name);
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
            catch (Exception) // I could use SelectedItem'?', but the user will not know what he is doing wrong
            {
                MessageBox.Show("Error, check the selected row", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region AdditionValueBaseTables

        public void AddAdmin(string login, string password)
        {
            executor.InsertUpdateDelete("INSERT INTO Admins(Login, Password) " +
                            "VALUES(@Login, @Password)",
                            new { Login = login, Password = password });
        }
        public void AddAuthor(string firstName, string lastName)
        {
            executor.InsertUpdateDelete("INSERT INTO Authors(FirstName, LastName) " +
                            "VALUES(@FirstName, @LastName)",
                            new { FirstName = firstName, LastName = lastName });
        }
        public void AddBook(string name, int authorId, int genreId,
            int publisherId, int number_of_pages, int year_of_publishing,
            decimal cost_price, decimal selling_price, bool continuation, int quantity)
        {
            executor.InsertUpdateDelete("INSERT INTO Books(Name, AuthorId, GenreId, PublisherId, " +
                                "Number_of_pages, Year_of_publishing, Cost_price, Selling_price, Continuation, Quantity) " +
                                "VALUES(@Name, @AuthorId, @GenreId, @PublisherId, @Number_of_pages, @Year_of_publishing, @Cost_price, @Selling_price," +
                                "@Continuation, @Quantity)",
                                new
                                {
                                    Name = name,
                                    AuthorId = authorId,
                                    GenreId = genreId,
                                    PublisherId = publisherId,
                                    Number_of_pages = number_of_pages,
                                    Year_of_publishing = year_of_publishing,
                                    Cost_price = cost_price,
                                    Selling_price = selling_price,
                                    Continuation = continuation,
                                    Quantity = quantity
                                });
        }
        public void AddCustomer(string firstName, string lastName, string login, string password)
        {
            executor.InsertUpdateDelete("INSERT INTO Customers(FirstName, LastName, Login, Password) " +
                            "VALUES(@FirstName, @LastName, @Login, @Password)",
                            new { FirstName = firstName, LastName = lastName, Login = login, Password = password });
        }
        public void AddGenre(string name)
        {
            executor.InsertUpdateDelete("INSERT INTO Genres(Name) " +
                            "VALUES(@Name)",
                            new { Name = name });
        }
        public void AddPublisher(string name)
        {
            executor.InsertUpdateDelete("INSERT INTO Publishers(Name) " +
                            "VALUES(@Name)",
                            new { Name = name });
        }
        public void AddSale(int bookId, int customerId, DateTime purchaseDate, decimal sellingPrice)
        {
            executor.InsertUpdateDelete("INSERT INTO BookSales(BookId, CustomerId, Purchase_date, Selling_price) " +
                                    "VALUES(@BookId, @CustomerId, @Purchase_date, @Selling_price)", new
                                    {
                                        BookId = bookId,
                                        CustomerId = customerId,
                                        Purchase_date = purchaseDate,
                                        Selling_price = sellingPrice
                                    });
        }
        public void AddSalesman(string firstName, string lastName, string login, string password)
        {
            executor.InsertUpdateDelete("INSERT INTO Salesmen(FirstName, LastName, Login, Password) " +
                            "VALUES(@FirstName, @LastName, @Login, @Password)",
                            new { FirstName = firstName, LastName = lastName, Login = login, Password = password });
        }
        public void AddShelvedBook(int bookId, int customerId)
        {
            executor.InsertUpdateDelete("INSERT INTO ShelvedBooks(BookId, CustomerId) " +
                                "VALUES(@BookId, @CustomerId)",
                                new
                                {
                                    BookId = bookId,
                                    CustomerId = customerId
                                });
        }
        public void AddStock(string name)
        {
            executor.InsertUpdateDelete("INSERT INTO Stocks(Name) " +
                                "VALUES(@Name)",
                                new { Name = name });
        }
        public void AddStockBook(int stockId, int bookId, int stockPercent)
        {
            executor.InsertUpdateDelete("INSERT INTO StocksBooks(StockId, BookId, StockPercent) " +
                                "VALUES(@StockId, @BookId, @StockPercent)",
                                new
                                {
                                    StockId = stockId,
                                    BookId = bookId,
                                    StockPercent = stockPercent
                                });
        }

        #endregion

        #region DeleteValueBaseTablesbyId

        public void DeleteAdmin(int id)
        {
            executor.InsertUpdateDelete("DELETE FROM Admins WHERE Admins.Id = @Id", new { Id = id });
        }
        public void DeleteAuthor(int id)
        {
            executor.InsertUpdateDelete("DELETE FROM Authors WHERE Authors.Id = @Id", new { Id = id });
        }
        public void DeleteBook(int id)
        {
            executor.InsertUpdateDelete("DELETE FROM Books WHERE Books.Id = @Id", new { Id = id });
        }
        public void DeleteCustomer(int id)
        {
            executor.InsertUpdateDelete("DELETE FROM Customers WHERE Customers.Id = @Id", new { Id = id });
        }
        public void DeleteGenre(int id)
        {
            executor.InsertUpdateDelete("DELETE FROM Genres WHERE Genres.Id = @Id", new { Id = id });
        }
        public void DeletePublisher(int id)
        {
            executor.InsertUpdateDelete("DELETE FROM Publishers WHERE Publishers.Id = @Id", new { Id = id });
        }
        public void DeleteSale(int id)
        {
            executor.InsertUpdateDelete("DELETE FROM BookSales WHERE BookSales.Id = @Id", new { Id = id });
        }
        public void DeleteSalesman(int id)
        {
            executor.InsertUpdateDelete("DELETE FROM Salesmen WHERE Salesmen.Id = @Id", new { Id = id });
        }
        public void DeleteShelvedBook(int id)
        {
            executor.InsertUpdateDelete("DELETE FROM ShelvedBooks WHERE ShelvedBooks.Id = @Id", new { Id = id });
        }
        public void DeleteStock(int id)
        {
            executor.InsertUpdateDelete("DELETE FROM Stocks WHERE Stocks.Id = @Id", new { Id = id });
        }
        public void DeleteStockBook(int id)
        {
            executor.InsertUpdateDelete("DELETE FROM StocksBooks WHERE StocksBooks.Id = @Id", new { Id = id });
        }


        #endregion

        #region UpdateValueBaseTables

        public void UpdateAdmin(int id, string login, string password)
        {
            executor.InsertUpdateDelete("Update Admins SET Login = @Login," +
                            " Password = @Password WHERE Id = @Id",
                            new { Login = login, Password = password, Id = id });
        }
        public void UpdateAuthor(int id, string firstName, string lastName)
        {
            executor.InsertUpdateDelete("Update Authors SET FirstName = @FirstName, LastName = @LastName " +
                            "WHERE Id = @Id",
                            new { FirstName = firstName, LastName = lastName, Id = id });
        }
        public void UpdateBook(int id, string name, int number_of_pages, int year_of_publishing,
            decimal cost_price, decimal selling_price, bool continuation, int quantity)
        {
            executor.InsertUpdateDelete("UPDATE Books " +
                            "Set Name = @Name, Number_of_pages = @Number_of_pages, Year_of_publishing = @Year_of_publishing, Cost_price = @Cost_price, " +
                            "Selling_price = @Selling_price, Continuation = @Continuation, Quantity = @Quantity " +
                            "Where Id = @Id",
                                new
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
        public void UpdateCustomer(int id, string firstName, string lastName, string login, string password)
        {
            executor.InsertUpdateDelete("Update Customers SET FirstName = @FirstName, LastName = @LastName, Login = @Login," +
                            " Password = @Password WHERE Id = @Id",
                            new { FirstName = firstName, LastName = lastName, Login = login, Password = password, Id = id });
        }
        public void UpdateGenre(int id, string name)
        {
            executor.InsertUpdateDelete("Update Genres SET Name = @Name " +
                            "WHERE Id = @Id",
                            new { Name = name, Id = id });
        }
        public void UpdatePublisher(int id, string name)
        {
            executor.InsertUpdateDelete("Update Publishers SET Name = @Name " +
                            "WHERE Id = @Id",
                            new { Name = name, Id = id });
        }
        public void UpdateSale(int id, DateTime purchaseDate, decimal sellingPrice)
        {
            executor.InsertUpdateDelete("UPDATE BookSales " +
                            "SET Purchase_date = @Purchase_date, Selling_price = @Selling_price " +
                            "Where Id = @Id",
                            new
                            {
                                Purchase_date = purchaseDate,
                                Selling_price = sellingPrice,
                                Id = id
                            });
        }
        public void UpdateSalesman(int id, string firstName, string lastName, string login, string password)
        {
            executor.InsertUpdateDelete("Update Salesmen SET FirstName = @FirstName, LastName = @LastName, Login = @Login," +
                            " Password = @Password WHERE Id = @Id",
                            new { FirstName = firstName, LastName = lastName, Login = login, Password = password, Id = id });
        }
        public void UpdateStock(int id, string name)
        {
            executor.InsertUpdateDelete("UPDATE Stocks SET Name = @Name " +
                            "WHERE Id = @Id",
                                new { Name = name, Id = id });
        }
        public void UpdateStockBook(int id, int stockPercent)
        {
            executor.InsertUpdateDelete("UPDATE StocksBooks SET StockPercent = @StockPercent " +
                            "WHERE Id = @Id",
                                new
                                {
                                    StockPercent = stockPercent,
                                    Id = id
                                });
        }

        #endregion

    }
}
