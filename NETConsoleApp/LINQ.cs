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
        }
    }
}
