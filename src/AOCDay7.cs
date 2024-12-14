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

    public AOCDay7()
    {
        all_ops = new List<Operation>();
        valid_ops = new List<Operation>();
        LoadInputString();
    }

    private void LoadInputString()
    {
        ///*
        string real_in = "..\\inputs\\input_day7.txt";
        string[] all_lines = File.ReadAllLines(real_in);
        //*/

        /*
        string test_in = "..\\inputs\\test_input7.txt";
        string[] all_lines = File.ReadAllLines(test_in);
        */
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
        for (int i = 0; i < all_ops.Count; i++)
        {
            Operation curr_op = all_ops[i];
            BigInteger check_val = curr_op.result_value;
            BigInteger res_val = 0;
            for (int j = 0; j < curr_op.op_values.Count; j++)
            {
                res_val += curr_op.op_values[j];
            }
            //Console.WriteLine(check_val + ": from Sum() -> " + res_val);
            if (res_val == curr_op.result_value)
            {
                //Console.WriteLine("found a valid op");
                valid_ops.Add(curr_op);
                continue;
            }

            res_val = 1;
            for (int j = 0; j < curr_op.op_values.Count; j++)
            {
                res_val *= curr_op.op_values[j];
            }
            //Console.WriteLine(check_val + ": from Mult() -> " + res_val);
            if (res_val == curr_op.result_value)
            {
                //Console.WriteLine("found a valid op");
                valid_ops.Add(curr_op);
                continue;
            }

            int num_of_operatons = curr_op.op_values.Count - 1;
            char[] op_version_one = {'+', '*' }; 
            char[] op_version_two = { '*', '+' };

            int idx = 0;
            string op_string = "";
            for (int j = 0; j < curr_op.op_values.Count; j++)
            {
                op_string += curr_op.op_values[j];
                if (j != (curr_op.op_values.Count-1))
                    op_string += ", " + op_version_one[idx] + " ";
                idx++;
                if (idx == 2)
                    idx = 0;
            }

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
            for (int j = 0; j < curr_op.op_values.Count; j++)
            {
                op_string += curr_op.op_values[j];
                if (j != (curr_op.op_values.Count - 1))
                    op_string += ", " + op_version_two[idx] + " ";
                idx++;
                if (idx == 2)
                    idx = 0;
            }

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
        }

        Console.WriteLine("\n" + valid_ops.Count + " valid operations");
        BigInteger total = 0;
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
        Console.WriteLine("\n\nsolved in " + +(watch.ElapsedMilliseconds) + "ms\n" + (watch.ElapsedMilliseconds / 1000) + "sec");
    }

}