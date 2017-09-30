using FirstChoiceApp.Gateway;
using FirstChoiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceApp.Manager
{
    public class SizeManager
    {
        SizeGateway objSizeGateway = new SizeGateway();

        internal List<Size> GetAllSize()
        {
            return objSizeGateway.GetAllSize();
        }

        internal bool CreateProductSize(Size objSize)
        {
            if (objSizeGateway.IsExist(objSize))
            {
                throw new Exception("Product Size Already Exist");
            }
            return objSizeGateway.CreateProductSize(objSize) > 0;
        }

        internal bool UpdateProductSize(Size objSize)
        {
            return objSizeGateway.UpdateProductSize(objSize) > 0;
        }
    }
}