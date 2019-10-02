using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day12
{
	class Program
	{
		static void Main(string[] args)
		{
			string input = System.IO.File.ReadAllText(@"C:\Users\msj\source\repos\AoC15\Day12\input.txt");
			
			Regex numbers = new Regex(@"-?\d+");

			int sum = 0;
			foreach(Match match in numbers.Matches(input))
			{
				sum += int.Parse(match.Value);
			}

			Console.WriteLine(sum);

			Regex red = new Regex(@"{[^{}\[\]]*}|\[[^{}\[\]]*\]");
			List<string> objects = new List<string>();

			while (true)
			{
				MatchCollection matches = red.Matches(input);

				if (matches.Count == 0)
				{
					break;
				}
				List<Match> matchList = new List<Match>();

				foreach(Match m in matches)
				{
					matchList.Add(m);
				}

				matchList.Reverse();

				foreach(Match m in matchList)
				{
					objects.Add(m.Value);
					input = input.Remove(m.Index, m.Length);
				}

			}
			sum = 0;


			Console.WriteLine(sum);
			Console.Read();
		}
	}
}
