using System;
using System.IO;
using System.Collections.Generic;

public class AOCDay1
{
	private string inputString;

	public AOCDay1()
	{
		loadInputString();
	}

	private void loadInputString()
	{
        string[] lines = File.ReadAllLines("C:\\Users\\jglatts\\source\\repos\\ZA-Elastomeric-Quoter\\input.txt");
        foreach (string line in lines)
        {
			inputString += line + "\n";
        }
    }

	public void solve()
	{
		List<int> left_list = new List<int>();
		List<int> right_list = new List<int>();
		foreach (string line in inputString.Split('\n')) 
		{
			string[] points = line.Split(' ');
			if (points.Length != 4)
				continue;
			if (Int32.TryParse(points[0], out int x1))
				left_list.Add(x1);
			else
				Console.WriteLine("error (left)" + points[0]);
			if (Int32.TryParse(points[3], out int x2))
				right_list.Add(x2);
            		else
                		Console.WriteLine("error (right)" + points[3]);
        	}
		Console.WriteLine("Left List Count " + left_list.Count);
		Console.WriteLine("Right List Count " + right_list.Count);
		left_list.Sort();
		right_list.Sort();
		
		int sum = 0;
		for (int i = 0; i < left_list.Count; i++) 
		{
			Console.WriteLine("The pairs are " + left_list[i] + ", " + right_list[i]);
			Console.WriteLine("diff is " + Math.Abs(left_list[i] - right_list[i]));
			sum += Math.Abs(left_list[i] - right_list[i]);	
		}

		Console.WriteLine("\n\n" + sum);
	}

	public static void Main(string[] args)
	{
		AOCDay1 a = new AOCDay1();
		a.solve();
	}
}
