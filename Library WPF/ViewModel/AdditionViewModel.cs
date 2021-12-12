using Library_WPF.Model;
using Library_WPF.Model.ShowModel;
using Library_WPF.Service;
using Library_WPF.Service.Command;
using Library_WPF.Service.Interface;
using Library_WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Library_WPF.ViewModel
{
    internal class AdditionViewModel : INotifyPropertyChanged
    {

        private TableAction _showTable;

        private LibraryAction libraryAction;
        private DataTable data;
        public ObservableCollection<string> Authors { get; set; }
        public ObservableCollection<string> Genres { get; set; }
        public ObservableCollection<string> Publishers { get; set; }
        public ObservableCollection<string> Customers { get; set; }
        public ObservableCollection<string> Books { get; set; }
        public ObservableCollection<string> Stocks { get; set; }

        private string _addBookVisible;
        private string _addSalesVisible;
        private string _addShelvedBooksVisible;
        private string _addStockBooksVisible;
        private string _name;
        private string _numofPages;
        private string _yearOfPublishing;
        private string _costPrice;
        private string _sellingPrice;
        private string _quantity;
        private string _selectedAuthor;
        private string _selectedGenre;
        private string _selectedPublisher;
        private bool _checkedContinuation;
        private string _selectedDate;
        private string _selectedBookName;
        private string _selectedCustomer;
        private string _selectedStock;
        private string _stockPercent;

        public string AddBookVisible
        {
            get => _addBookVisible;
            set
            {
                if (!Equals(value, _addBookVisible))
                {
                    _addBookVisible = value;
                    OnProperyChanged(nameof(AddBookVisible));
                }
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                if (!Equals(value, _name))
                {
                    _name = value;
                    OnProperyChanged(nameof(Name));
                }
            }
        }
        public string NumOfPages
        {
            get => _numofPages;
            set
            {
                if (!Equals(_numofPages, value))
                {
                    _numofPages = value;
                    OnProperyChanged(nameof(NumOfPages));
                }
            }
        }
        public string YearOfPublishing
        {
            get => _yearOfPublishing;
            set
            {
                if (!Equals(_yearOfPublishing, value))
                {
                    _yearOfPublishing = value;
                    OnProperyChanged(nameof(YearOfPublishing));
                }
            }
        }
        public string CostPrice
        {
            get => _costPrice;
            set
            {
                if (!Equals(_costPrice, value))
                {
                    _costPrice = value;
                    OnProperyChanged(nameof(CostPrice));
                }
            }
        }
        public string SellingPrice
        {
            get => _sellingPrice;
            set
            {
                if (!Equals(_sellingPrice, value))
                {
                    _sellingPrice = value;
                    OnProperyChanged(nameof(SellingPrice));
                }
            }
        }
        public string Quantity
        {
            get => _quantity;
            set
            {
                if (!Equals(_quantity, value))
                {
                    _quantity = value;
                    OnProperyChanged(nameof(Quantity));
                }
            }
        }
        public string SelectedAuthor
        {
            get => _selectedAuthor;
            set
            {
                if (!Equals(_selectedAuthor, value))
                {
                    _selectedAuthor = value;
                    OnProperyChanged(nameof(SelectedAuthor));
                }
            }
        }
        public string SelectedGenre
        {
            get => _selectedGenre;
            set
            {
                if (!Equals(_selectedGenre, value))
                {
                    _selectedGenre = value;
                    OnProperyChanged(nameof(SelectedGenre));
                }
            }
        }
        public string SelectedPublisher
        {
            get => _selectedPublisher;
            set
            {
                if (!Equals(_selectedPublisher, value))
                {
                    _selectedPublisher = value;
                    OnProperyChanged(nameof(SelectedPublisher));
                }
            }
        }
        public bool SelectedContinuation
        {
            get => _checkedContinuation;
            set
            {
                if (!Equals(_checkedContinuation, value))
                {
                    _checkedContinuation = value;
                    OnProperyChanged(nameof(SelectedContinuation));
                }
            }
        }
        public object SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (!Equals(_selectedDate, value.ToString()))
                {
                    _selectedDate = value.ToString();
                    OnProperyChanged(nameof(SelectedDate));
                }
            }
        }
        public string AddSalesVisible
        {
            get => _addSalesVisible;
            set
            {
                if (!Equals(value, _addSalesVisible))
                {
                    _addSalesVisible = value;
                    OnProperyChanged(nameof(AddSalesVisible));
                }
            }
        }
        public string SelectedBookName
        {
            get => _selectedBookName;
            set
            {
                if (!Equals(value, _selectedBookName))
                {
                    _selectedBookName = value;
                    OnProperyChanged(nameof(SelectedBookName));
                }
            }
        }
        public string SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (!Equals(value, _selectedCustomer))
                {
                    _selectedCustomer = value;
                    OnProperyChanged(nameof(SelectedCustomer));
                }
            }
        }
        public string AddShelvedBooksVisible
        {
            get => _addShelvedBooksVisible;
            set
            {
                if (!Equals(value, _addShelvedBooksVisible))
                {
                    _addShelvedBooksVisible = value;
                    OnProperyChanged(nameof(AddShelvedBooksVisible));
                }
            }
        }
        public string AddStockBooksVisible
        {
            get => _addStockBooksVisible;
            set
            {
                if (!Equals(value, _addStockBooksVisible))
                {
                    _addStockBooksVisible = value;
                    OnProperyChanged(nameof(AddStockBooksVisible));
                }
            }
        }
        public string SelectedStock
        {
            get => _selectedStock;
            set
            {
                if (!Equals(value, _selectedStock))
                {
                    _selectedStock = value;
                    OnProperyChanged(nameof(SelectedStock));
                }
            }
        }
        public string StockPercent
        {
            get => _stockPercent;
            set
            {
                if (!Equals(value, _stockPercent))
                {
                    _stockPercent = value;
                    OnProperyChanged(nameof(StockPercent));
                }
            }
        }

        Command _addCommand;

        public ICommand AddCommand => _addCommand;

        public AdditionViewModel(AdditionWindow additionWindow, TableAction showTable, IRepository executor)
        {
            _showTable = showTable;
            _name = String.Empty;
            libraryAction = new LibraryAction(new DapperExecutor());
            Authors = new ObservableCollection<string>();
            Genres = new ObservableCollection<string>();
            Publishers = new ObservableCollection<string>();
            Customers = new ObservableCollection<string>();
            Books = new ObservableCollection<string>();
            Stocks = new ObservableCollection<string>();
            AddBookVisible = "Hidden";
            AddSalesVisible = "Hidden";
            AddShelvedBooksVisible = "Hidden";
            AddStockBooksVisible = "Hidden";

            switch (showTable)
            {
                case TableAction.ShowBooks:
                    AddBookVisible = "Visible";
                    libraryAction.GetDataTable(TableAction.ShowAuthors, out data);
                    foreach (Author author in libraryAction.authors)
                    {
                        Authors.Add(author.FirstName + " " + author.LastName);
                    }
                    libraryAction.GetDataTable(TableAction.ShowGenres, out data);
                    foreach (Genre genre in libraryAction.genres)
                    {
                        Genres.Add(genre.Name);
                    }
                    libraryAction.GetDataTable(TableAction.ShowPublishers, out data);
                    foreach (Publisher publisher in libraryAction.publishers)
                    {
                        Publishers.Add(publisher.Name);
                    }
                    break;
                case TableAction.ShowSales:
                    AddSalesVisible = "Visible";
                    libraryAction.GetDataTable(TableAction.ShowBooks, out data);
                    foreach (ShowBook showBook in libraryAction.showBooks)
                    {
                        Books.Add(showBook.Name);
                    }
                    libraryAction.GetDataTable(TableAction.ShowCustomers, out data);
                    foreach (Customer customer in libraryAction.customers)
                    {
                        Customers.Add(customer.FirstName + " " + customer.LastName);
                    }
                    break;
                case TableAction.ShowShelvedBooks:
                    AddShelvedBooksVisible = "Visible";
                    libraryAction.GetDataTable(TableAction.ShowBooks, out data);
                    foreach (ShowBook showBook in libraryAction.showBooks)
                    {
                        Books.Add(showBook.Name);
                    }
                    libraryAction.GetDataTable(TableAction.ShowCustomers, out data);
                    foreach (Customer customer in libraryAction.customers)
                    {
                        Customers.Add(customer.FirstName + " " + customer.LastName);
                    }
                    break;
                case TableAction.ShowStockBooks:

                    AddStockBooksVisible = "Visible";
                    libraryAction.GetDataTable(TableAction.ShowStocks, out data);
                    foreach (Stock stock in libraryAction.stocks)
                    {
                        Stocks.Add(stock.Name);
                    }
                    libraryAction.GetDataTable(TableAction.ShowBooks, out data);
                    foreach (ShowBook showBook in libraryAction.showBooks)
                    {
                        Books.Add(showBook.Name);
                    }

                    break;
                case TableAction.None:
                    break;
                default:
                    break;

            }
            _addCommand = new Command(obj =>
            {
                try
                {
                    string addQuery;
                    switch (showTable)
                    {
                        case TableAction.ShowBooks:
                            {
                                addQuery = "INSERT INTO Books(Name, AuthorId, GenreId, PublisherId, " +
                                "Number_of_pages, Year_of_publishing, Cost_price, Selling_price, Continuation, Quantity) " +
                                "VALUES(@Name, @AuthorId, @GenreId, @PublisherId, @Number_of_pages, @Year_of_publishing, @Cost_price, @Selling_price," +
                                "@Continuation, @Quantity)";
                                int authorId = -1;
                                int genreId = -1;
                                int publisherId = -1;
                                foreach (Author author in libraryAction.authors)
                                {
                                    if ((author.FirstName + " " + author.LastName) == SelectedAuthor)
                                    {
                                        authorId = author.Id;
                                        break;
                                    }
                                }
                                foreach (Genre genre in libraryAction.genres)
                                {
                                    if (genre.Name == SelectedGenre)
                                    {
                                        genreId = genre.Id;
                                        break;
                                    }
                                }
                                foreach (Publisher publisher in libraryAction.publishers)
                                {
                                    if (publisher.Name == SelectedPublisher)
                                    {
                                        publisherId = publisher.Id;
                                        break;
                                    }
                                }

                                executor.InsertUpdateDelete(addQuery, new
                                {
                                    Name = Name,
                                    AuthorId = authorId,
                                    GenreId = genreId,
                                    PublisherId = publisherId,
                                    Number_of_pages = Int32.Parse(NumOfPages),
                                    Year_of_publishing = Int32.Parse(YearOfPublishing),
                                    Cost_price = Decimal.Parse(CostPrice),
                                    Selling_price = Decimal.Parse(SellingPrice),
                                    Continuation = _checkedContinuation,
                                    Quantity = Quantity
                                });

                            }
                            break;
                        case TableAction.ShowSales:
                            {
                                addQuery = "INSERT INTO BookSales(BookId, CustomerId, Purchase_date, Selling_price) " +
                                    "VALUES(@BookId, @CustomerId, @Purchase_date, @Selling_price)";
                                int bookId = -1;
                                int customerId = -1;
                                foreach (ShowBook showBook in libraryAction.showBooks)
                                {
                                    if (showBook.Name == SelectedBookName)
                                    {
                                        bookId = showBook.Id;
                                    }
                                }
                                foreach (Customer customer in libraryAction.customers)
                                {
                                    if (customer.FirstName + " " + customer.LastName == SelectedCustomer)
                                    {
                                        customerId = customer.Id;
                                    }
                                }
                                executor.InsertUpdateDelete(addQuery, new
                                {
                                    BookId = bookId,
                                    CustomerId = customerId,
                                    Purchase_date = DateTime.Parse(SelectedDate.ToString()),
                                    Selling_price = Decimal.Parse(SellingPrice)
                                });
                            }
                            break;
                        case TableAction.ShowShelvedBooks:
                            {
                                addQuery = "INSERT INTO ShelvedBooks(BookId, CustomerId) " +
                                "VALUES(@BookId, @CustomerId)";
                                int bookId = -1;
                                int customerId = -1;
                                foreach (ShowBook showBook in libraryAction.showBooks)
                                {
                                    if (showBook.Name == SelectedBookName)
                                    {
                                        bookId = showBook.Id;
                                    }
                                }
                                foreach (Customer customer in libraryAction.customers)
                                {
                                    if (customer.FirstName + " " + customer.LastName == SelectedCustomer)
                                    {
                                        customerId = customer.Id;
                                    }
                                }
                                executor.InsertUpdateDelete(addQuery, new
                                {
                                    BookId = bookId,
                                    CustomerId = customerId
                                });
                            }
                            break;
                        case TableAction.ShowStockBooks:
                            {
                                addQuery = "INSERT INTO StocksBooks(StockId, BookId, StockPercent) " +
                                "VALUES(@StockId, @BookId, @StockPercent)";
                                int stockId = -1;
                                int bookId = -1;
                                foreach (Stock stock in libraryAction.stocks)
                                {
                                    if (stock.Name == SelectedStock)
                                    {
                                        stockId = stock.Id;
                                    }
                                }
                                foreach (ShowBook showBook in libraryAction.showBooks)
                                {
                                    if (showBook.Name == SelectedBookName)
                                    {
                                        bookId = showBook.Id;
                                    }
                                }
                                executor.InsertUpdateDelete(addQuery, new
                                {
                                    StockId = stockId,
                                    BookId = bookId,
                                    StockPercent = StockPercent
                                });
                            }
                            break;
                        case TableAction.None:
                            break;
                        default:
                            break;

                    }
                }
                //catch (Exception)
                //{
                //    MessageBox.Show("Error, invalid data entered", "Addition Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //}
                finally
                {
                    additionWindow.Close();
                }
            });
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnProperyChanged(string properyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }
    }
}
