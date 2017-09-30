using FirstChoiceApp.Manager;
using FirstChoiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace FirstChoiceApp.Controllers
{
    [Authorize]
    public class MeasurementController : Controller
    {
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 10;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            MeasurementManager objMeasurementManager = new MeasurementManager();

            var measurementList = objMeasurementManager.GetAllMeasurement().OrderBy(x => x.MeasurementName).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                measurementList = objMeasurementManager.GetAllMeasurement().Where(x => x.MeasurementName.ToLower().Contains(searchString.ToLower())).OrderBy(x => x.MeasurementName).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    measurementList = measurementList.OrderByDescending(s => s.MeasurementName).ToList();
                    break;
                default:  // Name ascending 
                    measurementList = measurementList.OrderBy(s => s.MeasurementName).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(measurementList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Measurement objMeasurement)
        {
            MeasurementManager objMeasurementManager = new MeasurementManager();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objMeasurementManager.CreateMeasurement(objMeasurement))
                    {
                        ViewBag.Success = "Measurement Created Successfully";
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
            MeasurementManager objMeasurementManager = new MeasurementManager();

            var measurement = objMeasurementManager.GetAllMeasurement().Where(x => x.Id == id).FirstOrDefault();

            return View(measurement);
        }

        [HttpPost]
        public ActionResult Edit(Measurement objMeasurement)
        {
            MeasurementManager objMeasurementManager = new MeasurementManager();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objMeasurementManager.UpdateMeasurement(objMeasurement))
                    {
                        ViewBag.Success = "Measurement Updated Successfully";
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
            MeasurementManager objMeasurementManager = new MeasurementManager();

            var brand = objMeasurementManager.GetAllMeasurement().Where(x => x.MeasurementName.StartsWith(Prefix.ToLower()) || x.MeasurementName.StartsWith(Prefix.ToUpper())).OrderBy(x => x.MeasurementName).Take(10);

            return Json(brand, JsonRequestBehavior.AllowGet);
        }
    }
}