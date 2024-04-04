using MyShop.BUS;
using MyShop.DTO;
using System;
using System.Collections.Generic;
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
using System.Xml.Linq;

namespace MyShop.UI.Pages
{
    /// <summary>
    /// Interaction logic for ProductDetail.xaml
    /// </summary>
    public partial class ProductDetail : Page
    {
        private ProductDTO _product;
        private Frame _pageNavigation;
        private ProductBUS _productBUS;
        private CategoryBUS _categoryBUS;
        private PromotionBUS _promotionBUS;
        private CategoryDTO _category;
        protected PromotionDTO _promotion;
        private Home _home;
        private Data _currentData;
        private ProgressBar _loadingProgressBar;

        class Data : INotifyPropertyChanged
        {
            public string ProImage { get; set; }
            public ProductDTO Product { get; set; }
            public CategoryDTO Category { get; set; }
            public PromotionDTO? Promotion { get; set; }
            public string CatIcon { get; set; }
            public Data(ProductDTO productDTO, CategoryDTO categoryDTO, PromotionDTO? promotion)
            {
                this.Product = productDTO;
                this.Category = categoryDTO;
                ProImage = "\\" + productDTO.ImagePath;
                if (promotion != null)
                {
                    this.Promotion = promotion;
                }
                else
                {
                    this.Promotion = new();
                    this.Promotion.DiscountPercent = 0;
                }

            }

            public event PropertyChangedEventHandler? PropertyChanged;
        }

        public ProductDetail(Home home, ProductDTO product, Frame pageNavigation, ProgressBar loadingProgressBar)
        {
            InitializeComponent();
            _home = home;
            _product = product;
            _pageNavigation = pageNavigation;
            _productBUS = new ProductBUS();
            _categoryBUS = new CategoryBUS();
            _promotionBUS = new PromotionBUS();
            _loadingProgressBar = loadingProgressBar;

            CategoryDTO category = _categoryBUS.getCategoryById(_product.CatID);
            PromotionDTO promotion = null;
            var promotions = _promotionBUS.getAll();

            PromotionsCombobox.ItemsSource = promotions;
            if (_product.PromoID != null)
            {
                promotion = _promotionBUS.getPromotionById((int)_product.PromoID);
                _promotion = promotion;

                PromotionsCombobox.SelectedValue = (PromotionDTO)promotions.Where(item => item.IdPromo == _product.PromoID).ToList()[0];
            }
            else 
            {
                PromotionsCombobox.SelectedIndex = 0;
                _promotion = null;
            }

            _category = category;
            Data data = new(_product, category, _promotion);
            _currentData = data;

            DataContext = data;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var (key, page, currency, startPrice, endPrice) = _home.getCurrentState();

            _pageNavigation.NavigationService.Navigate(new Home(_pageNavigation, _loadingProgressBar, page, currency, key, startPrice, endPrice));
        }

        private void DelProduct_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult choice = MessageBox.Show("Are you sure?", "Warning", MessageBoxButton.OKCancel);

            if (choice == MessageBoxResult.OK)
            {
                var (key, page, currency, startPrice, endPrice) = _home.getCurrentState();
                _productBUS.delProduct(_product.ProId);
                _pageNavigation.NavigationService.Navigate(new Home(_pageNavigation, _loadingProgressBar, page, currency, key, startPrice, endPrice));
            }
            else
            {

            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var clonedProduct = (ProductDTO)_product.Clone();

            _pageNavigation.NavigationService.Navigate(new UpdateProduct(_product, _category, _pageNavigation));
        }

        int flag = 0;
        private void PromotionsCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var promotion = (PromotionDTO)PromotionsCombobox.SelectedValue;

            if (promotion != null && flag != 0)
            {
                _currentData.Promotion.copy(promotion);

                _product.PromoID = _currentData.Promotion.IdPromo;
                double percent = 1 - promotion.DiscountPercent * 1.0 / 100;
                _product.PromotionPrice = (decimal?)((double)_product.Price * percent);
                _productBUS.patchProduct(_product);
                _promotion = promotion;
                MessageBox.Show("Promotion added successfully", "Notification");
            }
            flag = 1;
        }
    }
}
