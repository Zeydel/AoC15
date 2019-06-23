using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
	class Program
	{
		static void Main(string[] args)
		{
			string input;
			using (StreamReader sr = new StreamReader(@"C:\Users\msj\source\repos\AoC15\Day1\input.txt"))
			{
				input = sr.ReadToEnd();
			}

			int floor = 0;
			int count = 1;
			foreach(char c in input)
			{
				if(c == '(')
				{
					floor++;
				}
				else if(c == ')')
				{
					floor--;
				}
				if(floor == -1)
				{
					Console.WriteLine("Enter Basement: " + count);
				}
				count++;

			}
			Console.WriteLine(floor);
			Console.ReadKey();
		}
	}
}
