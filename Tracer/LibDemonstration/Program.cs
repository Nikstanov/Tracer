using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LibDemonstration.SomeClasses;
using Tracer;


namespace LibDemonstration
{
    class Program
    {
        static private Tracer.Tracer _tracer;

        static void Main()
        {
            _tracer = new Tracer.Tracer(Thread.CurrentThread.ManagedThreadId);

            Thread thread1 = new Thread(Thread1);
            Thread thread2 = new Thread(Thread2);

            thread1.Start();
            thread2.Start();

            Foo foo = new Foo(_tracer);
            foo.MyMethod();

            thread1.Join();
            thread2.Join();

            _tracer.GetTraceResult();

            _tracer.GetMultiThreadResult("..//..//outputJSON.txt", "..//..//outputXML.txt");
        }

        private void SimpleTest()
        {
            Tracer.Tracer local_tracer = new Tracer.Tracer(Thread.CurrentThread.ManagedThreadId);

            MyClass myObject = new MyClass(_tracer);
            myObject.MethodA();

            _tracer.GetTraceResult();
            _tracer.GetMultiThreadResult("..//..//..//outputJSON.txt", "..//..//..//outputXML.txt");
        }

        static public void Thread1()
        {
            Tracer.Tracer local_tracer1 = new Tracer.Tracer(Thread.CurrentThread.ManagedThreadId);
            Foo foo = new Foo(local_tracer1);

            foo.MyMethod();
            foo.MySecondMethod();

            local_tracer1.GetTraceResult();
        }
        static public void Thread2()
        {
            Tracer.Tracer local_tracer2 = new Tracer.Tracer(Thread.CurrentThread.ManagedThreadId);
            Foo foo = new Foo(local_tracer2);
            Bar bar = new Bar(local_tracer2);

            foo.MyMethod();
            bar.InnerMethod();

            local_tracer2.GetTraceResult();
        }

    }
}
