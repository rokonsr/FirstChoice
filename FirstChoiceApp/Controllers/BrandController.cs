using FirstChoiceApp.Manager;
using FirstChoiceApp.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace FirstChoiceApp.Controllers
{
    [Authorize]
    public class BrandController : Controller
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

            BrandManager objBrandManager = new BrandManager();

            var brandList = objBrandManager.GetAllBrand().OrderBy(x => x.BrandName).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                brandList = objBrandManager.GetAllBrand().Where(x => x.BrandName.ToLower().Contains(searchString.ToLower())).OrderBy(x => x.BrandName).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    brandList = brandList.OrderByDescending(s => s.BrandName).ToList();
                    break;
                default:  // Name ascending 
                    brandList = brandList.OrderBy(s => s.BrandName).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(brandList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Brand objBrand)
        {
            BrandManager objBrandManager = new BrandManager();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objBrandManager.CreateBrand(objBrand))
                    {
                        ViewBag.Success = "Brand created successfully";
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
            BrandManager objBrandManager = new BrandManager();

            var brand = objBrandManager.GetAllBrand().Where(x => x.Id == id).FirstOrDefault();

            return View(brand);
        }

        [HttpPost]
        public ActionResult Edit(Brand objBrand)
        {
            BrandManager objBrandManager = new BrandManager();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objBrandManager.UpdateBrand(objBrand))
                    {
                        ViewBag.Success = "Brand Updated Successfully";
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
            BrandManager objBrandManager = new BrandManager();

            var brand = objBrandManager.GetAllBrand().Where(x => x.BrandName.StartsWith(Prefix.ToLower()) || x.BrandName.StartsWith(Prefix.ToUpper())).OrderBy(x => x.BrandName).Take(10);

            return Json(brand, JsonRequestBehavior.AllowGet);
        }
    }
}