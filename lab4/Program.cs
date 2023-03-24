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
        List<EmployeeTerritory> employeeTerritories = new Reader<EmployeeTerritory>().readList("data/employee_territories.csv", EmployeeTerritory.MapCsvToModel);
        List<Employee> employees = new Reader<Employee>().readList("data/employees.csv", Employee.MapCsvToModel);

        List<OrderDetail> orderDetails = new Reader<OrderDetail>().readList("data/order_details.csv", OrderDetail.MapCsvToModel);
        List<Order> orders = new Reader<Order>().readList("data/orders.csv", Order.MapCsvToModel);

        System.Console.WriteLine("\nSurname of all employees");
        foreach (Employee employee in employees)
        {
            Console.WriteLine(employee.lastname);
        }

        
        System.Console.WriteLine("\nSurname + Region + Territory of all employees");
        var query2 = from e in employees
                      join et in employeeTerritories on e.employeeid equals et.employeeid
                      join t in territories on et.territoryid equals t.territoryid
                      join r in regions on t.regionid equals r.regionid
                      select new { S = e.lastname, R = r.regiondescription, T = t.territorydescription };
        foreach (var p in query2.ToList())
            Console.WriteLine(p.S + " " + p.R + " " + p.T);
            

        System.Console.WriteLine("\nRegion + Surname of employees working in those regions");
        var query3 = from e in employees
                      join et in employeeTerritories on e.employeeid equals et.employeeid
                      join t in territories on et.territoryid equals t.territoryid
                      join r in regions on t.regionid equals r.regionid
                      orderby r.regiondescription
                      select new { R = r.regiondescription, S = e.lastname };
        foreach (var p in query3.ToList())
            Console.WriteLine(p.R + " " + p.S);

        System.Console.WriteLine("\nRegion + Count of employees working in those regions");
        var query4 = from e in employees
                      join et in employeeTerritories on e.employeeid equals et.employeeid
                      join t in territories on et.territoryid equals t.territoryid
                      join r in regions on t.regionid equals r.regionid
                      group e by r.regiondescription into g
                      select new { R = g.Key, C = g.Count() };
        foreach (var p in query4.ToList())
            Console.WriteLine(p.R + " " + p.C);
        
        System.Console.WriteLine("\nCount of orders + average value of order + max value of order for every employee");
        var query5 = from o in orders
                      join od in orderDetails on o.orderid equals od.orderid
                      group od by o.employeeid into g
                      select new { E = g.Key, C = g.Count(), A = g.Average(x => x.unitprice * x.quantity), M = g.Max(x => x.unitprice * x.quantity) };
        foreach (var p in query5.ToList())
            Console.WriteLine(p.E + " " + p.C + " " + p.A + " " + p.M);


    }
}


