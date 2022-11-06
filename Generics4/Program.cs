using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Generics4
{
    struct Point<T> where T: new()
    {
        public T X { get; set; }
        public T Y { get; set; }

        public Point(T x, T y)
        {
            X = x;
            Y = y;
        }
        public void Reset()
        {
          X = default(T); Y = default(T); 
        }
        public override string ToString()
        {
            return $"X = {X}, Y = {Y}";
        }
    }

    internal class Program
    {
        static void Swap<T>(ref T a, ref T b)
        {
            Console.WriteLine($"The type is {typeof(T)}");
            T temp = a;
            a = b;
            b = temp;
            
        }
        static void TypeInfo<T>()
        {
            Console.WriteLine($"The type is {typeof(T)}\n" +
                $"Type id is {typeof(T).GUID}");

        }
        static void Main(string[] args)
        {
            int a = 10, b = 15;
            Console.WriteLine($"a = {a}, b = {b}");
            Swap<int>(ref a, ref b);

            Console.WriteLine($"a = {a}, b = {b}");
            TypeInfo<string>();
            TypeInfo<int>(); //Необходимо написать параметр шаблона
            Console.WriteLine(default(int));

            Console.WriteLine("-------Point<T>-------");

            Point<int> p = new Point<int>(10, 10);
            Console.WriteLine("p. ToString()={0} ", p.ToString() ) ;
            p.Reset();
            Console.WriteLine("p.ToString()={0}", p.ToString());
            Console.WriteLine();
            // Точка с координатами типа double.
            Point<double> p2 = new Point<double>(5.4, 3.3);
            Console.WriteLine("p2.ToString() ={0}", p2.ToString());
            p2.Reset();
            Console.WriteLine("p2.ToString() = {0}", p2.ToString ());
            Console.ReadLine();


        }
    }
}
