using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FirstChoiceApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace FirstChoiceApp.Gateway
{
    public class CustomerGateway
    {
        DbConnection objConnection = new DbConnection();

        internal List<Customer> GetAllCustomer()
        {
            List<Customer> objCustomerList = new List<Customer>();

            string query = "SELECT * FROM Customer";

            SqlCommand command = new SqlCommand(query, objConnection.Connection());
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Customer objCustomer = new Customer()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        CustomerName = reader["CustomerName"].ToString(),
                        ContactNo = reader["ContactNo"].ToString(),
                        Email = reader["Email"].ToString(),
                        Address = reader["Address"].ToString()
                    };
                    objCustomerList.Add(objCustomer);
                }
            }
            return objCustomerList;
        }

        internal int UpdateCustomer(Customer objCustomer)
        {
            int countAffectedRow = 0;

            SqlCommand command = new SqlCommand("uspUpdateCustomer", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Id", objCustomer.Id);
            command.Parameters.AddWithValue("CustomerName", objCustomer.CustomerName);
            command.Parameters.AddWithValue("ContactNo", objCustomer.ContactNo);
            command.Parameters.AddWithValue("Email", objCustomer.Email);
            command.Parameters.AddWithValue("Address", objCustomer.Address);

            countAffectedRow = command.ExecuteNonQuery();

            return countAffectedRow;
        }

        internal int CreateCustomer(Customer objCustomer)
        {
            int countAffectedRow = 0;

            SqlCommand command = new SqlCommand("uspCreateCustomer", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("CustomerName", objCustomer.CustomerName);
            command.Parameters.AddWithValue("ContactNo", objCustomer.ContactNo);
            command.Parameters.AddWithValue("Email", objCustomer.Email);
            command.Parameters.AddWithValue("Address", objCustomer.Address);

            countAffectedRow = command.ExecuteNonQuery();

            return countAffectedRow;
        }

        internal bool IsExist(Customer objCustomer)
        {
            bool IsExist = GetAllCustomer().Exists(x => x.CustomerName == objCustomer.CustomerName);

            return IsExist;
        }
    }
}