using MyShop.DAO;
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
using System.Windows.Shapes;

namespace MyShop.UI
{
    /// <summary>
    /// Interaction logic for ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public ConfigWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string servername = ServerTermTextBox.Text;
            string username = UsernameTermTextBox.Text;
            string password = PasswordTermTextBox.Text;
            string databasename = DatabaseTermTextBox.Text;

            new DatabaseUtilities(
                  servername,
                  databasename,
                  username,
                  password
            );

            if (DatabaseUtilities.isSelectedDatabase == true)
            {
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }

        }
    }
}
