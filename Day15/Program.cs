using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day15
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"..\..\input.txt";
			string[] inputArray = System.IO.File.ReadAllLines(path);

			Dictionary<string, Ingredient> ingredients = new Dictionary<string, Ingredient>();

			for(int i = 0; i < inputArray.Length; i++)
			{
				string normalized = Regex.Replace(inputArray[i], "[^a-zA-Z0-9- ]", "");
				string[] splitString = normalized.Split(' ');

				string name = splitString[0];
				int capacity = int.Parse(splitString[2]);
				int durability = int.Parse(splitString[4]);
				int flavor = int.Parse(splitString[6]);
				int texture = int.Parse(splitString[8]);
				int calories = int.Parse(splitString[10]);

				Ingredient newIngredient = new Ingredient
				{
					Name = name,
					Capacity = capacity,
					Durability = durability,
					Flavor = flavor,
					Texture = texture,
					Calories = calories
				};

				ingredients.Add(name, newIngredient);
			}

			long bestValue = 0;
			long bestValueWith500Calories = 0;

			//Counter for sprinkles
			for (int i = 1; i <= 97; i++)
			{
				//Counter for peanutButter
				for (int j = 1; j + i <= 98; j++)
				{
					//Counter for frosting
					for (int k = 1; k + i + j <= 99; k++)
					{
						int l = 100 - k - j - i;

						long value = CalculateCookieValue(i, j, k, l, ingredients);
						if (value > bestValue)
						{
							bestValue = value;
							//Console.WriteLine(bestValue);
						}

						if(CalculateCalories(i, j, k, l, ingredients) == 500)
						{
							if(value > bestValueWith500Calories)
							{
								bestValueWith500Calories = value;
							}
						}
					}
				}
			}

			Console.WriteLine("Best cookie: " + bestValue);
			Console.WriteLine("Best cookie with 500 calories: " + bestValueWith500Calories);
			Console.ReadKey();
			
		}

		static long CalculateCookieValue(int tpsSprinkles, int tpsPeanutButter, int tpsFrosting, int tpsSugar, Dictionary<string, Ingredient> ingredients)
		{
			//Console.WriteLine(tpsSprinkles+tpsPeanutButter+tpsFrosting+tpsSugar);
			Ingredient Sprinkles = ingredients["Sprinkles"];
			Ingredient PeanutButter = ingredients["PeanutButter"];
			Ingredient Frosting = ingredients["Frosting"];
			Ingredient Sugar = ingredients["Sugar"];

			long capacity = (tpsSprinkles * Sprinkles.Capacity) + (tpsPeanutButter * PeanutButter.Capacity) + (tpsFrosting * Frosting.Capacity) + (tpsSugar * Sugar.Capacity);
			long durability = (tpsSprinkles * Sprinkles.Durability) + (tpsPeanutButter * PeanutButter.Durability) + (tpsFrosting * Frosting.Durability) + (tpsSugar * Sugar.Durability);
			long flavor = (tpsSprinkles * Sprinkles.Flavor) + (tpsPeanutButter * PeanutButter.Flavor) + (tpsFrosting * Frosting.Flavor) + (tpsSugar * Sugar.Flavor);
			long texture = (tpsSprinkles * Sprinkles.Texture) + (tpsPeanutButter * PeanutButter.Texture) + (tpsFrosting * Frosting.Texture) + (tpsSugar * Sugar.Texture);

			if(capacity < 0)
			{
				capacity = 0;
			}
			if (durability < 0)
			{
				durability = 0;
			}
			if (flavor < 0)
			{
				flavor = 0;
			}
			if (texture < 0)
			{
				texture = 0;
			}

			return capacity * durability * flavor * texture;
		}

		static long CalculateCalories(int tpsSprinkles, int tpsPeanutButter, int tpsFrosting, int tpsSugar, Dictionary<string, Ingredient> ingredients)
		{
			Ingredient Sprinkles = ingredients["Sprinkles"];
			Ingredient PeanutButter = ingredients["PeanutButter"];
			Ingredient Frosting = ingredients["Frosting"];
			Ingredient Sugar = ingredients["Sugar"];

			long calories = (tpsSprinkles * Sprinkles.Calories) + (tpsPeanutButter * PeanutButter.Calories) + (tpsFrosting * Frosting.Calories) + (tpsSugar * Sugar.Calories);
			return calories;
		}

	}

	class Ingredient
	{
		public string Name { get; set; }
		public int Capacity { get; set; }
		public int Durability { get; set; }
		public int Flavor { get; set; }
		public int Texture { get; set; }
		public int Calories { get; set; }
	}


}
