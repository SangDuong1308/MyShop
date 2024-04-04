using MyShop.UI.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Diagnostics;

namespace MyShop.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        const int DashBoard = 0;
        const int Stock = 1;
        const int Customers = 2;
        const int Categories = 3;
        //const int Promotion = 3;
        const int Orders = 4;
        const int Report = 5;
        private int _currentPage = 0;

        ObservableCollection<Item> Items = null;

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Items = new ObservableCollection<Item>()
            {
                new Item()
                {
                    FontIcon = "Home",
                    ItemName = "Dashboard"
                },
                new Item()
                {
                    FontIcon = "Box",
                    ItemName = "Stock"
                },
                new Item()
                {
                    FontIcon = "UserGroup",
                    ItemName = "Customers"
                },
                new Item()
                {
                    FontIcon = "Tag",
                    ItemName = "Categories"
                },
                new Item()
                {
                    FontIcon = "Truck",
                    ItemName = "Orders"
                },
                new Item()
                {
                    FontIcon = "PieChart",
                    ItemName = "Report"
                }
            };

            ListOfItems.ItemsSource = Items;


            var currentpage = Properties.Settings.Default.CurrentPage;

            _currentPage = int.TryParse(currentpage, out var currentPageInt) ? currentPageInt : 0;

            ListOfItems.SelectedIndex = _currentPage;

        }

        private void ListOfItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedIndex = ListOfItems.SelectedIndex;
            Properties.Settings.Default.CurrentPage = selectedIndex.ToString();

            Properties.Settings.Default.Save();

            changePage(selectedIndex, e);
        }

        private void changePage(int selectedIndex, SelectionChangedEventArgs e)
        {
            if (selectedIndex == DashBoard)
            {
                pageNavigation.NavigationService.Navigate(new DashBoard(loadingProgressBar));
            }

            if (selectedIndex == Stock)
            {
                pageNavigation.NavigationService.Navigate(new Home(pageNavigation, loadingProgressBar));
            }

            if (selectedIndex == Customers)
            {
                pageNavigation.NavigationService.Navigate(new Customer(pageNavigation, loadingProgressBar));
            }

            if (selectedIndex == Categories)
            {
                pageNavigation.NavigationService.Navigate(new Category(pageNavigation, loadingProgressBar));
            }

            if (selectedIndex == Orders)
            {
                pageNavigation.NavigationService.Navigate(new Order(pageNavigation, loadingProgressBar));
            }
            
            if (selectedIndex == Report)
            {
                pageNavigation.NavigationService.Navigate(new Report(pageNavigation,loadingProgressBar));
            }

            resetBorder();


            var addedContainer = ListOfItems.ItemContainerGenerator.ContainerFromItem(e.AddedItems[0]) as ListViewItem;
            if (addedContainer != null)
            {
                var border = FindVisualChild<Border>(addedContainer);
                if (border != null)
                {
                    border.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    border.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                    border.CornerRadius = new CornerRadius(10);
                    border.BorderThickness = new Thickness(1);
                    border.Width = 140;
                }
            }


            if (e.RemovedItems.Count > 0)
            {
                var removedContainer = ListOfItems.ItemContainerGenerator.ContainerFromItem(e.RemovedItems[0]) as ListViewItem;
                if (removedContainer != null)
                {
                    var border = FindVisualChild<Border>(removedContainer);
                    if (border != null)
                    {
                        border.Background = new SolidColorBrush(Colors.Transparent);
                        border.BorderThickness = new Thickness(0);
                    }
                }
            }
        }

        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T result)
                {
                    return result;
                }
                else
                {
                    T childResult = FindVisualChild<T>(child);
                    if (childResult != null)
                    {
                        return childResult;
                    }
                }
            }
            return null;
        }

        async private void ListOfItems_Loaded(object sender, RoutedEventArgs e)
        {
            if (ListOfItems.Items.Count > 0)
            {
                await Task.Delay(50);
                var container = ListOfItems.ItemContainerGenerator.ContainerFromIndex(_currentPage) as ListViewItem;
                if (container != null)
                {
                    var border = FindVisualChild<Border>(container);
                    if (border != null)
                    {
                        border.Background = new SolidColorBrush(Colors.WhiteSmoke);
                        border.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                        border.CornerRadius = new CornerRadius(10);
                        border.BorderThickness = new Thickness(1);
                        border.Width = 140;
                    }
                }
            }
        }

        private void resetBorder()
        {
            foreach (var item in ListOfItems.Items)
            {
                var container = ListOfItems.ItemContainerGenerator.ContainerFromItem(item) as ListViewItem;
                if (container != null)
                {
                    var border = FindVisualChild<Border>(container);
                    if (border != null)
                    {
                        border.Background = new SolidColorBrush(Colors.Transparent);
                        border.BorderThickness = new Thickness(0);
                    }
                }
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.UsernameRemember = false;
            Properties.Settings.Default.Save();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
