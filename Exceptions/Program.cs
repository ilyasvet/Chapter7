using Chapter7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
	class DevideByZeroException : ApplicationException
	{
		public DateTime Time { get; set; }
		public DevideByZeroException(string message = "You can't devide into 0"):base(message)
		{
			Time = DateTime.Now;
		}
	}

	class Program
	{
		static double Devide(int a, int? b)
		{
			return (double)a / (int)b;
		}

		static double Devide2(int a, int b)
		{
			if (b == 0)
			{
				DevideByZeroException ex = new DevideByZeroException();
				ex.HelpLink = "www.tusa228.ru";
				throw ex;
			}
			return (double)a / b;
		}
		static double Devide3(int a, int b)
		{
			try
			{
				return Devide2(a, b);
			}
			catch (DevideByZeroException)
			{
				throw; //передаёт исключение вверх по стеку вызовов
			}
		}
		//если исключение возникло во время обработки исключения, то внутреннему исключению в конструкторе надо
		//первым параметром передать внешнее исключение 
		// *обработчик* вызов внутреннего исключения: throw new Exception(outEx.Message, inEx)
		// сгенерированное исключение записывает новое исключение и сообщение из первого исключения
		static void Main(string[] args)
		{
			try
			{
				Console.WriteLine(Devide(1, null));
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Message: {ex.Message}\n" +
					$"StackTrace: {ex.StackTrace}\n" +
					$"InnerException: {ex.InnerException}\n" +
					$"Source: {ex.Source}\n" +
					$"TargetSite: {ex.TargetSite.MemberType} {ex.TargetSite.DeclaringType}\n" +
					$"HelpLink: {ex.HelpLink}\n" + //поумолчанию пусто
					$"Type ex: {ex.GetType()}"); 
			}
			try
			{
				Console.WriteLine(Devide2(1, 0));
			}
			catch (DevideByZeroException ex)
			{
				Console.WriteLine($"Message: {ex.Message}\n" +
					$"StackTrace: {ex.StackTrace}\n" +
					$"InnerException: {ex.InnerException}\n" +
					$"Source: {ex.Source}\n" +
					$"TargetSite: {ex.TargetSite.MemberType} {ex.TargetSite.DeclaringType}\n" +
					$"HelpLink: {ex.HelpLink}\n" +
					$"Time: {ex.Time}");
			}
			Console.WriteLine("================2021====================");
			try
			{
				Console.WriteLine(Devide3(1, 0));
			}
			catch (DevideByZeroException ex)
			when (ex.Time.Year == 2021) //фильтр исключения
			{
				Console.WriteLine($"Message: {ex.Message}\n" +
					$"StackTrace: {ex.StackTrace}\n" +
					$"InnerException: {ex.InnerException}\n" +
					$"Source: {ex.Source}\n" +
					$"TargetSite: {ex.TargetSite.MemberType} {ex.TargetSite.DeclaringType}\n" +
					$"HelpLink: {ex.HelpLink}\n" +
					$"Time: {ex.Time}");
			}
			catch
			{ }
			Console.WriteLine("================2022====================");
			try
			{
				Console.WriteLine(Devide3(1, 0));
			}
			catch (DevideByZeroException ex)
			when(ex.Time.Year == 2022)
			{
				Console.WriteLine($"Message: {ex.Message}\n" +
					$"StackTrace: {ex.StackTrace}\n" +
					$"InnerException: {ex.InnerException}\n" +
					$"Source: {ex.Source}\n" +
					$"TargetSite: {ex.TargetSite.MemberType} {ex.TargetSite.DeclaringType}\n" +
					$"HelpLink: {ex.HelpLink}\n" +
					$"Time: {ex.Time}");
			}
		}
	}
}
