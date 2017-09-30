using System;
using System.Linq;
using System.Web.Mvc;
using FirstChoiceApp.Manager;
using FirstChoiceApp.Models;
using PagedList;

namespace FirstChoiceApp.Controllers
{
    [Authorize]
    public class SupplierController : Controller
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

            SupplierManager objSupplierManager = new SupplierManager();

            var supplierList = objSupplierManager.GetSupplierList().OrderBy(x => x.SupplierName).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                supplierList = objSupplierManager.GetSupplierList().Where(x => x.SupplierName.ToLower().Contains(searchString.ToLower())).OrderBy(x => x.SupplierName).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    supplierList = supplierList.OrderByDescending(s => s.SupplierName).ToList();
                    break;
                default:  // Name ascending 
                    supplierList = supplierList.OrderBy(s => s.SupplierName).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(supplierList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SupplierInfo objSupplierInfo)
        {
            SupplierManager objSupplierManager = new SupplierManager();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objSupplierManager.CreateSupplier(objSupplierInfo))
                    {
                        ViewBag.Success = "Supplier created successfully";
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
            SupplierManager objSupplierManager = new SupplierManager();

            var supplier = objSupplierManager.GetSupplierList().Where(x => x.Id == id).FirstOrDefault();

            return View(supplier);
        }

        [HttpPost]
        public ActionResult Edit(SupplierInfo objSupplierInfo)
        {
            SupplierManager objSupplierManager = new SupplierManager();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objSupplierManager.UpdateSupplierInfo(objSupplierInfo))
                    {
                        ViewBag.Success = "Supplier Updated successfully";
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
            SupplierManager objSupplierManager = new SupplierManager();

            var suppliers = objSupplierManager.GetSupplierList().Where(x => x.SupplierName.StartsWith(Prefix.ToUpper()) || x.SupplierName.StartsWith(Prefix.ToLower())).OrderBy(x=> x.SupplierName).Take(10);
            
            return Json(suppliers, JsonRequestBehavior.AllowGet);
        }
    }
}