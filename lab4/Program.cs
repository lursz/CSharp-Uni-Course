using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System;
using lab4.assets;

/* -------------------------------------------------------------------------- */
/*                                   Reader                                   */
/* -------------------------------------------------------------------------- */
class Reader<T>
{
    public List<T> readList(String path, Func<String[], T> mapCsvToModel)
    {
        List<T> values = File.ReadAllLines(path).Skip(1).Select(v => mapCsvToModel(v.Split(','))).ToList();
        return values;
    }
}

/* -------------------------------------------------------------------------- */
/*                                    Main                                    */
/* -------------------------------------------------------------------------- */
class Program
{
    public static void Main(string[] args)
    {
        List<Region> regions = new Reader<Region>().readList("data/regions.csv", Region.MapCsvToModel);
        List<Territory> territories = new Reader<Territory>().readList("data/territories.csv", Territory.MapCsvToModel);
        List<EmployeeTerritory> employeesTerritories = new Reader<EmployeeTerritory>().readList("data/employee_territories.csv", EmployeeTerritory.MapCsvToModel);
        List<Employee> employees = new Reader<Employee>().readList("data/employees.csv", Employee.MapCsvToModel);

        List<OrderDetail> ordersDetails = new Reader<OrderDetail>().readList("data/orders_details.csv", OrderDetail.MapCsvToModel);
        List<Order> orders = new Reader<Order>().readList("data/orders.csv", Order.MapCsvToModel);

        System.Console.WriteLine("\n1. Last name of all employees");
        foreach (Employee employee in employees)
        {
            Console.WriteLine(employee.lastname);
        }


        System.Console.WriteLine("\n2. Last name + Region + Territory of all employees");
        var query2 = from e in employees
                     join et in employeesTerritories on e.employeeid equals et.employeeid
                     join t in territories on et.territoryid equals t.territoryid
                     join r in regions on t.regionid equals r.regionid
                     select new { S = e.lastname, R = r.regiondescription, T = t.territorydescription };
        foreach (var i in query2)
            Console.WriteLine(i.S + " - " + i.R + ", " + i.T);


        System.Console.WriteLine("\n3. Region + Surname of employees working in those regions");
        var query3 = from e in (from ee in employees
                                join et in employeesTerritories on ee.employeeid equals et.employeeid
                                join t in territories on et.territoryid equals t.territoryid
                                join r in regions on t.regionid equals r.regionid
                                select new { E = ee, R = r.regiondescription }).Distinct()
                     group e by e.R into g
                     select new { R = g.Key, S = g.Select(x => x.E.lastname).Distinct().ToList() };

        foreach (var i in query3)
            foreach (var j in i.S)
                Console.WriteLine(i.R + " " + j);

        // var query3 = from e in employees
        //               join et in employeesTerritories on e.employeeid equals et.employeeid
        //               join t in territories on et.territoryid equals t.territoryid
        //               join r in regions on t.regionid equals r.regionid
        //               orderby r.regiondescription
        //               select new { R = r.regiondescription, S = e.lastname };
        // foreach (var p in query3)
        //     Console.WriteLine(p.R + " " + p.S);

        System.Console.WriteLine("\n4. Region + Count of employees working in those regions");
        var query4 = from e in employees
                     join et in employeesTerritories on e.employeeid equals et.employeeid
                     join t in territories on et.territoryid equals t.territoryid
                     join r in regions on t.regionid equals r.regionid
                     group e by r into g
                     select new { R = g.Key, C = g.Distinct().Count() };
        foreach (var i in query4)
            Console.WriteLine(i.R.regiondescription + " " + i.C);

        System.Console.WriteLine("\n5. Count of orders + average value of order + max value of order for every employee");
        var query5 = from e in employees
                     join o in orders on e.employeeid equals o.employeeid
                     join od in ordersDetails on o.orderid equals od.orderid
                     group new {o,od} by e into g
                     select new { E = g.Key, C = g.Count(), A = g.Average(x => x.od.unitprice * x.od.quantity - x.od.discount), M = g.Max(x => x.od.unitprice * x.od.quantity - x.od.discount) };
        foreach (var i in query5)
            Console.WriteLine(i.E.lastname + ",     count: " + i.C + ",     avg value: " + i.A + ",     max value: " + i.M);


        // var query5 = from o in orders
        //              join od in ordersDetails on o.orderid equals od.orderid
        //              group od by o.employeeid into g
        //              select new { E = g.Key, C = g.Count(), A = g.Average(x => x.unitprice * x.quantity - x.discount), M = g.Max(x => x.unitprice * x.quantity - x.discount) };
        // foreach (var i in query5)
        //     Console.WriteLine(i.E + " " + i.C + " " + i.A + " " + i.M);


    }
}


