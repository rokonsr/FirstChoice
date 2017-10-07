using System;
using FirstChoiceApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace FirstChoiceApp.Gateway
{
    public class SaleGateway
    {
        DbConnection objConnection = new DbConnection();

        internal int CreateSale(Sale objSale)
        {
            int countAffectedRow = 0;

            SqlCommand command = new SqlCommand("uspCreateSale", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("CustomerId", objSale.CustomerId);
            command.Parameters.AddWithValue("InvoiceNo", objSale.InvoiceNo);
            command.Parameters.AddWithValue("SaleDate", objSale.SaleDate);
            command.Parameters.AddWithValue("TotalAmount", objSale.TotalAmount);
            command.Parameters.AddWithValue("PaidAmount", objSale.PaidAmount);
            command.Parameters.AddWithValue("DiscountAmount", objSale.DiscountAmount);
            command.Parameters.AddWithValue("SaleType", objSale.SaleType);

            countAffectedRow = command.ExecuteNonQuery();

            return countAffectedRow;
        }
    }
}