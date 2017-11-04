using System;
using System.Collections.Generic;
using FirstChoiceApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace FirstChoiceApp.Gateway
{
    public class OtherIncomeGateway
    {
        private DbConnection strCon = new DbConnection();

        internal List<IncomeType> GetIncomeTypeList()
        {
            List<IncomeType> objIncomeType = new List<IncomeType>();

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                string strSql = "SELECT * FROM IncomeType";
                SqlCommand command = new SqlCommand(strSql, conn);
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        IncomeType incomeType = new IncomeType()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            IncomeTypeTitle = reader["IncomeTypeTitle"].ToString(),
                            Remarks = reader["Remarks"].ToString()
                        };
                        objIncomeType.Add(incomeType);
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
            return objIncomeType;
        }

        internal int UpdateOtherIncome(OtherIncomeDetail otherIncomeDetail)
        {
            int countAffectedRow = 0;

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspUpdateOtherIncomeDetail", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Id", otherIncomeDetail.Id);
                command.Parameters.AddWithValue("IncomeTypeId", otherIncomeDetail.IncomeTypeId);
                command.Parameters.AddWithValue("IncomeAmount", otherIncomeDetail.IncomeAmount);
                command.Parameters.AddWithValue("Remarks", otherIncomeDetail.Remarks);

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

        internal bool IsExist(OtherIncomeDetail otherIncomeDetail)
        {
            bool isExist = false;

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                string strSql = "select * from SaleLedger where Convert(nvarchar, CreatedDate, 103) = Convert(nvarchar, '" + otherIncomeDetail.CreatedDate.ToShortDateString() + "', 103)";
                SqlCommand command = new SqlCommand(strSql, conn);
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();

                isExist = reader.HasRows;
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
            return isExist;
        }

        internal List<OtherIncomeDetail> GetAllOtherIncome()
        {
            List<OtherIncomeDetail> objOtherIncomeDetail = new List<OtherIncomeDetail>();

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                string strSql = "SELECT * FROM OtherIncomeDetail ID INNER JOIN IncomeType IT ON ID.IncomeTypeId = IT.Id";
                SqlCommand command = new SqlCommand(strSql, conn);
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OtherIncomeDetail otherIncomeDetail = new OtherIncomeDetail()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            IncomeTypeId = Convert.ToInt32(reader["IncomeTypeId"]),
                            IncomeTypeTitle = reader["IncomeTypeTitle"].ToString(),
                            IncomeAmount = Convert.ToDecimal(reader["IncomeAmount"]),
                            Remarks = reader["Remarks"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                        };
                        objOtherIncomeDetail.Add(otherIncomeDetail);
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
            return objOtherIncomeDetail;
        }

        internal int CreateOtherIncome(OtherIncomeDetail otherIncomeDetail)
        {
            int countAffectedRow = 0;

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspCreateOtherIncomeDetail", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("IncomeTypeId", otherIncomeDetail.IncomeTypeId);
                command.Parameters.AddWithValue("IncomeAmount", otherIncomeDetail.IncomeAmount);
                command.Parameters.AddWithValue("Remarks", otherIncomeDetail.Remarks);
                command.Parameters.AddWithValue("CreatedDate", otherIncomeDetail.CreatedDate);

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