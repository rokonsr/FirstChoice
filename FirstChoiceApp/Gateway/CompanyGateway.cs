using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using FirstChoiceApp.Models;

namespace FirstChoiceApp.Gateway
{
    public class CompanyGateway
    {
        private DbConnection strCon = new DbConnection();

        internal bool IsExist(CompanyInfo objCompanyInfo)
        {
            bool IsExist = false;

            string query = "SELECT * FROM Company";

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand objCommand = new SqlCommand(query, conn);
                SqlDataReader objReader = objCommand.ExecuteReader();

                IsExist = objReader.HasRows;
            }
            catch (Exception exception)
            {
                conn.Close();
                string error = exception.Message;
            }
            finally
            {
                conn.Close();
            }
            return IsExist;
        }

        internal int CreateCompany(CompanyInfo objCompanyInfo)
        {
            int affectedRowCount = 0;

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand objCommand = new SqlCommand("uspCreateCompany", conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("CompanyName", objCompanyInfo.CompanyName);
                objCommand.Parameters.AddWithValue("OwnerName", objCompanyInfo.OwnerName);
                objCommand.Parameters.AddWithValue("ContactNo", objCompanyInfo.ContactNo);
                objCommand.Parameters.AddWithValue("Email", objCompanyInfo.Email);
                objCommand.Parameters.AddWithValue("Address", objCompanyInfo.Address);
                objCommand.Parameters.AddWithValue("CompanyLogo", objCompanyInfo.CompanyLogo);

                affectedRowCount = objCommand.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                conn.Close();
                string error = exception.Message;
            }
            finally
            {
                conn.Close();
            }
            return affectedRowCount;
        }

        internal CompanyInfo CompanyDetail()
        {
            CompanyInfo objCompanyInfo = new CompanyInfo();

            string query = "SELECT * FROM Company";

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand objCommand = new SqlCommand(query, conn);
                SqlDataReader objReader = objCommand.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        objCompanyInfo = new CompanyInfo()
                        {
                            Id = Convert.ToInt32(objReader["Id"]),
                            CompanyName = objReader["CompanyName"].ToString(),
                            OwnerName = objReader["OwnerName"].ToString(),
                            ContactNo = objReader["ContactNo"].ToString(),
                            Address = objReader["Address"].ToString(),
                            Email = objReader["Email"].ToString(),
                            CompanyLogo = (byte[])objReader["CompanyLogo"]
                        };
                    }
                }
            }
            catch (Exception exception)
            {
                conn.Close();
                string error = exception.Message;
            }
            finally
            {
                conn.Close();
            }
            return objCompanyInfo;
        }

        internal int UpdateCompanyInfo(CompanyInfo objCompanyInfo)
        {
            int affectedRowCount = 0;

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand objCommand = new SqlCommand("uspUpdateCompany", conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("Id", objCompanyInfo.Id);
                objCommand.Parameters.AddWithValue("CompanyName", objCompanyInfo.CompanyName);
                objCommand.Parameters.AddWithValue("OwnerName", objCompanyInfo.OwnerName);
                objCommand.Parameters.AddWithValue("ContactNo", objCompanyInfo.ContactNo);
                objCommand.Parameters.AddWithValue("Email", objCompanyInfo.Email);
                objCommand.Parameters.AddWithValue("Address", objCompanyInfo.Address);
                objCommand.Parameters.AddWithValue("CompanyLogo", objCompanyInfo.CompanyLogo);

                affectedRowCount = objCommand.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                conn.Close();
                string error = exception.Message;
            }
            finally
            {
                conn.Close();
            }
            return affectedRowCount;
        }
    }
}