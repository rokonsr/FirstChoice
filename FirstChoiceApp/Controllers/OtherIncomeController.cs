using FirstChoiceApp.Manager;
using FirstChoiceApp.Models;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FirstChoiceApp.Controllers
{
    public class OtherIncomeController : Controller
    {
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AmountSortParm = String.IsNullOrEmpty(sortOrder) ? "amount_desc" : "";
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

            OtherIncomeManager objOtherIncomeManager = new OtherIncomeManager();

            var incomeDetailList = objOtherIncomeManager.GetAllOtherIncome().OrderByDescending(x => x.CreatedDate).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                incomeDetailList = objOtherIncomeManager.GetAllOtherIncome().Where(x => x.IncomeTypeTitle.ToLower().Contains(searchString.ToLower())).OrderByDescending(x => x.CreatedDate).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    incomeDetailList = incomeDetailList.OrderByDescending(s => s.IncomeTypeTitle).ToList();
                    break;
                case "amount_desc":
                    incomeDetailList = incomeDetailList.OrderByDescending(s => s.IncomeAmount).ToList();
                    break;
                default:  // Name ascending 
                    incomeDetailList = incomeDetailList.OrderByDescending(s => s.CreatedDate).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(incomeDetailList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CreateOtherIncome()
        {
            OtherIncomeManager objOtherIncomeManager = new OtherIncomeManager();
            ViewBag.IncomeType = objOtherIncomeManager.GetIncomeTypeList().ToList();

            return View();
        }

        [HttpPost]
        public ActionResult CreateOtherIncome(OtherIncomeDetail otherIncomeDetail)
        {
            OtherIncomeManager objOtherIncomeManager = new OtherIncomeManager();
            ViewBag.IncomeType = objOtherIncomeManager.GetIncomeTypeList().ToList();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objOtherIncomeManager.CreateOtherIncome(otherIncomeDetail))
                    {
                        ViewBag.Success = "Data Added Successfully";
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
            OtherIncomeManager objOtherIncomeManager = new OtherIncomeManager();
            ViewBag.IncomeType = objOtherIncomeManager.GetIncomeTypeList().ToList();
            var incomeDetailList = objOtherIncomeManager.GetAllOtherIncome().Where(x => x.Id == id).FirstOrDefault();

            return View(incomeDetailList);
        }

        [HttpPost]
        public ActionResult Edit(OtherIncomeDetail otherIncomeDetail)
        {
            OtherIncomeManager objOtherIncomeManager = new OtherIncomeManager();
            ViewBag.IncomeType = objOtherIncomeManager.GetIncomeTypeList().ToList();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objOtherIncomeManager.UpdateOtherIncome(otherIncomeDetail))
                    {
                        ViewBag.Success = "Data Updated Successfully";
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
    }
}