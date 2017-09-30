using System;
using System.Collections.Generic;
using FirstChoiceApp.Gateway;
using FirstChoiceApp.Models;

namespace FirstChoiceApp.Manager
{
    public class SupplierManager
    {
        private SupplierGateway objSupplierGateway = new SupplierGateway();

        internal List<SupplierInfo> GetSupplierList()
        {
            return objSupplierGateway.GetAllSupplier();
        }

        internal bool CreateSupplier(SupplierInfo objSupplierInfo)
        {
            if (objSupplierGateway.IsExist(objSupplierInfo))
            {
                throw new Exception("Supplier name already exist");
            }
            return objSupplierGateway.CreateSupplier(objSupplierInfo) > 0;
        }

        internal bool UpdateSupplierInfo(SupplierInfo objSupplierInfo)
        {
            return objSupplierGateway.UpdateSupplierInfo(objSupplierInfo) > 0;
        }
    }
}