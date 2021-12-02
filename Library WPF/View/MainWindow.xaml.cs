using Library_WPF.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library_WPF.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DapperExecutor.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Library; Integrated Security=True;";
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.ShowDialog();
        }

        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Label).Foreground = Brushes.Purple;
        }

        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Label).Foreground = Brushes.Blue;
        }
    }
}
