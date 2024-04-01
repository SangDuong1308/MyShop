using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Win32;
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

namespace MyShop.UI.Pages
{
    public partial class Home : System.Windows.Controls.Page
    {
        class Resources
        {
            public string FirstIcon { get; set; }
            public string LastIcon { get; set; }
            public string NextIcon { get; set; }
            public string PrevIcon { get; set; }
            public int ProductListWidth { get; set; }
        }

        class Data
        {
            public string? ProName { get; set; }
            public string? ProImage { get; set; }
            public string? CatIcon { get; set; }
            public string? CatName { get; set; }
            public decimal? PromotionPrice { get; set; }
            public string ScaleValue { get; set; }
            public int DiscountPercent { get; set; }

            public Data(ProductDTO productDTO, CategoryDTO categoryDTO, int discountPercent)
            {
                ProName = productDTO.ProName;
                ProImage = productDTO.ImagePath;
                PromotionPrice = productDTO.PromotionPrice;
                CatIcon = categoryDTO.CatIcon;
                CatName = categoryDTO.CatName;
                ScaleValue = "1";
                DiscountPercent = discountPercent;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
