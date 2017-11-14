using FirstChoiceApp.Manager;
using FirstChoiceApp.Models;
using FirstChoiceApp.Models.ViewModel;
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
            if (ModelState.IsValid)
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
            if (ModelState.IsValid)
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

        public ViewResult ExpenseDetail(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ExpenseTitleSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ExpenseAmountSortParm = String.IsNullOrEmpty(sortOrder) ? "amount_desc" : "";
            ViewBag.ExpenseDateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "Date";
            ViewBag.RemarksSortParm = String.IsNullOrEmpty(sortOrder) ? "remarks_desc" : "";

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

            var expenseDetails = objExpenseManager.GetAllExpenseDetail().OrderBy(x => x.ExpenseTitle).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                expenseDetails = objExpenseManager.GetAllExpenseDetail().Where(x => x.ExpenseTitle.ToLower().Contains(searchString.ToLower())).OrderBy(x => x.ExpenseTitle).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    expenseDetails = expenseDetails.OrderBy(s => s.ExpenseTitle).ToList();
                    break;
                case "date_desc":
                    expenseDetails = expenseDetails.OrderBy(s => s.ExpenseDate).ToList();
                    break;
                default:
                    expenseDetails = expenseDetails.OrderByDescending(s => s.ExpenseDate).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(expenseDetails.ToPagedList(pageNumber, pageSize));
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

            if (ModelState.IsValid)
            {
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
            }
            return RedirectToAction("ExpenseDetail");
        }

        public ActionResult EditExpenseDetail(int? Id)
        {
            ExpenseManager objExpenseManager = new ExpenseManager();
            ViewBag.ExpenseType = objExpenseManager.GetAllExpenseTypeList().ToList();
            var expenseDetails = objExpenseManager.GetAllExpenseDetail().Where(x => x.Id == Id).FirstOrDefault();

            return View(expenseDetails);
        }

        [HttpPost]
        public ActionResult EditExpenseDetail(ExpenseDetail expenseDetail)
        {
            ExpenseManager objExpenseManager = new ExpenseManager();
            ViewBag.ExpenseType = objExpenseManager.GetAllExpenseTypeList().ToList();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objExpenseManager.UpdateExpenseDetail(expenseDetail))
                    {
                        ViewBag.Success = "Update expense Successfully";
                        ModelState.Clear();
                    }
                }
                catch (Exception exception)
                {
                    ViewBag.Error = exception.Message;
                    return View();
                }
            }
            return RedirectToAction("ExpenseDetail");
        }

        public ViewResult IncomeExpenseDetail(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.IncomeSortParm = String.IsNullOrEmpty(sortOrder) ? "income_desc" : "";
            ViewBag.ExpenseSortParm = String.IsNullOrEmpty(sortOrder) ? "expense_desc" : "Date";
            ViewBag.BalanceSortParm = String.IsNullOrEmpty(sortOrder) ? "balance_desc" : "";

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

            var incomeExpenseDetails = objExpenseManager.GetIncomeExpenseDetail().OrderByDescending(x => x.D_Date).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                incomeExpenseDetails = objExpenseManager.GetIncomeExpenseDetail().Where(x => x.D_Date == DateTime.Parse(searchString)).OrderByDescending(x => x.D_Date).ToList();
            }
            switch (sortOrder)
            {
                case "income_desc":
                    incomeExpenseDetails = incomeExpenseDetails.OrderByDescending(s => s.Earning).ToList();
                    break;
                case "expense_desc":
                    incomeExpenseDetails = incomeExpenseDetails.OrderByDescending(s => s.Expense).ToList();
                    break;
                case "balance_desc":
                    incomeExpenseDetails = incomeExpenseDetails.OrderByDescending(s => s.Balance).ToList();
                    break;
                default:
                    incomeExpenseDetails = incomeExpenseDetails.OrderByDescending(s => s.D_Date).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(incomeExpenseDetails.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult IncomeSummary()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IncomeSummary(string StartDate, string EndDate)
        {
            ExpenseManager objExpenseManager = new ExpenseManager();
            var incomeExpenseDetails = objExpenseManager.GetIncomeSummary(StartDate, EndDate);

            return View(incomeExpenseDetails);
        }
    }
}