using System;
using System.Collections.Generic;
using FirstChoiceApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace FirstChoiceApp.Gateway
{
    public class CategoryGateway
    {
        private DbConnection strCon = new DbConnection();

        internal List<Category> GetAllCategory()
        {
            List<Category> objCategoryList = new List<Category>();

            string query = "SELECT * FROM Category";

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
                        Category objCategory = new Category()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            CategoryName = reader["CategoryName"].ToString(),
                        };
                        objCategoryList.Add(objCategory);
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
            return objCategoryList;
        }

        internal int UpdateCategory(Category objCategory)
        {
            int countAffectedRow = 0;

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspUpdateCategory", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Id", objCategory.Id);
                command.Parameters.AddWithValue("CategoryName", objCategory.CategoryName);

                countAffectedRow = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            return countAffectedRow;
        }

        internal bool IsExist(Category objCategory)
        {
            bool isExist = GetAllCategory().Exists(x => x.CategoryName == objCategory.CategoryName);

            return isExist;
        }

        internal int CreateCategory(Category objCategory)
        {
            int countAffectedRow = 0;

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspCreateCategory", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("CategoryName", objCategory.CategoryName);

                countAffectedRow = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            return countAffectedRow;
        }
    }
}