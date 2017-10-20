using System;
using FirstChoiceApp.Models;
using FirstChoiceApp.Gateway;
using System.Collections.Generic;

namespace FirstChoiceApp.Manager
{
    public class SaleManager
    {
        SaleGateway objSaleGateway = new SaleGateway();

        internal bool CreateSale(Sale objSale)
        {
            return objSaleGateway.CreateSale(objSale) > 0;
        }

        internal int GetSaleId(Sale objSale)
        {
            return objSaleGateway.GetSaleId(objSale);
        }

        internal void CreateSaleDetail(SaleDetail objSaleDetail)
        {
            objSaleGateway.CreateSaleDetail(objSaleDetail);
        }

        internal List<SaleLedger> GetSaleLedger()
        {
            return objSaleGateway.GetSaleLedger();
        }
    }
}