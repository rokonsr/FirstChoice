using FirstChoiceApp.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace FirstChoiceApp.Gateway
{
    public class LoginGateway
    {
        private DbConnection strCon = new DbConnection();
        
        internal Login GetAuthentication(Login objLogin)
        {
            Login login = new Login();

            string query = "SELECT * FROM Login WHERE Username = '" + objLogin.Username + "' AND Password = '" + objLogin.Password + "'";

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                
                SqlCommand command = new SqlCommand(query, conn);
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
            }
            catch (Exception)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            return login;
        }
    }
}