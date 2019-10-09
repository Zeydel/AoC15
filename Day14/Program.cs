using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"..\..\input.txt";
			string[] inputArray = System.IO.File.ReadAllLines(path);

			int seconds = 2503;

			List<Raindeer> raindeers = new List<Raindeer>();
			foreach(string str in inputArray)
			{
				string[] splitStr = str.Split(' ');

				string name = splitStr[0];
				int speed = int.Parse(splitStr[3]);
				int moveTime = int.Parse(splitStr[6]);
				int restTime = int.Parse(splitStr[13]);

				Raindeer newReindeer = new Raindeer
				{
					Name = name,
					Speed = speed,
					MoveTime = moveTime,
					RestTime = restTime,
					Distance = 0,
					Points = 0,
				};
				raindeers.Add(newReindeer);
			}
			foreach(Raindeer raindeer in raindeers)
			{
				raindeer.Prepare();
			}
			for(int i = 0; i < seconds; i++)
			{
				int curBest = 0;
				foreach(Raindeer raindeer in raindeers)
				{
					raindeer.Move();
					if(raindeer.Distance > curBest)
					{
						curBest = raindeer.Distance;
					}

				}
				foreach(Raindeer raindeer in raindeers)
				{
					if(raindeer.Distance == curBest)
					{
						raindeer.Points++;
					}
				}
			}

			int best = 0;
			int mostPoints = 0;
			foreach(Raindeer raindeer in raindeers)
			{
				if(raindeer.Distance > best)
				{
					best = raindeer.Distance;
				}

				if(raindeer.Points > mostPoints)
				{
					mostPoints = raindeer.Points;
				}
			}

			Console.WriteLine("Longest distance: " + best);
			Console.WriteLine("Most points: " + mostPoints);

			Console.ReadKey();

		}
	}

	class Raindeer
	{
		public int Speed { get; set; }
		public int MoveTime { get; set; }
		public int RestTime { get; set; }
		public int Distance { get; set; }
		public string Name { get; set; }
		public int Points { get; set; }

		int moveTimeLeft;
		int restTimeLeft;

		public void Prepare()
		{
			moveTimeLeft = MoveTime;
			restTimeLeft = 0;
		}

		public void Move()
		{
			if(moveTimeLeft > 0)
			{
				moveTimeLeft--;
				Distance += Speed;

				if(moveTimeLeft == 0)
				{
					restTimeLeft = RestTime;
				}
				return;
			}
			else
			{
				restTimeLeft--;
				if(restTimeLeft == 0)
				{
					moveTimeLeft = MoveTime;
				}
			}
		}
	}
}
