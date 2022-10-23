using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter7
{
	public class Person
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public override string ToString()
		{
			return $"Name: {Name}; Surname: {Surname}";
		}
		public override bool Equals(object obj) // если переопределяем equals, то нужно переопределить GetHashCode
		{
			return (obj as Person)?.ToString() == this.ToString(); //если obj не Person, то вернёт null
		}
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Person p = new Person() { Name = "Kirya", Surname = "Kulyai" };
			Person p2 = p;
			Console.WriteLine(p.ToString());
			Console.WriteLine(p.GetHashCode());
			Console.WriteLine(p.Equals(p.ToString())); //false потому что передаётся строка
			Console.WriteLine(p.Equals(p2)); // true по сути сравнивается один объект сам с собой
			Person p3 = new Person() { Name = "Vlad", Surname = "Volkov" };
			Console.WriteLine(p.Equals(p3)); // false объекты разные
			Person p4 = new Person() { Name = "Kirya", Surname = "Kulyai" };
			Console.WriteLine(p.Equals(p4)); // true объекты одинаковые
			Console.WriteLine(Object.ReferenceEquals(p, p4)); // false адрес объектов разный
			Console.WriteLine(Object.Equals(p,p4)); // true обьекты одинаковые. Вызывает переопределённый в классе метод
			Console.ReadLine();
		}
	}
}
