using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day24
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"..\..\input.txt";
			string[] inputArray = System.IO.File.ReadAllLines(path);

			int[] weighs = new int[inputArray.Count()];

			for (int i = 0; i < inputArray.Count(); i++)
			{
				weighs[i] = int.Parse(inputArray[i]);
			}

			int totalsum = weighs.Sum();
			int target = totalsum / 4;

			List<string> possible = new List<string>();

			int ones = 0;
			while (possible.Count == 0)
			{
				ones++;
				List<string> permutations = BinString(weighs.Length, ones).ToList();

				foreach (string permutation in permutations)
				{
					if (calculatePermutationSum(weighs, permutation) == target)
					{
						List<int> weightsWithoutPermutation = new List<int>();

						for (int i = 0; i < permutation.Length; i++)
						{
							if (permutation[i] == '0')
							{
								weightsWithoutPermutation.Add(weighs[i]);
							}
						}

						int anotherOnes = 0;

						bool permPossible = false;
						while (!permPossible && anotherOnes < weightsWithoutPermutation.Count - 1)
						{
							anotherOnes++;
							List<string> permutationsWithoutSomeWeights = BinString(weightsWithoutPermutation.Count, anotherOnes).ToList();

							foreach (string newPerm in permutationsWithoutSomeWeights)
							{
								if (calculatePermutationSum(weightsWithoutPermutation.ToArray(), newPerm) == target)
								{
									List<int> weightsWithoutOtherPermutation = new List<int>();

									for(int i = 0; i < newPerm.Length; i++)
									{
										if(newPerm[i] == '0')
										{
											weightsWithoutOtherPermutation.Add(weightsWithoutPermutation[i]);
										}
									}

									int anotherAnotherOnes = 0;

									while(!permPossible && anotherAnotherOnes < weightsWithoutOtherPermutation.Count - 1)
									{
										anotherAnotherOnes++;
										List<string> permutationsWithoutSomeMoreWeights = BinString(weightsWithoutOtherPermutation.Count, anotherAnotherOnes).ToList();

										foreach(string newNewPerm in permutationsWithoutSomeMoreWeights)
										{
											if(calculatePermutationSum(weightsWithoutOtherPermutation.ToArray(), newNewPerm) == target)
											{
												permPossible = true;
												possible.Add(permutation);
												break;
											}
										}
									}
								}
							}
						}
					}
				}
			}

			long bestQE = long.MaxValue;

			foreach(string perm in possible)
			{
				if(calculateQuantumEntanglement(weighs, perm) < bestQE)
				{
					bestQE = calculateQuantumEntanglement(weighs, perm);
				}
			}

			Console.Write(bestQE);

			Console.ReadKey();
		}

		static long calculateQuantumEntanglement(int[] weights, string permutation)
		{
			long product = 1;

			for(int i = 0; i < permutation.Length; i++)
			{
				if(permutation[i] == '1')
				{
					product *= weights[i];
				}
			}

			return product;
		}

		static int calculatePermutationSum(int[] weights, string permutaion)
		{
			int sum = 0;

			for (int i = 0; i < permutaion.Length; i++)
			{
				if (permutaion[i] == '1')
				{
					sum += weights[i];
				}
			}

			return sum;
		}

		static IEnumerable<string> BinString(int length, int bits)
		{
			if (length == 1)
			{
				yield return bits.ToString();
			}
			else
			{
				if (length > bits)
				{
					foreach (string s in BinString(length - 1, bits))
					{
						yield return "0" + s;
					}
				}
				if (bits > 0)
				{
					foreach (string s in BinString(length - 1, bits - 1))
					{
						yield return "1" + s;
					}
				}
			}
		}
	}
}
