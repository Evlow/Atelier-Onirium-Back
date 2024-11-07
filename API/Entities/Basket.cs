using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Basket
    {
        public int Id {get;set;}

        public string BuyerId {get;set;}

        public List<BasketItems> Items {get;set;} = new();

        public void AddItem(Creation creation, int quantity)
        {
            if (Items.All(item =>item.CreationId != creation.Id))
            {
                Items.Add(new BasketItems{Creation = creation, Quantity = quantity});
            }

            var existingItem = Items.FirstOrDefault(item => item.CreationId == creation.Id);
            if (existingItem != null) existingItem.Quantity+=quantity;
        }

        public void RemoveItem (int creationId, int quantity)
        {
            var item =Items.FirstOrDefault(item => item.CreationId == creationId);
            if (item == null) return;
            item.Quantity-= quantity;
            if(item.Quantity==0) Items.Remove (item);
        }
    }
}