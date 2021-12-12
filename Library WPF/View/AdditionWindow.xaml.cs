using Library_WPF.Service.Interface;
using Library_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AdditionWindow.xaml
    /// </summary>
    public partial class AdditionWindow : Window
    {
        public AdditionWindow(TableAction showTable, IRepository executor)
        {
            InitializeComponent();
            this.DataContext = new AdditionViewModel(this, showTable, executor);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int num;
            e.Handled = !Int32.TryParse(e.Text,out num);
        }
    }
}
