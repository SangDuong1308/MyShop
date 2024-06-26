﻿using Microsoft.Data.SqlClient;
using MyShop.DTO;
using MyShop.BUS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.DAO
{
    internal class ProductDAO
    {
        DatabaseUtilities db = DatabaseUtilities.getInstance();
        private ObservableCollection<PromotionDTO> _promotion = (new PromotionDAO()).getAll();
        public async Task<ObservableCollection<ProductDTO>> getAll()
        {
            ObservableCollection<ProductDTO> list = new ObservableCollection<ProductDTO>();

            await Task.Run(() =>
            {
                string sql = "select ProID, ProName, Des," +
                            " Price, ImagePath," +
                            "CatID, Quantity,PromoID, PromotionPrice, Block from product where Block = 0";
                var command = new SqlCommand(sql, db.connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProductDTO product = new ProductDTO();
                    product.ProId = (int)reader["ProID"];
                    product.ProName = reader["ProName"] == DBNull.Value ? "Product name error" : (string?)reader["ProName"];
                    product.Des = reader["Des"] == DBNull.Value ? null : (string?)reader["Des"];
                    product.Price = (decimal)reader["Price"];
                    product.ImagePath = reader["ImagePath"] == DBNull.Value ? "/Assets/Images/placeholder.png" : (string?)reader["ImagePath"];
                    product.CatID = (int)reader["CatID"];
                    product.Quantity = (int)reader["Quantity"];
                    product.PromotionPrice = reader["PromotionPrice"] == DBNull.Value ? product.Price : (decimal?)reader["PromotionPrice"];
                    product.PromoID = reader["PromoID"] == DBNull.Value ? null : (int?)reader["PromoID"];
                    product.Block = (int)reader["Block"];
                    list.Add(product);
                }

                System.Threading.Thread.Sleep(1000);

                reader.Close();
            });


            return list;
        }


        // TODO: code này là dựa trên mã nguồn của Thầy .
        public ObservableCollection<ProductDTO> getProducts(int currentPage = 1, int rowsPerPage = 10,
                string keyword = "")
        {
            var list = new ObservableCollection<ProductDTO>();
            string sql = "select ProID, ProName,Des," +
                         " Price, ImagePath," +
                         "CatID, PromoID  from product";

            var command = new SqlCommand(sql, db.connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                ProductDTO product = new ProductDTO();
                product.ProId = (int)reader["ProID"];
                product.ProName = reader["ProName"] == DBNull.Value ? "Product name error" : (string?)reader["ProName"];
                product.Des = reader["Des"] == DBNull.Value ? null : (string?)reader["Des"];
                product.Price = (decimal)reader["Price"];
                product.ImagePath = reader["ImagePath"] == DBNull.Value ? "/Assets/Images/placeholder.png" : (string?)reader["ImagePath"];
                product.PromotionPrice = reader["PromotionPrice"] == DBNull.Value ? (decimal)reader["Price"] : (decimal?)reader["PromotionPrice"];
                product.CatID = (int)reader["CatID"];
                product.PromoID = reader["PromoID"] == DBNull.Value ? null : (int?)reader["PromoID"];

                list.Add(product);
            }

            reader.Close();

            return list;
        }

        public void deleteProductById(int ProId)
        {
            string sql = $"""
                update product 
                set Block = {1}

                where ProID = {ProId}
                """;

            var command = new SqlCommand(sql, db.connection);

            command.ExecuteNonQuery();
        }
        public int insertProduct(ProductDTO productDTO)
        {
            // insert SQL
            string sql = "insert into product(ProName,Des, Price, PromotionPrice, CatID, Quantity,Block)" +
            "values(@ProName, @Des, @Price, @PromotionPrice, @CatID, @Quantity, @Block)";
            var command = new SqlCommand(sql, db.connection);

            command.Parameters.Add("@ProName", SqlDbType.NVarChar).Value = productDTO.ProName;
            command.Parameters.Add("@Des", SqlDbType.NVarChar).Value = productDTO.Des;
            command.Parameters.Add("@Price", SqlDbType.Money).Value = productDTO.Price;
            command.Parameters.Add("@PromotionPrice", SqlDbType.Money).Value = productDTO.PromotionPrice == null? productDTO.Price : productDTO.PromotionPrice;
            command.Parameters.Add("@CatID", SqlDbType.Int).Value = productDTO.CatID;
            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = productDTO.Quantity;
            command.Parameters.Add("@Block", SqlDbType.Int).Value = productDTO.Block;

            command.ExecuteNonQuery();

            // select SQL
            int id = -1;
            string sql1 = "SELECT TOP 1 ProID FROM product ORDER BY ProID DESC ";

            var command1 = new SqlCommand(sql1, db.connection);

            var reader = command1.ExecuteReader();
            while (reader.Read())
            {
                id = (int)reader["ProID"];
            }

            reader.Close();

            return id;
        }

        public void updateProduct(ProductDTO productDTO)
        {
            string sql = "update product " +
                "set ProName =  @ProName, Des = @Des," +
                "Price = @Price," + "CatID = @CatID, Quantity = @Quantity, PromoID = @PromoID, PromotionPrice = @PromotionPrice, Block = @Block " +
                "where ProID = @ProID";
            var command = new SqlCommand(sql, db.connection);

            command.Parameters.Add("@ProID", SqlDbType.Int).Value = productDTO.ProId;
            command.Parameters.Add("@ProName", SqlDbType.NVarChar).Value = productDTO.ProName;
            command.Parameters.Add("@Des", SqlDbType.NVarChar).Value = productDTO.Des;
            command.Parameters.Add("@Price", SqlDbType.Money).Value = productDTO.Price;
            command.Parameters.Add("@CatID", SqlDbType.Int).Value = productDTO.CatID;
            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = productDTO.Quantity;
            command.Parameters.Add("@PromotionPrice", SqlDbType.Money).Value = productDTO.PromotionPrice == null ? productDTO.Price : productDTO.PromotionPrice;
            command.Parameters.Add("@PromoID", SqlDbType.Int).Value = productDTO.PromoID == null ? DBNull.Value : productDTO.PromoID;
            command.Parameters.Add("@Block", SqlDbType.Int).Value = productDTO.Block;

            command.ExecuteNonQuery();
        }
        public void updateImage(int id, string key)
        {
            // update SQL
            string sql = $"""
                update product 
                set ImagePath = 'Assets/Images/books/{key}.png'
                where ProID = {id}
                """;

            var command = new SqlCommand(sql, db.connection);

            command.ExecuteNonQuery();
        }

        public async Task<int> countTotalProduct()
        {
            int total = 0;
            await Task.Run(() =>
            {
                string sql = $"""
                select sum(Quantity) as sum from product
                """;

                var command = new SqlCommand(sql, db.connection);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    total = (int)reader["sum"];
                }

                System.Threading.Thread.Sleep(200);

                reader.Close();

                return total;
            });

            return total;
        }

        public ProductDTO getProductById(int id) 
        {
            List<ProductDTO> list = new List<ProductDTO>();
            ProductDTO result = new ProductDTO();

            string sql = "select ProID, ProName, Des," +
                         "Price, ImagePath," +
                         "CatID, Quantity, PromoID, PromotionPrice, Block from product where ProID = @id";

            var command = new SqlCommand(sql, db.connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                ProductDTO product = new ProductDTO();
                product.ProId = (int)reader["ProID"];
                product.ProName = reader["ProName"] == DBNull.Value ? "Product name error" : (string?)reader["ProName"];
                product.Des = reader["Des"] == DBNull.Value ? null : (string?)reader["Des"];
                product.Price = (decimal)reader["Price"];
                product.ImagePath = reader["ImagePath"] == DBNull.Value ? null : (string?)reader["ImagePath"];
                product.CatID = (int)reader["CatID"];
                product.PromoID = reader["PromoID"] == DBNull.Value ? null : (int?)reader["PromoID"];
                product.PromotionPrice = reader["PromotionPrice"] == DBNull.Value ? (decimal)reader["Price"] : (decimal?)reader["PromotionPrice"];
                product.Quantity = (int)reader["Quantity"];

                list.Add(product);
            }

            reader.Close();
            result = list[0];

            return result;
        }

        public async Task<ObservableCollection<ProductDTO>> getTop5Product()
        {
            ObservableCollection<ProductDTO> list = new ObservableCollection<ProductDTO>();
            await Task.Run(() =>
            {
                string sql = "select top 5 ProID, ProName, Des," +
                             "Price, ImagePath," +
                             "CatID, Quantity,  PromoID,PromotionPrice, Block from product where Quantity <= 100 and Quantity > 0 order by Quantity asc";
                var command = new SqlCommand(sql, db.connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProductDTO product = new ProductDTO();
                    product.ProId = (int)reader["ProID"];
                    product.ProName = reader["ProName"] == DBNull.Value ? "Product name error" : (string?)reader["ProName"];
                    product.Des = reader["Des"] == DBNull.Value ? null : (string?)reader["Des"];
                    product.Price = (decimal)reader["Price"];
                    product.ImagePath = reader["ImagePath"] == DBNull.Value ? null : (string?)reader["ImagePath"];
                    product.CatID = (int)reader["CatID"];
                    product.Quantity = (int)reader["Quantity"];
                    product.PromoID = reader["PromoID"] == DBNull.Value ? null : (int?)reader["PromoID"];
                    product.PromotionPrice = reader["PromotionPrice"] == DBNull.Value ? (decimal)reader["Price"] : (decimal?)reader["PromotionPrice"];
                    product.Block = (int)reader["Block"];

                    list.Add(product);
                }
                System.Threading.Thread.Sleep(200);

                reader.Close();
            });



            return list;
        }
    }
}
