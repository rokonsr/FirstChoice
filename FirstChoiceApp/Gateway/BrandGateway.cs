using System;
using System.Collections.Generic;
using FirstChoiceApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace FirstChoiceApp.Gateway
{
    public class BrandGateway
    {
        private DbConnection strCon = new DbConnection();

        internal List<Brand> GetAllBrand()
        {
            List<Brand> objBrandList = new List<Brand>();

            string query = "SELECT * FROM Brand";

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
                        Brand objBrand = new Brand()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            BrandName = reader["BrandName"].ToString()
                        };
                        objBrandList.Add(objBrand);
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
            return objBrandList;
        }

        internal int UpdateBrand(Brand objBrand)
        {
            int countAffectedRow = 0;

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspUpdateBrand", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Id", objBrand.Id);
                command.Parameters.AddWithValue("BrandName", objBrand.BrandName);

                countAffectedRow = command.ExecuteNonQuery();
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
            return countAffectedRow;
        }

        internal bool IsExist(Brand objBrand)
        {
            bool isExist = GetAllBrand().Exists(x => x.BrandName == objBrand.BrandName);

            return isExist;
        }

        internal int CreateBrand(Brand objBrand)
        {
            int countAffectedRow = 0;

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspCreateBrand", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("BrandName", objBrand.BrandName);

                countAffectedRow = command.ExecuteNonQuery();
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
            return countAffectedRow;
        }
    }
}