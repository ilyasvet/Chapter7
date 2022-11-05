using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Generics
{

    class Point
    {
        public int x;
        public int y;
    }
    struct Point1
    {
        public int x;
        public int y;
    }
    class Program
	{

        static void SimpleBoxUnboxOperation()
        {
            // Создать переменную ValueType (int).
            int mylnt = 25;
            // Упаковать int в ссылку на object,
            object boxedlnt = mylnt;
            Console.WriteLine("BoxedInt {0}", boxedlnt);
            Console.WriteLine("Myint {0}", mylnt);
            mylnt = 10;
            boxedlnt = 14;
            Console.WriteLine("BoxedInt {0}", boxedlnt);
            Console.WriteLine("MyInt {0}", mylnt);
            //Переменные остаются независимыми
            //Только boxedint хранится в куче
            object boxedintRef = boxedlnt;
            boxedintRef = 111;
            Console.WriteLine("Boxed int {0}", boxedlnt);
            Console.WriteLine("Ref {0}", boxedintRef);
            //Значение переменной boxedlnt копируется, так что это 2 разные переменные в куче

            int unBoxed = (int)boxedintRef; //Требуется явное преведение типов, так как boxedintRef - оbject
            //long unBoxedL = (long)boxedintRef; //Ошибка времени выполнения из-за несоответствия типов.
        }
        static void Main(string[] args)
		{
            SimpleBoxUnboxOperation();
            ArrayList array = new ArrayList();
            array.Add(19);
            array.Add(20);//Происходит упаковка, так как массив хранит object
            int i = (int)array[0]; //Необходимо преведение, так как возвращает он тоже object
            Console.WriteLine("{0}",i); //Тут тоже просходит упаковка, так как в этом случае i должен быть object
            
            Console.WriteLine($"UsualStr {CheckUsualStr()}"); //30000
            Console.WriteLine($"Usual {CheckUsual()}"); //50000
            Console.WriteLine($"Generic {CheckGeneric()}"); // 10000
            Console.WriteLine($"UsualRef {CheckUsualRef()}"); // 10000
            
        }

        static long CheckUsual()
        {
            var start = DateTime.Now.Ticks;
            ArrayList arr = new ArrayList();
            for (int i = 0; i < 100000; i++)
            {
                arr.Add(10);
            }
            var end = DateTime.Now.Ticks;
            return end - start;
        }
        static long CheckGeneric()
        {
            var start = DateTime.Now.Ticks;
            List<int> arr = new List<int>();
            for (int i = 0; i < 100000; i++)
            {
                arr.Add(10);
            }
            var end = DateTime.Now.Ticks;
            return end - start;
        }
        static long CheckUsualRef()
        {
            List<Point> l = new List<Point>();
            for (int i = 0; i < 100000; i++)
            {
                l.Add(new Point() { x = 0, y = 0});
            }

            var start = DateTime.Now.Ticks;
            ArrayList arr = new ArrayList();
            for (int i = 0; i < 100000; i++)
            {
                arr.Add(l[i]);
            }
            var end = DateTime.Now.Ticks;
            return end - start;
        }
        static long CheckUsualStr()
        {
            List<Point1> l = new List<Point1>();
            for (int i = 0; i < 100000; i++)
            {
                l.Add(new Point1() { x = 0, y = 0 });
            }

            var start = DateTime.Now.Ticks;
            ArrayList arr = new ArrayList();
            for (int i = 0; i < 100000; i++)
            {
                arr.Add(l[i]);
            }
            var end = DateTime.Now.Ticks;
            return end - start;
        }
    }
}
