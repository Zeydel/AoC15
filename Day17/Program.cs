using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"..\..\input.txt";
			string[] inputArray = System.IO.File.ReadAllLines(path);

			List<int> containers = new List<int>();

			foreach(string str in inputArray)
			{
				containers.Add(int.Parse(str));
			}

			int max = (int) Math.Pow(2, containers.Count);

			int count = 0;

			List<int> containerCount = new List<int>();

			for(int i = 0; i <= inputArray.Length; i++)
			{
				containerCount.Add(0);
			}

			for(int i = 1; i < max; i++)
			{
				string binary = Convert.ToString(i, 2);

				while(binary.Length < containers.Count)
				{
					binary = "0" + binary;
				}

				int sum = 0;
				int currentContainerCount = 0;
				for(int j = 0; j < binary.Length; j++)
				{
					if(binary[j] == '1')
					{
						currentContainerCount++;
						sum += containers[j];
					}
				}

				if(sum == 150)
				{
					count++;
					containerCount[currentContainerCount]++;
				}
			}

			Console.WriteLine("Different ways to reach 150: " + count);

			for(int i = 1; i < containerCount.Count; i++)
			{
				if(containerCount[i] != 0)
				{
					Console.WriteLine("Lowest amount of containers: " + i + " with " + containerCount[i] + " different combinations");
					break;
				}
			}

			Console.Read();
		}
	}
}
