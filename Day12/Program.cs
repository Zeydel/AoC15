using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day12
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"..\..\input.txt";
			string inputArray = System.IO.File.ReadAllText(path);

			Regex numbers = new Regex(@"-?\d+");

			int sum = 0;
			foreach (Match match in numbers.Matches(inputArray))
			{
				sum += int.Parse(match.Value);
			}

			Console.WriteLine(sum);
			Node node = createNode(inputArray);
			Console.WriteLine(calculateValue(node));

			Console.Read();


		}

		static Node createNode(string input)
		{
			if(input[0] == '{')
			{
				input = input.Substring(1);
			}
			if(input[input.Length-1] == '}')
			{
				input = input.Substring(0, input.Length - 1);
			}

			Node newNode = new Node
			{
				Children = new List<Node>(),
			};
			 
			int curlyCount = Regex.Matches(input, @"{").Count;

			if (curlyCount > 0)
			{
				for (int i = input.Length - 1; i >= 0; i--)
				{
					if (input[i] == '}')
					{
						int curlyMatch = 1;
						for(int j = i-1; j >= 0; j--)
						{
							if(input[j] == '}')
							{
								curlyMatch++;
							}
							else if(input[j] == '{')
							{
								curlyMatch--;
							}

							if(curlyMatch == 0)
							{
								newNode.Children.Add(createNode(input.Substring(j, i - j+1)));
								input = input.Remove(j, i - j+1);
								i = j - 1;
								break;
							}

						}
					}
				}
			}

			newNode.Properties = input;
			return newNode;
		}

		static int calculateValue(Node node)
		{
			bool valid = true;
			if (node.Properties.Contains("red"))
			{
				foreach(Match match in Regex.Matches(node.Properties, @"red"))
				{
					int squareStartLeft = Regex.Matches(node.Properties.Substring(0, match.Index),"\\[").Count;
					int squareEndLeft = Regex.Matches(node.Properties.Substring(0, match.Index),"\\]").Count;

					if (squareStartLeft == squareEndLeft)
					{
						valid = false;
						break;
					}

				}
				if (!valid)
				{
					return 0;
				}
			}

			Regex numbers = new Regex(@"-?\d+");
			int value = 0;
			MatchCollection matches = numbers.Matches(node.Properties);
			foreach(Match match in matches)
			{
				value += int.Parse(match.Value);
			}

			int childrenSum = 0;
			foreach(Node child in node.Children)
			{
				childrenSum += calculateValue(child);
			}

			return childrenSum + value;
		}

	}


	public class Node
	{
		public string Properties { get; set; }
		public List<Node> Children { get; set; }
	}
}
