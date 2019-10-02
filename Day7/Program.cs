using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Day7
{
	class Program
	{
		static Dictionary<string, string> wires;
		static Dictionary<string, ushort> values;
		//static int count = 0;
		static void Main(string[] args)
		{
			string input;
			using (StreamReader sr = new StreamReader(@"C:\Users\msj\source\repos\AoC15\Day7\input.txt"))
			{
				input = sr.ReadToEnd();
			}
			string[] inputArray = Regex.Split(input, "\n");

			wires = new Dictionary<string, string>();
			values = new Dictionary<string, ushort>();
			values.Add("b", 3176);
			string[] splitLine;

			foreach (string line in inputArray)
			{
				splitLine = Regex.Split(line, " -> ");
				wires.Add(splitLine[1], splitLine[0]);
			}
			Console.WriteLine(CalculateValue("a"));
			Console.ReadKey();
		}

		static ushort CalculateValue(string wireName)
		{
			if (values.ContainsKey(wireName))
			{
				return values[wireName];
			}
			ushort val = 0;
			if (Regex.IsMatch(wireName, @"^\d+$"))
			{
				return ushort.Parse(wireName);
			}

			string[] instructions = Regex.Split(wires[wireName], " ");
			if (instructions.Count() == 1)
			{
				val = CalculateValue(instructions[0]);
			}
			else if (instructions.Count() == 2)
			{
				val = (ushort)~CalculateValue(instructions[1]);
			}
			else if (instructions.Count() == 3)
			{
				string input1 = instructions[0];
				string input2 = instructions[2];
				switch (instructions[1])
				{
					case "AND":
						val = (ushort)(CalculateValue(input1) & CalculateValue(input2));
						break;
					case "OR":
						val = (ushort)(CalculateValue(input1) | CalculateValue(input2));
						break;
					case "LSHIFT":
						val = (ushort)(CalculateValue(input1) << CalculateValue(input2));
						break;
					case "RSHIFT":
						val = (ushort)(CalculateValue(input1) >> CalculateValue(input2));
						break;
				}
			}
			values.Add(wireName, val);
			return val;
		}
	}
}
