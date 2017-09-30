using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FirstChoiceApp.Models;
using System.Data;

namespace FirstChoiceApp.Gateway
{
    public class SupplierGateway
    {
        private DbConnection objDbConnection = new DbConnection();

        internal List<SupplierInfo> GetAllSupplier()
        {
            List<SupplierInfo> objSupplierInfos = new List<SupplierInfo>();

            string query = "SELECT * FROM Supplier";

            SqlCommand objCommand = new SqlCommand(query, objDbConnection.Connection());
            SqlDataReader objReader = objCommand.ExecuteReader();

            if (objReader.HasRows)
            {
                while (objReader.Read())
                {
                    SupplierInfo objSupplierInfo = new SupplierInfo()
                    {
                        Id = Convert.ToInt32(objReader["Id"]),
                        SupplierName = objReader["SupplierName"].ToString(),
                        ContactNo = objReader["ContactNo"].ToString(),
                        Email = objReader["Email"].ToString(),
                        Address = objReader["Address"].ToString()
                    };
                    objSupplierInfos.Add(objSupplierInfo);
                }
            }
            return objSupplierInfos;
        }

        internal int UpdateSupplierInfo(SupplierInfo objSupplierInfo)
        {
            int affectedRowCount = 0;

            SqlCommand objCommand = new SqlCommand("uspUpdateSupplier", objDbConnection.Connection());
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Parameters.AddWithValue("Id", objSupplierInfo.Id);
            objCommand.Parameters.AddWithValue("SupplierName", objSupplierInfo.SupplierName);
            objCommand.Parameters.AddWithValue("ContactNo", objSupplierInfo.ContactNo);
            objCommand.Parameters.AddWithValue("Email", objSupplierInfo.Email);
            objCommand.Parameters.AddWithValue("Address", objSupplierInfo.Address);

            affectedRowCount = objCommand.ExecuteNonQuery();

            return affectedRowCount;
        }

        internal SupplierInfo GetSupplierInfoForEdit(int? id)
        {
            SupplierInfo objSupplier = new SupplierInfo();

            string query = "SELECT * FROM SUPPLIER WHERE Id = '" + id + "'";

            SqlCommand objCommand = new SqlCommand(query, objDbConnection.Connection());
            SqlDataReader objReader = objCommand.ExecuteReader();

            if (objReader.HasRows)
            {
                while (objReader.Read())
                {
                    objSupplier = new SupplierInfo()
                    {
                        Id = Convert.ToInt32(objReader["Id"]),
                        SupplierName = objReader["SupplierName"].ToString(),
                        ContactNo = objReader["ContactNo"].ToString(),
                        Email = objReader["Email"].ToString(),
                        Address = objReader["Address"].ToString()
                    };
                }
            }
            return objSupplier;
        }

        internal int CreateSupplier(SupplierInfo objSupplierInfo)
        {
            int affectedRowCount = 0;

            SqlCommand objCommand = new SqlCommand("uspCreateSupplier", objDbConnection.Connection());
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Parameters.AddWithValue("SupplierName", objSupplierInfo.SupplierName);
            objCommand.Parameters.AddWithValue("ContactNo", objSupplierInfo.ContactNo);
            objCommand.Parameters.AddWithValue("Email", objSupplierInfo.Email);
            objCommand.Parameters.AddWithValue("Address", objSupplierInfo.Address);

            affectedRowCount = objCommand.ExecuteNonQuery();

            return affectedRowCount;
        }

        internal bool IsExist(SupplierInfo objSupplierInfo)
        {
            bool IsExist = GetAllSupplier().Exists(x => x.SupplierName == objSupplierInfo.SupplierName);

            return IsExist;
        }
    }
}