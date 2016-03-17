using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petapoco
{
    class Program
    {
        static void Main(string[] args)
        {
            TruncateSalesAndSalesPerson();

            InsertIntoSalesPeopleSql("Deacon");
            InsertIntoSalesPeopleSql("Edan");
            InsertIntoSalesPeopleSql("Yardley");
            InsertIntoSalesPeopleSql("Farrah");
            InsertIntoSalesPeopleSql("Jessamine");
            InsertIntoSalesPeopleSql("Isabelle");
            InsertIntoSalesPeopleSql("Judah");

            InsertIntoSalesPeopleSql(1, new DateTime(2015, 10, 15), 2846);
            InsertIntoSalesPeopleSql(1, new DateTime(2015, 09, 21), 8852);
            InsertIntoSalesPeopleSql(2, new DateTime(2015, 12, 07), 5255);
            InsertIntoSalesPeopleSql(2, new DateTime(2015, 02, 27), 5259);
            InsertIntoSalesPeopleSql(3, new DateTime(2015, 11, 23), 7244);
            InsertIntoSalesPeopleSql(4, new DateTime(2015, 12, 21), 8711);
            InsertIntoSalesPeopleSql(1, new DateTime(2015, 10, 02), 740);
            InsertIntoSalesPeopleSql(5, new DateTime(2015, 04, 02), 9970);
            InsertIntoSalesPeopleSql(6, new DateTime(2015, 12, 25), 6009);
            InsertIntoSalesPeopleSql(7, new DateTime(2015, 07, 03), 9703);

            string query = @"select * from sales;";
            string query2 = @"";
            string preface = "";
            QueryAndShowAllSales(query);

            query = @"select Max(pretaxamount) from sales";
            preface = "Highest Sales: ";
            QueryAndShowIntValue(query, preface);

            query = @"select Min(pretaxamount) from sales";
            preface = "Lowest Sales: ";
            QueryAndShowIntValue(query, preface);

            query = @"select distinct name from salespeople;";
            QueryAndShowDistinctSalespeople(query);

            query = @"select abs(datediff(DAY, Min(saledate),Max(saledate))) from sales";
            preface = "Date Difference (days): ";
            QueryAndShowIntValue(query, preface);

            query = @"select Year(saledate) as 'Year', Month(saledate) as 'Month', sum(pretaxamount) as 'SalesPerMonth' from sales Group By Year(saledate), Month(saledate);";
            QueryAndShowAllSalesByMonth(query);

            query = @"select salesman_id, sum(pretaxamount) as 'TotalSales' from sales group by salesman_id;";
            QueryAndShowNamesAndSales(query);

            Console.ReadLine();

            TruncateSalesAndSalesPerson();
        }

        private static void TruncateSalesAndSalesPerson()
        {
            var db = new PetaPoco.Database("dbstring");

            db.Delete<Sales>(@"truncate table sales;");
            db.Delete<Salespeople>(@"truncate table salespeople;");
        }

        private static void QueryAndShowNamesAndSales(string query)
        {
            var db = new PetaPoco.Database("dbstring");

            Console.WriteLine();
            foreach (var a in db.Fetch<SalesTotal>(query))
            {
                Console.WriteLine($"Salesman_ID: {a.salesman_id} Total Sales: {a.TotalSales}");
            }

        }

        private static void QueryAndShowAllSalesByMonth(string query)
        {
            var db = new PetaPoco.Database("dbstring");

            Console.WriteLine();
            foreach (var a in db.Query<SalesMade>(query))
            {
                Console.WriteLine($"Year: {a.Year} Month: {a.Month} Sales/Month: {a.SalesPerMonth}");
            }
        }

        private static void QueryAndShowDistinctSalespeople(string query)
        {
            var db = new PetaPoco.Database("dbstring");

            Console.WriteLine();
            Console.WriteLine("All Distinct Names");
            foreach (var a in db.Query<Salespeople>(query))
            {
                Console.WriteLine(a.name);
            }
        }

        private static void QueryAndShowIntValue(string query, string preface)
        {
            var db = new PetaPoco.Database("dbstring");
            Console.WriteLine();
            var queryResponse = db.Single<int>(query);

            Console.WriteLine($"{preface} {queryResponse}");
        }

        private static void QueryAndShowAllSales(string query)
        {
            var db = new PetaPoco.Database("dbstring");

            Console.WriteLine();
            foreach (var a in db.Query<Sales>(query))
            {
                Console.WriteLine($"Salesman_ID: {a.salesman_id} Sale Date: {a.saledate} Sales Amount: {a.pretaxamount}");
            }
        }

        private static void InsertIntoSalesPeopleSql(int id, DateTime dateTime, int amount)
        {
            var db = new PetaPoco.Database("dbstring");
            var sale = new Sales();

            sale.salesman_id = id;
            sale.saledate = dateTime;
            sale.pretaxamount = amount;

            db.Insert("sales", "salesman_id", sale);
        }

        public static void InsertIntoSalesPeopleSql(string name)
        {
            var db = new PetaPoco.Database("dbstring");
            var salesperson = new Salespeople();

            salesperson.name = name;

            db.Insert("salespeople", "salesman_id", salesperson);
        }
    }
}
