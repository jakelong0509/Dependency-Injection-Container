using Autofac;
using System;
using System.Reflection;
namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            
            builder.RegisterType(typeof(TestManager));
            builder.RegisterType<test>().As<ITest>();

            builder.Register(c => new TestForReal(c.Resolve<ITest>()));
            builder.Register(c => new TestForRealver2(c.Resolve<ITest>()));
            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var component1 = scope.Resolve<TestManager>();
                component1.PrintSomething();
                var component2 = scope.Resolve<TestForReal>();
                component2.PrintHello();
                var component3 = scope.Resolve<TestForRealver2>();
                component3.PrintHello();
            }

        }

        
    }

    interface ITest
    {
        void PrintHello();

    }

    class test : ITest
    {
        public void PrintHello()
        {
            Type t = this.GetType();
            Console.WriteLine("Hello From " + t.ToString());
        }
    }

    class TestManager
    {
        ITest _test;
        public TestManager(ITest test)
        {
            _test = test;
        }

        public void PrintHelloManager()
        {
            _test.PrintHello();
        }

        public void PrintSomething()
        {
            Console.WriteLine("Doing something else!!!");
        }
    }
    //interface ITestForReal
    //{
    //    void PrintForReal();
    //}

    class TestForReal : ITest
    {
        ITest _test;
        public TestForReal(ITest test)
        {
            _test = test;
        }
        public void PrintHello()
        {
            Console.WriteLine("Hello from " + this.GetType().ToString());
        }
    }

    class TestForRealver2 : ITest
    {
        ITest _test2;
        public TestForRealver2(ITest test)
        {
            _test2 = test;
        }

        public void PrintHello()
        {
            Console.WriteLine("Hello from " + this.GetType().ToString());
        }
    }

    //class TestForRealManager
    //{
    //    ITest _testforreal;
    //    public TestForRealManager(ITest testforreal)
    //    {
    //        _testforreal = testforreal;
    //    }
    //    public void Print()
    //    {
    //        _testforreal.PrintHello();
    //    }
    //}
}
