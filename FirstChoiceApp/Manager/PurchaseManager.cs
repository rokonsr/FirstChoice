using System;
using FirstChoiceApp.Models;
using FirstChoiceApp.Gateway;
using System.Collections.Generic;

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
    }
}