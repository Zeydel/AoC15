using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"..\..\input.txt";
			string input = System.IO.File.ReadAllText(path);

			HashSet<Coordinate> coordinates = new HashSet<Coordinate>();

			Santa santa = new Santa(0, 0);
			Santa roboSanta = new Santa(0, 0);
			Santa[] santas = { santa, roboSanta };

			int turn = 0;

			Coordinate start = new Coordinate(santa.x, santa.y);
			start.gifts++;
			coordinates.Add(start);
			foreach (char c in input)
			{
				Santa current = santas[turn];

				if (c == '<')
				{
					current.x--;
				}
				else if (c == '>')
				{
					current.x++;
				}
				else if (c == 'v')
				{
					current.y--;
				}
				else if (c == '^')
				{
					current.y++;
				}
				Coordinate newCoordinate = coordinates.Where(w => w.x == current.x && w.y == current.y).FirstOrDefault();
	
				if(newCoordinate == null)
				{
					coordinates.Add(new Coordinate(current.x, current.y));
				}
				else
				{
					newCoordinate.gifts++;
				}

				turn = ++turn % 2;
			}
			Console.WriteLine(coordinates.Count);
			Console.ReadKey();

		}
	}

	public class Coordinate
	{
		public int x { get; set; }
		public int y { get; set; }
		public int gifts { get; set; }

		public Coordinate(int x, int y)
		{
			this.x = x;
			this.y = y;
			gifts = 1;
		}
	}

	public class Santa
	{
		public int x { get; set; }
		public int y { get; set; }

		public Santa(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}
}
