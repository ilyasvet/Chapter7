using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	class Point : ICloneable
	{ 
		public int X { get; set; }
		public int Y { get; set; }
		public Point() { X = 0; Y = 0; }

		public object Clone()
		{
			return MemberwiseClone();
		}
		public override string ToString()
		{
			return $"X = {X} Y = {Y} ;";
		}
	}
	interface IPrinteable
	{
		void Print();
	}
	interface IPointy : IPrinteable
	{
		byte PointsCount { get; }
		Point[] GetPoints();
	}

	abstract class Shape
	{

		private char sym = '$';
		private int height;
		private int pos;
		private int indentHigh;
		private int indentLow;

		public char Sym
		{
			get => sym;
			set => sym = value;
		}

		public int Height
		{
			get => height;
			set
			{
				if (value >= 0)
				{
					height = value;
				}
				else
				{
					Console.WriteLine("Warning, property \"Pos\" wasn't changed");
				}
			}
		}

		public int Pos
		{
			get => pos;
			set
			{
				if (value >= 0)
				{
					pos = value;
				}
				else
				{
					Console.WriteLine("Warning, property \"Pos\" wasn't changed");
				}
			}
		}

		public int IndentHigh
		{
			get => indentHigh;
			set
			{
				if (value >= 0)
				{
					indentHigh = value;
				}
				else
				{

					Console.WriteLine("Warning, property \"IndentHigh\" wasn't changed");
				}
			}
		}

		public int IndentLow
		{
			get => indentLow;
			set
			{
				if (value >= 0)
				{
					indentLow = value;
				}
				else
				{
					Console.WriteLine("Warning, property \"IndentLow\" wasn't changed");
				}
			}
		}

		public void ChangePos(int ch, int h, int low)
		{
			Pos = ch;
			IndentHigh = h;
			IndentLow = low;
		}

		public void Move(int ch, int h, int low)
		{
			Pos += ch;
			IndentHigh += h;
			IndentLow += low;
		}

		public void Scale(double k)
		{
			Height = (int)(k * double.Parse(Height.ToString()));
		}

		protected void MoveRight()
		{
			for (int i = 0; i < pos; i++)
			{
				Console.Write('\t');
			}
		}
		protected void MoveY(int pos)
		{
			for (int i = 1; i <= pos; i++)
			{
				Console.WriteLine(i);
			}
		}
		protected void WriteBorder()
		{
			for (int i = 0; i < 114; i++)
			{
				if (i % 8 == 0)
				{
					Console.Write(i / 8);
				}
				else
				{
					Console.Write('-');
				}

			}
			Console.WriteLine();
		}

		public abstract void Draw(); //наследники обязаны определить этот метод
	}

	class SquareComparer : IComparer
	{
		int IComparer.Compare(object x, object y)//Можно сделать много таких вспомогательных классов 
			//И каждый будет сортировать по определённому признаку
		{
			Square s1 = x as Square;
			Square s2 = y as Square;
			return s1.Pos.CompareTo(s2.Pos);
		}
	}

	class Square : Shape, IPointy, IEnumerable<Point>, ICloneable, IComparable
	{
		public static IComparer Comparer
		{ 
			get => new SquareComparer() ;
		}

		public IEnumerable<Point> GetEnumerable() //Так называемый именованный итератор
		{
			return Get();
			IEnumerable<Point> Get()
			{
				foreach (var item in points)
				{
					yield return item;
				}
			}
		}

		public Square(int height = 1)
		{
			Height = height;
			points = new Point[4];
			for (int i = 0; i < points.Length; i++)
			{
				points[i] = new Point();
			}
		}

		private Point[] points;

		public byte PointsCount => (byte)points.Length;

		public override void Draw()
		{
			WriteBorder();
			MoveY(IndentHigh);
			for (int i = 0; i < Height; i++)
			{
				MoveRight();
				for (int j = 0; j < Height; j++)
				{
					Console.Write(Sym);
				}
				Console.WriteLine();
			}
			MoveY(IndentLow);
			WriteBorder();
		}

		Point[] IPointy.GetPoints()
		{
			return points;
		}//явно реализованные методы являются private

		public void Print()
		{
			Console.WriteLine("Iprinteable");
		}

		public IEnumerator<Point> GetEnumerator()
		{
			foreach (Point item in points)
			{
				yield return item;
			}
			//return points.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator(); //Теперь элемент foreach является Point, а не object
		}

		public object Clone()
		{
			var Clone = MemberwiseClone(); // этот метод не копирует ссылочные типы, а копирует лишь ссылки на них
			(Clone as Square).points = new Point[4];
			for (int i = 0; i < points.Length; i++)
			{
				(Clone as Square).points[i] = points[i].Clone() as Point;
			}
			return Clone;
		}
		public override string ToString()
		{
			return $"{points[0]} {points[1]} {points[2]} {points[3]}";
		}

		public int CompareTo(object obj)
		{
			var temp = obj as Square;
			if (temp != null)
			{
				return Height.CompareTo(temp.Height);
			}
			throw new ArgumentException();
		}
	}
	class Treangle : Shape, IEnumerable
	{
		public Treangle(int height = 1)
		{
			Height = height;
		}
		public override void Draw()
		{
			WriteBorder();
			MoveY(IndentHigh);
			for (int i = 0; i < Height; i++)
			{
				MoveRight();
				for (int j = 0; j < Height - i - 1; j++)
				{
					Console.Write(" ");
				}
				for (int j = 0; j < (i + 1) * 2; j++)
				{
					Console.Write(Sym);
				}
				for (int j = 0; j < Height - i - 1; j++)
				{
					Console.Write(" ");
				}
				Console.WriteLine();
			}
			MoveY(IndentLow);
			WriteBorder();

		}

		public IEnumerator GetEnumerator()
		{
			Console.WriteLine("GetEnumerator started");
			yield return this.Height; // При достижении оператора yield return, местоположение сохраняется и 
			yield return this.IndentHigh; //при следующем вызове продолжается с него.
			yield return this.IndentLow; //возвращаемый тип - object
			yield return this.Pos; //Выполнение функции начинается только при вызове MoveNext()
			yield return this.Sym;
		}
	}
}
