using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NETConsoleApp
{
    class LINQ
    {
        class Profile
        {
            public string Name { get; set; }
            public int Height { get; set; }
        }

        class Product
        {
            public string Title { get; set; }
            public string Star { get; set; }
        }

        class Class
        {
            public string Name { get; set; }
            public int[] Score { get; set; }
        }

        public void TestLINQ()
        {
            // simple example
            int[] numbers = { 9, 2, 6, 4, 5, 3, 7, 8, 1, 10 };

            var result = from n in numbers //@20180111-vincent: result is IEnumerable<T>
                         where n % 2 == 0
                         orderby n //@20180111-vincent: ascending (default) or descending can be added
                         select n;

            Console.WriteLine("LINQ...");
            foreach (var i in result) Console.WriteLine("{0} ", i);

            // decorate select 
            Profile[] arrProfile =
            {
                new Profile() { Name = "Jordan", Height = 198 },
                new Profile() { Name = "Lebrone", Height = 203 },
                new Profile() { Name = "Magic", Height = 206 },
                new Profile() { Name = "Bad", Height = 206 },
                new Profile() { Name = "Kobe", Height = 203 },
            };

            var profiles = from profile in arrProfile
                           where profile.Height <= 203
                           orderby profile.Height
                           select new
                           {
                               Name = profile.Name,
                               InchHeight = profile.Height * 0.393
                           };
            foreach (var profile in profiles)
            {
                Console.WriteLine("{0} {1}", profile.Name, profile.InchHeight);
            }

            // double from
            Class[] arrClass =
            {
                new Class() { Name = "ABC", Score = new int[] {99, 80, 7, 24} },
                new Class() { Name = "DEF", Score = new int[] {69, 30, 3, 44} },
                new Class() { Name = "GHI", Score = new int[] {92, 88, 0, 17} },
            };

            var classes = from c in arrClass
                          from s in c.Score //@20180111-vincent: return scores of c 
                          where s < 60
                          orderby s
                          select new { c.Name, Lowest = s };
            foreach (var c in classes)
            {
                Console.WriteLine("{0} {1}", c.Name, c.Lowest);
            }

            // group by
            Profile[] groupProfile =
            {
                new Profile() { Name = "Jordan", Height = 198 },
                new Profile() { Name = "Lebrone", Height = 203 },
                new Profile() { Name = "Magic", Height = 206 },
                new Profile() { Name = "Bad", Height = 206 },
                new Profile() { Name = "Kobe", Height = 203 },
            };

            var listProfile = from profile in groupProfile
                              orderby profile.Name
                              group profile by profile.Height > 200 into g //@20180113-vincent: group .. by ... into g
                              select new { GroupKey = g.Key, Profiles = g };

            foreach (var Group in listProfile)
            {
                Console.WriteLine("less than 200: {0}", Group.GroupKey);
                foreach (var profile in Group.Profiles)
                {
                    Console.WriteLine(" {0}, {1}", profile.Name, profile.Height);
                }
            }

            // inner join
            Profile[] bballProfile =
            {
                new Profile() { Name = "Jordan", Height = 198 },
                new Profile() { Name = "Lebrone", Height = 203 },
                new Profile() { Name = "Magic", Height = 206 },
                new Profile() { Name = "Bad", Height = 206 },
                new Profile() { Name = "Kobe", Height = 203 },
            };

            Product[] bballProduct =
            {
                new Product() { Star = "Jordan", Title = "Chicago" },
                new Product() { Star = "Lebrone", Title = "Cleveland" },
                new Product() { Star = "Lebrone", Title = "Caverials" },
                new Product() { Star = "Magic", Title = "LA" },
                new Product() { Star = "Bad", Title = "Boston" },
            };

            var innerlistProfile =
                from profile in bballProfile
                join product in bballProduct on profile.Name equals product.Star
                select new
                {
                    Name = profile.Name,
                    Work = product.Title,
                    Height = profile.Height
                };
            Console.WriteLine(" -- inner join --");
            foreach (var profile in innerlistProfile)
            {
                Console.WriteLine("name: {0}, work: {1}, height:{2}", profile.Name, profile.Work, profile.Height);
            }

            var outerlistProfile =
                from profile in bballProfile
                join product in bballProduct on profile.Name equals product.Star into ps
                from product in ps.DefaultIfEmpty(new Product { Title = "No team" })
                select new
                {
                    Name = profile.Name,
                    Work = product.Title,
                    Height = profile.Height
                };
            Console.WriteLine(" -- Outer join --");
            foreach (var profile in outerlistProfile)
            {
                Console.WriteLine("name: {0}, work: {1}, height:{2}", profile.Name, profile.Work, profile.Height);
            }
        }
    }
}
