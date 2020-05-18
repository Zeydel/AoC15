using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day19
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"..\..\input.txt";
			string[] inputArray = System.IO.File.ReadAllLines(path);
			Dictionary<string, int> found = new Dictionary<string, int>();

			string original = inputArray.Last();
			found.Add(original, 0);
			List<KeyValuePair<string, string>> replacements = new List<KeyValuePair<string, string>>();

			for (int i = 0; i < inputArray.Length - 2; i++)
			{
				string[] splitString = inputArray[i].Replace(@" => ", "=").Split('=');
				replacements.Add(new KeyValuePair<string, string>(splitString[0], splitString[1]));
			}

			string checking = original;
			while(checking != "e")
			{
				foreach(KeyValuePair<string, string> keyValuePair in replacements)
				{
					int replacementIndex = checking.LastIndexOf(keyValuePair.Value);

					if (replacementIndex != -1)
					{
						int replacementDepth = found[checking] + 1;
						checking = checking.Remove(replacementIndex, keyValuePair.Value.Length);
						checking = checking.Insert(replacementIndex, keyValuePair.Key);
						if(found.ContainsKey(checking) && found[checking] >= replacementDepth)
						{
							continue;
						}
						else
						{
							found[checking] = replacementDepth;
						}
					}
				}
			}

			Console.WriteLine(found["e"]);
			Console.ReadKey();
		}
	}
}
