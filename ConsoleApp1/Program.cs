namespace ConsoleApp1
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Class c1 = new Class(1);

            Class c2 = new Class() { MyProperty = 2 };

            Class c3 = new Class
            {
                MyProperty = 3
            };

            Class c4 = c3;
            if (c3 == c4)
            { }

            Console.WriteLine(c4);

            Record r1 = new Record(1, "Nisse");
            Record r2 = r1 with { Name = "Kalle" };
            Record r3 = r2 with { Id = 2 };

            Record r4 = r3;
            if (r3 == r4)
            { }

            Console.WriteLine(r4);

            (int nummer, string namn) = r4;
        }
    }

    public record Record(int Id, string Name);

    public class Class
    {
        public Class(int myProperty) => MyProperty = myProperty;


        public Class()
        {

        }

        public int MyProperty { get; init; }
    }

}
