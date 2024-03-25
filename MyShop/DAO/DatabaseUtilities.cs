using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace MyShop.DAO
{
    class DatabaseUtilities
    {
        private string _server;
        private string _databaseName;
        private string _user;
        private string _password;
        public static bool isSelectedDatabase = false;

        private static DatabaseUtilities _instance = null;
        SqlConnection _connection;
        public static DatabaseUtilities getInstance()
        {
            if (_instance == null)
            {
                _instance = new DatabaseUtilities();
            }
            return _instance;
        }

        public DatabaseUtilities()
        {
            _server = "sqlexpress";
            _databaseName = "MyShop";
            _user = "sa";
            _password = "123";
            _connection = null;
        }

        public DatabaseUtilities(string server, string databaseName, string user, string password)
        {
            _server = server;
            _databaseName = databaseName;
            _user = user;
            _password = password;

            string connectionString = $"""
                Server = {server};
                User ID = {user}; Password={password};
                Database = {databaseName};
                TrustServerCertificate=True;
                Trusted_Connection=yes;
                """;

            _connection = new SqlConnection(connectionString);

            try
            {
                _connection.Open();
                isSelectedDatabase = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Cannot connect to database. Reason: {ex.Message}");
            }

            _instance = this;
        }

        public SqlConnection connection { get { return _connection; } }
    }
}
