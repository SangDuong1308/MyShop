using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Data.SqlClient;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DAO
{
    public class CustomerDAO
    {
        DatabaseUtilities db = DatabaseUtilities.getInstance();

        public ObservableCollection<CustomerDTO> getAll()
        {
            ObservableCollection<CustomerDTO> list = new ObservableCollection<CustomerDTO>();

            string sql = "select CusID, CusName, CusTel, CusAddress from customer";

            var command = new SqlCommand(sql, db.connection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                CustomerDTO customer = new CustomerDTO();
                customer.CusID = (int)reader["CusID"];
                customer.CusName = (string)reader["CusName"];
                customer.CusTel = (string)reader["CusTel"];
                customer.CusAddress = (string)reader["CusAddress"];

                list.Add(customer);
            }

            reader.Close();

            return list;
        }

        public int insertCustomer(CustomerDTO customer)
        {
            string sql = "insert into customer(CusName, CusTel, CusAddress)" +
                         "values(@CusName, @CusTel, @CusAddress)";
            var command = new SqlCommand(sql, db.connection);

            command.Parameters.Add("@CusName", SqlDbType.NVarChar).Value = customer.CusName;
            command.Parameters.Add("@CusTel", SqlDbType.NVarChar).Value = customer.CusTel;
            command.Parameters.Add("@CusAddress", SqlDbType.NVarChar).Value = customer.CusAddress;

            command.ExecuteNonQuery();

            // select SQL
            int id = -1;
            string sql1 = "SELECT TOP 1 CusID FROM customer ORDER BY CusID DESC ";

            var command1 = new SqlCommand(sql1, db.connection);

            var reader = command1.ExecuteReader();
            while (reader.Read())
            {
                id = (int)reader["CusID"];
            }

            reader.Close();

            return id;
        }

        public string getNameById(int cusID)
        {
            List<CustomerDTO> list = new List<CustomerDTO>();
            CustomerDTO result = new();

            string sql = $"select CusID, CusName from customer where CusID = @id";

            var command = new SqlCommand(sql, db.connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = cusID;

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                CustomerDTO customer = new CustomerDTO();

                customer.CusID = (int)reader["CusID"];
                customer.CusName = (string)reader["CusName"];

                list.Add(customer);
            }

            reader.Close();
            result = list[0];

            return result.CusName;
        }

        public CustomerDTO getCustomerById(int cusID)
        {
            List<CustomerDTO> list = new List<CustomerDTO>();
            CustomerDTO result = new();

            string sql = $"select CusID, CusName from customer where CusID = @id";

            var command = new SqlCommand(sql, db.connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = cusID;

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                CustomerDTO customer = new CustomerDTO();

                customer.CusID = (int)reader["CusID"];
                customer.CusName = (string)reader["CusName"];

                list.Add(customer);
            }

            reader.Close();
            result = list[0];

            return result;
        }
        public Tuple<Boolean, string> delCustomerById(int CusID)
        {
            string message = "";
            bool isSuccess = true;
            Tuple<Boolean, string> result = new Tuple<bool, string>(isSuccess, message);

            string sql = $"""
                delete customer 
                where CusID = {CusID}
                """;

            var command = new SqlCommand(sql, db.connection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                message = ex.Message;
                isSuccess = false;
                return new Tuple<bool, string>(isSuccess, message);
            }

            return result;
        }
    }
}
