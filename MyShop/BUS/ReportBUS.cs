﻿using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using MyShop.DAO;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.BUS
{
    public class ReportBUS
    {
        private List<ShopOrderDTO> _orders;
        private OrderBUS _orderBUS;
        private OrderDAO _orderDAO;
        public ReportBUS()
        {
            var orderDAO = new OrderDAO();
            var orderBUS = new OrderBUS();
            _orderBUS = orderBUS;
            _orderDAO = orderDAO;
        }

        private async Task<List<ShopOrderDTO>> getData()
        {
            var ob = await _orderDAO.getAll();

            return ob.ToList();
        }

        public async Task<List<Decimal>> groupPriceTotalByMonth(int year)
        {
            _orders = await getData();
            List<Decimal> result = new List<Decimal>();

            for (int month = 1; month <= 12; month++)
            {
                List<decimal> prices = new List<decimal>();
                foreach (var order in _orders)
                {
                    if (order.CreateAt.Month == month && order.CreateAt.Year == year)
                    {
                        prices.Add((decimal)order.FinalTotal);
                    }
                }
                var totalPrice = prices.Sum();

                result.Add(totalPrice);
            }

            return result;
        }

        public async Task<List<int>> groupQuantityOfProductByMonth(ProductDTO product, int year)
        {
            _orders = await getData();
            List<int> result = new List<int>();

            for (int month = 1; month <= 12; month++)
            {
                int quantity = 0;
                foreach (var order in _orders)
                {
                    if (order.CreateAt.Month == month
                        && order.CreateAt.Year == year)
                    {
                        var purchases = _orderBUS.findPurchaseDTOs(order.OrderID);
                        foreach (var purchase in purchases)
                        {
                            if (purchase.ProID == product.ProId)
                            {
                                quantity += purchase.Quantity;
                            }
                        }
                    }
                }

                result.Add(quantity);
            }

            return result;
        }

        public async Task<List<Decimal>> groupProfitTotalByMonth(int year)
        {
            _orders = await getData();
            List<Decimal> result = new List<Decimal>();

            for (int month = 1; month <= 12; month++)
            {
                List<decimal> prices = new List<decimal>();
                foreach (var order in _orders)
                {
                    if (order.CreateAt.Month == month && order.CreateAt.Year == year)
                    {
                        prices.Add((decimal)order.ProfitTotal);
                    }
                }
                var totalPrice = prices.Sum();

                result.Add(totalPrice);
            }

            return result;
        }

        public async Task<List<decimal>> groupProfitTotalByWeek(int month, int year)
        {
            _orders = await getData();
            List<decimal> result = new List<decimal>();
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime lastDayOfMonth = new DateTime(year, month, daysInMonth);
            int weekCount = (int)Math.Ceiling((double)daysInMonth / 7);

            for (int week = 1; week <= 5; week++)
            {
                decimal totalPrice = 0;
                DateTime startDate = firstDayOfMonth.AddDays((week - 1) * 7);
                DateTime endDate = startDate.AddDays(6);

                foreach (var order in _orders)
                {
                    if (order.CreateAt >= startDate && order.CreateAt <= endDate)
                    {
                        totalPrice += (decimal)order.ProfitTotal;
                    }
                }

                result.Add(totalPrice);

                if (weekCount < 5 && week == weekCount)
                {
                    for (int i = week + 1; i <= 5; i++)
                    {
                        result.Add(0);
                    }
                }
            }

            return result;
        }

        public async Task<List<int>> groupQuantityOfProductByWeek(ProductDTO product, int year, int month)
        {
            _orders = await getData();
            List<int> result = new List<int>();
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime lastDayOfMonth = new DateTime(year, month, daysInMonth);
            int weekCount = (int)Math.Ceiling((double)daysInMonth / 7);

            for (int week = 1; week <= 5; week++)
            {
                int quantity = 0;
                DateTime startDate = firstDayOfMonth.AddDays((week - 1) * 7);
                DateTime endDate = startDate.AddDays(6);

                foreach (var order in _orders)
                {
                    if (order.CreateAt >= startDate && order.CreateAt <= endDate)
                    {
                        var purchases = _orderBUS.findPurchaseDTOs(order.OrderID);
                        foreach (var purchase in purchases)
                        {
                            if (purchase.ProID == product.ProId)
                            {
                                quantity += purchase.Quantity;
                            }
                        }
                    }
                }

                result.Add(quantity);

                if (weekCount < 5 && week == weekCount)
                {
                    for (int i = week + 1; i <= 5; i++)
                    {
                        result.Add(0);
                    }
                }
            }

            return result;
        }

        public async Task<List<decimal>> groupPriceTotalByWeek(int month, int year)
        {
            _orders = await getData();
            List<decimal> result = new List<decimal>();
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime lastDayOfMonth = new DateTime(year, month, daysInMonth);
            int weekCount = (int)Math.Ceiling((double)daysInMonth / 7);

            for (int week = 1; week <= 5; week++) // loop over 5 weeks only
            {
                decimal totalPrice = 0;
                DateTime startDate = firstDayOfMonth.AddDays((week - 1) * 7);
                DateTime endDate = startDate.AddDays(6);

                foreach (var order in _orders)
                {
                    if (order.CreateAt >= startDate && order.CreateAt <= endDate)
                    {
                        totalPrice += (decimal)order.FinalTotal;
                    }
                }

                result.Add(totalPrice);

                if (weekCount < 5 && week == weekCount)
                {
                    for (int i = week + 1; i <= 5; i++)
                    {
                        result.Add(0);
                    }
                }
            }

            return result;
        }

        public async Task<List<Decimal>> groupPriceTotalByYear()
        {
            _orders = await getData();
            List<Decimal> result = new List<Decimal>();

            for (int year = 2021; year <= 2023; year++)
            {
                List<decimal> prices = new List<decimal>();
                foreach (var order in _orders)
                {
                    if (order.CreateAt.Year == year)
                    {
                        // này nguy hiểm :))) nhưng kệ 
                        prices.Add((decimal)order.FinalTotal);
                    }
                }
                var totalPrice = prices.Sum();

                result.Add(totalPrice);
            }

            return result;
        }

        public async Task<List<Decimal>> groupProfitTotalByYear()
        {
            _orders = await getData();
            List<Decimal> result = new List<Decimal>();

            for (int year = 2021; year <= 2023; year++)
            {
                List<decimal> prices = new List<decimal>();
                foreach (var order in _orders)
                {
                    if (order.CreateAt.Year == year)
                    {
                        prices.Add((decimal)order.ProfitTotal);
                    }
                }
                var totalPrice = prices.Sum();

                result.Add(totalPrice);
            }

            return result;
        }


        public async Task<List<int>> groupQuantityOfProductByYear(ProductDTO product)
        {
            _orders = await getData();
            List<int> result = new List<int>();

            for (int year = 2021; year <= 2023; year++)
            {
                int quantity = 0;
                foreach (var order in _orders)
                {
                    if (order.CreateAt.Year == year)
                    {
                        var purchases = _orderBUS.findPurchaseDTOs(order.OrderID);
                        foreach (var purchase in purchases)
                        {
                            if (purchase.ProID == product.ProId)
                            {
                                quantity += purchase.Quantity;
                            }
                        }
                    }
                }

                result.Add(quantity);
            }

            return result;
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public async Task<List<Decimal>> groupPriceTotalByDate(DateTime startDate, DateTime endDate)
        {
            _orders = await getData();
            List<Decimal> result = new List<Decimal>();

            foreach (DateTime day in EachDay(startDate, endDate))
            {
                List<decimal> prices = new List<decimal>();
                foreach (var order in _orders)
                {
                    if (order.CreateAt.Date == day)
                    {
                        prices.Add((decimal)order.FinalTotal);
                    }
                }
                var totalPrice = prices.Sum();

                result.Add(totalPrice);
            }

            return result;
        }

        public async Task<List<Decimal>> groupProfitTotalByDate(DateTime startDate, DateTime endDate)
        {
            _orders = await getData();
            List<Decimal> result = new List<Decimal>();

            foreach (DateTime day in EachDay(startDate, endDate))
            {
                List<decimal> prices = new List<decimal>();
                foreach (var order in _orders)
                {
                    if (order.CreateAt.Date == day)
                    {
                        prices.Add((decimal)order.ProfitTotal);
                    }
                }
                var totalPrice = prices.Sum();

                result.Add(totalPrice);
            }

            return result;
        }

        public async Task<List<int>> groupQuantityOfProductByDate(ProductDTO product, DateTime startDate, DateTime endDate)
        {
            _orders = await getData();
            List<int> result = new List<int>();

            foreach (DateTime day in EachDay(startDate, endDate))
            {
                int quantity = 0;
                foreach (var order in _orders)
                {
                    if (order.CreateAt.Date == day)
                    {
                        var purchases = _orderBUS.findPurchaseDTOs(order.OrderID);
                        foreach (var purchase in purchases)
                        {
                            if (purchase.ProID == product.ProId)
                            {
                                quantity += purchase.Quantity;
                            }
                        }
                    }
                }
                result.Add(quantity);
            }

            return result;
        }

        public List<String> EachDayConverter(DateTime startDate, DateTime endDate)
        {
            List<string> result = new List<string>();

            foreach (DateTime day in EachDay(startDate, endDate))
            {
                result.Add(day.Date.ToString());
            }

            return result;
        }
    }
}
