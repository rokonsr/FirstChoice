using System;
using FirstChoiceApp.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using FirstChoiceApp.Models.ViewModel;

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
                string error = exception.Message;
            }
            finally
            {
                conn.Close();
            }
            return objExpenseType;
        }

        internal int UpdateExpenseDetail(ExpenseDetail expenseDetail)
        {
            int countAffectedRow = 0;

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                string strSql = "UPDATE ExpenseDetail SET ExpenseTypeId = '" + expenseDetail.ExpenseTypeId + "', ExpenseAmount = '" + expenseDetail.ExpenseAmount + "', Remarks = '" + expenseDetail.Remarks + "' WHERE Id = '" + expenseDetail.Id + "'";
                SqlCommand command = new SqlCommand(strSql, conn);
                command.CommandType = CommandType.Text;

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

        internal List<IncomeExpenseDetail> GetIncomeSummary(string StartDate, string EndDate)
        {
            List<IncomeExpenseDetail> objIncomeExpenseDetail = new List<IncomeExpenseDetail>();

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspGetIncomeSummary", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("StartDate", StartDate);
                command.Parameters.AddWithValue("EndDate", EndDate);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        IncomeExpenseDetail incomeExpense = new IncomeExpenseDetail()
                        {
                            Earning = Convert.ToDecimal(reader["Earning"]),
                            Expense = Convert.ToDecimal(reader["Expense"]),
                            Balance = Convert.ToDecimal(reader["Balance"])
                        };
                        objIncomeExpenseDetail.Add(incomeExpense);
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
            return objIncomeExpenseDetail;
        }

        internal List<IncomeExpenseDetail> GetIncomeExpenseDetail()
        {
            List<IncomeExpenseDetail> objIncomeExpenseDetail = new List<IncomeExpenseDetail>();

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspDailyIncome", conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        IncomeExpenseDetail incomeExpense = new IncomeExpenseDetail()
                        {
                            D_Date = reader["D_Date"].ToString(),
                            Earning = Convert.ToDecimal(reader["Earning"]),
                            Expense = Convert.ToDecimal(reader["Expense"]),
                            Balance = Convert.ToDecimal(reader["Balance"])
                        };
                        objIncomeExpenseDetail.Add(incomeExpense);
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
            return objIncomeExpenseDetail;
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
                string error = exception.Message;
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
                string error = exception.Message;
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
                string error = exception.Message;
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