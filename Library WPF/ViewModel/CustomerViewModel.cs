using Library_WPF.Model;
using Library_WPF.Model.ShowModel;
using Library_WPF.Service;
using Library_WPF.Service.Command;
using Library_WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace Library_WPF.ViewModel
{
    public enum TableActionWithParam
    {
        FindInCustBookbyName = 1,
        FindInCustBookbyAuthor,
        FindInCustBookbyGenre,
        ShowShelvedBooksbyCustId,
        ShowSalesbyCustId,
        None
    }
    internal class CustomerViewModel : INotifyPropertyChanged
    {
        private TableAction _showAction;
        private TableActionWithParam _actionWithParam;
        private LibraryAction libraryAction;
        private Customer currentCustomer;
        private DataTable _data;

        private string _customerName;
        private string _selectedFilter;
        private string _searchText;
        private string _selectedGenreFilter;

        public string CustomerName
        {
            get => _customerName;
            set
            {
                if (!Equals(_customerName, value))
                {
                    _customerName = value;
                    OnPropertyChanged(nameof(CustomerName));
                }
            }
        }
        public string SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                if (!Equals(_selectedFilter, value))
                {
                    _selectedFilter = value;
                    OnPropertyChanged(nameof(SelectedFilter));
                }
            }
        }
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (!Equals(_searchText, value))
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                }
            }
        }
        public string SelectedGenreFilter
        {
            get => _selectedGenreFilter;
            set
            {
                if (!Equals(_selectedGenreFilter, value))
                {
                    _selectedGenreFilter = value;
                    OnPropertyChanged(nameof(SelectedGenreFilter));
                }
            }
        }


        private Command _showBookForCustomer;
        private Command _showNewBookForCustomer;
        private Command _findByFilter;
        private Command _showPopularBooksForCustomer;
        private Command _showPopularAuthors;
        private Command _showPopularGenres;
        private Command _showShelvedBooks;
        private Command _showPurchase;
        private Command _buyBook;
        private Command _postponeBook;
        private Command _cancelPostpone;
        public ICommand ShowBookForCustomer => _showBookForCustomer;
        public ICommand ShowNewBookForCustomer => _showNewBookForCustomer;
        public ICommand FindByFilter => _findByFilter;
        public ICommand ShowPopularBooksForCustomer => _showPopularBooksForCustomer;
        public ICommand ShowPopularAuthors => _showPopularAuthors;
        public ICommand ShowPopularGenres => _showPopularGenres;
        public ICommand ShowShelvedBooks => _showShelvedBooks;
        public ICommand ShowPurchase => _showPurchase;
        public ICommand BuyBook => _buyBook;
        public ICommand PostponeBook => _postponeBook;
        public ICommand CancelPostpone => _cancelPostpone;

        public ObservableCollection<string> ComboBoxFindFilter { get; set; }
        public ObservableCollection<string> FilterGenre { get; set; }


        public CustomerViewModel(CustomerWindow customerWindow, DataGrid dataGrid, Customer _currentCustomer)
        {
            currentCustomer = _currentCustomer;
            CustomerName = currentCustomer.FirstName + " " + currentCustomer.LastName;
            _showAction = TableAction.None;
            _actionWithParam = TableActionWithParam.None;
            libraryAction = new LibraryAction(new DapperExecutor());
            ComboBoxFindFilter = new ObservableCollection<string>() { "Name", "Author", "Genre" };
            SelectedFilter = ComboBoxFindFilter.First();

            FilterGenre = new ObservableCollection<string>() { "Day", "Month", "Year" };
            SelectedGenreFilter = FilterGenre.First();

            _showBookForCustomer = new Command(obj =>
            {
                _actionWithParam = TableActionWithParam.None;
                _showAction = TableAction.ShowBookForCustomer;

                libraryAction.GetDataTable(_showAction, out _data);

                dataGrid.ItemsSource = _data.AsDataView();
            });

            _showNewBookForCustomer = new Command(obj =>
            {
                _actionWithParam = TableActionWithParam.None;
                _showAction = TableAction.ShowNewBookForCustomer;

                libraryAction.GetDataTable(_showAction, out _data);

                dataGrid.ItemsSource = _data.AsDataView();
            });

            _findByFilter = new Command(obj =>
            {
                switch (_selectedFilter)
                {
                    case "Name":
                        _actionWithParam = TableActionWithParam.FindInCustBookbyName;
                        break;
                    case "Author":
                        _actionWithParam = TableActionWithParam.FindInCustBookbyAuthor;
                        break;
                    case "Genre":
                        _actionWithParam = TableActionWithParam.FindInCustBookbyGenre;
                        break;
                    default:
                        return;
                }
                _showAction = TableAction.ShowBookForCustomer;
                libraryAction.GetDataTableWithParams(_actionWithParam, out _data, SearchText);

                dataGrid.ItemsSource = _data.AsDataView();
            });

            _showPopularBooksForCustomer = new Command(obj =>
            {
                _actionWithParam = TableActionWithParam.None;
                _showAction = TableAction.ShowPopularBooksForCustomer;

                libraryAction.GetDataTable(_showAction, out _data);

                dataGrid.ItemsSource = _data.AsDataView();
            });

            _showPopularAuthors = new Command(obj =>
            {
                _actionWithParam = TableActionWithParam.None;
                _showAction = TableAction.ShowPopularAuthors;

                libraryAction.GetDataTable(_showAction, out _data);

                dataGrid.ItemsSource = _data.AsDataView();
            });

            _showPopularGenres = new Command(obj =>
            {
                _actionWithParam = TableActionWithParam.None;
                switch (_selectedGenreFilter)
                {
                    case "Day":
                        _showAction = TableAction.ShowPopularGenresForDay;
                        break;
                    case "Month":
                        _showAction = TableAction.ShowPopularGenresForMonth;
                        break;
                    case "Year":
                        _showAction = TableAction.ShowPopularGenresForYear;
                        break;
                    default:
                        break;
                }
                libraryAction.GetDataTable(_showAction, out _data);

                dataGrid.ItemsSource = _data.AsDataView();
            });

            _showShelvedBooks = new Command(obj =>
            {
                _actionWithParam = TableActionWithParam.ShowShelvedBooksbyCustId;
                _showAction = TableAction.None;

                libraryAction.GetDataTableWithParams(_actionWithParam, out _data, currentCustomer.Id);

                dataGrid.ItemsSource = _data.AsDataView();
            });

            _showPurchase = new Command(obj =>
            {
                _actionWithParam = TableActionWithParam.ShowSalesbyCustId;
                _showAction = TableAction.None;

                libraryAction.GetDataTableWithParams(_actionWithParam, out _data, currentCustomer.Id);

                dataGrid.ItemsSource = _data.AsDataView();
            });

            _buyBook = new Command(obj =>
            {
                switch (_showAction)
                {
                    case TableAction.ShowBookForCustomer:
                    case TableAction.ShowNewBookForCustomer:
                    case TableAction.ShowPopularBooksForCustomer:
                        ShowBookForCustomer bookForCustomer = libraryAction.ShowBookForCustomers.ElementAtOrDefault(dataGrid.SelectedIndex);
                        if (bookForCustomer != null)
                        {
                            libraryAction.AddSale(bookForCustomer.Id, currentCustomer.Id, DateTime.Now, bookForCustomer.Total);

                            _showBookForCustomer.Execute(null);
                        }
                        break;
                    case TableAction.None:
                        if (_actionWithParam == TableActionWithParam.ShowShelvedBooksbyCustId)
                        {
                            ShowShelvedBook shelvedBook = libraryAction.ShowShelvedBooks.ElementAtOrDefault(dataGrid.SelectedIndex);
                            if (shelvedBook != null)
                            {
                                libraryAction.DeleteShelvedBook(shelvedBook.Id);
                                libraryAction.GetDataTable(TableAction.ShowBookForCustomer, out _data);
                                ShowBookForCustomer book = libraryAction.ShowBookForCustomers.Where(x => x.Id == shelvedBook.BookId).FirstOrDefault();
                                libraryAction.AddSale(book.Id, currentCustomer.Id, DateTime.Now, book.Total);

                                _showShelvedBooks.Execute(null);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }, obj =>
            {
                if (dataGrid.SelectedItem != null)
                {
                    switch (_showAction)
                    {
                        case TableAction.ShowBookForCustomer:
                        case TableAction.ShowNewBookForCustomer:
                        case TableAction.ShowPopularBooksForCustomer:
                            return true;
                        case TableAction.None:
                            if (_actionWithParam == TableActionWithParam.ShowShelvedBooksbyCustId)
                            {
                                return true;
                            }
                            break;
                        default:
                            break;
                    }
                }
                return false;
            });

            _postponeBook = new Command(obj =>
            {
                ShowBookForCustomer bookForCustomer = libraryAction.ShowBookForCustomers.ElementAt(dataGrid.SelectedIndex) ?? null;
                if (bookForCustomer != null)
                {
                    libraryAction.AddShelvedBook(bookForCustomer.Id, currentCustomer.Id);

                    libraryAction.GetDataTable(_showAction, out _data);

                    dataGrid.ItemsSource = _data.AsDataView();
                }
            }, obj =>
            {
                switch (_showAction)
                {
                    case TableAction.ShowBookForCustomer:
                    case TableAction.ShowNewBookForCustomer:
                    case TableAction.ShowPopularBooksForCustomer:
                        return true;
                    default:
                        break;
                }
                return false;
            });

            _cancelPostpone = new Command(obj =>
            {
                ShowShelvedBook shelvedBook = libraryAction.ShowShelvedBooks.ElementAtOrDefault(dataGrid.SelectedIndex);
                if (shelvedBook != null)
                {
                    libraryAction.DeleteShelvedBook(shelvedBook.Id);

                    _showShelvedBooks.Execute(null);
                }
            }, obj =>
            {
                return _actionWithParam == TableActionWithParam.ShowShelvedBooksbyCustId;
            });

        }

        public void CheckCommand()
        {
            _buyBook.Check();
            _postponeBook.Check();
            _cancelPostpone.Check();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
