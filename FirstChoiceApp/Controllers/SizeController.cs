using FirstChoiceApp.Manager;
using FirstChoiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace FirstChoiceApp.Controllers
{
    [Authorize]
    public class SizeController : Controller
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

            SizeManager objSizeManager = new SizeManager();

            var productSizeList = objSizeManager.GetAllSize().OrderBy(x => x.ProductSize).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                productSizeList = objSizeManager.GetAllSize().Where(x => x.ProductSize.ToLower().Contains(searchString.ToLower())).OrderBy(x => x.ProductSize).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    productSizeList = productSizeList.OrderByDescending(s => s.ProductSize).ToList();
                    break;
                case "item_desc":
                    productSizeList = productSizeList.OrderByDescending(s => s.ItemName).ToList();
                    break;
                default:  // Name ascending 
                    productSizeList = productSizeList.OrderBy(s => s.ProductSize).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(productSizeList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            ItemManager objItemManager = new ItemManager();
            ViewBag.Item = objItemManager.GetAllItem().ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Create(Size objSize)
        {
            SizeManager objSizeManager = new SizeManager();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objSizeManager.CreateProductSize(objSize))
                    {
                        ViewBag.Success = "Product Size Created Successfully";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception exception)
                {
                    ViewBag.Error = exception.Message;
                }
            }
            ItemManager objItemManager = new ItemManager();
            ViewBag.Item = objItemManager.GetAllItem().ToList();

            return View();
        }

        public ActionResult Edit(int? id)
        {
            ItemManager objItemManager = new ItemManager();
            ViewBag.Item = objItemManager.GetAllItem().ToList();

            SizeManager objSizeManager = new SizeManager();

            var productSize = objSizeManager.GetAllSize().Where(x => x.Id == id).FirstOrDefault();

            return View(productSize);
        }

        [HttpPost]
        public ActionResult Edit(Size objSize)
        {
            SizeManager objSizeManager = new SizeManager();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objSizeManager.UpdateProductSize(objSize))
                    {
                        ViewBag.Success = "Product Size Updated Successfully";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception exception)
                {
                    ViewBag.Error = exception.Message;
                }
            }
            ItemManager objItemManager = new ItemManager();
            ViewBag.Item = objItemManager.GetAllItem().ToList();

            return View();
        }

        [HttpPost]
        public JsonResult AutoComplete(string Prefix)
        {
            SizeManager objSizeManager = new SizeManager();

            var brand = objSizeManager.GetAllSize().Where(x => x.ProductSize.StartsWith(Prefix.ToLower()) || x.ProductSize.StartsWith(Prefix.ToUpper())).OrderBy(x => x.ProductSize).Take(10);

            return Json(brand, JsonRequestBehavior.AllowGet);
        }
    }
}