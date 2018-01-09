using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETConsoleApp
{
    class Lambda
    {
        delegate int Calculate(int a, int b);
        delegate int NoArgu();
        public void TestLambda()
        {
            Calculate c = delegate (int a, int b) //@20180109-vincent: delegate can't be lambda style
            {
                return a + b;
            };

            var x = c(10, 20);

            Calculate c1 = (int a, int b) => a + b;
            var y = c1(10, 20);

            Console.WriteLine("Lambda " + x + ", " + y);

            NoArgu na = () => 100+200 ; //@20180109-vincent: no argument style
        }
    }
}
