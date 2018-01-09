using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace NETConsoleApp
{
    //@20180109-vincent: AllowMultiple is used to allow multiple attributes
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method, 
        AllowMultiple = true)]
    class History : System.Attribute
    {
        public string Programmer
        {
            get;
            set;
        }

        public double Version
        {
            get;
            set;
        }

        public string Changes
        {
            get;
            set;
        }

        public History(string programmer)
        {
            Programmer = programmer;
        }
    }
    //@20180109-vincent: Attribute name of param can be applied when constructor requirement met.
    [History("Bob", Version = 1.2, Changes = "2013-01-01 Modified class")]
    [History("Sean", Version = 1.1, Changes = "2012-11-01 Created class")]
    class ReflectionAttribute
    {
        public void TestReflection()
        {
            int a = 0;
            Type type = a.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo field in fields) Console.WriteLine("Type {0}, Name: {1}",
                field.FieldType.Name, field.Name);
            MethodInfo[] methods = type.GetMethods();
            foreach (MethodInfo method in methods)
            {
                Console.WriteLine("Type:{0}, Name:{1}", method.ReturnType.Name, method.Name);
            }
        }
        [Obsolete("this method is old do not use it.....")]
        public void TestAttribute()
        {
            //@20180109-vincent: 
            Console.WriteLine("TestAttribute");
            ReflectionAttribute.WriteLine("Fun programming !!!");
        }

        public static void WriteLine(string message,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            Console.WriteLine("{0}(Line:{1}) {2}: {3}", file, line, member, message);
        }
    }
}
