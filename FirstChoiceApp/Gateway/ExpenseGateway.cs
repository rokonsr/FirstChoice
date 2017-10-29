using System;
using FirstChoiceApp.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace FirstChoiceApp.Gateway
{
    public class ExpenseGateway
    {
        private DbConnection strCon = new DbConnection();

        internal List<ExpenseType> GetAllExpenseType()
        {
            List<ExpenseType> objExpenseType = new List<ExpenseType>();

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                string strSql = "SELECT * FROM ExpenseType";
                SqlCommand command = new SqlCommand(strSql, conn);
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ExpenseType expenseType = new ExpenseType()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            ExpenseTitle = reader["ExpenseTitle"].ToString(),
                            Remarks = reader["Remarks"].ToString()
                        };
                        objExpenseType.Add(expenseType);
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
            return objExpenseType;
        }

        internal bool IsExist(ExpenseType expenseType)
        {
            bool isExist = GetAllExpenseType().Exists(x => x.ExpenseTitle.Equals(expenseType.ExpenseTitle));
            return isExist;
            //bool isExist = GetAllItem().Exists(x => x.ItemName == objItem.ItemName);
            //return isExist;
        }

        internal int CreateExpenseType(ExpenseType expenseType)
        {
            int countAffectedRow = 0;

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspCreateExpenseType", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ExpenseTitle", expenseType.ExpenseTitle);
                command.Parameters.AddWithValue("Remarks", expenseType.Remarks);

                countAffectedRow = command.ExecuteNonQuery();
            }
            catch (Exception ex)
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