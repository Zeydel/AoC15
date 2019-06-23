using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day7
{
	class Program
	{
		static Dictionary<string, string> wires;
		//static int count = 0;
		static void Main(string[] args)
		{
			string input;
			using (StreamReader sr = new StreamReader(@"C:\Users\msj\source\repos\AoC15\Day7\input.txt"))
			{
				input = sr.ReadToEnd();
			}
			//input = "123 -> x\n456 -> y\nx AND y -> d\nx OR y -> e\nx LSHIFT 2 -> f\ny RSHIFT 2 -> g\nNOT x -> h\nNOT y -> i";
			string[] inputArray = Regex.Split(input, "\n");

			wires = new Dictionary<string, string>();
			string[] splitLine;

			foreach (string line in inputArray)
			{
				splitLine = Regex.Split(line, " -> ");
				wires.Add(splitLine[1], splitLine[0]);
				Console.WriteLine(splitLine[1] + " " + splitLine[0]);
			}

			Console.WriteLine(CalculateValue("a"));
			Console.ReadKey();
		}

		static ushort CalculateValue(string wireName)
		{

			if (Regex.IsMatch(wireName, @"^\d+$"))
			{
				return ushort.Parse(wireName);
			}

			string[] instructions = Regex.Split(wires[wireName], " ");
			if (instructions.Count() == 1)
			{
				return CalculateValue(instructions[0]);
			}
			else if (instructions.Count() == 2)
			{
				return (ushort)~CalculateValue(instructions[1]);
			}
			else if (instructions.Count() == 3)
			{
				string input1 = instructions[0];
				string input2 = instructions[2];
				switch (instructions[1])
				{
					case "AND":
						return (ushort)(CalculateValue(input1) & CalculateValue(input2));
					case "OR":
						return (ushort)(CalculateValue(input1) | CalculateValue(input2));
					case "LSHIFT":
						return (ushort)(CalculateValue(input1) << CalculateValue(input2));
					case "RSHIFT":
						return (ushort)(CalculateValue(input1) >> CalculateValue(input2));
				}
			}
			return 0;
		}
	}
}
