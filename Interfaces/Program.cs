using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	class Program
	{
		
		static void PrintMessageAboutIpointy(IPointy pointy)
		{
			Console.WriteLine($"There is {pointy?.PointsCount} points");
		}
		static IPointy GetPointy(Square square)
		{
			return square;
		}

		static void Main(string[] args)
		{
			int[] arr = new int[3] { 1, 2, 3 };
			var a = arr.Clone();
			int l = (a as Array).Length;

			IPointy sq = new Square();
			//sq не является явно Square  и не может вызывать его методы
			Square sq2 = new Square();
			Treangle tr1 = new Treangle();
			if (sq2 is IPointy s)
			{
				Console.WriteLine("This is Ipointy. Count {0}",s.PointsCount);
			}
			else
			{
				Console.WriteLine("This is not Ipointy");
			}
			if (tr1 is IPointy t)
			{
				Console.WriteLine("This is Ipointy. Count {0}", t.PointsCount);
			}
			else
			{
				Console.WriteLine("This is not Ipointy");
			}
			PrintMessageAboutIpointy(tr1 as IPointy);//отправится null, ошибки не будет, так как случай обработан в функции
			PrintMessageAboutIpointy(sq2);
			Shape[] shapes = new Shape[8];
			for (int i = 0; i < 8; i+=2)
			{
				shapes[i] = new Square();
				shapes[i + 1] = new Treangle();
			}
			int count = 0;
			foreach (var shape in shapes)
			{
				if (shape is IPointy)
				{
					count++;
				}
			}
			Console.WriteLine($"There are {count} Ipointy objects");
			IPointy ip = GetPointy(sq2);
			Console.WriteLine($"ip is {ip.GetType()}");
			Console.WriteLine($"sq is {sq2.GetType()}");//выведет одно и то же
			sq2.Height = 999;
			Console.WriteLine((ip as Square)?.Height); //объект один и тот же

			//Можно создать массив интерфейса, и тогда можно будет обращаться ко всем его обхектам как к объекту, совместимоуму 
			// C IPointy, НЕСМОТРЯ НА РАЗНЫЕ ИЕРАРХИИ КЛАССОВ.
			// МАССИВ ЭТОГО ИНТЕРФЕЙСА МОЖЕТ СОДЕРЖАТЬ ОБЬКТЫ ЛЮБЫХ КЛАССОВ И СТРУКТУР, РЕАЛИЗУЮЩИХ ЭТОТ ИНТЕРФЕЙС.

			var arrP = ip.GetPoints();
			//Ip является ссылкой на Ipointy, поэтому через него можно обратиться к явно реализованным методам
			// sq2.GetPoints(); ошибка компиляции
			IPrinteable ipr = new Square();
			//Square реализует Ipointy, который унаследован от Iprinteable, значит Square реализует Iprinteable 
			// и может быть ссылкой на него
			//IPrinteable ipr2 = new Treangle(); ошибка компиляции

			//Если класс реализует интерфейс, который наследует ещё интерфейс, то он должен реализовать и интерфейс-родитель

			//В отличии от классов интерфейс может расширять сразу несколько интерфейсов

			//Главное преимущество интерфейсов в сравнении с арбстрактными классами в том, что
			//они могут устанавливать общее поведение для классов из разных иерархий


			Console.WriteLine("\n\nHash codes of points of square");
			foreach (var p in sq as Square)
			{
				Console.WriteLine(p.GetHashCode());
			}
			IEnumerator<Point> en = sq2.GetEnumerator(); //Ienumerator может работать как итератор, переводя на следующий элемент
			en.MoveNext();
			Console.WriteLine("2nd way");
			Console.WriteLine(en.Current.GetHashCode());
			en.MoveNext();
			Console.WriteLine(en.Current.GetHashCode());

			tr1.Height = 100;
			tr1.IndentHigh = 4;
			tr1.IndentLow = 5;
			tr1.Pos = 10;
			Console.WriteLine("\n\nFields of tr");
			foreach (var item in tr1) //Перечисляет все поля tr1
			{
				Console.WriteLine(item);
			}
			Console.WriteLine("2nd way");
			IEnumerator ent = tr1.GetEnumerator();
			ent.MoveNext(); //Вот тут будет GetEnumerator started

			Console.WriteLine("\n\nNamed iterator");
			List<Point> iterList = sq2.GetEnumerable().ToList(); //Можно привести к листу Point
			foreach (var item in sq2.GetEnumerable()) // Возвращаемое значение Ienumerable<Point>
				// Поэтомк тип item - Point
			{
				Console.WriteLine(item);
			}

			Console.WriteLine("\n\nIcloneable");
			Square sq3 = sq2.Clone() as Square;
			Console.WriteLine(sq2);
			Console.WriteLine(sq3);
			sq2.Height = 100000;
			Console.WriteLine(sq2.Height);
			Console.WriteLine(sq3.Height);
				Point p1 = (sq2 as IPointy).GetPoints()[0];
				Point p2 = (sq3 as IPointy).GetPoints()[0];
			p2.X = 100;
			Console.WriteLine(p2.X);
			p2.X = 13;
			Console.WriteLine(p1.X); // это разные точки в памяти


			Console.WriteLine("\n\nIComparable");
			Square[] sqs = new Square[5];
			Console.WriteLine("before sort");
			sqs[0] = new Square();
			sqs[0].Height = 144;
			sqs[0].Pos = 14414;
			sqs[1] = new Square();
			sqs[1].Height = 10000;
			sqs[1].Pos = 14434;
			sqs[2] = new Square();
			sqs[2].Height = 1434;
			sqs[2].Pos = 14;
			sqs[3] = new Square();
			sqs[3].Height = 14;
			sqs[3].Pos = 414;
			sqs[4] = new Square();
			sqs[4].Height = 1444;
			sqs[4].Pos = 1;
			foreach (var item in sqs)
			{
				Console.WriteLine($"height {item.Height} pos {item.Pos}");
			}

			Console.WriteLine("after sort height");
			Array.Sort(sqs);
			foreach (var item in sqs)
			{
				Console.WriteLine($"height {item.Height} pos {item.Pos}");
			}
			Console.WriteLine("after sort pos");
			Array.Sort(sqs, Square.Comparer);
			foreach (var item in sqs)
			{
				Console.WriteLine($"height {item.Height} pos {item.Pos}");
			}
		}
	}
}
