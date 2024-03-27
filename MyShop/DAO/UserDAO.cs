using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using MyShop.DTO;
using System.Diagnostics;
using System.Windows.Documents;

namespace MyShop.DAO
{
    internal class UserDAO
    {
        DatabaseUtilities db = DatabaseUtilities.getInstance();
        public UserDTO GetOne(string username, string password)
        {
            var query = "SELECT * FROM [user] WHERE Username = @Username AND Password = @Password";

            var command = new SqlCommand(query, db.connection);
            //AesHelper aesHelper = new AesHelper();
            //string encryptedText = aesHelper.Encrypt(password);
            command.Parameters.AddWithValue("@Username", username);
            //command.Parameters.AddWithValue("@Password", encryptedText);
            command.Parameters.AddWithValue("@Password", password);
            var reader = command.ExecuteReader();

            UserDTO user = null;
            if (reader.Read())
            {
                user = new UserDTO
                {
                    UserID = (int)reader["UserID"],
                    Username = (string)reader["Username"],
                    Password = (string)reader["Password"],
                    Fullname = (string)reader["Fullname"]
                };
            }
            reader.Close();
            return user;
        }
        public bool CreateUser(UserDTO user)
        {
            var query = "INSERT INTO [user] (Username, Password, Fullname) VALUES (@Username, @Password, @Fullname)";

            var command = new SqlCommand(query, db.connection);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Fullname", user.Fullname);

            var rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }
    }
}
