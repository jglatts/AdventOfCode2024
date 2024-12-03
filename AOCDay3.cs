using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class AOCDay3
{
    private string inputString;
    private string searchSeq;

    public AOCDay3()
    {
        searchSeq = "mul(";
        LoadInputString();
    }

    private void LoadInputString()
    {
        foreach (string line in File.ReadAllLines("C:\\Users\\jglatts\\Documents\\AOC24\\input_day3.txt"))
            inputString += line;
    }

    public void Solve()
    {
        List<string> valid_strings = new List<string>();
        for (int i = 0; i < inputString.Length; i++)
        {
            int search_idx = 0;
            if (inputString[i] == searchSeq[search_idx])
            {
                int count = 0;
                bool is_valid = true;
                i++;
                for (int j = search_idx + 1; j < searchSeq.Length; j++)
                {
                    if (inputString[i] != searchSeq[j])
                    {
                        is_valid = false;
                        break;
                    }
                    else
                    {
                        i++;
                        count++;
                    }
                }
                if (is_valid)
                {
                    string theStr = inputString.Substring((i - count) - 1, 12);
                    if (theStr.IndexOf(',') != -1 && theStr.IndexOf(')') != -1)
                    {
                        theStr = inputString.Substring((i - count) - 1, theStr.IndexOf(')') + 1);
                        valid_strings.Add(theStr);
                    }
                }
            }
        }

        int sum = 0;
        string value_str = "";
        for (int i = 0; i < valid_strings.Count; i++)
        {
            try
            {
                value_str = valid_strings[i];
                Console.WriteLine(value_str);

                int dist = value_str.IndexOf(',') - value_str.IndexOf('(');
                string val_one = value_str.Substring(value_str.IndexOf('(') + 1, dist - 1);
                dist = value_str.IndexOf(')') - value_str.IndexOf(',');
                string val_two = value_str.Substring(value_str.IndexOf(',') + 1, dist - 1);
                Console.WriteLine("need to mult " + val_one + " and " + val_two);
                int x1 = Int32.Parse(val_one);
                int x2 = Int32.Parse(val_two);
                sum += x1 * x2;
            }
            catch (Exception ex){
                Console.WriteLine(value_str + "\ncaused" + ex.Message);
            }
        }

        Console.WriteLine("\n\n" + sum);
    }

    public void Print()
    {
        Console.WriteLine(inputString);
    }

    public static void Main(string[] args)
    {
        AOCDay3 a = new AOCDay3();
        a.Solve();
    }
}
