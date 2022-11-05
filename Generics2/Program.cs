using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics2
{
    class SortPeopleByAge : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            return x.Age.CompareTo(y.Age);
        }
    }

    class Person
    {
        public static IComparer<Person> comparerAge { get => new SortPeopleByAge(); }
        public string Name { get; set; }
        public int Age { get; set; }
        public override string ToString()
        {
            return $"{Name}, {Age} years old";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("-----list-----\n");
            List<Person> people = new List<Person>()
            {
                new Person(){ Name= "Kirill", Age = 19},
                new Person(){ Name= "Alex", Age = 14},
                new Person(){ Name= "Roma", Age = 16},
                new Person(){ Name= "Artyom", Age = 13},
            };
            foreach (Person person in people)
            {
                Console.WriteLine(person);
            }
            people.Insert(2, new Person() { Name = "Vasya", Age = 17 });
            foreach (Person person in people)
            {
                Console.WriteLine(person);
            }

            Console.WriteLine("\n\n-----Stack-----\n");

            Stack<Person> stackOfPeople = new Stack<Person>();
            stackOfPeople.Push(new Person
            { Name = "Homer", Age = 47 });
            stackOfPeople.Push(new Person
            { Name = "Marge", Age = 45 });
            stackOfPeople.Push(new Person
            { Name = "Lisa",  Age = 9 });
            // Просмотреть верхний элемент, вытолкнуть его и просмотреть снова.
            Console.WriteLine("First person is: {0}", stackOfPeople.Peek());
            Console.WriteLine("Popped off {0}", stackOfPeople.Pop());
            Console.WriteLine("\nFirst person is: {0}", stackOfPeople.Peek());
            Console.WriteLine("Popped off {0}", stackOfPeople.Pop());
            Console.WriteLine("\nFirst person item is: {0}", stackOfPeople.Peek()); //peek смотрит, но не удаляет
            Console.WriteLine("Popped off {0}", stackOfPeople.Pop()); //pop удаляет
            //Console.WriteLine("\nFirst person item is: {0}", stackOfPeople.Peek()); //ошибка, стек пуст


            Console.WriteLine("\n\n-----Stack-----\n");

            Queue<Person> peopleQ = new Queue<Person>();
            peopleQ.Enqueue(new Person
            {
                Name = "Lisa",
                Age = 9
            });
            peopleQ.Enqueue(new Person
            {
                Name = "Homer",
                Age=47
            });
            peopleQ.Enqueue(new Person
            {
                Name = "Marge",
                Age = 45
            });
            
          
            // Заглянуть, кто первый в очереди.
            Console.WriteLine("{0} is first in line!", peopleQ.Peek().Name);
            // Удалить всех из очереди.
            GetCoffee(peopleQ.Dequeue());
            GetCoffee(peopleQ.Dequeue());
            GetCoffee(peopleQ.Dequeue());
            //peopleQ.Peek(); //ошибка, очередь пуста


            Console.WriteLine("\n\n-----SortedSet-----\n");

            SortedSet<Person> people1 = new SortedSet<Person>(Person.comparerAge);
            //Конструктор принимает компаратор (IComparer<T>), который устанавливает порядок сортировки
            people1.Add(new Person() { Name = "Gomer", Age = 47 });
            people1.Add(new Person() { Name = "Liza", Age = 9 });
            people1.Add(new Person() { Name = "Marge", Age = 45 });
            people1.Add(new Person() { Name = "Gomer", Age = 47 });
            foreach (Person person1 in people1)
            {
                Console.WriteLine(person1);
            }

            Console.WriteLine("\n\n-----Dictionary-----\n");
            // Наполнить с помощью метода Add().
            Dictionary<string, Person> peopleA = new Dictionary<string, Person>();
            peopleA.Add("Homer", new Person
            {
                Name = "Homer",
            
                Age = 47
            });
            peopleA.Add("Marge", new Person
            {
                Name = "Marge",
                
                Age = 45
            });
            peopleA.Add("Lisa", new Person
            {
                Name = "Lisa",
                
                Age = 9
            });
            // Получить элемент с ключом Homer.
            Person homer = peopleA["Homer"];
            Console.WriteLine(homer);
            // Наполнить с помощью синтаксиса инициализации.
            SortedDictionary<string, Person> peopleB = new SortedDictionary<string, Person>()
            //public SortedDictionary(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
            //Сортировка ключей
            {
                {"Marge", new Person { Name = "Marge",  Age = 45 } },
                { "Lisa", new Person { Name = "Lisa", Age = 9 } },
                {"Homer", new Person { Name = "Homer",  Age = 47}},
            };
            // Получить элемент с ключом Lisa.
            Person lisa = peopleB["Lisa"];
            Console.WriteLine(lisa);
            var a = peopleB.Keys;
            foreach (var item in a)
            {
                Console.WriteLine(item); //Вывести только ключи
            }


        }
        static void GetCoffee(Person p)
        {
            Console.WriteLine("{0} got coffee!", p.Name);
        }

    }
}
