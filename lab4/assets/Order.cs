using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4.assets
{
    public class Order
    {
        public String orderid { get; set; }
        public String customerid { get; set; }
        public String employeeid { get; set; }
        public String orderdate { get; set; }
        public String requireddate { get; set; }
        public String shippeddate { get; set; }
        public String shipvia { get; set; }
        public String freight { get; set; }
        public String shipname { get; set; }
        public String shipaddress { get; set; }
        public String shipcity { get; set; }
        public String shipregion { get; set; }
        public String shippostalcode { get; set; }
        public String shipcountry { get; set; }

        public static Order MapCsvToModel(string[] values)
        {
            return new Order
            {
                orderid = values[0],
                customerid = values[1],
                employeeid = values[2],
                orderdate = values[3],
                requireddate = values[4],
                shippeddate = values[5],
                shipvia = values[6],
                freight = values[7],
                shipname = values[8],
                shipaddress = values[9],
                shipcity = values[10],
                shipregion = values[11],
                shippostalcode = values[12],
                shipcountry = values[13]
            };
        }

        public override string ToString()
        {
            return orderid + " " + customerid + " " + employeeid + " " + orderdate + " " + requireddate + " " + shippeddate + " " + shipvia + " " + freight + " " + shipname + " " + shipaddress + " " + shipcity + " " + shipregion + " " + shippostalcode + " " + shipcountry;
        }
        

    }
}