﻿using FirstChoiceApp.Manager;
using FirstChoiceApp.Models;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FirstChoiceApp.Controllers
{
    public class ExpenseController : Controller
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

            ExpenseManager objExpenseManager = new ExpenseManager();

            var expenseTypeList = objExpenseManager.GetAllExpenseTypeList().OrderBy(x => x.ExpenseTitle).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                expenseTypeList = objExpenseManager.GetAllExpenseTypeList().Where(x => x.ExpenseTitle.ToLower().Contains(searchString.ToLower())).OrderBy(x => x.ExpenseTitle).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    expenseTypeList = expenseTypeList.OrderByDescending(s => s.ExpenseTitle).ToList();
                    break;
                default:  // Name ascending 
                    expenseTypeList = expenseTypeList.OrderBy(s => s.ExpenseTitle).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(expenseTypeList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CreateExpenseType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateExpenseType(ExpenseType expenseType)
        {
            try
            {
                ExpenseManager objExpenseManager = new ExpenseManager();

                if (objExpenseManager.CreateExpenseType(expenseType))
                {
                    ViewBag.Success = "Expense Type Created Successfully";
                    ModelState.Clear();
                }
            }
            catch (Exception exception)
            {
                ViewBag.Error = exception.Message;
                return View();
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditExpenseType(int? id)
        {
            ExpenseManager objExpenseManager = new ExpenseManager();

            var expenseType = objExpenseManager.GetAllExpenseTypeList().Where(x => x.Id == id).FirstOrDefault();
            return View(expenseType);
        }

        [HttpPost]
        public ActionResult EditExpenseType(ExpenseType expenseType)
        {
            try
            {
                ExpenseManager objExpenseManager = new ExpenseManager();

                if (objExpenseManager.UpdateExpenseType(expenseType))
                {
                    ViewBag.Success = "Expense Type Updated Successfully";
                    ModelState.Clear();
                }
            }
            catch (Exception exception)
            {
                ViewBag.Error = exception.Message;
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult AutoComplete(string Prefix)
        {
            ExpenseManager objExpenseManager = new ExpenseManager();

            var expenseTitles = objExpenseManager.GetAllExpenseTypeList().Where(x => x.ExpenseTitle.StartsWith(Prefix.ToLower()) || x.ExpenseTitle.StartsWith(Prefix.ToUpper())).OrderBy(x => x.ExpenseTitle).Take(10);

            CustomerManager objCustomerManager = new CustomerManager();

            var customers = objCustomerManager.GetAllCustomer().Where(x => x.CustomerName.StartsWith(Prefix.ToLower()) || x.CustomerName.StartsWith(Prefix.ToUpper())).OrderBy(x => x.CustomerName).Take(10);

            return Json(expenseTitles, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExpenseDetail()
        {
            ExpenseManager objExpenseManager = new ExpenseManager();

            var expenseDetail = objExpenseManager.GetAllExpenseDetail();
            return View(expenseDetail);
        }

        public ActionResult CreateExpenseDetail()
        {
            ExpenseManager objExpenseManager = new ExpenseManager();
            ViewBag.ExpenseType = objExpenseManager.GetAllExpenseTypeList().ToList();

            return View();
        }

        [HttpPost]
        public ActionResult CreateExpenseDetail(ExpenseDetail expenseDetail)
        {
            ExpenseManager objExpenseManager = new ExpenseManager();
            ViewBag.ExpenseType = objExpenseManager.GetAllExpenseTypeList().ToList();

            try
            {
                if (objExpenseManager.CreateExpenseDetail(expenseDetail))
                {
                    ViewBag.Success = "Expense Added Successfully";
                    ModelState.Clear();
                }
            }
            catch (Exception exception)
            {
                ViewBag.Error = exception.Message;
                return View();
            }
            return RedirectToAction("ExpenseDetail");
        }
    }
}