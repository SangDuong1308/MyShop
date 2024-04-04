using MyShop.BUS;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace MyShop.UI.Pages
{
    /// <summary>
    /// Interaction logic for OrderDetail.xaml
    /// </summary>
    public partial class OrderDetail : Page
    {
        public class Data : INotifyPropertyChanged
        {
            public ProductDTO Product { get; set; }
            public PurchaseDTO Purchase { get; set; }

            public event PropertyChangedEventHandler? PropertyChanged;
        }

        public class OrderInfo : INotifyPropertyChanged
        {
            public CustomerDTO Customer { get; set; }
            public ShopOrderDTO Order { get; set; }

            public event PropertyChangedEventHandler? PropertyChanged;
        }


        private ShopOrderDTO _order;
        private List<ProductDTO> _products;
        private List<PurchaseDTO> _purchases;
        private OrderBUS _orderBUS;
        private CustomerDTO _customer;
        private Frame _pageNavigation;
        private ObservableCollection<Data>? _dataList;
        private OrderInfo? _orderInfo;

        public OrderDetail(ShopOrderDTO order, Frame pageNavigation)
        {
            InitializeComponent();
            try
            {
                var productBUS = new ProductBUS();
                var orderBUS = new OrderBUS();
                _orderBUS = orderBUS;
                var customerBUS = new CustomerBUS();

                // Initialize data
                _products = new List<ProductDTO>();
                _order = order;
                _pageNavigation = pageNavigation;

                _purchases = orderBUS.findPurchaseDTOs(_order.OrderID);

                ObservableCollection<Data> dataList = new ObservableCollection<Data>();
                foreach (var purchase in _purchases)
                {
                    var product = productBUS.findProductById(purchase.ProID);
                    _products.Add(product);
                    Data item = new Data();
                    item.Product = product;
                    item.Purchase = purchase;
                    dataList.Add(item);
                }

                _customer = customerBUS.findCustomerById(_order.CusID);
                OrderInfo orderInfo = new OrderInfo();
                orderInfo.Customer = _customer;
                orderInfo.Order = _order;

                DataContext = orderInfo;
                _orderInfo = orderInfo;
                productsListView.ItemsSource = dataList;
                _dataList = dataList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while initializing OrderDetail: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.NavigationService.GoBack();
        }

        private void DelOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult choice = MessageBox.Show("Are you sure?", "Notification", MessageBoxButton.OKCancel);

            if (choice == MessageBoxResult.OK)
            {
                _orderBUS.delOrderById(_order.OrderID);
                _pageNavigation.NavigationService.GoBack();
            }
            else
            {

            }

        }

        private void UpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            //_pageNavigation.NavigationService.Navigate(new UpdateOrder(_pageNavigation, _orderInfo, _dataList));
        }

    }
}
