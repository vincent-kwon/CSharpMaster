using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Exception;

namespace NETConsoleApp
{
    class InvalidArgumentException : Exception
    {
        public InvalidArgumentException()
        {
        }

        public InvalidArgumentException(string message) : base(message)
        {
        }

        public object Argument
        {
            get;
            set;
        }

        public string Range
        {
            get;
            set;
        }
    }
    class Exceptions
    {
        public static void TestException()
        {
            try
            {
                throw new InvalidArgumentException
                {
                    Argument = "argument",
                    Range = "0~200"
                };
            }
            catch(InvalidArgumentException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Range + ", " + e.Argument);
            }
            catch(IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(FormatException e)
            {

            }
            catch(DivideByZeroException e)
            {

            }
            finally
            {
          
            }
            //catch(DividedByZeroException e)
        }
    }
}
