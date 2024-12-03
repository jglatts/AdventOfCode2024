using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class AOCDay2_Part2
{
    private string inputString;

    public AOCDay2_Part2()
    {
        loadInputString();
    }

    private void loadInputString()
    {
        foreach (string line in File.ReadAllLines("C:\\Users\\jglatts\\Documents\\AOC24\\input_day2.txt"))
            inputString += line + "\n";
    }

    public bool isValidSeqeunce(List<int> values)
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

    public void solve()
    {
        int count = 0;
        
        foreach (string line in inputString.Split('\n'))
        {
            if (line.Length == 0)
                continue;

            string[] values = line.Split(' ');
            List<int> new_vals  = values.Select(x => Int32.Parse(x)).ToList();

            if (isValidSeqeunce(new_vals))
            {
                count++;
            }
            else
            {
                (bool check, int val) ret = validAfterRemoval(new_vals);  
                if (ret.check)
                {
                    count++;
                }
            }
        }
        
        Console.WriteLine(count);
    }

    public (bool check, int val) validAfterRemoval(List<int> values)
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
            
            if (isValidSeqeunce(new_values))
            {
                ret = true;
                break;
            }
        }

        return (ret, i);
    }
    
    public void print()
    {
        Console.WriteLine(inputString.Split('\n').Length);
    }

    public static void Main(string[] args)
    {
        AOCDay2_Part2 a = new AOCDay2_Part2();
        a.solve();
    }
}
