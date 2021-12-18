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
        }

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((AdminViewModel)this.DataContext).CheckCommand();
        }
    }
}
