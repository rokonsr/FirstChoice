using System;
using System.Collections.Generic;
using FirstChoiceApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace FirstChoiceApp.Gateway
{
    public class ProductGateway
    {
        DbConnection objConnection = new DbConnection();

        internal List<Product> GetAllProduct()
        {
            List<Product> objProductList = new List<Product>();

            SqlCommand command = new SqlCommand("uspGetAllProduct", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Product objProduct = new Product()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        BrandId = Convert.ToInt32(reader["BrandId"]),
                        BrandName = reader["BrandName"].ToString(),
                        ItemId = Convert.ToInt32(reader["ItemId"]),
                        ItemName = reader["ItemName"].ToString(),
                        MeasurementId = Convert.ToInt32(reader["MeasurementId"]),
                        MeasurementName = reader["MeasurementName"].ToString(),
                        SizeId = Convert.ToInt32(reader["SizeId"]),
                        ProductSize = reader["ProductSize"].ToString(),
                        TypeId = Convert.ToInt32(reader["TypeId"]),
                        Stock = Convert.ToDecimal(reader["Stock"]),
                        Price = Convert.ToDecimal(reader["Price"]),
                        TypeName = reader["TypeName"].ToString(),
                        ProductCode = reader["ProductCode"].ToString(),
                        ProductName = reader["ProductName"].ToString()
                    };
                    objProductList.Add(objProduct);
                }
            }
            return objProductList;
        }

        internal bool HasSizeId(Product objProduct)
        {
            bool hasTypeId = false;
            hasTypeId = GetAllProduct().Exists(x => x.SizeId > 0 && x.ItemId == objProduct.ItemId && x.Id == objProduct.Id);
            return hasTypeId;
        }

        internal bool HasTypeId(Product objProduct)
        {
            throw new NotImplementedException();
        }

        internal int UpdateProduct(Product objProduct)
        {
            throw new NotImplementedException();
        }

        internal bool IsExist(Product objProduct)
        {
            bool isExist = false;
            isExist = GetAllProduct().Exists(
                                                x => x.BrandId == objProduct.BrandId && 
                                                x.ItemId == objProduct.ItemId && 
                                                x.MeasurementId == objProduct.MeasurementId && 
                                                x.SizeId == objProduct.SizeId && 
                                                x.TypeId == objProduct.TypeId &&
                                                x.ProductCode.Equals(objProduct.ProductCode)
                                            );
            return isExist;
        }

        internal int CreateProduct(Product objProduct)
        {
            int countAffectedRow = 0;

            SqlCommand command = new SqlCommand("uspCreateProduct", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("BrandId", objProduct.BrandId);
            command.Parameters.AddWithValue("ItemId", objProduct.ItemId);
            command.Parameters.AddWithValue("MeasurementId", objProduct.MeasurementId);
            command.Parameters.AddWithValue("SizeId", objProduct.SizeId);
            command.Parameters.AddWithValue("TypeId", objProduct.TypeId);
            command.Parameters.AddWithValue("ProductCode", objProduct.ProductCode);
            command.Parameters.AddWithValue("ProductName", objProduct.ProductName);

            countAffectedRow = command.ExecuteNonQuery();

            return countAffectedRow;
        }
    }
}