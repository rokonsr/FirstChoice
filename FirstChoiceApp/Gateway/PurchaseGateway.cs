using System;
using FirstChoiceApp.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace FirstChoiceApp.Gateway
{
    public class PurchaseGateway
    {
        DbConnection objConnection = new DbConnection();

        internal int CreatePurchase(Purchase objPurchase)
        {
            int countAffectedRow = 0;

            SqlCommand command = new SqlCommand("uspCreatePurchase", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("SupplierId", objPurchase.SupplierId);
            command.Parameters.AddWithValue("InvoiceNo", objPurchase.InvoiceNo);
            command.Parameters.AddWithValue("PurchaseDate", objPurchase.PurchaseDate);
            command.Parameters.AddWithValue("TotalAmount", objPurchase.TotalAmount);
            command.Parameters.AddWithValue("PaidAmount", objPurchase.PaidAmount);
            command.Parameters.AddWithValue("DiscountAmount", objPurchase.DiscountAmount);
            command.Parameters.AddWithValue("PurchaseType", objPurchase.PurchaseType);

            countAffectedRow = command.ExecuteNonQuery();

            return countAffectedRow;
        }

        internal List<Product> GetProductByInvoiceNo(string invoiceNo)
        {
            List<Product> objProductList = new List<Product>();

            SqlCommand command = new SqlCommand("uspGetProductByInvoiceNo", objConnection.Connection());
            command.Parameters.AddWithValue("InvoiceNo", invoiceNo);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Product objProduct = new Product()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ProductName = reader["ProductName"].ToString()
                    };
                    objProductList.Add(objProduct);
                }
            }
            return objProductList;
        }

        internal List<PurchaseLedger> GetPurchaseLedger()
        {
            List<PurchaseLedger> purchaseLedgerList = new List<PurchaseLedger>();

            SqlCommand command = new SqlCommand("uspPurchaseLedgerDetail", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    PurchaseLedger purchaseLedger = new PurchaseLedger()
                    {
                        SupplierId = reader["SupplierId"] != DBNull.Value ? Convert.ToInt32(reader["SupplierId"]) : 0,
                        SupplierName = reader["SupplierName"].ToString(),
                        PurchaseAmount = reader["PurchaseAmount"] != DBNull.Value ? Convert.ToDecimal(reader["PurchaseAmount"]) : 0,
                        PaidAmount = reader["PaidAmount"] != DBNull.Value ? Convert.ToDecimal(reader["PaidAmount"]) : 0,
                        Balance = reader["Balance"] != DBNull.Value ? Convert.ToDecimal(reader["Balance"]) : 0,
                        Status = reader["Status"].ToString()
                    };
                    purchaseLedgerList.Add(purchaseLedger);
                }
            }
            return purchaseLedgerList;
        }

        internal List<Purchase> GetAllPurchase()
        {
            List<Purchase> objPurchaseList = new List<Purchase>();

            string query = "SELECT * FROM Purchase";

            SqlCommand command = new SqlCommand(query, objConnection.Connection());
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Purchase objPurchase = new Purchase()
                    {
                        SupplierId = Convert.ToInt32(reader["SupplierId"]),
                        InvoiceNo = reader["InvoiceNo"].ToString(),
                        PurchaseType = Convert.ToInt32(reader["PurchaseType"])
                    };
                    objPurchaseList.Add(objPurchase);
                }
            }
            return objPurchaseList;
        }
        
        internal void CreatePurchaseDetail(PurchaseDetail objPurchaseDetail)
        {
            SqlCommand command = new SqlCommand("uspCreatePurchaseDetail", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("PurchaseType", objPurchaseDetail.PurchaseType);
            command.Parameters.AddWithValue("PurchaseId", objPurchaseDetail.PurchaseId);
            command.Parameters.AddWithValue("ProductId", objPurchaseDetail.ProductId);
            command.Parameters.AddWithValue("PurchaseRate", objPurchaseDetail.PurchaseRate);
            command.Parameters.AddWithValue("SaleRate", objPurchaseDetail.SaleRate);
            command.Parameters.AddWithValue("Quantity", objPurchaseDetail.Quantity);

            command.ExecuteNonQuery();
        }

        internal int GetPurchaseId(Purchase objPurchase)
        {
            int purchaseId = 0;

            string query = "SELECT Id FROM Purchase WHERE SupplierId = '" + objPurchase.SupplierId + "' AND InvoiceNo = '" + objPurchase.InvoiceNo + "'";

            SqlCommand command = new SqlCommand(query, objConnection.Connection());
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    purchaseId = Convert.ToInt32(reader["Id"]);
                }
            }
            return purchaseId;
        }

        internal int Payment(PurchaseLedger purchaseLedger)
        {
            int countAffectedRow = 0;

            SqlCommand command = new SqlCommand("uspPurchasePayment", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("SupplierId", purchaseLedger.SupplierId);
            command.Parameters.AddWithValue("PaidAmount", purchaseLedger.PaidAmount);
            command.Parameters.AddWithValue("PaidDate", purchaseLedger.PaidDate);

            countAffectedRow = command.ExecuteNonQuery();

            return countAffectedRow;
        }
    }
}