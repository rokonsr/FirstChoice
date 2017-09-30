using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FirstChoiceApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace FirstChoiceApp.Gateway
{
    public class SizeGateway
    {
        DbConnection objConnection = new DbConnection();

        internal List<Size> GetAllSize()
        {
            List<Size> objSizeList = new List<Size>();

            SqlCommand command = new SqlCommand("uspGetAllSize", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Size objSize = new Size()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ProductSize = reader["ProductSize"].ToString(),
                        ItemId = Convert.ToInt32(reader["ItemId"]),
                        ItemName = reader["ItemName"].ToString()
                    };
                    objSizeList.Add(objSize);
                }
            }
            return objSizeList;
        }

        internal int UpdateProductSize(Size objSize)
        {
            int countAffectedRow = 0;

            SqlCommand command = new SqlCommand("uspUpdateProductSize", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Id", objSize.Id);
            command.Parameters.AddWithValue("ProductSize", objSize.ProductSize);
            command.Parameters.AddWithValue("ItemId", objSize.ItemId);

            countAffectedRow = command.ExecuteNonQuery();

            return countAffectedRow;
        }

        internal bool IsExist(Size objSize)
        {
            bool isExist = GetAllSize().Exists(x => x.ProductSize == objSize.ProductSize && x.ItemId == objSize.ItemId);

            return isExist;
        }

        internal int CreateProductSize(Size objSize)
        {
            int countAffectedRow = 0;

            SqlCommand command = new SqlCommand("uspCreateProductSize", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("ItemId", objSize.ItemId);
            command.Parameters.AddWithValue("ProductSize", objSize.ProductSize);

            countAffectedRow = command.ExecuteNonQuery();

            return countAffectedRow;
        }
    }
}