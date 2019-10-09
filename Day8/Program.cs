using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day8
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"..\..\input.txt";
			string[] inputArray = System.IO.File.ReadAllLines(path);

			int codeCharCount = 0;
			int memCharCount = 0;
			int encodecCharCount = 0;
			string formatString;
			string encodeString;
			Console.WriteLine(Regex.Replace("\"", "\"", "\\\""));
			foreach(string str in inputArray)
			{
				codeCharCount += str.Length;
				formatString = Regex.Replace(str, @"\\\\", "a");
				formatString = Regex.Replace(formatString, "\\\\\"", "a");
				formatString = Regex.Replace(formatString, "\\\\x..", "a");
				formatString = formatString.Substring(1, formatString.Length - 2);
				memCharCount += formatString.Length;

				encodeString = Regex.Replace(str, "\\\\", @"\\");
				encodeString = Regex.Replace(encodeString, "\"", "\\\"");
				encodeString = "\"" + encodeString + "\"";
				encodecCharCount += encodeString.Length;
				
			}
			Console.WriteLine(codeCharCount - memCharCount);
			Console.WriteLine(encodecCharCount - codeCharCount);

			Console.ReadKey();
		}
	}
}
