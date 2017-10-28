using System;
using FirstChoiceApp.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace FirstChoiceApp.Gateway
{
    public class SaleGateway
    {
        private DbConnection strCon = new DbConnection();

        internal int CreateSale(Sale objSale)
        {
            int countAffectedRow = 0;

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspCreateSale", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("CustomerId", objSale.CustomerId);
                command.Parameters.AddWithValue("InvoiceNo", objSale.InvoiceNo);
                command.Parameters.AddWithValue("SaleDate", objSale.SaleDate);
                command.Parameters.AddWithValue("TotalAmount", objSale.TotalAmount);
                command.Parameters.AddWithValue("PaidAmount", objSale.PaidAmount);
                command.Parameters.AddWithValue("DiscountAmount", objSale.DiscountAmount);
                command.Parameters.AddWithValue("SaleType", objSale.SaleType);

                countAffectedRow = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            return countAffectedRow;
        }

        internal List<Product> GetAllProductByCodeInvoice(string productCode, string invoiceNo)
        {
            List<Product> objProductList = new List<Product>();

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspGetAllProductByCodeInvoice", conn);
                command.Parameters.AddWithValue("ProductCode", productCode);
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
                            ProductName = reader["ProductName"].ToString(),
                            Stock = Convert.ToDecimal(reader["Quantity"]),
                            Price = Convert.ToDecimal(reader["Price"]),
                            InvoiceNo = reader["InvoiceNo"].ToString(),
                            ProductCode = reader["ProductCode"].ToString()
                        };
                        objProductList.Add(objProduct);
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
            return objProductList;
        }

        internal List<Product> GetAllProductByIdInvoice(int productId, string invoiceNo)
        {
            List<Product> objProductList = new List<Product>();

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspGetAllProductByInvoice", conn);
                command.Parameters.AddWithValue("ProductId", productId);
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
                            ProductName = reader["ProductName"].ToString(),
                            Stock = Convert.ToDecimal(reader["Quantity"]),
                            Price = Convert.ToDecimal(reader["Price"]),
                            InvoiceNo = reader["InvoiceNo"].ToString(),
                            ProductCode = reader["ProductCode"].ToString()
                        };
                        objProductList.Add(objProduct);
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
            return objProductList;
        }

        internal int Payment(SaleLedger saleLedger)
        {
            int countAffectedRow = 0;

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspSalePayment", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("CustomerId", saleLedger.CustomerId);
                command.Parameters.AddWithValue("PaidAmount", saleLedger.PaidAmount);
                command.Parameters.AddWithValue("PaidDate", saleLedger.PaidDate);

                countAffectedRow = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            return countAffectedRow;
        }

        internal List<Product> GetProductByInvoiceNo(string invoiceNo)
        {
            List<Product> objProductList = new List<Product>();

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspGetProductForSaleByInvoiceNo", conn);
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
            }
            catch (Exception)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            return objProductList;
        }

        internal List<Sale> GetAllSale()
        {
            List<Sale> objSaleList = new List<Sale>();

            string query = "SELECT * FROM Sale";

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Sale objSale = new Sale()
                        {
                            CustomerId = Convert.ToInt32(reader["CustomerId"]),
                            InvoiceNo = reader["InvoiceNo"].ToString(),
                            SaleType = Convert.ToInt32(reader["SaleType"])
                        };
                        objSaleList.Add(objSale);
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
            return objSaleList;
        }

        internal List<SaleLedger> GetSaleLedger()
        {
            List<SaleLedger> saleLedgerList = new List<SaleLedger>();

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspSaleLedgerDetail", conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SaleLedger saleLedger = new SaleLedger()
                        {
                            CustomerId = reader["CustomerId"] != DBNull.Value ? Convert.ToInt32(reader["CustomerId"]) : 0,
                            CustomerName = reader["CustomerName"].ToString(),
                            SaleAmount = reader["SaleAmount"] != DBNull.Value ? Convert.ToDecimal(reader["SaleAmount"]) : 0,
                            PaidAmount = reader["PaidAmount"] != DBNull.Value ? Convert.ToDecimal(reader["PaidAmount"]) : 0,
                            Balance = reader["Balance"] != DBNull.Value ? Convert.ToDecimal(reader["Balance"]) : 0,
                            Status = reader["Status"].ToString()
                        };
                        saleLedgerList.Add(saleLedger);
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
            return saleLedgerList;
        }

        internal void CreateSaleDetail(SaleDetail objSaleDetail)
        {
            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("uspCreateSaleDetail", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("SaleType", objSaleDetail.SaleType);
                command.Parameters.AddWithValue("SaleId", objSaleDetail.SaleId);
                command.Parameters.AddWithValue("ProductId", objSaleDetail.ProductId);
                command.Parameters.AddWithValue("SaleRate", objSaleDetail.SaleRate);
                command.Parameters.AddWithValue("Quantity", objSaleDetail.Quantity);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }

        internal int GetSaleId(Sale objSale)
        {
            int saleId = 0;

            string query = "SELECT Id FROM Sale WHERE CustomerId = '" + objSale.CustomerId + "' AND InvoiceNo = '" + objSale.InvoiceNo + "'";

            SqlConnection conn = new SqlConnection(strCon.Connection());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        saleId = Convert.ToInt32(reader["Id"]);
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
            return saleId;
        }
    }
}