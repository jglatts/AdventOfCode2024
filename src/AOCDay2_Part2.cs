using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class AOCDay2_Part2
{
    private string inputString;

    public AOCDay2_Part2()
    {
        LoadInputString();
    }

    private void LoadInputString()
    {
        foreach (string line in File.ReadAllLines("C:\\Users\\jglatts\\Documents\\AOC24\\input_day2.txt"))
            inputString += line + "\n";
    }

    public bool IsValidSeqeunce(List<int> values)
    {
        bool is_valid = true;
        bool is_increasing = values[0] < values[1];

        for (int i = 0; i < (values.Count - 1); i++)
        {
            int x1 = values[i];
            int x2 = values[i + 1];
            int val = Math.Abs(x2 - x1);
            if (val > 3 || val == 0)
            {
                is_valid = false;
                break;
            }
            else if (is_increasing && x1 > x2)
            {
                is_valid = false;
                break;
            }
            else if (!is_increasing && x2 > x1)
            {
                is_valid = false;
                break;
            }
        }

        return is_valid;
    }

    public void Solve()
    {
        int count = 0;
        foreach (string line in inputString.Split('\n'))
        {
            if (line.Length == 0)
                continue;

            List<int> new_vals  = line.Split(' ').Select(x => Int32.Parse(x)).ToList();
            if (IsValidSeqeunce(new_vals))
            {
                count++;
            }
            else
            {
                (bool check, int val) ret = ValidAfterRemoval(new_vals);  
                if (ret.check)
                {
                    count++;
                }
            }
        }
        Console.WriteLine(count);
    }

    public (bool check, int val) ValidAfterRemoval(List<int> values)
    {
        bool ret = false;
        int i;

        for (i = 0; i < values.Count; i++)
        {
            List<int> new_values = new List<int>();
            for (int j = 0; j < i; j++) 
                new_values.Add(values[j]);
            for (int k = i + 1; k < values.Count; k++)
                new_values.Add(values[k]);
            
            if (IsValidSeqeunce(new_values))
            {
                ret = true;
                break;
            }
        }

        return (ret, i);
    }
    
    public void Print()
    {
        Console.WriteLine(inputString.Split('\n').Length);
    }

    public static void Main(string[] args)
    {
        AOCDay2_Part2 a = new AOCDay2_Part2();
        a.Solve();
    }
}
