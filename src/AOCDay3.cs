using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class AOCDay3
{
    private string inputString;
    private string searchSeq;
    private string enableString;
    private string disableString;

    public AOCDay3()
    {
        LoadInputString();
        searchSeq = "mul(";
        enableString = "do()";
        disableString = "don't()";
    }

    private void LoadInputString()
    {
        foreach (string line in File.ReadAllLines("C:\\Users\\jglatts\\Documents\\AOC24\\input_day3.txt"))
            inputString += line;
    }

    public void Solve()
    {
        List<string> valid_strings = new List<string>();
        bool isEnabled = true;
        int count = 0;

        for (int i = 0; i < inputString.Length; i++)
        {
            int dont_index = 0;
            bool dont_was_found = false;
            if (inputString[i] == disableString[dont_index])
            {
                dont_was_found = true;
                int copy = i + 1;
                for (int k = dont_index+1; k < disableString.Length; k++) 
                {
                    if (inputString[copy] != disableString[k])
                    {
                        dont_was_found = false;
                        break;
                    }
                    else
                    {
                        copy++;
                        count++;
                    }
                }
            }

            if (dont_was_found)
            {
                Console.WriteLine("found a dont");
                isEnabled = false;
            }

            int do_index = 0;
            bool do_was_found = false;
            count = 0;
            if (inputString[i] == enableString[do_index])
            {
                do_was_found = true;
                int copy = i + 1;
                for (int k = do_index + 1; k < enableString.Length; k++)
                {
                    if (inputString[copy] != enableString[k])
                    {
                        do_was_found = false;
                        break;
                    }
                    else 
                    {
                        copy++;
                        count++;
                    }
                }

            }

            if (do_was_found)
            {
                Console.WriteLine("found a do");
                isEnabled = true;
            }

            int search_idx = 0;
            if (inputString[i] == searchSeq[search_idx])
            {
                count = 0;
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
                if (is_valid && isEnabled)
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
            catch (Exception ex) {
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
