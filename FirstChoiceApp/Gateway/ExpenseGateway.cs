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
            catch (Exception exception)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            return objExpenseType;
        }

        internal List<ExpenseDetail> GetAllExpenseDetail()
        {
            List<ExpenseDetail> objExpenseDetails = new List<ExpenseDetail>();

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspGetExpenseDetail", conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ExpenseDetail expenseDetail = new ExpenseDetail()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            ExpenseTypeId = Convert.ToInt32(reader["ExpenseTypeId"]),
                            ExpenseTitle = reader["ExpenseTitle"].ToString(),
                            ExpenseAmount = Convert.ToDecimal(reader["ExpenseAmount"]),
                            ExpenseDate = Convert.ToDateTime(reader["ExpenseDate"]),
                            Remarks = reader["Remarks"].ToString()
                        };
                        objExpenseDetails.Add(expenseDetail);
                    }
                }
            }
            catch (Exception exception)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            return objExpenseDetails;
        }

        internal int CreateExpenseDetail(ExpenseDetail expenseDetail)
        {
            int countAffectedRow = 0;

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspCreateExpenseDetail", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ExpenseTypeId", expenseDetail.ExpenseTypeId);
                command.Parameters.AddWithValue("ExpenseAmount", expenseDetail.ExpenseAmount);
                command.Parameters.AddWithValue("ExpenseDate", expenseDetail.ExpenseDate);
                command.Parameters.AddWithValue("Remarks", expenseDetail.Remarks);

                countAffectedRow = command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            return countAffectedRow;
        }

        internal int UpdateExpenseType(ExpenseType expenseType)
        {
            int countAffectedRow = 0;

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspUpdateExpenseType", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Id", expenseType.Id);
                command.Parameters.AddWithValue("ExpenseTitle", expenseType.ExpenseTitle);
                command.Parameters.AddWithValue("Remarks", expenseType.Remarks);

                countAffectedRow = command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            return countAffectedRow;
        }

        internal bool IsExist(ExpenseType expenseType)
        {
            bool isExist = GetAllExpenseType().Exists(x => x.ExpenseTitle.Equals(expenseType.ExpenseTitle));
            return isExist;
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
            catch (Exception exception)
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