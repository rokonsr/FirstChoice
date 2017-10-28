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

        internal List<Sale> GetAllSale()
        {
            return objSaleGateway.GetAllSale();
        }

        internal List<Product> GetProductByInvoiceNo(string invoiceNo)
        {
            return objSaleGateway.GetProductByInvoiceNo(invoiceNo);
        }

        internal List<Product> GetAllProductByIdInvoice(int productId, string invoiceNo)
        {
            return objSaleGateway.GetAllProductByIdInvoice(productId, invoiceNo);
        }

        internal List<Product> GetAllProductByCodeInvoice(string productCode, string invoiceNo)
        {
            return objSaleGateway.GetAllProductByCodeInvoice(productCode, invoiceNo);
        }

        internal bool Payment(SaleLedger saleLedger)
        {
            return objSaleGateway.Payment(saleLedger) > 0;
        }
    }
}