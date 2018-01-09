using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NETConsoleApp
{
    class FunctionAction
    {
        public void TestFunction()
        {
            //@20180109-vincent: Func has return value which is always last one while Action has no return;
            Func<int> func1 = () => 10;
            Func<int, string> func2 = (i) => i.ToString() ; // how to define it?
            Console.WriteLine("Test Func: " + func1() + "," + func2(1000));
        }

        public void TestAction()
        {
            Action act1 = () => Console.WriteLine("Action1~~~~~");
            act1();
            Action<int> act2 = (int x) => Console.WriteLine("Action2~~~~~ " + x);
            act2(9999);
        }

        public void TestExpression()
        {
            Expression const1 = Expression.Constant(1);
            Expression const2 = Expression.Constant(2);
            Expression leftExp = Expression.Multiply(const1, const2);

            Expression param1 = Expression.Parameter(typeof(int));
            Expression param2 = Expression.Parameter(typeof(int));

            Expression rightExp = Expression.Subtract(param1, param2);

            Expression exp = Expression.Add(leftExp, rightExp);

            Expression<Func<int, int, int>> expression =
                Expression<Func<int, int, int>>.Lambda<Func<int, int, int>>(
                        exp, new ParameterExpression[] 
                        {
                            (ParameterExpression)param1,
                            (ParameterExpression)param2
                        }
                    );
            Func<int, int, int> func = expression.Compile();

            Console.WriteLine("1*2+(7-8) should be one = " + func(7, 8));
        }
    }
}
