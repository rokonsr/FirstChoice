using FirstChoiceApp.Manager;
using FirstChoiceApp.Models;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FirstChoiceApp.Controllers
{
    [Authorize]
    public class ProductTypeController : Controller
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

            ProductTypeManager objProductTypeManager = new ProductTypeManager();

            var productTypeList = objProductTypeManager.GetAllProductType().OrderBy(x => x.TypeName).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                productTypeList = objProductTypeManager.GetAllProductType().Where(x => x.TypeName.ToLower().Contains(searchString.ToLower())).OrderBy(x => x.TypeName).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    productTypeList = productTypeList.OrderByDescending(s => s.TypeName).ToList();
                    break;
                case "item_desc":
                    productTypeList = productTypeList.OrderByDescending(s => s.ItemName).ToList();
                    break;
                default:  // Name ascending 
                    productTypeList = productTypeList.OrderBy(s => s.TypeName).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(productTypeList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            ItemManager objItemManager = new ItemManager();
            ViewBag.Item = objItemManager.GetAllItem().ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductType objProductType)
        {
            ProductTypeManager objProductTypeManager = new ProductTypeManager();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objProductTypeManager.CreateProductType(objProductType))
                    {
                        ViewBag.Success = "Product type created successfully";
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

            ProductTypeManager objProductTypeManager = new ProductTypeManager();

            var productType = objProductTypeManager.GetAllProductType().Where(x => x.Id == id).FirstOrDefault();

            return View(productType);
        }

        [HttpPost]
        public ActionResult Edit(ProductType objProductType)
        {
            ProductTypeManager objProductTypeManager = new ProductTypeManager();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objProductTypeManager.UpdateProductType(objProductType))
                    {
                        ViewBag.Success = "Product type updated successfully";
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
            ProductTypeManager objProductTypeManager = new ProductTypeManager();

            var typeName = objProductTypeManager.GetAllProductType().Where(x => x.TypeName.StartsWith(Prefix.ToLower()) || x.TypeName.StartsWith(Prefix.ToUpper())).OrderBy(x => x.TypeName).Take(10);

            return Json(typeName, JsonRequestBehavior.AllowGet);
        }
    }
}