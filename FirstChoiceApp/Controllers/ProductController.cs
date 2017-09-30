using FirstChoiceApp.Manager;
using FirstChoiceApp.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstChoiceApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ItemSortParm = String.IsNullOrEmpty(sortOrder) ? "item_desc" : "";
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

            ProductManager objProductManager = new ProductManager();
            var productList = objProductManager.GetAllProduct().OrderBy(x=>x.ProductName).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                productList = objProductManager.GetAllProduct().Where(x => x.ProductName.ToLower().Contains(searchString.ToLower())).OrderBy(x => x.ProductName).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    productList = productList.OrderByDescending(s => s.ProductName).ToList();
                    break;
                case "item_desc":
                    productList = productList.OrderByDescending(s => s.ItemName).ToList();
                    break;
                default:  // Name ascending 
                    productList = productList.OrderBy(s => s.ProductName).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(productList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            BrandManager objBrandManager = new BrandManager();
            ViewBag.Brand = objBrandManager.GetAllBrand().ToList();

            ItemManager objItemManager = new ItemManager();
            ViewBag.Item = objItemManager.GetAllItem().ToList();

            MeasurementManager objMeasurementManager = new MeasurementManager();
            ViewBag.Measurement = objMeasurementManager.GetAllMeasurement().ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            ProductManager objProductManager = new ProductManager();

            ModelState.Where(m => m.Key == "SizeId").FirstOrDefault().Value.Errors.Clear();
            ModelState.Where(m => m.Key == "TypeId").FirstOrDefault().Value.Errors.Clear();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objProductManager.CreateProduct(objProduct))
                    {
                        ViewBag.Success = "Product created successfully";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception exception)
                {
                    ViewBag.Error = exception.Message;
                }
            }
            BrandManager objBrandManager = new BrandManager();
            ViewBag.Brand = objBrandManager.GetAllBrand().ToList();

            ItemManager objItemManager = new ItemManager();
            ViewBag.Item = objItemManager.GetAllItem().ToList();

            MeasurementManager objMeasurementManager = new MeasurementManager();
            ViewBag.Measurement = objMeasurementManager.GetAllMeasurement().ToList();

            return View();
        }

        public ActionResult Edit(int? id)
        {
            BrandManager objBrandManager = new BrandManager();
            ViewBag.Brand = objBrandManager.GetAllBrand().ToList();

            ItemManager objItemManager = new ItemManager();
            ViewBag.Item = objItemManager.GetAllItem().ToList();

            MeasurementManager objMeasurementManager = new MeasurementManager();
            ViewBag.Measurement = objMeasurementManager.GetAllMeasurement().ToList();

            SizeManager objSizeManager = new SizeManager();
            ViewBag.Size = objSizeManager.GetAllSize().ToList();

            ProductTypeManager objProductTypeManager = new ProductTypeManager();
            ViewBag.ProductType = objProductTypeManager.GetAllProductType().ToList();

            ProductManager objProductManager = new ProductManager();
            var productDetail = objProductManager.GetAllProduct().Where(x => x.Id == id).FirstOrDefault();

            return View(productDetail);
        }

        [HttpPost]
        public ActionResult Edit(Product objProduct)
        {
            ProductManager objProductManager = new ProductManager();

            ModelState.Where(m => m.Key == "SizeId").FirstOrDefault().Value.Errors.Clear();
            ModelState.Where(m => m.Key == "Type").FirstOrDefault().Value.Errors.Clear();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objProductManager.UpdateProduct(objProduct))
                    {
                        ViewBag.Success = "Product updated successfully";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception exception)
                {
                    ViewBag.Error = exception.Message;
                }
            }
            BrandManager objBrandManager = new BrandManager();
            ViewBag.Brand = objBrandManager.GetAllBrand().ToList();

            ItemManager objItemManager = new ItemManager();
            ViewBag.Item = objItemManager.GetAllItem().ToList();

            MeasurementManager objMeasurementManager = new MeasurementManager();
            ViewBag.Measurement = objMeasurementManager.GetAllMeasurement().ToList();

            SizeManager objSizeManager = new SizeManager();
            ViewBag.Size = objSizeManager.GetAllSize().ToList();

            ProductTypeManager objProductTypeManager = new ProductTypeManager();
            ViewBag.ProductType = objProductTypeManager.GetAllProductType().ToList();
            
            return View();
        }

        [HttpPost]
        public JsonResult GetItemDetails(int? itemId)
        {
            SizeManager objSizeManager = new SizeManager();
            ViewBag.Size = objSizeManager.GetAllSize().ToList();

            ProductTypeManager objProductTypeManager = new ProductTypeManager();
            ViewBag.ProductType = objProductTypeManager.GetAllProductType().ToList();

            var objSize = objSizeManager.GetAllSize().Where(x => x.ItemId == itemId).ToList();
            var objProductType = objProductTypeManager.GetAllProductType().Where(x => x.ItemId == itemId).ToList();

            var itemDetails = new { objSize = objSize, objProductType = objProductType };
            return Json(itemDetails, JsonRequestBehavior.AllowGet);
        }
    }
}