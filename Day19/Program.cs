using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"..\..\input.txt";
			string[] inputArray = System.IO.File.ReadAllLines(path);

			string original = inputArray.Last();
			List<KeyValuePair<string, string>> replacements = new List<KeyValuePair<string, string>>();

			for(int i = 0; i < inputArray.Length-2; i++)
			{
				string[] splitString = inputArray[i].Replace(@" => ", "=").Split('=');
				replacements.Add(new KeyValuePair<string, string>(splitString[0], splitString[1]));
			}

			List<string> newStrings = new List<string>();

			foreach(KeyValuePair<string, string> replacement in replacements)
			{
				int i;
				string copy = original;
				while ((i = copy.LastIndexOf(replacement.Key)) != -1)
				{
					string replaced = original.Remove(i, replacement.Key.Length).Insert(i, replacement.Value);
					if (!newStrings.Contains(replaced))
					{
						newStrings.Add(replaced);
					}
					copy = copy.Remove(i, replacement.Key.Length);
				}
			}

			Console.WriteLine(newStrings.Count());
			Console.ReadKey();
		}
	}
}
