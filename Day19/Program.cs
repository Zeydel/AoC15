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
		static Dictionary<string, int> bestPaths = new Dictionary<string, int>();
		static int least = int.MaxValue;
		static HashSet<string> searched = new HashSet<string>();
		static void Main(string[] args)
		{
			string path = @"..\..\input.txt";
			string[] inputArray = System.IO.File.ReadAllLines(path);

			string original = inputArray.Last();
			List<KeyValuePair<string, string>> replacements = new List<KeyValuePair<string, string>>();

			for (int i = 0; i < inputArray.Length - 2; i++)
			{
				string[] splitString = inputArray[i].Replace(@" => ", "=").Split('=');
				replacements.Add(new KeyValuePair<string, string>(splitString[0], splitString[1]));
			}

			List<string> newStrings = new List<string>();

			foreach (KeyValuePair<string, string> replacement in replacements)
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

			string target = "e";
			bool originalFound = false;
			StringBuilder sb = new StringBuilder();
			int best = NaiveMatch(target, original, replacements, 0);
			Console.WriteLine(best);

			Node root = new Node(original);
			root.Depth = 0;
			Stack<Node> nextNodes = new Stack<Node>();
			nextNodes.Push(root);
			HashSet<string> newerStrings = new HashSet<string>();
			newerStrings.Add(original);

			//Stopwatch s = new Stopwatch();
			//int count = 0;

			while (!(nextNodes.Count() == 0))
			{
				//count++;
				//if(count == 1000)
				//{
				//	Console.WriteLine(s.ElapsedMilliseconds);
				//	s.Restart();
				//	count = 0;
				//}
				Node cur = nextNodes.Pop();
				if (cur.Depth >= best)
				{
					continue;
				}
				string copy = cur.Value;

				foreach (KeyValuePair<string, string> replacement in replacements)
				{

					int i;
					foreach (Match m in Regex.Matches(copy, replacement.Value))
					{
						i = m.Index;

						string replaced = sb.Append(copy).Remove(i, replacement.Value.Length).Insert(i, replacement.Key).ToString();
						sb.Clear();

						if (!bestPaths.ContainsKey(replaced) || bestPaths[replaced] > cur.Depth + 1)
						{
							Node newNode = new Node(replaced);
							newNode.Depth = cur.Depth + 1;
							bestPaths[replaced] = newNode.Depth;
							nextNodes.Push(newNode);
							if (replaced.Equals(target))
							{
								best = newNode.Depth;
								Console.WriteLine("New best found:" + best);
								//originalFound = true;
							}
						}
					}
				}
			}
			Console.WriteLine(best);
			Console.ReadKey();
		}

		static int NaiveMatch(string current, string target, List<KeyValuePair<string, string>> replacements, int curDepth)
		{
			Stack<Node> nextNodes = new Stack<Node>();
			Node newNode = new Node(current);
			newNode.Depth = curDepth;
			nextNodes.Push(newNode);
			StringBuilder sb = new StringBuilder();
			if (target.Length < least)
			{
				least = target.Length;
				Console.WriteLine(least);
			}

			while (!(nextNodes.Count() == 0))
			{

				Node cur = nextNodes.Pop();
				string copy = cur.Value;
				foreach (KeyValuePair<string, string> replacement in replacements)
				{

					int i;
					foreach (Match m in Regex.Matches(copy, replacement.Key))
					{
						i = m.Index;

						string replaced = sb.Append(copy).Remove(i, replacement.Key.Length).Insert(i, replacement.Value).ToString();
						sb.Clear();

						if (searched.Contains(replaced))
						{
							continue;
						}
						else
						{
							if (replaced.Equals(target))
							{
								return cur.Depth + 1;
							}

							int p = PrefixMatch(replaced, target);
							int s = SuffixMatch(replaced, target);
							if (p > 0 || s > 0)
							{
								int best = NaiveMatch(replaced.Substring(p, replaced.Length - (p + s)), target.Substring(p, target.Length - (p + s)), replacements, cur.Depth + 1);
								if(best > -1)
								{
									return best;
								}
							}
							newNode = new Node(replaced);
							newNode.Depth = cur.Depth + 1;
							nextNodes.Push(newNode);
							searched.Add(replaced);

						}


					}
				}
			}
			return -1;
		}

		static int PrefixMatch(string current, string target)
		{
			int count = 0;
			for (int i = 0; i < target.Length && i < current.Length; i++)
			{
				if (current[i] == target[i])
				{
					count++;
				}
				else
				{
					return count;
				}
			}
			return count;
		}

		static int SuffixMatch(string current, string target)
		{
			int count = 0;
			for (int i = 1; i < target.Length && i < current.Length; i++)
			{
				if (current[current.Length - i] == target[target.Length - 1])
				{
					count++;
				}
				else
				{
					return count;
				}
			}
			return count;
		}
	}



	class Node
	{
		public Node(string value)
		{
			Value = value;
			//Children = new List<Node>();
		}
		public int Depth { get; set; }
		public string Value { get; set; }
	}
}
