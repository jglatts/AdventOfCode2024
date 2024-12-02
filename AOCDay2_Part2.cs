using System;
using System.IO;
using System.Collections.Generic;

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
        {
            inputString += line + "\n";
        }
    }

    private bool checkIncreasing(string line)
    {
        string[] values = line.Split(' ');
        bool ret = false;
        int i = 0;

        while (true)
        {
            if (Int32.TryParse(values[i], out int x1) &&
                Int32.TryParse(values[i + 1], out int x2))
            {
                if (x1 == x2)
                    i++;
                else if (x2 > x1)
                {
                    ret = true;
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        return ret;
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

            List<int> new_vals = new List<int>();
            string[] values = line.Split(' ');
            foreach (string s in values)
                new_vals.Add(Int32.Parse(s));

            if (isValidSeqeunce(new_vals))
            {
                count++;
            }
            else
            {
                (bool check, int val) ret = validAfterRemoval(line, values);  
                if (ret.check)
                {
                    count++;
                }
            }
        }
        Console.WriteLine(count);
    }

    public (bool check, int val) validAfterRemoval(string line, string[] values)
    {
        bool ret = false;
        int i;

        for (i = 0; i < values.Length; i++)
        {
            List<int>new_values = new List<int>();
            for (int j = 0; j < i; j++)
                new_values.Add(Int32.Parse(values[j]));
            for (int k = i + 1; k < values.Length; k++)
                new_values.Add(Int32.Parse(values[k]));
            
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
