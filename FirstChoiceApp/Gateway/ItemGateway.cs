using System;
using System.Collections.Generic;
using FirstChoiceApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace FirstChoiceApp.Gateway
{
    public class ItemGateway
    {
        private DbConnection strCon = new DbConnection();

        internal List<Item> GetAllItem()
        {
            List<Item> objItemList = new List<Item>();

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspGetAllItem", conn);
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

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspCreateItem", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ItemName", objItem.ItemName);
                command.Parameters.AddWithValue("CategoryId", objItem.CategoryId);

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

        public int UpdateItemName(Item objItem)
        {
            int countAffectedRow = 0;

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspUpdateItem", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Id", objItem.Id);
                command.Parameters.AddWithValue("ItemName", objItem.ItemName);
                command.Parameters.AddWithValue("CategoryId", objItem.CategoryId);

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