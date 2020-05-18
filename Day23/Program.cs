using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"..\..\input.txt";
			string[] inputArray = System.IO.File.ReadAllLines(path);

			long[] registers = new long[] { 1, 0 };

			for(int i = 0; i < inputArray.Length; i++)
			{
				string[] split = inputArray[i].Split(' ');

				if(split[0] == "jmp")
				{
					i += jmp(split[1]);
					continue;
				}
				int register;

				if (split[1].Contains("a"))
				{
					register = 0;
				}
				else
				{
					register = 1;
				}

				if(split.Length == 3)
				{

					if(split[0] == "jie")
					{
						i += (int) jie(split[2], registers[register]);
					}
					else
					{
						i += (int) jio(split[2], registers[register]);
					}

					continue;
				}

				if(split[0] == "hlf")
				{
					registers[register] = hlf(registers[register]);
				}
				else if(split[0] == "tpl")
				{
					registers[register] = tpl(registers[register]);
				}
				else if(split[0] == "inc")
				{
					registers[register] = inc(registers[register]);
				}
			}

			Console.WriteLine(registers[1]);
			Console.ReadKey();
		}

		static long hlf(long r)
		{
			return r / 2;
		}

		static long tpl(long r)
		{
			return r * 3;
		}

		static long inc(long r)
		{
			return ++r;
		}

		static int jmp(string jmp)
		{
			if (jmp.Contains("+"))
			{
				return int.Parse(jmp.Remove(0, 1)) - 1;
			}
			else
			{
				return -int.Parse(jmp.Remove(0, 1)) - 1;
			}
		}

		static long jie(string jie, long r)
		{
			if(r % 2 == 0)
			{
				return jmp(jie);
			}
			else
			{
				return 0;
			}
		}

		static long jio(string jio, long r)
		{
			if (r == 1)
			{
				return jmp(jio);
			}
			else
			{
				return 0;
			}
		}
	}
}
