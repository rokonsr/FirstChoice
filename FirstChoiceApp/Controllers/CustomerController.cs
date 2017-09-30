using FirstChoiceApp.Manager;
using FirstChoiceApp.Models;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FirstChoiceApp.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ContactSortParm = String.IsNullOrEmpty(sortOrder) ? "contact_desc" : "";
            ViewBag.EmailSortParm = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";
            ViewBag.AddressSortParm = String.IsNullOrEmpty(sortOrder) ? "address_desc" : "";
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

            CustomerManager objCustomerManager = new CustomerManager();

            var customerList = objCustomerManager.GetAllCustomer().OrderBy(x => x.CustomerName).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                customerList = objCustomerManager.GetAllCustomer().Where(x => x.CustomerName.ToLower().Contains(searchString.ToLower())).OrderBy(x => x.CustomerName).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    customerList = customerList.OrderByDescending(s => s.CustomerName).ToList();
                    break;
                case "contact_desc":
                    customerList = customerList.OrderByDescending(s => s.ContactNo).ToList();
                    break;
                case "email_desc":
                    customerList = customerList.OrderByDescending(s => s.Email).ToList();
                    break;
                case "address_desc":
                    customerList = customerList.OrderByDescending(s => s.Address).ToList();
                    break;
                default:  // Name ascending 
                    customerList = customerList.OrderBy(s => s.CustomerName).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(customerList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer objCustomer)
        {
            CustomerManager objCustomerManager = new CustomerManager();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objCustomerManager.CreateCustomer(objCustomer))
                    {
                        ViewBag.Success = "Customer created successfully";
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
            CustomerManager objCustomerManager = new CustomerManager();

            var customer = objCustomerManager.GetAllCustomer().Where(x => x.Id == id).FirstOrDefault();

            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(Customer objCustomer)
        {
            CustomerManager objCustomerManager = new CustomerManager();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objCustomerManager.UpdateCustomer(objCustomer))
                    {
                        ViewBag.Success = "Customer Updated successfully";
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
            CustomerManager objCustomerManager = new CustomerManager();

            var customers = objCustomerManager.GetAllCustomer().Where(x => x.CustomerName.StartsWith(Prefix.ToLower()) || x.CustomerName.StartsWith(Prefix.ToUpper())).OrderBy(x => x.CustomerName).Take(10);

            return Json(customers, JsonRequestBehavior.AllowGet);
        }
    }
}