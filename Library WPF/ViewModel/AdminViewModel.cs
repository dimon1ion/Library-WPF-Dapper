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
    public enum TableAction
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
        ShowBookForCustomer,
        ShowNewBookForCustomer,
        ShowPopularBooksForCustomer,
        ShowPopularAuthors,
        ShowPopularGenresForDay,
        ShowPopularGenresForMonth,
        ShowPopularGenresForYear,
        None
    }
    public class AdminViewModel : INotifyPropertyChanged
    {
        private DataTable _data;
        private DapperExecutor _dapper;
        private LibraryAction libraryActions;

        private TableAction _showAction;

        //private IEnumerable<Customer> customers;
        //private IEnumerable<Salesman> salesmen;
        //private IEnumerable<Admin> admins;
        //private IEnumerable<Author> authors;
        //private IEnumerable<Publisher> publishers;
        //private IEnumerable<Genre> genres;
        //private IEnumerable<ShowBook> showBooks;
        //private IEnumerable<ShowSale> showSales;
        //private IEnumerable<ShowShelvedBook> showShelvedBooks;
        //private IEnumerable<Stock> stocks;
        //private IEnumerable<ShowStockBook> showStockBooks;

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
        private Command _add;

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
        public ICommand Add => _add;

        public AdminViewModel(AdminWindow adminWindow, DataGrid dataGrid)
        {
            //_data = ((DataView)dataGrid.ItemsSource).ToTable();
            //MessageBox.Show(dataGrid.SelectedItem.ToString());
            //MessageBox.Show(((DataRowView)dataGrid.SelectedItem).Row["Id"].ToString());
            _data = new DataTable();
            _dapper = new DapperExecutor();
            _showAction = TableAction.None;
            libraryActions = new LibraryAction(_dapper);

            _showCustomers = new Command(obj =>
            {
                _showAction = TableAction.ShowCustomers;
                CheckCommand();

                libraryActions.GetDataTable(_showAction, out _data);

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

            _showManagers = new Command(obj =>
            {
                _showAction = TableAction.ShowManagers;
                CheckCommand();

                libraryActions.GetDataTable(_showAction, out _data);

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

            _showAdmins = new Command(obj =>
            {
                _showAction = TableAction.ShowAdmins;
                CheckCommand();

                libraryActions.GetDataTable(_showAction, out _data);

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

            _showAuthors = new Command(obj =>
            {
                _showAction = TableAction.ShowAuthors;
                CheckCommand();

                libraryActions.GetDataTable(_showAction, out _data);

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

            _showPublishers = new Command(obj =>
            {
                _showAction = TableAction.ShowPublishers;
                CheckCommand();

                libraryActions.GetDataTable(_showAction, out _data);

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

            _showGenres = new Command(obj =>
            {
                _showAction = TableAction.ShowGenres;
                CheckCommand();

                libraryActions.GetDataTable(_showAction, out _data);

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

            _showBooks = new Command(obj =>
            {
                _showAction = TableAction.ShowBooks;
                CheckCommand();

                libraryActions.GetDataTable(_showAction, out _data);

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

            _showSales = new Command(obj =>
            {
                _showAction = TableAction.ShowSales;
                CheckCommand();

                libraryActions.GetDataTable(_showAction, out _data);

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

            _showShelvedBooks = new Command(obj =>
            {
                _showAction = TableAction.ShowShelvedBooks;
                CheckCommand();

                libraryActions.GetDataTable(_showAction, out _data);

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

            _showStocks = new Command(obj =>
            {
                _showAction = TableAction.ShowStocks;
                CheckCommand();

                libraryActions.GetDataTable(_showAction, out _data);

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

            _showStockBooks = new Command(obj =>
            {
                _showAction = TableAction.ShowStockBooks;
                CheckCommand();

                libraryActions.GetDataTable(_showAction, out _data);

                dataGrid.ItemsSource = _data.AsDataView();
            }); // Ready

            _update = new Command(obj =>
            {
                libraryActions.UpdateGrid(_showAction, out _data, ref dataGrid);
                Refresh();


            }, obj =>
            {
                return _showAction != TableAction.ShowShelvedBooks && _showAction != TableAction.None;
            });

            _delete = new Command(obj =>
            {
                try
                {
                    libraryActions.DeleteFromGrid(_showAction, ref dataGrid);
                    MessageBoxResult dialogResult = MessageBox.Show("Delete successful! Do you want refresh table?", "", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        Refresh();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error, check the selected row", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }, obj =>
            {
                return dataGrid.SelectedItem != null;
            });

            _add = new Command(obj =>
            {
                libraryActions.AddNewValueFromGrid(_showAction, ref dataGrid);
                Refresh();

            }, obj =>
            {
                return _showAction != TableAction.None;
            });

        }

        public void CheckCommand()
        {
            _update.Check();
            _delete.Check();
            _add.Check();
        }

        private void Refresh()
        {
            switch (_showAction)
            {
                case TableAction.ShowCustomers:
                    _showCustomers.Execute(null);
                    break;
                case TableAction.ShowManagers:
                    _showManagers.Execute(null);
                    break;
                case TableAction.ShowAdmins:
                    _showAdmins.Execute(null);
                    break;
                case TableAction.ShowAuthors:
                    ShowAuthors.Execute(null);
                    break;
                case TableAction.ShowPublishers:
                    _showPublishers.Execute(null);
                    break;
                case TableAction.ShowGenres:
                    _showGenres.Execute(null);
                    break;
                case TableAction.ShowBooks:
                    _showBooks.Execute(null);
                    break;
                case TableAction.ShowSales:
                    _showSales.Execute(null);
                    break;
                case TableAction.ShowStocks:
                    _showStocks.Execute(null);
                    break;
                case TableAction.ShowStockBooks:
                    _showStockBooks.Execute(null);
                    break;
                case TableAction.ShowShelvedBooks:
                    _showShelvedBooks.Execute(null);
                    break;
                case TableAction.None:
                    break;
                default:
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string properyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }
    }
}
