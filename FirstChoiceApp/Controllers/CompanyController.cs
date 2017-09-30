using System;
using System.Web;
using System.Web.Mvc;
using FirstChoiceApp.Manager;
using FirstChoiceApp.Models;

namespace FirstChoiceApp.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        public ActionResult Index()
        {
            CompanyManager objCompanyManager = new CompanyManager();

            var companyDetails = objCompanyManager.CompanyDetail();

            return View(companyDetails);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompanyInfo objCompanyInfo, HttpPostedFileBase logo)
        {
            bool status = false;

            if (logo != null)
            {
                byte[] bytes;
                int BytestoRead;
                int numBytesRead;

                bytes = new byte[logo.ContentLength];
                BytestoRead = (int)logo.ContentLength;
                numBytesRead = 0;
                while (BytestoRead > 0)
                {
                    int n = logo.InputStream.Read(bytes, numBytesRead, BytestoRead);
                    if (n == 0) break;
                    numBytesRead += n;
                    BytestoRead -= n;
                }
                objCompanyInfo.CompanyLogo = bytes;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    CompanyManager objCompanyManager = new CompanyManager();
                    if (objCompanyManager.CreateCompany(objCompanyInfo))
                    {
                        ViewBag.Success = "Company created successfully";

                        status = true;

                        ModelState.Clear();
                    }
                }
                catch (Exception exception)
                {
                    ViewBag.Error = exception.Message;
                }
            }
            if (status)
            {
                return RedirectToAction("Index");
            }
            return View(objCompanyInfo);
        }

        public ActionResult Edit(int? id)
        {
            CompanyManager objCompanyManager = new CompanyManager();

            var companyDetails = objCompanyManager.CompanyDetail();

            return View(companyDetails);
        }

        [HttpPost]
        public ActionResult Edit(CompanyInfo objCompanyInfo, HttpPostedFileBase logo)
        {
            bool status = false;

            if (logo != null)
            {
                byte[] bytes;
                int BytestoRead;
                int numBytesRead;

                bytes = new byte[logo.ContentLength];
                BytestoRead = (int)logo.ContentLength;
                numBytesRead = 0;
                while (BytestoRead > 0)
                {
                    int n = logo.InputStream.Read(bytes, numBytesRead, BytestoRead);
                    if (n == 0) break;
                    numBytesRead += n;
                    BytestoRead -= n;
                }
                objCompanyInfo.CompanyLogo = bytes;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    CompanyManager objCompanyManager = new CompanyManager();
                    if (objCompanyManager.UpdateCompanyInfo(objCompanyInfo))
                    {
                        ViewBag.Success = "Company updated successfully";

                        status = true;

                        ModelState.Clear();
                    }
                }
                catch (Exception exception)
                {
                    ViewBag.Error = exception.Message;
                }
            }
            if (status)
            {
                return RedirectToAction("Index");
            }
            return View(objCompanyInfo);
        }
	}
}