using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DTO
{
    public class ProductDTO : ICloneable, INotifyPropertyChanged
    {
        public ProductDTO() {
            Block = 0;
        }
        public int ProId { get; set; }
        public string? ProName { get; set; }
        public string? Des { get; set;}
        public decimal Price { get; set; }
        public decimal? PromotionPrice { get; set; }
        public string? ImagePath { get; set; }
        public int CatID { get; set; }
        public int Quantity { get; set; }
        public int? PromoID{ get; set; }
        public int? Block { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public object Clone()
        {
            ProductDTO clonedProduct = new ProductDTO();
            clonedProduct.ProId = this.ProId;
            clonedProduct.ProName = this.ProName;
            clonedProduct.Des = this.Des;
            clonedProduct.Price = this.Price;
            clonedProduct.PromotionPrice = this.PromotionPrice;
            clonedProduct.ImagePath = this.ImagePath;
            clonedProduct.CatID = this.CatID;
            clonedProduct.Quantity = this.Quantity;
            clonedProduct.PromoID = this.PromoID;
            clonedProduct.Block = this.Block;

            return clonedProduct;
        }

        public void copy(ProductDTO other)
        {
            this.ProId = other.ProId;
            this.ProName = other.ProName;
            this.Des = other.Des;
            this.Price = other.Price;
            this.PromotionPrice = other.PromotionPrice;
            this.ImagePath = other.ImagePath;
            this.CatID = other.CatID;
            this.Quantity = other.Quantity;
            this.PromoID = other.PromoID;
            this.Block = other.Block;
        }
    }
}
