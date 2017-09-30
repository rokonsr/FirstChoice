using FirstChoiceApp.Manager;
using FirstChoiceApp.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace FirstChoiceApp.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CategorySortParm = String.IsNullOrEmpty(sortOrder) ? "cat_desc" : "";
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

            ItemManager objItemManager = new ItemManager();

            var itemList = objItemManager.GetAllItem().OrderBy(x => x.ItemName).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                itemList = objItemManager.GetAllItem().Where(x => x.ItemName.ToLower().Contains(searchString.ToLower())).OrderBy(x => x.ItemName).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    itemList = itemList.OrderByDescending(s => s.ItemName).ToList();
                    break;
                case "cat_desc":
                    itemList = itemList.OrderByDescending(s => s.CategoryName).ToList();
                    break;
                default:  // Name ascending 
                    itemList = itemList.OrderBy(s => s.ItemName).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(itemList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            CategoryManager objCategoryManager = new CategoryManager();

            ViewBag.Category = objCategoryManager.GetAllCategory().ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Create(Item objItem)
        {
            ItemManager objItemManager = new ItemManager();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objItemManager.CreateItem(objItem))
                    {
                        ViewBag.Success = "Item Created Successfully";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception exception)
                {
                    ViewBag.Error = exception.Message;
                }
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            CategoryManager objCategoryManager = new CategoryManager();
            ViewBag.Category = objCategoryManager.GetAllCategory().ToList();

            ItemManager objItemManager = new ItemManager();
            var itemName = objItemManager.GetAllItem().Where(x => x.Id == id).FirstOrDefault();

            return View(itemName);
        }

        [HttpPost]
        public ActionResult Edit(Item objItem)
        {
            ItemManager objItemManager = new ItemManager();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objItemManager.UpdateItemName(objItem))
                    {
                        ViewBag.Success = "Item Updated Successfully";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception exception)
                {
                    ViewBag.Error = exception.Message;
                }
            }
            return View();
        }

        [HttpPost]
        public JsonResult AutoComplete(string Prefix)
        {
            ItemManager objItemManager = new ItemManager();

            var itemName = objItemManager.GetAllItem().Where(x => x.ItemName.StartsWith(Prefix.ToLower()) || x.ItemName.StartsWith(Prefix.ToUpper())).OrderBy(x => x.ItemName).Take(10);

            return Json(itemName, JsonRequestBehavior.AllowGet);
        }
    }
}