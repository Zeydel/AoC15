using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Day4
{
	class Program
	{
		static void Main(string[] args)
		{
			string input = "yzbqklnj";

			int append = 0;
			MD5 md5 = MD5.Create();

			byte[] inputBytes, hashbytes;
			string hash;
			StringBuilder sb = new StringBuilder();
			while (true)
			{
				inputBytes = Encoding.ASCII.GetBytes(input + append);
				hashbytes = md5.ComputeHash(inputBytes);

				for(int i = 0; i < hashbytes.Length; i++)
				{
					sb.Append(hashbytes[i].ToString("x2"));
				}
				hash = sb.ToString();

				if(hash.Substring(0, 6).Equals("000000"))
				{
					Console.WriteLine(hash);
					Console.WriteLine(append);
					Console.ReadKey();
					break;
				}
				sb.Clear();
				append++;
			}


		}

	}
}
