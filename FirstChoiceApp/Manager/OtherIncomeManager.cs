using FirstChoiceApp.Gateway;
using FirstChoiceApp.Models;
using System;
using System.Collections.Generic;

namespace FirstChoiceApp.Manager
{
    public class OtherIncomeManager
    {
        OtherIncomeGateway objOtherIncomeGateway = new OtherIncomeGateway();

        internal List<IncomeType> GetIncomeTypeList()
        {
            return objOtherIncomeGateway.GetIncomeTypeList();
        }

        internal List<OtherIncomeDetail> GetAllOtherIncome()
        {
            return objOtherIncomeGateway.GetAllOtherIncome();
        }

        internal bool CreateOtherIncome(OtherIncomeDetail otherIncomeDetail)
        {
            if (!objOtherIncomeGateway.IsExist(otherIncomeDetail))
            {
                throw new Exception("Please select another date");
            }
            return objOtherIncomeGateway.CreateOtherIncome(otherIncomeDetail) > 0;
        }

        internal bool UpdateOtherIncome(OtherIncomeDetail otherIncomeDetail)
        {
            return objOtherIncomeGateway.UpdateOtherIncome(otherIncomeDetail) > 0;
        }
    }
}