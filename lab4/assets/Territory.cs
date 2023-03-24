using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4.assets
{
    public class Territory
    {
        public String territoryid { get; set; }
        public String territorydescription { get; set; }
        public String regionid { get; set; }

        public static Territory MapCsvToModel(string[] values)
        {
            return new Territory
            {
                territoryid = values[0],
                territorydescription = values[1],
                regionid = values[2]
            };
        }

        public override string ToString()
        {
            return territoryid + " " + territorydescription + " " + regionid;
        }
    }
}