using FirstChoiceApp.Manager;
using FirstChoiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FirstChoiceApp.Controllers
{
    public class SaleController : Controller
    {
        public ActionResult Create()
        {
            CustomerManager objCustomerManager = new CustomerManager();
            ViewBag.CustomerList = objCustomerManager.GetAllCustomer().ToList();

            ProductManager objProductManager = new ProductManager();
            ViewBag.ProductList = objProductManager.GetAllProduct().ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Create(Sale objSale)
        {
            CustomerManager objCustomerManager = new CustomerManager();
            ViewBag.CustomerList = objCustomerManager.GetAllCustomer().ToList();

            ProductManager objProductManager = new ProductManager();
            ViewBag.ProductList = objProductManager.GetAllProduct().ToList();

            if (Session["SaleDetail"] == null)
            {
                @ViewBag.Error = "Please add product";
                return View();
            }

            try
            {
                SaleManager objSaleManager = new SaleManager();

                if (objSaleManager.CreateSale(objSale))
                {
                    int SaleId = objSaleManager.GetSaleId(objSale);

                    if (SaleId < 1)
                    {
                        @ViewBag.Error = "Product add failed";
                        return View();
                    }

                    var list = Session["SaleDetail"] as List<SaleDetail>;

                    foreach (var item in list)
                    {
                        SaleDetail objSaleDetail = new SaleDetail();
                        objSaleDetail.SaleType = objSale.SaleType;
                        objSaleDetail.SaleId = SaleId;
                        objSaleDetail.ProductId = item.ProductId;
                        objSaleDetail.Quantity = item.Quantity;
                        objSaleDetail.PurchaseRate = item.PurchaseRate;
                        objSaleDetail.SaleRate = item.SaleRate;

                        objSaleManager.CreateSaleDetail(objSaleDetail);
                    }
                    ViewBag.Message = "Add product succesfully";
                    ModelState.Clear();
                    Session["SaleDetail"] = null;
                }
            }
            catch (Exception)
            {
                @ViewBag.Error = "Product add failed";
            }

            return View();
        }

        [HttpPost]
        public JsonResult SaleDetails(int productId, decimal quantity, decimal purchaseRate, decimal saleRate, string productName)
        {
            bool isExist = false;
            var list = Session["SaleDetail"] as List<SaleDetail>;

            List<SaleDetail> objSaleDetailList = new List<SaleDetail>();
            if (list != null)
            {
                isExist = list.Exists(x => x.ProductId == productId);
            }

            if (!isExist)
            {
                if (quantity != 0)
                {
                    SaleDetail objSaleDetail = new SaleDetail()
                    {
                        ProductId = productId,
                        ProductName = productName,
                        PurchaseRate = purchaseRate,
                        SaleRate = saleRate,
                        Quantity = quantity
                    };
                    objSaleDetailList.Add(objSaleDetail);

                    if (Session["SaleDetail"] == null)
                    {
                        Session["SaleDetail"] = objSaleDetailList;
                    }
                    else
                    {
                        list.Add(objSaleDetail);
                    }
                    list = Session["SaleDetail"] as List<SaleDetail>;
                }
            }
            else
            {
                if (quantity == 0)
                {
                    list.Remove(list.SingleOrDefault(x => x.ProductId == productId));
                }
                foreach (var item in list.Where(x => x.ProductId == productId))
                {
                    item.Quantity = quantity;
                    item.SaleRate = saleRate;
                }
            }

            decimal totalAmount = 0;

            foreach (var item in list)
            {
                totalAmount += item.Quantity * item.SaleRate;
            }

            var saleDetail = new { list = list, totalAmount = totalAmount };

            return Json(saleDetail, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProductById(int? productId)
        {
            ProductManager objProductManager = new ProductManager();

            Product objProduct = new Product();
            List<Product> objProductList = objProductManager.GetAllProduct().Where(x => x.Id == productId).ToList();

            foreach (var item in objProductList)
            {
                objProduct.ProductCode = item.ProductCode;
                objProduct.Price = item.Price;
            }

            return Json(objProduct, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProductByCode(string productCode)
        {
            ProductManager objProductManager = new ProductManager();

            Product objProduct = new Product();
            List<Product> objProductList = objProductManager.GetAllProduct().Where(x => x.ProductCode.Equals(productCode)).ToList();

            foreach (var item in objProductList)
            {
                objProduct.Id = item.Id;
                objProduct.Price = item.Price;
            }

            return Json(objProduct, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSupplierDue(int customerId)
        {
            PurchaseManager objPurchaseManager = new PurchaseManager();
            var supplierDue = objPurchaseManager.GetPurchaseLedger().Where(x => x.SupplierId == customerId).Select(x => x.Balance);

            return Json(supplierDue, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AutoComplete(string Prefix, int customerId)
        {
            PurchaseManager objPurchaseManager = new PurchaseManager();

            var invoiceNo = objPurchaseManager.GetAllPurchase().Where(x => x.InvoiceNo.StartsWith(Prefix.ToLower()) || x.InvoiceNo.StartsWith(Prefix.ToUpper())).Where(x => x.SupplierId == customerId).Where(x => x.PurchaseType == 1).Take(10);

            return Json(invoiceNo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProduct(string invoiceNo)
        {
            PurchaseManager objPurchaseManager = new PurchaseManager();
            var productList = objPurchaseManager.GetProductByInvoiceNo(invoiceNo).ToList();

            return Json(productList, JsonRequestBehavior.AllowGet);
        }

        //public ViewResult Ledger(string sortOrder, string currentFilter, string searchString, int? page)
        //{
        //    ViewBag.CurrentSort = sortOrder;
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewBag.PurchaseSortParm = String.IsNullOrEmpty(sortOrder) ? "purchase_desc" : "";
        //    ViewBag.PaidSortParm = String.IsNullOrEmpty(sortOrder) ? "paid_desc" : "";
        //    ViewBag.DueSortParm = String.IsNullOrEmpty(sortOrder) ? "due_desc" : "";
        //    ViewBag.StatusSortParm = String.IsNullOrEmpty(sortOrder) ? "status_desc" : "";
        //    ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

        //    if (searchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }

        //    ViewBag.CurrentFilter = searchString;

        //    PurchaseManager purchaseManager = new PurchaseManager();

        //    var purchaseLedger = purchaseManager.GetPurchaseLedger().OrderBy(x => x.SupplierName).ToList();

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        purchaseLedger = purchaseManager.GetPurchaseLedger().Where(x => x.Status.ToLower().Contains(searchString.ToLower())).OrderBy(x => x.SupplierName).ToList();
        //    }
        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            purchaseLedger = purchaseLedger.OrderByDescending(s => s.SupplierName).ToList();
        //            break;
        //        case "purchase_desc":
        //            purchaseLedger = purchaseLedger.OrderByDescending(s => s.PurchaseAmount).ToList();
        //            break;
        //        case "paid_desc":
        //            purchaseLedger = purchaseLedger.OrderByDescending(s => s.PaidAmount).ToList();
        //            break;
        //        case "due_desc":
        //            purchaseLedger = purchaseLedger.OrderByDescending(s => s.DueAmount).ToList();
        //            break;
        //        case "status_desc":
        //            purchaseLedger = purchaseLedger.OrderByDescending(s => s.Status).ToList();
        //            break;
        //        default:  // Name ascending 
        //            purchaseLedger = purchaseLedger.OrderBy(x => x.SupplierName).ToList();
        //            break;
        //    }

        //    int pageSize = 10;
        //    int pageNumber = (page ?? 1);
        //    return View(purchaseLedger.ToPagedList(pageNumber, pageSize));
        //}

        //public ActionResult Payment()
        //{
        //    PurchaseManager objPurchaseManager = new PurchaseManager();
        //    ViewBag.SupplierList = objPurchaseManager.GetPurchaseLedger().Where(x => x.Balance != 0).ToList();

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Payment(PurchaseLedger purchaseLedger)
        //{
        //    PurchaseManager objPurchaseManager = new PurchaseManager();
        //    ViewBag.SupplierList = objPurchaseManager.GetPurchaseLedger().Where(x => x.Balance != 0).ToList();

        //    try
        //    {
        //        if (objPurchaseManager.Payment(purchaseLedger))
        //        {
        //            ViewBag.Success = "Payment Done";
        //            ModelState.Clear();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //    }
        //    return View();
        //}
    }
}