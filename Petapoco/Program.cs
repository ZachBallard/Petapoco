using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petapoco
{
    class Program
    {
        static void Main(string[] args)
        {
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
            string preface = "";
            QueryAndShowAllSales(query);

            query = @"select Max(pretaxamount) from sales";
            preface = "Highest Sales: ";
            QueryAndShowMinMax(query, preface);

            query = @"select Min(pretaxamount) from sales";
            preface = "Lowest Sales: ";
            QueryAndShowMinMax(query, preface);

            query = @"select distinct name from salespeople;";
            QueryAndShowDistinctSalespeople(query);

            Console.ReadLine();
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

        private static void QueryAndShowMinMax(string query, string preface)
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
                Console.WriteLine($"{a.salesman_id} {a.saledate} {a.pretaxamount}");
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
