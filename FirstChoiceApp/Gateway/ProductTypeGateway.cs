using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FirstChoiceApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace FirstChoiceApp.Gateway
{
    public class ProductTypeGateway
    {
        DbConnection objConnection = new DbConnection();

        internal List<ProductType> GetAllProductType()
        {
            List<ProductType> objProductTypeList = new List<ProductType>();

            SqlCommand command = new SqlCommand("uspGetAllProductType", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ProductType objProductType = new ProductType()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        TypeName = reader["TypeName"].ToString(),
                        ItemId = Convert.ToInt32(reader["ItemId"]),
                        ItemName = reader["ItemName"].ToString()
                    };
                    objProductTypeList.Add(objProductType);
                }
            }
            return objProductTypeList;
        }

        internal int UpdateProductType(ProductType objProductType)
        {
            int countAffectedRow = 0;

            SqlCommand command = new SqlCommand("uspUpdateProductType", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Id", objProductType.Id);
            command.Parameters.AddWithValue("TypeName", objProductType.TypeName);
            command.Parameters.AddWithValue("ItemId", objProductType.ItemId);

            countAffectedRow = command.ExecuteNonQuery();

            return countAffectedRow;
        }

        internal bool IsExist(ProductType objProductType)
        {
            bool isExist = GetAllProductType().Exists(x => x.TypeName == objProductType.TypeName && x.ItemId == objProductType.ItemId);

            return isExist;
        }

        internal int CreateProductType(ProductType objProductType)
        {
            int countAffectedRow = 0;

            SqlCommand command = new SqlCommand("uspCreateProductType", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("TypeName", objProductType.TypeName);
            command.Parameters.AddWithValue("ItemId", objProductType.ItemId);

            countAffectedRow = command.ExecuteNonQuery();

            return countAffectedRow;
        }
    }
}