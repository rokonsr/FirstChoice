using System;
using System.Collections.Generic;
using FirstChoiceApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace FirstChoiceApp.Gateway
{
    public class CategoryGateway
    {
        DbConnection objConnection = new DbConnection();

        internal List<Category> GetAllCategory()
        {
            List<Category> objCategoryList = new List<Category>();

            string query = "SELECT * FROM Category";

            SqlCommand command = new SqlCommand(query, objConnection.Connection());
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
            return objCategoryList;
        }

        internal int UpdateCategory(Category objCategory)
        {
            int countAffectedRow = 0;

            SqlCommand command = new SqlCommand("uspUpdateCategory", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Id", objCategory.Id);
            command.Parameters.AddWithValue("CategoryName", objCategory.CategoryName);

            countAffectedRow = command.ExecuteNonQuery();

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

            SqlCommand command = new SqlCommand("uspCreateCategory", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("CategoryName", objCategory.CategoryName);

            countAffectedRow = command.ExecuteNonQuery();

            return countAffectedRow;
        }
    }
}