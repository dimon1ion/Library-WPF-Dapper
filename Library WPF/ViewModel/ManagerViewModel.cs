using Library_WPF.Model;
using Library_WPF.Service;
using Library_WPF.Service.Command;
using Library_WPF.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Library_WPF.ViewModel
{
    internal class ManagerViewModel : INotifyPropertyChanged
    {
        private LibraryAction libraryActions;
        private TableAction _showAction;
        private DapperExecutor _dapper;
        private DataTable _data;
        private string _managerName;


        public string ManagerName
        {
            get => _managerName;
            set
            {
                if (!Equals(_managerName, value))
                {
                    _managerName = value;
                    OnPropertyChanged(nameof(ManagerName));
                }
            }
        }

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


        public ManagerViewModel(ManagerWindow managerWindow, Salesman currentSalesman, DataGrid dataGrid)
        {
            ManagerName = currentSalesman.FirstName + " " + currentSalesman.LastName;
            _data = new DataTable();
            _dapper = new DapperExecutor();
            _showAction = TableAction.None;
            libraryActions = new LibraryAction(_dapper);

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
                libraryActions.AddNewValue(_showAction, ref dataGrid);
                Refresh();

            }, obj =>
            {
                return _showAction != TableAction.None;
            });

        }

        private void Refresh()
        {
            switch (_showAction)
            {
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
                default:
                    break;
            }
        }

        public void CheckCommand()
        {
            _update.Check();
            _delete.Check();
            _add.Check();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
