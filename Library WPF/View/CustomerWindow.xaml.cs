using Library_WPF.Model;
using Library_WPF.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerWindow(Customer currentCustomer)
        {
            InitializeComponent();
            this.DataContext = new CustomerViewModel(this, grid, currentCustomer);
        }

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((CustomerViewModel)this.DataContext).CheckCommand();
        }
    }
}
