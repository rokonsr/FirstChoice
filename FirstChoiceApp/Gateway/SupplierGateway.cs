using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FirstChoiceApp.Models;
using System.Data;

namespace FirstChoiceApp.Gateway
{
    public class SupplierGateway
    {
        private DbConnection strCon = new DbConnection();

        internal List<SupplierInfo> GetAllSupplier()
        {
            List<SupplierInfo> objSupplierInfos = new List<SupplierInfo>();

            string query = "SELECT * FROM Supplier";

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
                        SupplierInfo objSupplierInfo = new SupplierInfo()
                        {
                            Id = Convert.ToInt32(objReader["Id"]),
                            SupplierName = objReader["SupplierName"].ToString(),
                            ContactNo = objReader["ContactNo"].ToString(),
                            Email = objReader["Email"].ToString(),
                            Address = objReader["Address"].ToString()
                        };
                        objSupplierInfos.Add(objSupplierInfo);
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
            return objSupplierInfos;
        }

        internal int UpdateSupplierInfo(SupplierInfo objSupplierInfo)
        {
            int affectedRowCount = 0;

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand objCommand = new SqlCommand("uspUpdateSupplier", conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("Id", objSupplierInfo.Id);
                objCommand.Parameters.AddWithValue("SupplierName", objSupplierInfo.SupplierName);
                objCommand.Parameters.AddWithValue("ContactNo", objSupplierInfo.ContactNo);
                objCommand.Parameters.AddWithValue("Email", objSupplierInfo.Email);
                objCommand.Parameters.AddWithValue("Address", objSupplierInfo.Address);

                affectedRowCount = objCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            return affectedRowCount;
        }

        internal SupplierInfo GetSupplierInfoForEdit(int? id)
        {
            SupplierInfo objSupplier = new SupplierInfo();

            string query = "SELECT * FROM SUPPLIER WHERE Id = '" + id + "'";

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
                        objSupplier = new SupplierInfo()
                        {
                            Id = Convert.ToInt32(objReader["Id"]),
                            SupplierName = objReader["SupplierName"].ToString(),
                            ContactNo = objReader["ContactNo"].ToString(),
                            Email = objReader["Email"].ToString(),
                            Address = objReader["Address"].ToString()
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
            return objSupplier;
        }

        internal int CreateSupplier(SupplierInfo objSupplierInfo)
        {
            int affectedRowCount = 0;

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand objCommand = new SqlCommand("uspCreateSupplier", conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("SupplierName", objSupplierInfo.SupplierName);
                objCommand.Parameters.AddWithValue("ContactNo", objSupplierInfo.ContactNo);
                objCommand.Parameters.AddWithValue("Email", objSupplierInfo.Email);
                objCommand.Parameters.AddWithValue("Address", objSupplierInfo.Address);

                affectedRowCount = objCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            return affectedRowCount;
        }

        internal bool IsExist(SupplierInfo objSupplierInfo)
        {
            bool IsExist = GetAllSupplier().Exists(x => x.SupplierName == objSupplierInfo.SupplierName);

            return IsExist;
        }
    }
}