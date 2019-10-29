using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"..\..\input.txt";
			string[] inputArray = System.IO.File.ReadAllLines(path);

			bool[,] lights = new bool[102, 102];

			for (int i = 0; i < 100; i++)
			{
				for (int j = 0; j < 100; j++)
				{
					if (inputArray[j][i] == '#')
					{
						lights[j + 1, i + 1] = true;
					}
				}
			}

			for (int count = 0; count < 100; count++)
			{
				bool[,] newState = new bool[102, 102];

				for (int i = 1; i <= 100; i++)
				{
					for (int j = 1; j <= 100; j++)
					{
						int neighborOnCount = 0;

						for (int k = i - 1; k <= i + 1; k++)
						{
							for (int l = j - 1; l <= j + 1; l++)
							{
								if (k == i && l == j)
								{
									continue;
								}
								if (lights[l, k])
								{
									neighborOnCount++;
								}
							}
						}

						if (lights[j, i])
						{
							if (neighborOnCount == 2 || neighborOnCount == 3)
							{
								newState[j, i] = true;
							}
						}
						else
						{
							if (neighborOnCount == 3)
							{
								newState[j, i] = true;
							}
						}
					}
				}

				lights = newState;
				lights[1, 1] = true;
				lights[1, 100] = true;
				lights[100, 1] = true;
				lights[100, 100] = true;
			}

			int lightsOnCount = 0;

			for (int i = 1; i <= 100; i++)
			{
				for (int j = 1; j <= 100; j++)
				{
					if (lights[j, i])
					{
						lightsOnCount++;
					}
				}
			}

			Console.WriteLine(lightsOnCount);
			Console.Read();
		}
	}
}
