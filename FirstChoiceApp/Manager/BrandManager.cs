using FirstChoiceApp.Gateway;
using FirstChoiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceApp.Manager
{
    public class BrandManager
    {
        BrandGateway objBrandGateway = new BrandGateway();

        internal List<Brand> GetAllBrand()
        {
            return objBrandGateway.GetAllBrand();
        }

        internal bool CreateBrand(Brand objBrand)
        {
            if (objBrandGateway.IsExist(objBrand))
            {
                throw new Exception("Brand already exist");
            }
            return objBrandGateway.CreateBrand(objBrand) > 0;
        }

        internal bool UpdateBrand(Brand objBrand)
        {
            return objBrandGateway.UpdateBrand(objBrand) > 0;
        }
    }
}