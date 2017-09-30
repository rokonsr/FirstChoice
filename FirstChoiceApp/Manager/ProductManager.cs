using FirstChoiceApp.Gateway;
using FirstChoiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceApp.Manager
{
    public class ProductManager
    {
        ProductGateway objProductGateway = new ProductGateway();

        internal List<Product> GetAllProduct()
        {
            return objProductGateway.GetAllProduct();
        }

        internal bool CreateProduct(Product objProduct)
        {
            if (objProductGateway.IsExist(objProduct))
            {
                throw new Exception("Product already exist");
            }
            return objProductGateway.CreateProduct(objProduct) > 0;
        }

        internal bool UpdateProduct(Product objProduct)
        {
            if (!objProductGateway.HasSizeId(objProduct))
            {
                throw new Exception("Please select product size");
            }
            if (objProductGateway.HasTypeId(objProduct))
            {
                throw new Exception("Please select product type");
            }
            return objProductGateway.UpdateProduct(objProduct) > 0;
        }
    }
}