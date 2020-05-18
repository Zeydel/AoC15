using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21
{
	class Program
	{
		static void Main(string[] args)
		{
			//Hit Points: 100
			//Damage: 8
			//Armor: 2
			Character Boss = new Character
			{
				HP = 100,
				Damage = 8,
				Armor = 2
			};

			Item[] Weapons = new Item[]
			{
				new Item(8, 4, 0),
				new Item(10, 5, 0),
				new Item(25, 6, 0),
				new Item(40, 7, 0),
				new Item(74, 8, 0),
			};

			Item[] Armor = new Item[]
			{
				new Item(13, 0, 1),
				new Item(31, 0, 2),
				new Item(53, 0, 3),
				new Item(75, 0, 4),
				new Item(102, 0, 5),
			};

			Item[] Rings = new Item[]
			{
				new Item(25, 1, 0),
				new Item(50, 2, 0),
				new Item(100, 3, 0),
				new Item(20, 0, 1),
				new Item(40, 0, 2),
				new Item(60, 0, 3),
			};

			int lowestEquipmentCostToWin = int.MaxValue;
			int highestEqipmentCostToLose = int.MinValue;

			var permutations = GeneratePermutations(Weapons.Length, Armor.Length, Rings.Length);

			foreach(int[] permutation in permutations)
			{
				Character pc = new Character
				{
					HP = 100,
					Damage = 0,
					Armor = 0,
					EquipmentCost = 0,
				};

				pc.AddStats(Weapons[permutation[0]]);

				if(permutation[1] >= 0)
				{
					pc.AddStats(Armor[permutation[1]]);
				}

				if (permutation[2] >= 0)
				{
					pc.AddStats(Rings[permutation[2]]);
				}

				if (permutation[3] >= 0)
				{
					pc.AddStats(Rings[permutation[3]]);
				}

				int turnsToKillBoss = CalculateHitsToKill(pc, Boss);
				int turnsToKillPlayer = CalculateHitsToKill(Boss, pc);

				if(turnsToKillBoss <= turnsToKillPlayer)
				{
					if(pc.EquipmentCost < lowestEquipmentCostToWin)
					{
						lowestEquipmentCostToWin = pc.EquipmentCost;
					}
				}

				if(turnsToKillPlayer < turnsToKillBoss)
				{
					if(pc.EquipmentCost > highestEqipmentCostToLose)
					{
						highestEqipmentCostToLose = pc.EquipmentCost;
					}
				}
			}

			Console.WriteLine(lowestEquipmentCostToWin);
			Console.WriteLine(highestEqipmentCostToLose);
			Console.ReadKey();
		}

		public static List<int[]> GeneratePermutations(int firstLength, int secondLength, int thirdLength)
		{
			List<int[]> permutations = new List<int[]>();

			for(int i = 0; i < firstLength; i++)
			{
				for(int j = -1; j < secondLength; j++)
				{
					for(int k = -2; k < thirdLength; k++)
					{
						for(int l = k+1; l < thirdLength; l++)
						{
							permutations.Add(new int[] { i, j, k, l });
						}
					}
				}
			}

			return permutations;
		}

		public static int CalculateHitsToKill(Character first, Character second)
		{
			float damage = first.Damage - second.Armor;
			if(damage < 1)
			{
				damage = 1;
			}

			float health = second.HP;

			double turnsToKill = Math.Ceiling( health / damage );

			return (int)turnsToKill;
		}
	}

	class Character
	{
		public int HP { get; set; }
		public int Damage { get; set; }
		public int Armor { get; set; }
		public int EquipmentCost { get; set; }

		public void AddStats(Item item)
		{
			this.Damage += item.Damage;
			this.Armor += item.Armor;
			this.EquipmentCost += item.Cost;
		}
	}

	class Item
	{
		public Item(int Cost, int Damage, int Armor)
		{
			this.Cost = Cost;
			this.Damage = Damage;
			this.Armor = Armor;
		}
		public int Cost { get; set; }
		public int Damage { get; set; }
		public int Armor { get; set; }
	}
}
