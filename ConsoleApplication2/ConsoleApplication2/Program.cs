using System;

namespace ConsoleApplication2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Method2((c1) =>
                {
                    Method1(ref c1);
                    Console.WriteLine(c1.Name);
                });
        }

        public static void Method2(Action<C1> action)
        {
            C1 c1 = new C1 { Name = "Method 2" };
            action(ref c1);
            Console.WriteLine(c1.Name);
        }

        public static void Method1(ref C1 name)
        {
            name = new C1 { Name = "Method 1" };
        }

        public class C1
        {
            public string Name { get; set; }
        }
    }
}