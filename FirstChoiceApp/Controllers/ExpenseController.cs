using FirstChoiceApp.Manager;
using FirstChoiceApp.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FirstChoiceApp.Controllers
{
    public class ExpenseController : Controller
    {
        public ActionResult Index()
        {
            ExpenseManager objExpenseManager = new ExpenseManager();

            var expenseTypeList = objExpenseManager.GetAllExpenseTypeList();
            return View(expenseTypeList);
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
            catch (Exception)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditExpenseType(int? id)
        {
            ExpenseManager objExpenseManager = new ExpenseManager();

            var expenseType = objExpenseManager.GetAllExpenseTypeList().Where(x=>x.Id == id).FirstOrDefault();
            return View(expenseType);
        }
    }
}