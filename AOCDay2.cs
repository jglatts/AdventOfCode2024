using System;
using System.IO;
using System.Collections.Generic;

public class AOCDay2
{
    private string inputString;

    public AOCDay2()
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
                Int32.TryParse(values[i+1], out int x2))
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

    public void solve()
    {
        int count = 0;
        foreach (string line in inputString.Split('\n'))
        {
            if (line.Length == 0)
                continue;

            string[] values = line.Split(' ');
            bool is_valid = true;
            bool is_increasing = checkIncreasing(line);
            for (int i = 0; i < (values.Length - 1); i++)
            {
                if (Int32.TryParse(values[i], out int x1) &&
                    Int32.TryParse(values[i + 1], out int x2))
                {
                    int val = Math.Abs(x2 - x1);
                    if (val > 3 || val == 0)
                    {
                        //Console.WriteLine("bad values " + line);
                        is_valid = false;
                        break;
                    }
                    else if (is_increasing && x1 > x2)
                    {
                        //Console.WriteLine("bad (not increasing)" + line);
                        is_valid = false;
                        break;
                    }
                    else if (!is_increasing && x2 > x1)
                    {
                        //Console.WriteLine("bad (not decreasing)" + line);
                        is_valid = false;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("error parsing vals");
                }
            }
            if (is_valid)
            {
                Console.WriteLine(line + " is valid");
                count++;
            }
        }
        Console.WriteLine(count);
    }

    public void print()
    {
        Console.WriteLine(inputString.Split('\n').Length);
    }

    public static void Main(string[] args)
    {
        AOCDay2 a = new AOCDay2();
        a.solve();
    }
}
