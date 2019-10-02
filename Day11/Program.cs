using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
	class Program
	{
		static void Main(string[] args)
		{
			string input = "vzbxkghb";

			while (true)
			{
				if (checkConditions(input))
				{
					break;
				}
				input = incrementPassword(input);
			}

			Console.WriteLine("Next password: " + input);
			input = incrementPassword(input);

			while (true)
			{
				if (checkConditions(input))
				{
					break;
				}
				input = incrementPassword(input);
			}

			Console.WriteLine("Next password: " + input);

			Console.Read();
		}

		static string incrementPassword(string password)
		{
			char[] passwordAsArray = password.ToArray();
			passwordAsArray[password.Length-1]++;
			if(passwordAsArray[password.Length-1] <= 'z')
			{
				return new string(passwordAsArray);
			}
			else
			{
				return incrementPassword(password.Substring(0, password.Length - 1)) + 'a';
			}
		}

		static bool checkConditions(string password)
		{
			bool threeIncreasing = false;
			for(int i = 0; i < password.Length-2; i++)
			{
				if((password[i+1] - password[i] == 1) && (password[i+2] - password[i] == 2))
				{
					threeIncreasing = true;
				}
			}

			if (!threeIncreasing)
			{
				return false;
			}

			if (password.Contains("i") || password.Contains("o") || password.Contains("l"))
			{
				return false;
			}

			int doubleCounter = 0;
			for(int i = 0; i < password.Length-1; i++)
			{
				if(password[i] == password[i + 1])
				{
					doubleCounter++;
					i++;
				}
			}

			if(doubleCounter < 2)
			{
				return false;
			}

			return true;
		}
	}
}
