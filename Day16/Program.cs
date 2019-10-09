using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day16
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"..\..\input.txt";
			string[] inputArray = System.IO.File.ReadAllLines(path);

			Aunt giftGiver = new Aunt
			{
				Properties = new Dictionary<string, int>()
			};
			giftGiver.Properties.Add("children", 3);
			giftGiver.Properties.Add("cats", 7);
			giftGiver.Properties.Add("samoyeds", 2);
			giftGiver.Properties.Add("pomeranians", 3);
			giftGiver.Properties.Add("akitas", 0);
			giftGiver.Properties.Add("vizslas", 0);
			giftGiver.Properties.Add("goldfish", 5);
			giftGiver.Properties.Add("trees", 3);
			giftGiver.Properties.Add("cars", 2);
			giftGiver.Properties.Add("perfumes", 1);

			List<Aunt> aunts = new List<Aunt>();
			foreach (string str in inputArray)
			{
				string normalized = Regex.Replace(str, "[^a-zA-Z0-9 ]", "");
				string[] splitString = normalized.Split(' ');
				int number = int.Parse(splitString[1]);
				Aunt newAunt = new Aunt
				{
					Number = number,
					Properties = new Dictionary<string, int>()
				};

				for (int i = 2; i < splitString.Length; i += 2)
				{
					newAunt.Properties.Add(splitString[i], int.Parse(splitString[i + 1]));
				}

				aunts.Add(newAunt);
			}

			foreach (Aunt aunt in aunts)
			{
				bool rightAunt = true;
				foreach (string key in aunt.Properties.Keys)
				{
					if (key.Equals("cats") || key.Equals("trees"))
					{
						if (!(giftGiver.Properties[key] < aunt.Properties[key]))
						{
							rightAunt = false;
							break;
						}
					}
					else if (key.Equals("pomeranians") || key.Equals("goldfish"))
					{
						if (!(giftGiver.Properties[key] > aunt.Properties[key]))
						{
							rightAunt = false;
							break;
						}
					}
					else
					{

						if (giftGiver.Properties[key] != aunt.Properties[key])
						{
							rightAunt = false;
							break;
						}
					}
				}

				if (rightAunt)
				{
					Console.WriteLine(aunt.Number);
					break;
				}
			}

			Console.ReadKey();
		}
	}

	class Aunt
	{
		public int Number { get; set; }
		public Dictionary<string, int> Properties { get; set; }
	}
}
