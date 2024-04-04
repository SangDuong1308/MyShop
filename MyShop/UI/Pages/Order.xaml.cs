using MyShop.BUS;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Design.Behavior;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyShop.UI.Pages
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        Frame _pageNavigation;
        private OrderBUS _orderBUS;
        private CustomerBUS _customerBUS;
        private ObservableCollection<ShopOrderDTO>? _orders = null;
        private ObservableCollection<Data> _list;
        private int _currentPage = 1;
        private int _rowsPerPage = 8;
        private int _totalItems = 0;
        private int _totalPages = 0;
        private DateTime? _currentStartDate = null;
        private DateTime? _currentEndDate = null;
        private ProgressBar _loadingProgressBar;
        public class Data
        {
            public int OrderID { get; set; }
            public DateTime CreateAt { get; set; }
            public string CusName { get; set; }
            public string FinalTotal { get; set; }

            public Data(ShopOrderDTO list, CustomerBUS customer)
            {

                customer = new CustomerBUS();
                OrderID = list.OrderID;
                CreateAt = list.CreateAt.Date;
                CusName = customer.getNameById(list.CusID);
                FinalTotal = string.Format("{0:N0} $", list.FinalTotal);
            }
        }

        public Order(Frame pageNavigation, ProgressBar loadingProgressBar)
        {
            _pageNavigation = pageNavigation;
            _customerBUS = new CustomerBUS();
            _orderBUS = new OrderBUS();
            _loadingProgressBar = loadingProgressBar;
            InitializeComponent();
        }

        private async void updateDataSource(int page = 1, DateTime? startDate = null, DateTime? endDate = null)
        {
            ObservableCollection<Data> list = new ObservableCollection<Data>();
            try
            {
                _currentPage = page;
                _loadingProgressBar.IsIndeterminate = true;
                (_orders, _totalItems) = await _orderBUS.findOrderBySearch(_currentPage, _rowsPerPage, startDate, endDate);
                _loadingProgressBar.IsIndeterminate = false;
                foreach (var order in _orders)
                {
                    list.Add(new Data(order, _customerBUS));
                }
                ordersListView.ItemsSource = list;

                infoTextBlock.Text = $"Showing {_orders.Count} of {_totalItems} Orders";
                _list = list;
                updatePagingInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occur: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void updatePagingInfo()
        {
            _totalPages = _totalItems / _rowsPerPage +
                   (_totalItems % _rowsPerPage == 0 ? 0 : 1);

            pageInfoTextBlock.Text = $"{_currentPage}/{_totalPages}";
        }


        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.NavigationService.Navigate(new AddOrder(_pageNavigation, _list));
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int i = ordersListView.SelectedIndex;

            var order = _orders[i];
            if (order != null)
            {
                //_pageNavigation.NavigationService.Navigate(new OrderDetail(order, _pageNavigation));
            }
        }

        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            updateDataSource(1, _currentStartDate, _currentEndDate);
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                updateDataSource(_currentPage, _currentStartDate, _currentEndDate);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                updateDataSource(_currentPage, _currentStartDate, _currentEndDate);
            }
        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {
            updateDataSource(_totalPages, _currentStartDate, _currentEndDate);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                updateDataSource(1, _currentStartDate, _currentEndDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading orders: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            _currentStartDate = StartDate.SelectedDate;
            _currentEndDate = EndDate.SelectedDate;
            updateDataSource(1, _currentStartDate, _currentEndDate);
            updatePagingInfo();
        }
    }
}
