using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4.assets
{
    public class Employee
    {
        public String employeeid { get; set; }
        public String lastname { get; set; }
        public String firstname { get; set; }
        public String title { get; set; }
        public String titleofcourtesy { get; set; }
        public String birthdate { get; set; }
        public String hiredate { get; set; }
        public String address { get; set; }
        public String city { get; set; }
        public String region { get; set; }
        public String postalcode { get; set; }
        public String country { get; set; }
        public String homephone { get; set; }
        public String extension { get; set; }
        public String photo { get; set; }
        public String notes { get; set; }
        public String reportsTo { get; set; }
        public String photoPath { get; set; }

        public static Employee MapCsvToModel(string[] values)
        {
            return new Employee
            {
                employeeid = values[0],
                lastname = values[1],
                firstname = values[2],
                title = values[3],
                titleofcourtesy = values[4],
                birthdate = values[5],
                hiredate = values[6],
                address = values[7],
                city = values[8],
                region = values[9],
                postalcode = values[10],
                country = values[11],
                homephone = values[12],
                extension = values[13],
                photo = values[14],
                notes = values[15],
                reportsTo = values[16],
                photoPath = values[17]
            };
        }

        public override string ToString()
        {
            return employeeid + " " + lastname + " " + firstname + " " + title + " " + titleofcourtesy + " " + birthdate + " " + hiredate + " " + address + " " + city + " " + region + " " + postalcode + " " + country + " " + homephone + " " + extension + " " + photo + " " + notes + " " + reportsTo + " " + photoPath;
        }
    }
}