using FirstChoiceApp.Gateway;
using FirstChoiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceApp.Manager
{
    public class ProductTypeManager
    {
        ProductTypeGateway objProductTypeGateway = new ProductTypeGateway();

        internal List<ProductType> GetAllProductType()
        {
            return objProductTypeGateway.GetAllProductType();
        }

        internal bool CreateProductType(ProductType objProductType)
        {
            if (objProductTypeGateway.IsExist(objProductType))
            {
                throw new Exception("Product type alreay exist");
            }
            return objProductTypeGateway.CreateProductType(objProductType) > 0;
        }

        internal bool UpdateProductType(ProductType objProductType)
        {
            return objProductTypeGateway.UpdateProductType(objProductType) > 0;
        }
    }
}