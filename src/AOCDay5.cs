using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class AOCDay5
{
    private string inputString;
    private Dictionary<int, List<int>> rules;
    private List<List<int>> page_updates;
    private List<(int key, int val)> broken_rule;

    public AOCDay5()
    {
        rules = new Dictionary<int, List<int>>(); 
        page_updates = new List<List<int>>();
        broken_rule = new List<(int key, int val)>();
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
        int total_sum = 0;
        List<int> idx_list = new List<int>();
        List<List<int>> valid_updates = new List<List<int>>();
        List<List<int>> valid_after_fix = new List<List<int>>();
        List<List<int>> needs_fix = new List<List<int>>();
        broken_rule = new List<(int key, int val)>();
        List<int> copy_pg = new List<int>();
        bool is_valid = true;
        broken_rule = new List<(int key, int val)>();
        
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
                            broken_rule.Add((curr, pg[z]));
                            is_valid = false;
                            break;
                        }
                    }
                    if (!is_valid)
                        break;
                }
                if (!is_valid)
                    break;
            }
            if (is_valid)
            {
                total++;
                valid_updates.Add(copy_pg);
            }
            else 
            { 
                needs_fix.Add(copy_pg);
            }
        }

        Console.WriteLine("\n" + total + " valid rows: ");
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
        Console.WriteLine("\n(valid-rows) answer: " + total_sum);

        Console.WriteLine("\nrows that need update: ");
        for (int i = 0; i < needs_fix.Count; i++)
        {
            foreach (var sss in needs_fix[i])
            {
                Console.Write(sss + " ");
            }
            int idx_1 = -1;
            int idx_2 = -1;
            if (i < broken_rule.Count)
            {
                idx_1 = needs_fix[i].IndexOf(broken_rule[i].key);
                idx_2 = needs_fix[i].IndexOf(broken_rule[i].val);
                if (idx_1 < 0 || idx_2 < 0)
                    continue;
                int temp = needs_fix[i][idx_1];
                needs_fix[i][idx_1] = needs_fix[i][idx_2];
                needs_fix[i][idx_2] = temp;
            }
        }

        Console.WriteLine("\nswapping updates: ");
        int c = 0;
        List<List<int>> updated = new List<List<int>>();
        for (c = 0; c < needs_fix.Count; c++)
        {
            List<int> fix_copy = new List<int>(needs_fix[c]);
            bool is_fix_vald = false;
            while (!is_fix_vald)
            {
                foreach (int xx in fix_copy)
                    Console.Write(xx + " ");

                if (isValidRow(fix_copy))
                {
                    Console.WriteLine("\t-> is good after update");
                    is_fix_vald = true;
                    updated.Add(fix_copy);
                }
                else
                {
                    Console.WriteLine("\t-> is not good after update -> ("
                                        + broken_rule[0].key + ", " +
                                        broken_rule[0].val + ")");
                    doRowSwap(fix_copy);
                }
            }
        }

        Console.WriteLine("the new valid lists are: ");
        total = 0;
        foreach (var vv in updated)
        {
            if (vv.Count % 2 != 0)
            {
                Console.WriteLine("adding (" + vv[vv.Count / 2] + ")");
                total += vv[vv.Count / 2];
            }
            foreach(int new_ele in vv)
            {
                Console.Write(new_ele + " ");
            }
            Console.WriteLine("");
        }

        Console.WriteLine("\nnew answer :" + total);
    }

    public bool isValidRow(List<int> pg)
    {
        broken_rule = new List<(int key, int val)>();
        int total = 0;
        bool is_valid = true;

        for (int k = 0; k < pg.Count; k++)
        {
            int curr = pg[k];
            is_valid = true;
            for (int z = k + 1; z < pg.Count; z++)
            {
                if (!rules.ContainsKey(pg[z]))
                    continue;

                List<int> check_rules = rules[pg[z]];
                for (int zz = 0; zz < check_rules.Count; zz++)
                {
                    if (curr == check_rules[zz])
                    {
                        broken_rule.Add((curr, pg[z]));
                        is_valid = false;
                        break;
                    }
                }
                if (!is_valid)
                    break;
            }
            if (!is_valid)
                break;
        }
        if (is_valid)
        {
            total++;
        }

        return is_valid;
    }

    public void doRowSwap(List<int> needs_fix)
    {
        for (int i = 0; i < needs_fix.Count; i++)
        {
            Console.Write(needs_fix[i] + " ");
            int idx_1 = -1;
            int idx_2 = -1;
            if (i < broken_rule.Count)
            {
                Console.WriteLine("\nfailed elements: " + broken_rule[i] + "\n");
                idx_1 = needs_fix.IndexOf(broken_rule[i].key);
                idx_2 = needs_fix.IndexOf(broken_rule[i].val);
                if (idx_1 < 0 || idx_2 < 0)
                    continue;
                int temp = needs_fix[idx_1];
                needs_fix[idx_1] = needs_fix[idx_2];
                needs_fix[idx_2] = temp;
                return;
            }
        }
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
