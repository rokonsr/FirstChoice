﻿using FirstChoiceApp.Manager;
using FirstChoiceApp.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FirstChoiceApp.Controllers
{
    [Authorize]
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

                if (objSale.SaleType == 1)
                {
                    objSale.InvoiceNo = DateTime.Now.ToString("yyMMddHHmmss");
                }

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
                        objSaleDetail.SaleRate = item.SaleRate;

                        objSaleManager.CreateSaleDetail(objSaleDetail);
                    }
                    ViewBag.Message = "product sale succesfully";
                    ModelState.Clear();
                    Session["SaleDetail"] = null;
                }
            }
            catch (Exception exception)
            {
                @ViewBag.Error = "Product sale failed";
                string msg = exception.Message;
            }

            return View();
        }

        [HttpPost]
        public JsonResult SaleDetails(int productId, decimal quantity, decimal saleRate, string productName)
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
                objProduct.Stock = item.Stock;
                objProduct.Price = item.Price;
            }

            return Json(objProduct, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProductByIdInvoice(int productId, string invoiceNo)
        {
            SaleManager objSaleManager = new SaleManager();

            Product objProduct = new Product();
            List<Product> objProductList = objSaleManager.GetAllProductByIdInvoice(productId, invoiceNo);

            foreach (var item in objProductList)
            {
                objProduct.ProductCode = item.ProductCode;
                objProduct.Stock = item.Stock;
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
                objProduct.Stock = item.Stock;
                objProduct.Price = item.Price;
            }

            return Json(objProduct, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProductByCodeInvoice(string productCode, string invoiceNo)
        {
            SaleManager objSaleManager = new SaleManager();

            Product objProduct = new Product();
            List<Product> objProductList = objSaleManager.GetAllProductByCodeInvoice(productCode, invoiceNo);

            foreach (var item in objProductList)
            {
                objProduct.Id = item.Id;
                objProduct.Stock = item.Stock;
                objProduct.Price = item.Price;
            }

            return Json(objProduct, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCustomerDue(int? customerId)
        {
            SaleManager objSaleManager = new SaleManager();
            var customerDue = objSaleManager.GetSaleLedger().Where(x => x.CustomerId == customerId).Select(x => x.Balance);

            return Json(customerDue, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AutoComplete(string Prefix, int customerId)
        {
            SaleManager objSaleManager = new SaleManager();

            var invoiceNo = objSaleManager.GetAllSale().Where(x => x.InvoiceNo.Contains(Prefix.ToLower()) || x.InvoiceNo.Contains(Prefix.ToUpper())).Where(x => x.CustomerId == customerId).Where(x => x.SaleType == 1).Take(10);

            return Json(invoiceNo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProduct(string invoiceNo)
        {
            PurchaseManager objPurchaseManager = new PurchaseManager();
            SaleManager objSaleMnager = new SaleManager();
            var productList = objSaleMnager.GetProductByInvoiceNo(invoiceNo).ToList();

            return Json(productList, JsonRequestBehavior.AllowGet);
        }

        public ViewResult Ledger(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PurchaseSortParm = String.IsNullOrEmpty(sortOrder) ? "purchase_desc" : "";
            ViewBag.PaidSortParm = String.IsNullOrEmpty(sortOrder) ? "paid_desc" : "";
            ViewBag.DueSortParm = String.IsNullOrEmpty(sortOrder) ? "due_desc" : "";
            ViewBag.StatusSortParm = String.IsNullOrEmpty(sortOrder) ? "status_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            //PurchaseManager purchaseManager = new PurchaseManager();
            SaleManager saleManager = new SaleManager();

            var saleLedger = saleManager.GetSaleLedger().OrderBy(x => x.CustomerName).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                saleLedger = saleManager.GetSaleLedger().Where(x => x.Status.ToLower().Contains(searchString.ToLower())).OrderBy(x => x.CustomerName).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    saleLedger = saleLedger.OrderByDescending(s => s.CustomerName).ToList();
                    break;
                case "purchase_desc":
                    saleLedger = saleLedger.OrderByDescending(s => s.SaleAmount).ToList();
                    break;
                case "paid_desc":
                    saleLedger = saleLedger.OrderByDescending(s => s.PaidAmount).ToList();
                    break;
                case "due_desc":
                    saleLedger = saleLedger.OrderByDescending(s => s.DueAmount).ToList();
                    break;
                case "status_desc":
                    saleLedger = saleLedger.OrderByDescending(s => s.Status).ToList();
                    break;
                default:  // Name ascending 
                    saleLedger = saleLedger.OrderBy(x => x.Balance).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(saleLedger.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Payment()
        {
            SaleManager objSaleManager = new SaleManager();
            ViewBag.CustomerList = objSaleManager.GetSaleLedger().Where(x => x.Balance != 0).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Payment(SaleLedger saleLedger)
        {
            SaleManager objSaleManager = new SaleManager();
            ViewBag.CustomerList = objSaleManager.GetSaleLedger().Where(x => x.Balance != 0).ToList();

            try
            {
                if (objSaleManager.Payment(saleLedger))
                {
                    ViewBag.Success = "Payment Done";
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
    }
}