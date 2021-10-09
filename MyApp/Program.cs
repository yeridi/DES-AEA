using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();
        static void Main(string[] args)
        {
            JoiningLambda();
            Console.Read();
        }
        static void IntroToLinkQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;

            foreach (int num in numQuery)
            {
                Console.Write("{0,1}", num);
            }
        }
        static void DataSource()
        {
            var queryAllCustumers = from cust in context.clientes
                                    select cust;
            foreach (var item in queryAllCustumers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void Filtering()
        {
            var queryLondonCustumers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;
            foreach (var item in queryLondonCustumers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }
        static void Ordering()
        {
            var queryLondonCustumers =
                from cust in context.clientes
                where cust.Ciudad == "London"
                orderby cust.NombreCompañia ascending
                select cust;

            foreach (var item in queryLondonCustumers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void Grouping()
        {
            var queryCustumerByCitty =
                from cust in context.clientes
                group cust by cust.Ciudad;

            foreach (var customGroup in queryCustumerByCitty)
            {
                Console.WriteLine(customGroup.Key);
                foreach (clientes customer in customGroup)
                {
                    Console.WriteLine("    {0}", customer.NombreCompañia);
                }
            }
        }
        static void Grouping2()
        {
            var custQuery =
                from cust in context.clientes
                group cust by cust.Ciudad into custGroup
                where custGroup.Count() > 2
                orderby custGroup.Key
                select custGroup;
            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }
        static void Joining()
        {
            var innerJoinQuery =
                from cust in context.clientes
                join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };
            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }

        static void IntroToLinkLambda()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var newNumbers = numbers.Where(x => x % 2 == 0).ToList() ;
            Console.WriteLine(string.Join(" ", newNumbers));
        }
        static void DataSourceLambda()
        {
            var Data = context.clientes.Select(x=>x.NombreCompañia).ToList();
            foreach (var item in Data) {
                Console.WriteLine(item);
            }
           
        }
        static void FilteringLambda()
        {
            var c = context.clientes;
            var Data = c.Where(x => x.Ciudad =="Londres").Select(s => s.Ciudad).ToList();
            foreach (var item in Data)
            {
                Console.WriteLine(item);
            }
            
        }
        static void OrderingLambda()
        {
            var c = context.clientes;
            var Data = c.Where(x => x.Ciudad == "Londres").OrderBy(z => z.NombreCompañia).Select(s=>s.NombreCompañia).ToList();
            foreach (var item in Data)
            {
                Console.WriteLine(item);
            }
        }
        static void GroupingLamdba()
        {
            var Data = context.clientes.GroupBy(x => x.Ciudad).Where(z => z.Count() > 2).OrderBy(r => r.Key).Select(p => p.Key).ToList();
            foreach (var item in Data)
            {
                Console.WriteLine(item);
            }
        }
        static void JoiningLambda()
        {
            var c = context.Pedidos;
            var w = context.clientes;
            var Data = context.clientes.Join(w,wKey=>wKey.idCliente, wTypes => wTypes.idCliente,(cn,cType)=>new { Name=cn.NombreCompañia});
            foreach (var item in Data)
            {
                Console.WriteLine(item);
            }
        }
    }
    //https://www.c-sharpcorner.com/blogs/get-data-from-database-using-lambda-expression-in-sql-using-not-invalueor-list-of-value1  .OrderBy(z => z.NombreCompañia)
}
