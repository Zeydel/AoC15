using System;
using System.Numerics;

namespace Day25
{
	class Program
	{
		//INPUT: To continue, please consult the code grid in the manual.  Enter the code at row 2978, column 3083.
		static void Main(string[] args)
		{
			int row = 2978;
			int column = 3083;
			int position = CalculatePositionFromRowAndColumn(row, column);
			Console.WriteLine(CalculateCodeFromPositon(position));
			Console.ReadKey();
		}

		static int CalculateCodeFromPositon(int position)
		{
			position--;
			BigInteger firstCode = 20151125;
			BigInteger factor = 252533;
			BigInteger mod = 33554393;

			BigInteger code = (firstCode * (BigInteger.ModPow(factor, position, mod))) % mod;

			return (int) code;
		}

		static int CalculatePositionFromRowAndColumn(int row, int column)
		{
			int startColumn = (row + column) - 1;
			int triangleNumber = (startColumn * (startColumn + 1)) / 2;
			return triangleNumber - (row - 1);
		}
	}
}
