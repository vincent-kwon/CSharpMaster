using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETConsoleApp
{
    interface IRunnable
    {
        void Run();
        int Gun
        {
            get;
            set;
        } //@20180108-vincent: no ; (different)
    }

    interface IFlyable
    {
        void Fly();
    }

    class FlyingCar : IRunnable, IFlyable
    {
        public void Run() //@20180108-vincent: should public in this class for interface impl
        {

        }

        public void Fly()
        {

        }

        public int Gun
        {
            get; set;
        }
    }

    abstract class AbstractBase
    {
        protected abstract void ProtectedMethod(); //@20180108-vincent: abstract, override
        public string SerialID
        {
            get; set;
        }
        abstract public DateTime ProductDate
        {
            get;
            set;
        }
    }

    class Derived : AbstractBase
    {
        protected override void ProtectedMethod() //@20180108-vincent: override 에서 제한자를 수정할 수 없음
        {

        }
        public override DateTime ProductDate
        {
            get;
            set;
        }
    }

    class ArmorSuite
    {
        public virtual void Initialize() //@20180108-vincent: virtual, override
        {
            Console.WriteLine("Armored");
        }
    }

    class Ironman : ArmorSuite
    {
        public new void Initialize() //@20180108-vincent: hiding
        {
            //base.Initialize();
            Console.WriteLine("Ironman");
        }
    }

    class WarMachine : ArmorSuite
    {
        public override void Initialize()
        {
            base.Initialize();
            Console.WriteLine("Warmachine");
        }
    }

    class Ironman2 : ArmorSuite
    {
        public sealed override void Initialize() //@20180108-vincent: sealed override
        {
            Console.WriteLine("Ironman2");
        }

        private int i; //@20180108-vincent: private field

        public int MyProperty
        {
            get;
            set;
        }

        public int MyProperty2
        {
            get //@20180108-vincent: if only get is present, read-only property
            {
                return 0;
            }
            set
            {
                i = value;
            }
        }

        class NestedClass
        {
            public NestedClass()
            {
                Ironman2 tony2 = new Ironman2();
                Ironman2 tony = new Ironman2
                {
                    MyProperty = 1, //@20180108-vincent: watch out this property
                    MyProperty2 = 2
                }; //@20180108-vincent: watch out ; after property constructor

                tony2.i = 1000; //@20180108-vincent: parent's private can be accessed

                var myInstance = new { Name = "Tony", Age = "12" }; //@20180108-vincent: anonymous type
            }
        }
    }
}
