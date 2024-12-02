using System;
using System.IO;
using System.Collections.Generic;

public class AOCDay1_Part2
{
    private string inputString;

    public AOCDay1_Part2()
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

        Dictionary<int, int> dict = new Dictionary<int, int>();
        for (int i = 0; i < right_list.Count; i++)
        {
            for (int j = 0; j < left_list.Count; j++)
            {
                if (left_list[j] > right_list[i])
                    break;
                if (left_list[j] == right_list[i])
                {
                    Console.WriteLine("found pair at " + left_list[j] + " and " + right_list[i]);
                    if (dict.ContainsKey(left_list[j]))
                    {
                        dict[left_list[j]]++;
                    }
                    else 
                    {
                        dict[left_list[j]] = 1;
                    }
                }
            }
        }

        int sum = 0;
        foreach (int i in dict.Keys)
        {
            sum += i * dict[i];
            Console.WriteLine(i + " was repeted " + dict[i] + " times");
        }

        Console.WriteLine(sum);
    }

    public static void Main(string[] args)
    {
        AOCDay1_Part2 a = new AOCDay1_Part2();
        a.solve();
    }
}
