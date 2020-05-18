using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day22
{
	class Program
	{
		static void Main(string[] args)
		{
			Spell[] spells = new Spell[]
			{
				new Spell
					{
						Cost = 53,
						Damage = 4,
						Healing = 0
					},
					new Spell
					{
						Cost = 73,
						Damage = 2,
						Healing = 2
					},
					new Spell
					{
						Cost = 113,
						Damage = 0,
						Healing = 0,
						SpellEffect = new Effect
						{
							Damage = 0,
							Armor = 7,
							ManaGain = 0,
							Turns = 6
						}
					},
					new Spell
					{
						Cost = 173,
						Damage = 0,
						Healing = 0,
						SpellEffect = new Effect
						{
							Damage = 3,
							Armor = 0,
							ManaGain = 0,
							Turns = 6
						}
					},
					new Spell
					{
						Cost = 229,
						Damage = 0,
						Healing = 0,
						SpellEffect = new Effect
						{
							Damage = 0,
							Armor = 0,
							ManaGain = 101,
							Turns = 5
						}
					},
			};

			Stack<Tuple<Node, Spell>> turns = new Stack<Tuple<Node, Spell>>();


			// Create first turns
			foreach (Spell spell in spells)
			{
				Character pc = new Character
				{
					HP = 50,
					Damage = 0,
					Armor = 0,
					Mana = 500,
					Spells = spells,
				};

				Character Boss = new Character
				{
					HP = 55,
					Damage = 8,
					Armor = 0,
					Mana = 0,
					Spells = new Spell[0],
				};

				Node firstTurn = new Node
				{
					Children = new List<Node>(),
					ManaSpent = 0,
					pc = pc,
					Boss = Boss,
					playersTurn = true,
					activeEffects = new Dictionary<Effect, int>(),
					Depth = 0
				};

				turns.Push(Tuple.Create<Node, Spell>(firstTurn, spell));
			}

			int bestKill = int.MaxValue;
			int count = 0;

			while (turns.Count() > 0)
			{
				count++;
				var current = turns.Pop();

				var currentTurn = current.Item1;
				var currentSpell = current.Item2;

				if (currentTurn.playersTurn)
				{
					currentTurn.pc.HP--;

					if (currentTurn.pc.HP <= 0)
					{
						continue;
						//}
					}

					List<Effect> playedEffects = new List<Effect>();
					foreach (var effect in currentTurn.activeEffects)
					{

						if (effect.Value > 0)
						{
							effect.Key.ApplyEffect(currentTurn.pc, currentTurn.Boss, effect.Value);
							playedEffects.Add(effect.Key);
						}
						else if (effect.Value == 0)
						{
							effect.Key.RemoveEffect(currentTurn.pc);
							playedEffects.Add(effect.Key);
						}
					}

					foreach (Effect playedEffect in playedEffects)
					{
						currentTurn.activeEffects[playedEffect]--;
					}

					if (currentTurn.Boss.HP <= 0)
					{
						if (currentTurn.ManaSpent < bestKill)
						{
							bestKill = currentTurn.ManaSpent;
						}
						continue;
					}

					if (currentTurn.playersTurn)
					{
						if (currentSpell != null)
						{
							if (currentSpell.Cost < currentTurn.pc.Mana && currentTurn.ManaSpent + currentSpell.Cost < bestKill)
							{
								currentTurn.Boss.HP -= currentSpell.Damage;
								currentTurn.pc.HP += currentSpell.Healing;

								currentTurn.ManaSpent += currentSpell.Cost;
								currentTurn.pc.Mana -= currentSpell.Cost;

								if (currentSpell.SpellEffect != null && !currentTurn.activeEffects.ContainsKey(currentSpell.SpellEffect))
								{
									currentTurn.activeEffects.Add(currentSpell.SpellEffect, currentSpell.SpellEffect.Turns);
								}
								else if (currentSpell.SpellEffect != null && currentTurn.activeEffects.ContainsKey(currentSpell.SpellEffect) && currentTurn.activeEffects[currentSpell.SpellEffect] < 1)
								{
									currentTurn.activeEffects[currentSpell.SpellEffect] = currentSpell.SpellEffect.Turns;
								}
							}
							else
							{
								continue;
							}
						}

						if (currentTurn.Boss.HP <= 0)
						{
							if (currentTurn.ManaSpent < bestKill)
							{
								bestKill = currentTurn.ManaSpent;
							}
							continue;
						}

						Character pc = new Character
						{
							HP = currentTurn.pc.HP,
							Damage = currentTurn.pc.Damage,
							Armor = currentTurn.pc.Armor,
							Mana = currentTurn.pc.Mana,
							Spells = spells,
						};

						Character Boss = new Character
						{
							HP = currentTurn.Boss.HP,
							Damage = currentTurn.Boss.Damage,
							Armor = currentTurn.Boss.Armor,
							Mana = currentTurn.Boss.Mana,
							Spells = currentTurn.Boss.Spells,
						};

						Node nextTurn = new Node
						{
							Parent = currentTurn,
							Children = new List<Node>(),
							ManaSpent = currentTurn.ManaSpent,
							pc = pc,
							Boss = Boss,
							playersTurn = !currentTurn.playersTurn,
							activeEffects = currentTurn.activeEffects.ToDictionary(entry => entry.Key, entry => entry.Value),
							Depth = currentTurn.Depth + 1,
						};
						//currentTurn.Children.Add(nextTurn);
						turns.Push(Tuple.Create<Node, Spell>(nextTurn, null));
					}
					else
					{
						int damageToDeal = currentTurn.Boss.Damage - currentTurn.pc.Armor;
						if (damageToDeal < 1)
						{
							damageToDeal = 1;
						}

						currentTurn.pc.HP -= damageToDeal;

						foreach (Spell spell in currentTurn.pc.Spells)
						{

							Character pc = new Character
							{
								HP = currentTurn.pc.HP,
								Damage = currentTurn.pc.Damage,
								Armor = currentTurn.pc.Armor,
								Mana = currentTurn.pc.Mana,
								Spells = spells,
							};

							Character Boss = new Character
							{
								HP = currentTurn.Boss.HP,
								Damage = currentTurn.Boss.Damage,
								Armor = currentTurn.Boss.Armor,
								Mana = currentTurn.Boss.Mana,
								Spells = currentTurn.Boss.Spells,
							};

							Node nextTurn = new Node
							{
								//Parent = currentTurn,
								Children = new List<Node>(),
								ManaSpent = currentTurn.ManaSpent,
								pc = pc,
								Boss = Boss,
								playersTurn = !currentTurn.playersTurn,
								activeEffects = currentTurn.activeEffects.ToDictionary(entry => entry.Key, entry => entry.Value),
								Depth = currentTurn.Depth + 1,
							};
							//currentTurn.Children.Add(nextTurn);
							turns.Push(Tuple.Create<Node, Spell>(nextTurn, spell));
						}

						//int damageToDealNoSpell = currentTurn.Boss.Damage - currentTurn.pc.Armor;
						//if (damageToDealNoSpell < 1)
						//{
						//	damageToDealNoSpell = 1;
						//}

						//currentTurn.pc.HP -= damageToDealNoSpell;

						//Character pcNoSpell = new Character
						//{
						//	HP = currentTurn.pc.HP,
						//	Damage = currentTurn.pc.Damage,
						//	Armor = currentTurn.pc.Armor,
						//	Mana = currentTurn.pc.Mana,
						//	Spells = spells,
						//};

						//Character BossNoSpell = new Character
						//{
						//	HP = currentTurn.Boss.HP,
						//	Damage = currentTurn.Boss.Damage,
						//	Armor = currentTurn.Boss.Armor,
						//	Mana = currentTurn.Boss.Mana,
						//	Spells = currentTurn.Boss.Spells,
						//};

						//Node nextTurnNoSpell = new Node
						//{
						//	Parent = currentTurn,
						//	Children = new List<Node>(),
						//	ManaSpent = currentTurn.ManaSpent,
						//	pc = pcNoSpell,
						//	Boss = BossNoSpell,
						//	playersTurn = !currentTurn.playersTurn,
						//	activeEffects = currentTurn.activeEffects.ToDictionary(entry => entry.Key, entry => entry.Value),
						//	Depth = currentTurn.Depth + 1,
						//};

						//currentTurn.Children.Add(nextTurnNoSpell);
						//turns.Push(Tuple.Create<Node, Spell>(nextTurnNoSpell, null));
					}
				}
				Console.WriteLine(bestKill);
				Console.WriteLine(count);
				Console.ReadKey();
			}


		}

		class Character
		{
			public int HP { get; set; }
			public int Damage { get; set; }
			public int Armor { get; set; }
			public int Mana { get; set; }
			public Spell[] Spells { get; set; }
		}

		class Spell
		{
			public int Cost { get; set; }
			public int Damage { get; set; }
			public int Healing { get; set; }
			public Effect SpellEffect { get; set; }

		}

		class Effect
		{
			public int Damage { get; set; }
			public int Armor { get; set; }
			public int ManaGain { get; set; }
			public int Turns { get; set; }

			public void ApplyEffect(Character pc, Character boss, int turn)
			{
				if (turn == Turns)
				{
					pc.Armor += Armor;
				}

				boss.HP -= Damage;

				pc.Mana += ManaGain;
			}

			public void RemoveEffect(Character pc)
			{
				pc.Armor -= Armor;
			}
		}

		class Node
		{
			public Node Parent { get; set; }
			public List<Node> Children { get; set; }
			public int ManaSpent { get; set; }
			public Character pc { get; set; }
			public Character Boss { get; set; }
			public bool playersTurn { get; set; }
			public Dictionary<Effect, int> activeEffects { get; set; }
			public int Depth { get; set; }

		}
	}
}
