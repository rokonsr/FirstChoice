using System;
using FirstChoiceApp.Models;
using FirstChoiceApp.Gateway;

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
            throw new NotImplementedException();
        }

        internal void CreateSaleDetail(SaleDetail objSaleDetail)
        {
            throw new NotImplementedException();
        }
    }
}