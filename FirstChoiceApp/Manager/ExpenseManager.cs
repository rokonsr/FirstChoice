using System;
using FirstChoiceApp.Models;
using FirstChoiceApp.Gateway;
using System.Collections.Generic;
using System.Linq;

namespace FirstChoiceApp.Manager
{
    public class ExpenseManager
    {
        ExpenseGateway objExpenseGateway = new ExpenseGateway();

        internal bool CreateExpenseType(ExpenseType expenseType)
        {
            if (objExpenseGateway.IsExist(expenseType))
            {
                throw new Exception("Expense type already exist");
            }
            return objExpenseGateway.CreateExpenseType(expenseType) > 0;
        }

        internal List<ExpenseType> GetAllExpenseTypeList()
        {
            return objExpenseGateway.GetAllExpenseType();
        }

        internal List<ExpenseType> GetAllExpenseTypeList(int? id)
        {
            return objExpenseGateway.GetAllExpenseType();
        }

        internal bool UpdateExpenseType(ExpenseType expenseType)
        {
            return objExpenseGateway.UpdateExpenseType(expenseType) > 0;
        }

        internal bool CreateExpenseDetail(ExpenseDetail expenseDetail)
        {
            return objExpenseGateway.CreateExpenseDetail(expenseDetail) > 0;
        }

        internal List<ExpenseDetail> GetAllExpenseDetail()
        {
            return objExpenseGateway.GetAllExpenseDetail();
        }

        internal bool UpdateExpenseDetail(ExpenseDetail expenseDetail)
        {
            return objExpenseGateway.UpdateExpenseDetail(expenseDetail) > 0;
        }
    }
}