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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyShop.UI.Pages
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Page
    {
        private CustomerBUS _customerBUS;
        private ObservableCollection<CustomerDTO> _customers;
        private Frame _pageNavigation;
        private ProgressBar _loadingProgressBar;
        public Customer(Frame pageNavigation, ProgressBar loadingProgressBar)
        {
            _pageNavigation = pageNavigation;
            _loadingProgressBar = loadingProgressBar;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _customerBUS = new CustomerBUS();

                _customers = _customerBUS.getAll();

                categoriesListView.ItemsSource = _customers;
            }
            catch (Exception ex)
            {
                // Handle the exception, you can log it or display an error message
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveCategory_Click(object sender, RoutedEventArgs e)
        {
            var customer = new CustomerDTO();

            customer.CusName = NameTermTextBox.Text;
            customer.CusTel = TelTermTextBox.Text;
            customer.CusAddress = AddTermTextBox.Text;


            int id = _customerBUS.addCustomer(customer);

            customer.CusID = id;
            _customers.Add(customer);

            MessageBox.Show("Customer added successfully!", "Notification", MessageBoxButton.OK);
        }

        private void DelCategory_Click(object sender, RoutedEventArgs e)
        {
            int i = categoriesListView.SelectedIndex;

            if (i == -1)
            {
                MessageBox.Show("Please choose category", "Notification", MessageBoxButton.OK);
            }
            else
            {
                MessageBoxResult choice = MessageBox.Show("Are you sure?", "Notification", MessageBoxButton.OKCancel);

                if (choice == MessageBoxResult.OK)
                {

                    var CusID = _customers[i].CusID;
                    bool isSuccess; string message;

                    (isSuccess, message) = _customerBUS.delCustomerById(CusID);
                    if (!isSuccess)
                    {
                        MessageBox.Show(message, "Notification");
                    }
                    else
                    {
                        _customers.RemoveAt(i);
                    }
                }
                else
                {

                }
            }
        }
    }
}
