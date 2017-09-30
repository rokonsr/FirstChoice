using System;
using System.Collections.Generic;
using FirstChoiceApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace FirstChoiceApp.Gateway
{
    public class ItemGateway
    {
        DbConnection objConnection = new DbConnection();

        internal List<Item> GetAllItem()
        {
            List<Item> objItemList = new List<Item>();

            SqlCommand command = new SqlCommand("uspGetAllItem", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Item objItem = new Item()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ItemName = reader["ItemName"].ToString(),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        CategoryName = reader["CategoryName"].ToString()
                    };
                    objItemList.Add(objItem);
                }
            }
            return objItemList;
        }

        internal bool IsExist(Item objItem)
        {
            bool isExist = GetAllItem().Exists(x => x.ItemName == objItem.ItemName);
            return isExist;
        }

        internal int CreateItem(Item objItem)
        {
            int countAffectedRow = 0;

            SqlCommand command = new SqlCommand("uspCreateItem", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("ItemName", objItem.ItemName);
            command.Parameters.AddWithValue("CategoryId", objItem.CategoryId);

            countAffectedRow = command.ExecuteNonQuery();

            return countAffectedRow;
        }

        public int UpdateItemName(Item objItem)
        {
            int countAffectedRow = 0;

            SqlCommand command = new SqlCommand("uspUpdateItem", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Id", objItem.Id);
            command.Parameters.AddWithValue("ItemName", objItem.ItemName);
            command.Parameters.AddWithValue("CategoryId", objItem.CategoryId);

            countAffectedRow = command.ExecuteNonQuery();

            return countAffectedRow;
        }
    }
}