using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETConsoleApp
{
    class SpecialOperators
    {
        static int? GetNullableInt()
        {
            return null;
        }

        static string GetStringValue()
        {
            return null;
        }

        public static void TestSpecials()
        {
            // ??
            int? x = 100;

            int y = x ?? -1; //@20180110-vincent: if x == null, set y to -1. x and -1 should be same type

            int i = GetNullableInt() ?? default(int); // when ?. can't be applied: Method call or return is not property

            // Null-conditional operators
            int?[] customers = null;

            int? length = customers?.Length; //@20180110-vincent: null if customer is null. If customers is not-null, set Length to length
                                             //Good when accessing properties. length maybe just skipped?? TODO:                  

            Console.WriteLine("?. and ?? are easy {0}, {1} and {2}", y, i, length);
        }
    }
}
