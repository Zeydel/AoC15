using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day2
{
	class Program
	{
		static void Main(string[] args)
		{

			string path = @"..\..\input.txt";
			string[] inputArray = System.IO.File.ReadAllLines(path);

			int paperTotal = 0;
			int ribbonTotal = 0;
			int[] boxDimensions;
			foreach(string boxString in inputArray)
			{
				int volume = 1;
				boxDimensions = Array.ConvertAll(Regex.Split(boxString, "x"), s => int.Parse(s));
				Array.Sort(boxDimensions);
				for(int i = 0; i < boxDimensions.Count(); i++)
				{
					for(int j = i+1; j < boxDimensions.Count(); j++)
					{
						paperTotal += boxDimensions[i] * boxDimensions[j] * 2;
					}
					volume *= boxDimensions[i];
				}
				paperTotal += boxDimensions[0] * boxDimensions[1];
				ribbonTotal += volume;
				ribbonTotal += boxDimensions[0] + boxDimensions[0] + boxDimensions[1] + boxDimensions[1];

			}


			Console.WriteLine("Paper total: " + paperTotal);
			Console.WriteLine("Ribbon total: " + ribbonTotal);
			Console.ReadKey();
		}
	}
}
