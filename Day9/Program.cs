using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day9
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"..\..\input.txt";
			string[] inputArray = System.IO.File.ReadAllLines(path);

			Dictionary<string, Dictionary<string, int>> cities = new Dictionary<string, Dictionary<string, int>>();

			foreach(string str in inputArray)
			{
				string[] splitString = str.Split(' ');

				int distance = int.Parse(splitString[4]);
				string city1 = splitString[0];
				string city2 = splitString[2];

				if (!cities.ContainsKey(city1))
				{
					cities.Add(city1, new Dictionary<string, int>());
				}

				if (!cities.ContainsKey(city2))
				{
					cities.Add(city2, new Dictionary<string, int>());
				}

				cities[city1].Add(city2, distance);
				cities[city2].Add(city1, distance);	
			}

			List<Node> trees = new List<Node>();
			
			foreach(string city in cities.Keys)
			{
				trees.Add(makeTree(cities, new List<string>(), city, ""));
			}

			int shortest = int.MaxValue;

			foreach (Node root in trees)
			{
				int length = CalculateShortest(root);
				
				if(length < shortest)
				{
					shortest = length;
					
				}
			}

			Console.WriteLine("Shortest route " + shortest);

			int longest = int.MinValue;

			foreach (Node root in trees)
			{
				int length = CalculateLongest(root);

				if (length > longest)
				{
					longest = length;
					
				}
			}

			Console.WriteLine("Longest route " + longest);

			Console.ReadKey();
			
		}

		static Node makeTree(Dictionary<string, Dictionary<string, int>> cities, List<string> visited, string current, string previous)
		{
			visited.Add(current);
			Node newNode;
			if (!string.IsNullOrEmpty(previous))
			{
				newNode = new Node
				{
					cost = cities[current][previous],
					children = new List<Node>()
				};
			}
			else
			{
				newNode = new Node
				{
					cost = 0,
					children = new List<Node>()
				};
			}

			foreach(string nextCity in cities.Keys)
			{
				if (!visited.Contains(nextCity))
				{
					newNode.children.Add(makeTree(cities, visited, nextCity, current));
				}
			}
			visited.Remove(current);
			return newNode;
		}

		static int CalculateShortest(Node root)
		{
			int shortest = int.MaxValue;

			foreach(Node child in root.children)
			{
				int length = CalculateShortest(child);

				if(length < shortest)
				{
					shortest = length;
				}
			}

			if(root.children.Count == 0)
			{
				shortest = 0;
			}

			return shortest + root.cost;
		}

		static int CalculateLongest(Node root)
		{
			int longest = int.MinValue;

			foreach (Node child in root.children)
			{
				int length = CalculateLongest(child);

				if (length > longest)
				{
					longest = length;
				}
			}

			if (root.children.Count == 0)
			{
				longest = 0;
			}

			return longest + root.cost;
		}


		internal class Node
		{
			public List<Node> children { get; set; }
			public int cost { get; set; }
		}


	}
	
}