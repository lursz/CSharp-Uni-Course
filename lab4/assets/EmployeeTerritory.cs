using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4.assets
{
    public class EmployeeTerritory
    {
        public String employeeid { get; set; }
        public String territoryid { get; set; }

        public static EmployeeTerritory MapCsvToModel(string[] values)
        {
            return new EmployeeTerritory
            {
                employeeid = values[0],
                territoryid = values[1]
            };
        }

        public override string ToString()
        {
            return employeeid + " " + territoryid;
        }
    }
}