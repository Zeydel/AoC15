using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
	class Program
	{
		static void Main(string[] args)
		{
			string input = "1113122113";

			for(int i = 0; i < 50; i++)
			{
				input = LookAndSay(input);
			}
			Console.WriteLine(input);
			Console.WriteLine(input.Length);
			Console.ReadKey();
		}

		static string LookAndSay(string s)
		{
			StringBuilder output = new StringBuilder();

			char prev = s[0];
			char cur = 'ø';
			int count = 1;

			for (int i = 1; i < s.Length; i++)
			{
				cur = s[i];
				if(cur != prev)
				{
					output.Append(count.ToString() + prev.ToString());

					prev = cur;

					count = 0;

				}

				count++;
			}

			output.Append(count.ToString() + cur.ToString());
			return output.ToString();
		}
	}
}
