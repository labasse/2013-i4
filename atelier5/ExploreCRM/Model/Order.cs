using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ExploreCRM.Model
{
    public partial class Order
    {
        public decimal Amount
        {
            get 
            {
                return (this.Freight.HasValue ? this.Freight.Value:0m)
                    + this.Order_Details.Sum(detail => detail.Unit_Price * detail.Quantity);
            }
        }
    }
}
