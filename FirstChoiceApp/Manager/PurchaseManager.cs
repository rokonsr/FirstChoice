using System;
using FirstChoiceApp.Models;
using FirstChoiceApp.Gateway;
using System.Collections.Generic;
using FirstChoiceApp.Models.ViewModel;

namespace FirstChoiceApp.Manager
{
    public class PurchaseManager
    {
        PurchaseGateway objPurchaseGateway = new PurchaseGateway();

        internal bool CreatePurchase(Purchase objPurchase)
        {
            return objPurchaseGateway.CreatePurchase(objPurchase) > 0;
        }

        internal void CreatePurchaseDetail(PurchaseDetail objPurchaseDetail)
        {
            objPurchaseGateway.CreatePurchaseDetail(objPurchaseDetail);
        }

        internal int GetPurchaseId(Purchase objPurchase)
        {
            return objPurchaseGateway.GetPurchaseId(objPurchase);
        }

        internal List<Purchase> GetAllPurchase()
        {
            return objPurchaseGateway.GetAllPurchase();
        }

        internal List<Product> GetProductByInvoiceNo(string invoiceNo)
        {
            return objPurchaseGateway.GetProductByInvoiceNo(invoiceNo);
        }

        internal List<PurchaseLedger> GetPurchaseLedger()
        {
            return objPurchaseGateway.GetPurchaseLedger();
        }

        internal bool Payment(PurchaseLedger purchaseLedger)
        {
            return objPurchaseGateway.Payment(purchaseLedger) > 0;
        }

        internal List<Product> GetAllProductByInvoice(int productCode, string invoiceNo)
        {
            return objPurchaseGateway.GetAllProductByInvoice(productCode, invoiceNo);
        }

        internal List<Product> GetAllProductByCodeInvoice(string productCode, string invoiceNo)
        {
            return objPurchaseGateway.GetAllProductByCodeInvoice(productCode, invoiceNo);
        }

        internal List<InvoiceSummaryPurchase> GetInvoiceDetil(string invoiceNo)
        {
            return objPurchaseGateway.GetInvoiceDetail(invoiceNo);
        }
    }
}