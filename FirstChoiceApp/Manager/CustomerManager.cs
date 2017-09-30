using FirstChoiceApp.Gateway;
using FirstChoiceApp.Models;
using System;
using System.Collections.Generic;

namespace FirstChoiceApp.Manager
{
    public class CustomerManager
    {
        CustomerGateway objCustomerGateway = new CustomerGateway();

        internal List<Customer> GetAllCustomer()
        {
            return objCustomerGateway.GetAllCustomer();
        }

        internal bool CreateCustomer(Customer objCustomer)
        {
            if (objCustomerGateway.IsExist(objCustomer))
            {
                throw new Exception("Customer name already exist");
            }
            return objCustomerGateway.CreateCustomer(objCustomer) > 0;
        }

        internal bool UpdateCustomer(Customer objCustomer)
        {
            return objCustomerGateway.UpdateCustomer(objCustomer) > 0;
        }
    }
}