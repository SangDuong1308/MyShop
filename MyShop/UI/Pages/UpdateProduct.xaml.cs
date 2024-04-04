using Microsoft.Win32;
using MyShop.BUS;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.IO;
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
using Path = System.IO.Path;

namespace MyShop.UI.Pages
{
    /// <summary>
    /// Interaction logic for UpdateProduct.xaml
    /// </summary>
    public partial class UpdateProduct : Page
    {
        private bool _isImageChanged = false;
        private FileInfo? _selectedImage = null;
        private CategoryBUS _categoryBUS;
        private ProductBUS _productBUS;
        private PromotionBUS _promotionBUS;
        private System.Windows.Controls.Frame _pageNavegation;
        private ProductDTO _productDTO;
        private CategoryDTO _categoryDTO;
        public UpdateProduct(ProductDTO product, CategoryDTO categoryDTO, System.Windows.Controls.Frame pageNavigation)
        {
            InitializeComponent();

            _categoryDTO = categoryDTO;
            _pageNavegation = pageNavigation;
            _categoryBUS = new CategoryBUS();
            _productBUS = new ProductBUS();
            _promotionBUS = new PromotionBUS();

            var categories = _categoryBUS.getAll();


            CategoryCombobox.ItemsSource = categories;

            foreach (var category in categories)
            {
                if (category.CatID == categoryDTO.CatID)
                {
                    categoryDTO = category;
                }
            }

            CategoryCombobox.SelectedValue = categoryDTO;
            _productDTO = product;

            DataContext = product;
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            screen.Filter = "Files|*.png; *.jpg; *.jpeg;";
            if (screen.ShowDialog() == true)
            {
                _isImageChanged = true;
                _selectedImage = new FileInfo(screen.FileName);

                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(screen.FileName, UriKind.Absolute);
                bitmap.EndInit();

                AddedImage.Source = bitmap;
            }
        }

        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var categoryDTO = (CategoryDTO)CategoryCombobox.SelectedValue;

                int discountPercent = 0;
                if (_productDTO.PromoID != null)
                {
                    discountPercent = _promotionBUS.getPromotionById((int)_productDTO.PromoID).DiscountPercent;
                }
                int id = _productDTO.ProId;
                _productDTO.ProId = id;
                _productDTO.ProName = NameTermTextBox.Text;
                _productDTO.Des = DesTermTextBox.Text;
                _productDTO.Price = Decimal.Parse(PriceTermTextBox.Text);
                double percent = 1 - discountPercent * 1.0 / 100;
                _productDTO.PromotionPrice = (decimal?)((double)_productDTO.Price * percent);
                _productDTO.CatID = categoryDTO.CatID;

                var categoryTemp = _categoryBUS.getCategoryById(_productDTO.CatID);
                _categoryDTO.CatID = categoryTemp.CatID;
                _categoryDTO.CatName = categoryTemp.CatName;
                _categoryDTO.CatIcon = categoryTemp.CatIcon;

                _productDTO.Quantity = int.Parse(QuantityTermTextBox.Text);
                _productDTO.Block = 0;

                _productBUS.patchProduct(_productDTO);

                //Handle selected Image
                if (_selectedImage != null && _isImageChanged)
                {
                    string key = Path.GetFileNameWithoutExtension(_selectedImage.Name);
                    _productDTO.ImagePath = _productBUS.uploadImage(_selectedImage, id, key);
                }

                MessageBox.Show("Updated successfully", "Notification", MessageBoxButton.OK);
                _pageNavegation.NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavegation.NavigationService.GoBack();
        }
    }
}
