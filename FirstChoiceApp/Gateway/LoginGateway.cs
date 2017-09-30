using FirstChoiceApp.Models;
using System;
using System.Data.SqlClient;

namespace FirstChoiceApp.Gateway
{
    public class LoginGateway
    {
        DbConnection connection = new DbConnection();

        internal Login GetAuthentication(Login objLogin)
        {
            Login login = new Login();

            string query = "SELECT * FROM Login WHERE Username = '" + objLogin.Username + "' AND Password = '" + objLogin.Password + "'";

            SqlCommand command = new SqlCommand(query, connection.Connection());
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    login = new Login()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString()
                    };
                }
            }
            return login;
        }
    }
}