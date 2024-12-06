using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class AOCDay5
{
    private string inputString;
    private Dictionary<int, List<int>> rules;
    private List<List<int>> page_updates;

    public AOCDay5()
    {
        rules = new Dictionary<int, List<int>>();
        page_updates = new List<List<int>>();
        LoadInputString();
    }

    private void LoadInputString()
    {
        inputString = "";
        foreach (string line in File.ReadAllLines("C:\\Users\\jglatts\\Documents\\AOC24\\input_day5.txt"))
        {
            if (line.Contains("|"))
            {
                string[] rule_vals = line.Split('|');
                int first_page = Int32.Parse(rule_vals[0]);
                int second_page = Int32.Parse(rule_vals[1]);
                if (!rules.ContainsKey(first_page))
                {
                    rules[first_page] = new List<int>();
                }
                rules[first_page].Add(second_page);
            }
            if (line.Contains(",")) 
            {
                List<int> the_pages = new List<int>();
                foreach (string s in line.Split(','))
                    the_pages.Add(Int32.Parse(s));
                page_updates.Add(the_pages);
            }
        }

        Console.WriteLine("rules map: ");
        foreach (var entry in rules)
        {
            Console.Write(entry.Key + " | ");
            foreach (int ele in entry.Value)
                Console.Write(ele + " ");
            Console.WriteLine("");
        }

        Console.WriteLine("\npage updates: ");
        foreach (var pgg in page_updates)
        {
            foreach (var cc in pgg)
                Console.Write(cc + " ");
            Console.WriteLine("");
        }

    }

    public void Solve()
    {
        int total = 0;
        List<int> idx_list = new List<int>();
        List<List<int>> valid_updates = new List<List<int>>();
        List<int> copy_pg = new List<int>();
        bool is_valid = true;

        foreach (var pg in page_updates)
        {
            copy_pg = new List<int>(pg);
            is_valid = true;
            for (int k = 0; k < pg.Count; k++)
            {
                int curr = pg[k];
                for (int z = k + 1; z < pg.Count; z++)
                {
                    if (!rules.ContainsKey(pg[z]))
                        continue;

                    List<int> check_rules = rules[pg[z]];
                    for (int zz = 0; zz < check_rules.Count; zz++)
                    {
                        if (curr == check_rules[zz])
                        {
                            is_valid = false;
                            break;
                        }
                    }
                    if (!is_valid)
                        break;
                }
            }
            if (is_valid)
            {
                total++;
                valid_updates.Add(copy_pg);
            }
        }



        Console.WriteLine("\n" + total + " valid rows: ");

        int total_sum = 0;
        foreach (var valid_list in valid_updates)
        {
            if (((valid_list.Count % 2) != 0))
            { 
                total_sum += valid_list[valid_list.Count/2];
            }
            foreach (var ss in valid_list)
            {
                Console.Write(ss + " ");
            }
            Console.WriteLine("");
        }

        Console.WriteLine("\n\n\nanswer: " + total_sum);
    }

    public void Print()
    {
        Console.WriteLine(inputString);
    }

    public static void Main(string[] args)
    {
        AOCDay5 a = new AOCDay5();
        a.Solve();
    }

}
