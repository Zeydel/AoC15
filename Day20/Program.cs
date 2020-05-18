using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
	class Program
	{
		static Dictionary<int, int> elfCounter = new Dictionary<int, int>();
		static int firstElf = 1;

		static void Main(string[] args)
		{
			int input = 33100000;

			int i = 0;
			while (CalculatePresents(++i) < input)
			{
			}
			Console.WriteLine(i);
			Console.ReadKey();
		}

		private static int CalculatePresents(int houseNumber)
		{
			int presents = 0;

			presents += houseNumber * 11;
			elfCounter[houseNumber] = 1;

			if (elfCounter[firstElf] == 50)
			{
				firstElf++;
			}

			for (int i = firstElf; i <= houseNumber / 2; i++)
			{
				if(elfCounter[i] == 50)
				{
					continue;
				}

				if(houseNumber % i == 0)
				{
					presents += i * 11;
					elfCounter[i] = elfCounter[i] + 1;
				}

			}

			return presents;
		}
	}
}
