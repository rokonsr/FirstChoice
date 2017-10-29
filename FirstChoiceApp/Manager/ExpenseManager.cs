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
    }
}