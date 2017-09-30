using FirstChoiceApp.Manager;
using FirstChoiceApp.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace FirstChoiceApp.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
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

            CategoryManager objCategoryManager = new CategoryManager();

            var categoryList = objCategoryManager.GetAllCategory().OrderBy(x => x.CategoryName).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                categoryList = objCategoryManager.GetAllCategory().Where(x => x.CategoryName.ToLower().Contains(searchString.ToLower())).OrderBy(x => x.CategoryName).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    categoryList = categoryList.OrderByDescending(s => s.CategoryName).ToList();
                    break;
                default:  // Name ascending 
                    categoryList = categoryList.OrderBy(s => s.CategoryName).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(categoryList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category objCategory)
        {
            CategoryManager objCategoryManager = new CategoryManager();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objCategoryManager.CreateCategory(objCategory))
                    {
                        ViewBag.Success = "Category created successfully";
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

            var category = objCategoryManager.GetAllCategory().Where(x => x.Id == id).FirstOrDefault();

            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category objCategory)
        {
            CategoryManager objCategoryManager = new CategoryManager();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objCategoryManager.UpdateCategory(objCategory))
                    {
                        ViewBag.Success = "Category Updated successfully";
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
            CategoryManager objCategoryManager = new CategoryManager();

            var category = objCategoryManager.GetAllCategory().Where(x => x.CategoryName.StartsWith(Prefix.ToLower()) || x.CategoryName.StartsWith(Prefix.ToUpper())).OrderBy(x => x.CategoryName).Take(10);

            return Json(category, JsonRequestBehavior.AllowGet);
        }
    }
}