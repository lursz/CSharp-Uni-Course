using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4.assets
{
    public class Region
    {
        public String regionid { get; set; }
        public String regiondescription { get; set; }


        public static Region MapCsvToModel(string[] values)
        {
            return new Region
            {
                regionid = values[0],
                regiondescription = values[1]
            };
        }

        public override string ToString()
        {
            return regionid + " " + regiondescription;
        }
    }


}