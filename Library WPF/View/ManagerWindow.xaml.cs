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
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow(Salesman currentSalesman)
        {
            InitializeComponent();
            this.DataContext = new ManagerViewModel(this, currentSalesman, grid);
        }

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((ManagerViewModel)this.DataContext).CheckCommand();
        }
    }
}
