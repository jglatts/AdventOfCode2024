/*
 * compile with:
 *   csc AOCDay7.cs /r:System.Numerics.dll
 */
using System;
using System.Numerics;
using System.IO;
using System.Collections.Generic;

public class Operation
{
    public BigInteger result_value;
    public List<BigInteger> op_values;

    public Operation() 
    {
        op_values = new List<BigInteger>();
    }
}

public class AOCDay7
{
    private List<Operation> all_ops;
    private List<Operation> valid_ops;
    private List<string> perm_strings;
    private int debug;

    public AOCDay7()
    {
        all_ops = new List<Operation>();
        valid_ops = new List<Operation>();
        perm_strings = new List<string>();
        debug = 0;
        LoadInputString();
    }

    private void LoadInputString()
    {
        /*
        string real_in = "..\\inputs\\input_day7.txt";
        string[] all_lines = File.ReadAllLines(real_in);
        */

        ///*
        string test_in = "..\\inputs\\test_input7.txt";
        string[] all_lines = File.ReadAllLines(test_in);
        //*/
        for (int i = 0; i < all_lines.Length; i++)
        {
            Operation op = new Operation();
            string[] line_values = all_lines[i].Split(':');
            string[] op_vals = line_values[1].Split(' ');
            op.result_value = BigInteger.Parse(line_values[0]);
            for (int j = 1; j < op_vals.Length; j++)
            {
                op.op_values.Add(BigInteger.Parse(op_vals[j]));
            }
            all_ops.Add(op);
        }
        Console.WriteLine("input:");
        Print();
    }

    public void Solve()
    {
        // with permuatations
        //  ans: 435567148864
        // still too low
        BigInteger total = 0;

        for (int i = 0; i < all_ops.Count; i++)
        {
            Operation curr_op = all_ops[i];
            BigInteger check_val = curr_op.result_value;
            BigInteger res_val = 0;
            string formula_str = "";
            for (int j = 0; j < curr_op.op_values.Count; j++)
            {
                res_val += curr_op.op_values[j];
                formula_str += curr_op.op_values[j];
                if (j != curr_op.op_values.Count - 1)
                    formula_str += " + ";
            }
            //Console.WriteLine(check_val + ": from Sum() -> " + res_val);
            if (res_val == curr_op.result_value)
            {
                //Console.WriteLine("found a valid op " + formula_str);
                valid_ops.Add(curr_op);
                continue;
            }

            res_val = 1;
            formula_str = "";
            for (int j = 0; j < curr_op.op_values.Count; j++)
            {
                res_val *= curr_op.op_values[j];
                formula_str += curr_op.op_values[j];
                if (j != curr_op.op_values.Count - 1)
                    formula_str += " * ";
            }
            //Console.WriteLine(check_val + ": from Mult() -> " + res_val);
            if (res_val == curr_op.result_value)
            {
                //Console.WriteLine("found a valid op " + formula_str);
                valid_ops.Add(curr_op);
                continue;
            }

            char[] op_version_one = {'+', '*' }; 
            char[] op_version_two = { '*', '+' };
            int idx = 0;
            string op_string = "";
            string op_perm_string = "";

            for (int j = 0; j < curr_op.op_values.Count; j++)
            {
                op_string += curr_op.op_values[j];
                if (j != (curr_op.op_values.Count - 1))
                {
                    op_string += ", " + op_version_one[idx] + " ";
                    op_perm_string += op_version_one[idx];
                }
                idx++;
                if (idx == 2)
                    idx = 0;
            }

            perm_strings = new List<string>();
            debug = 0;
            Console.WriteLine("permuatiting " + op_perm_string + " at row " + i);
            Permutate(op_perm_string, 0);

            string[] new_ops = op_string.Split(',');
            res_val = Int32.Parse(new_ops[0]);
            for (int j = 1; j < new_ops.Length; j++)
            {
                if (new_ops[j].Contains("+"))
                {
                    string[] parse = new_ops[j].Split('+');
                    int parsed_val = Int32.Parse(parse[1]);
                    res_val += parsed_val;
                    //Console.WriteLine("added " + parsed_val + " curr res_val " + res_val); ;
                }
                else if (new_ops[j].Contains("*"))
                {
                    string[] parse = new_ops[j].Split('*');
                    int parsed_val = Int32.Parse(parse[1]);
                    res_val *= parsed_val;
                    //Console.WriteLine("added " + parsed_val + " curr res_val " + res_val); ;
                }
            }
            //Console.WriteLine("after parsing -> val " + res_val);
            if (res_val == curr_op.result_value)
            {
                valid_ops.Add(curr_op);
                continue;
            }

            idx = 0;
            op_string = "";
            op_perm_string = "";
            for (int j = 0; j < curr_op.op_values.Count; j++)
            {
                op_string += curr_op.op_values[j];
                if (j != (curr_op.op_values.Count - 1))
                {
                    op_string += ", " + op_version_two[idx] + " ";
                    op_perm_string += op_version_two[idx];
                }
                idx++;
                if (idx == 2)
                    idx = 0;
            }

            perm_strings = new List<string>();
            debug = 0;
            Console.WriteLine("permuatiting " + op_perm_string + " at row " + i);
            Permutate(op_perm_string, 0);

            new_ops = op_string.Split(',');
            res_val = Int32.Parse(new_ops[0]);
            for (int j = 1; j < new_ops.Length; j++)
            {
                if (new_ops[j].Contains("+"))
                {
                    string[] parse = new_ops[j].Split('+');
                    int parsed_val = Int32.Parse(parse[1]);
                    res_val += parsed_val;
                    //Console.WriteLine("added " + parsed_val + " curr res_val " + res_val); ;
                }
                else if (new_ops[j].Contains("*"))
                {
                    string[] parse = new_ops[j].Split('*');
                    int parsed_val = Int32.Parse(parse[1]);
                    res_val *= parsed_val;
                    //Console.WriteLine("added " + parsed_val + " curr res_val " + res_val); ;
                }
            }
            //Console.WriteLine("after parsing -> val " + res_val);
            if (res_val == curr_op.result_value)
            {
                valid_ops.Add(curr_op);
                continue;
            }

            // didnt find any other valid ones
            // need to check more permuations here
            long num_of_operations = curr_op.op_values.Count - 1;
            long num_val = (long)Math.Pow(2, num_of_operations);
            Console.Write("total of " + num_val + " to check for the string " +  curr_op.result_value + ": ");
            for (int k = 0; k < curr_op.op_values.Count; k++)
                Console.Write(curr_op.op_values[k] + " ");
            Console.WriteLine();

            if (num_of_operations == 1)
            {
                Console.WriteLine("no more valid checks");
                continue;
            }

            // right track, but needs further refinement
            idx = 0;
            string permuatation_str = "";
            for (int k = 0; k < num_of_operations; k++)
            {
                permuatation_str += op_version_one[idx];
                idx++;
                if (idx == 2)
                    idx = 0;
            }

            //Console.WriteLine("need to permuate " + permuatation_str);
            perm_strings = new List<string>();
            debug = 0;
            Permutate(permuatation_str, 0);

            // slowing down runtime quite a bit here
            for (int j = 0; j < perm_strings.Count; j++)
            {
                string new_check_ops = perm_strings[j];
                BigInteger bg = curr_op.op_values[0];
                int op_idx = 1;
                for (int k = 0; k < new_check_ops.Length; k++)
                {
                    if (new_check_ops[k] == '+')
                    {
                        bg += curr_op.op_values[op_idx++];
                        if (bg > curr_op.result_value)
                            break;
                    }
                    else if (new_check_ops[k] == '*')
                    {
                        bg *= curr_op.op_values[op_idx++];
                        if (bg > curr_op.result_value)
                            break;
                    }
                }

                if (bg == curr_op.result_value)
                {
                    valid_ops.Add(curr_op);
                    Console.WriteLine("its a match!");
                    break;
                }
                //Console.WriteLine(perm_strings[j]);
            }
        }

        Console.WriteLine("\n" + valid_ops.Count + " valid operations");
        foreach (Operation op in valid_ops)
        {
            Console.Write(op.result_value + ": ");
            foreach (int ele in op.op_values)
            {
                Console.Write(ele + " ");
            }
            total += op.result_value;
            Console.WriteLine();
        }
        Console.WriteLine("\nanswer: " + total);  
        
    }

    public string Swap(string s, int i, int j)
    {
        char[] charArray = s.ToCharArray();
        char temp = charArray[i];
        charArray[i] = charArray[j];
        charArray[j] = temp;
        return new string(charArray);
    }

    public void Permutate(string s, int idx)
    {
        if (idx == s.Length - 1)
        {
            if (!perm_strings.Contains(s))
            {
                //Console.WriteLine(s);
                perm_strings.Add(s);
            }
            Console.WriteLine(s);
            return;
        }

        for (int i = idx; i < s.Length; i++)
        {
            s = Swap(s, idx, i);
            Permutate(s, idx + 1);
            s = Swap(s, idx, i);
        }
    }

    public void Print()
    {
        for (int i = 0; i < all_ops.Count; i++)
        {
            Console.Write(all_ops[i].result_value + ": ");
            for (int j = 0; j < all_ops[i].op_values.Count; j++)
            {
                Console.Write(all_ops[i].op_values[j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public static void Main(string[] args)
    {
        AOCDay7 a = new AOCDay7();
        var watch = System.Diagnostics.Stopwatch.StartNew();
        a.Solve();
        watch.Stop();
        Console.WriteLine("\n\nsolved in " + +(watch.ElapsedMilliseconds) + "ms\n" + (watch.ElapsedMilliseconds / 1000) + "sec\n" + ((watch.ElapsedMilliseconds / 1000)/60.0) + "minutes");
    }

}