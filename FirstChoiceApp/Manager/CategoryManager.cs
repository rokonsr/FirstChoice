using FirstChoiceApp.Gateway;
using FirstChoiceApp.Models;
using System;
using System.Collections.Generic;

namespace FirstChoiceApp.Manager
{
    public class CategoryManager
    {
        CategoryGateway objCategoryGateway = new CategoryGateway();

        internal List<Category> GetAllCategory()
        {
            return objCategoryGateway.GetAllCategory();
        }

        internal bool CreateCategory(Category objCategory)
        {
            if (objCategoryGateway.IsExist(objCategory))
            {
                throw new Exception("Category already exist");
            }
            return objCategoryGateway.CreateCategory(objCategory) > 0;
        }

        internal bool UpdateCategory(Category objCategory)
        {
            return objCategoryGateway.UpdateCategory(objCategory) > 0;
        }
    }
}