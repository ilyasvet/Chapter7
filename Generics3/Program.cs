using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------ObservableCollection--------");
            ObservableCollection<int> ints = new ObservableCollection<int>();
            string var = Console.ReadLine();
            if(var == "1")
            {
                ints.CollectionChanged += Ints_CollectionChanged;
            }
            else
            {
                ints.CollectionChanged += Ints_CollectionChanged1;
            }
            ints.Add(3);
            ints.Add(4);
            ints.Add(5);
            ints.Add(6);
            ints.RemoveAt(3);
            ints.Clear();

        }

        private static void Ints_CollectionChanged1(object sender, NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("Обработчик 1");
            for (int i = 0; i < e.OldItems?.Count; i++)
            {
                Console.Write(e.OldItems[i] + " ");

            }
            for (int i = 0; i < e.NewItems?.Count; i++)
            {
                Console.Write(e.NewItems[i] + " ");

            }
            Console.WriteLine();
        }

        //public virtual event NotifyCollectionChangedEventHandler CollectionChanged; член ObservableCollection
        //является делегатом, который обрабатывает события, вызывая методы
        //Этот делегат может вызывать любой метод,
        //который принимает object в первом параметре и NotifyCollectionChangedEventArgs — во втором
        //Может запускать сразу 2 метода последовательно 
        private static void Ints_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("Обработчик 0");
            Console.WriteLine(e.Action);
        }
    }
}
