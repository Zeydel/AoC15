using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day5
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"..\..\input.txt"; 
			string[] inputArray = System.IO.File.ReadAllLines(path);

			char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
			string[] badSequences = {"ab", "cd", "pq", "xy" };

			int niceCount = 0;
			
			foreach(string name in inputArray)
			{
				bool doubleLetter = false;
				bool badSequence = false;
				int vowelCount = 0;

				for(int i = 0; i < name.Length; i++)
				{
					if (vowels.Contains(name[i]))
					{
						vowelCount++;
					}
					if(i < name.Length - 1)
					{
						if(name[i] == name[i + 1])
						{
							doubleLetter = true;
						}
						if(badSequences.Contains(name.Substring(i, 2)))
						{
							badSequence = true;
						}
					}
				}


				if(vowelCount < 3 || !doubleLetter || badSequence)
				{
					continue;
				}
				else
				{
					niceCount++;
				}
			}
			Console.WriteLine("By old rules: " + niceCount);

			niceCount = 0;
			foreach(string name in inputArray)
			{
				List<string> substrings = new List<string>();
				bool repeatPair = false;
				bool repeatLetter = false;

				for(int i = 0; i < name.Length-1; i++)
				{
					if(substrings.Contains(name.Substring(i, 2)))
					{
						repeatPair = true;
					}
					if (i > 0)
					{
						substrings.Add(name.Substring(i - 1, 2));
					}
					if(i+2 < name.Length)
					{
						if(name[i] == name[i + 2])
						{
							repeatLetter = true;
						}
					}
					
				}
				if (repeatPair && repeatLetter)
				{
					niceCount++;
				}
			}
			Console.WriteLine("By new rules: " + niceCount);
			Console.ReadKey();
		}
	}
}
