using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day13
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"..\..\input.txt";
			string[] inputArray = System.IO.File.ReadAllLines(path);

			Dictionary<string, Dictionary<string, int>> relationShips = new Dictionary<string, Dictionary<string, int>>();

			foreach (string str in inputArray)
			{
				string[] split = str.Split(' ');

				string name1 = split[0];
				string name2 = Regex.Replace(split[10], @"[^a-zA-Z]", "");

				int happiness = int.Parse(split[3]);

				if (split[2].Equals("lose"))
				{
					happiness = -happiness;
				}

				if (!relationShips.ContainsKey(name1))
				{
					relationShips.Add(name1, new Dictionary<string, int>());
				}

				if (!relationShips.ContainsKey(name2))
				{
					relationShips.Add(name2, new Dictionary<string, int>());
				}

				if (!relationShips[name1].ContainsKey(name2))
				{
					relationShips[name1].Add(name2, 0);
				}

				if (!relationShips[name2].ContainsKey(name1))
				{
					relationShips[name2].Add(name1, 0);
				}

				relationShips[name1][name2] += happiness;
				relationShips[name2][name1] += happiness;

			}

			List<Node> roots = new List<Node>();

			foreach (string str in relationShips.Keys)
			{
				Node newNode = new Node
				{
					name = str,
					value = 0,
					children = new List<Node>()
				};

				foreach (string other in relationShips.Keys)
				{
					if (str.Equals(other))
					{
						continue;
					}
					newNode.children.Add(createTree(other, relationShips, new List<string>() { str }, newNode));
					roots.Add(newNode);
				}

			}
			int best = 0;
			foreach (Node root in roots)
			{
				int value = calculateValue(root);
				if (value > best)
				{
					best = value;
				}
			}

			Console.WriteLine(best);

			const string me = "me";
			relationShips.Add(me, new Dictionary<string, int>());

			foreach (string name in relationShips.Keys)
			{
				if (name.Equals(me))
				{
					continue;
				}
				relationShips[name].Add(me, 0);
				relationShips[me].Add(name, 0);
			}

			List<Node> newRoots = new List<Node>();

			foreach (string str in relationShips.Keys)
			{
				Node newNode = new Node
				{
					name = str,
					value = 0,
					children = new List<Node>()
				};

				foreach (string other in relationShips.Keys)
				{
					if (str.Equals(other))
					{
						continue;
					}
					newNode.children.Add(createTree(other, relationShips, new List<string>() { str }, newNode));
					newRoots.Add(newNode);
				}

			}
			best = 0;
			foreach (Node root in newRoots)
			{
				int value = calculateValue(root);
				if (value > best)
				{
					best = value;
				}
			}

			Console.WriteLine(best);
			Console.ReadKey();

		}

		static int calculateValue(Node root)
		{
			int bestChild = 0;
			foreach (Node child in root.children)
			{
				int childValue = calculateValue(child);
				if (childValue > bestChild)
				{
					bestChild = childValue;
				}
			}

			return bestChild + root.value;
		}

		static Node createTree(string name, Dictionary<string, Dictionary<string, int>> relationShips, List<string> used, Node parent)
		{
			Node newNode = new Node
			{
				children = new List<Node>(),
				name = name,
				value = relationShips[name][parent.name],
				Parent = parent
			};
			used.Add(name);

			foreach (string str in relationShips.Keys)
			{
				if (used.Contains(str))
				{
					continue;
				}
				else
				{
					newNode.children.Add(createTree(str, relationShips, used, newNode));
				}
			}
			used.Remove(name);

			if (newNode.children.Count == 0)
			{
				Node root = newNode;
				while (root.Parent != null)
				{
					root = root.Parent;
				}

				newNode.value += relationShips[newNode.name][root.name];
			}
			return newNode;
		}

	}

	class Node
	{
		public Node Parent { get; set; }
		public List<Node> children { get; set; }
		public int value { get; set; }
		public string name { get; set; }
	}
}
