using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day6
{
	class Program
	{
		static void Main(string[] args)
		{
			string input;
			using (StreamReader sr = new StreamReader(@"C:\Users\msj\source\repos\AoC15\Day6\input.txt"))
			{
				input = sr.ReadToEnd();
			}
			string[] inputArray = Regex.Split(input, "\n");

			int[,] lights = new int[1000, 1000];
			for (int i = 0; i < lights.GetLength(0); i++)
			{
				for (int j = 0; j < lights.GetLength(1); j++)
				{
					lights[i, j] = 0;
				}
			}

			foreach (string instruction in inputArray)
			{
				string[] currentInstruction = Regex.Split(instruction, " ");
				int[] start, end;



				switch (currentInstruction[0])
				{
					case "toggle":
						start = Array.ConvertAll(Regex.Split(currentInstruction[1], ","), s => int.Parse(s));
						end = Array.ConvertAll(Regex.Split(currentInstruction[3], ","), s => int.Parse(s));
						for (int i = start[0]; i <= end[0]; i++)
						{
							for (int j = start[1]; j <= end[1]; j++)
							{
								lights[i, j] = lights[i, j] + 2;
							}
						}
						break;
					case "turn":
						start = Array.ConvertAll(Regex.Split(currentInstruction[2], ","), s => int.Parse(s));
						end = Array.ConvertAll(Regex.Split(currentInstruction[4], ","), s => int.Parse(s));
						if (currentInstruction[1].Equals("on"))
						{
							for (int i = start[0]; i <= end[0]; i++)
							{
								for (int j = start[1]; j <= end[1]; j++)
								{
									lights[i, j]++;
								}
							}
						}
						else
						{
							for (int i = start[0]; i <= end[0]; i++)
							{
								for (int j = start[1]; j <= end[1]; j++)
								{
									if (lights[i, j] > 0)
									{
										lights[i, j]--;
									}
								}
							}
						}
						break;
					default:
						break;
				}

			}
			int totalLight = 0;

			for (int i = 0; i < lights.GetLength(0); i++)
			{
				for (int j = 0; j < lights.GetLength(1); j++)
				{
					totalLight += lights[i, j];
				}
			}
			Console.WriteLine(totalLight);
			Console.Read();

		}
	}
}
