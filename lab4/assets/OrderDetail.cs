using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4.assets
{
    public class OrderDetail
    {
        public String orderid { get; set; }
        public String productid { get; set; }
        public double unitprice { get; set; }
        public double quantity { get; set; }
        public double discount { get; set; }

        public static OrderDetail MapCsvToModel(string[] values)
        {
            return new OrderDetail
            {
                orderid = values[0],
                productid = values[1],
                unitprice = double.Parse(values[2]),
                quantity = double.Parse(values[3]),
                discount = double.Parse(values[4])
            };
        }

        public override string ToString()
        {
            return orderid + " " + productid + " " + unitprice + " " + quantity + " " + discount;
        }
    }
}