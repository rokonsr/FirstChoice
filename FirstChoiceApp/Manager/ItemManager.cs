using FirstChoiceApp.Gateway;
using FirstChoiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceApp.Manager
{
    public class ItemManager
    {
        ItemGateway objItemGateway = new ItemGateway();

        internal List<Item> GetAllItem()
        {
            return objItemGateway.GetAllItem();
        }

        internal bool CreateItem(Item objItem)
        {
            if (objItemGateway.IsExist(objItem))
            {
                throw new Exception("Item already exist");
            }
            return objItemGateway.CreateItem(objItem) > 0;
        }

        public bool UpdateItemName(Item objItem)
        {
            return objItemGateway.UpdateItemName(objItem) > 0;
        }
    }
}