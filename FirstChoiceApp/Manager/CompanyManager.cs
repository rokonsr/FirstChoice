using System;
using FirstChoiceApp.Gateway;
using FirstChoiceApp.Models;

namespace FirstChoiceApp.Manager
{
    public class CompanyManager
    {
        CompanyGateway objCompanyGateway = new CompanyGateway();

        internal bool CreateCompany(CompanyInfo objCompanyInfo)
        {
            if (objCompanyGateway.IsExist(objCompanyInfo))
            {
                throw new Exception("You can not create more than 1 company");
            }
            return objCompanyGateway.CreateCompany(objCompanyInfo) > 0;
        }

        public CompanyInfo CompanyDetail()
        {
            return objCompanyGateway.CompanyDetail();
        }

        internal CompanyInfo GetCompanyInfoById(int? id)
        {
            throw new NotImplementedException();
        }

        internal bool UpdateCompanyInfo(CompanyInfo objCompanyInfo)
        {
            return objCompanyGateway.UpdateCompanyInfo(objCompanyInfo) > 0;
        }
    }
}