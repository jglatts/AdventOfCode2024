using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

public class Operation
{
    public int result_value;
    public List<int> op_values;

    public Operation() 
    {
        op_values = new List<int>();
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
        //string real_in = "..\\inputs\\input_day7.txt";
        string test_in = "..\\inputs\\test_input7.txt";
        string[] all_lines = File.ReadAllLines(test_in);
        //string[] all_lines = File.ReadAllLines(real_in);
        for (int i = 0; i < all_lines.Length; i++)
        {
            Operation op = new Operation();
            string[] line_values = all_lines[i].Split(':');
            string[] op_vals = line_values[1].Split(' ');
            op.result_value = Int32.Parse(line_values[0]);
            for (int j = 1; j < op_vals.Length; j++)
            {
                op.op_values.Add(Int32.Parse(op_vals[j]));
            }
            all_ops.Add(op);
        }
        Print();
    }

    public void Solve()
    {
        for (int i = 0; i < all_ops.Count; i++)
        {
            Operation curr_op = all_ops[i];

            // check if a straight '+' is valid
            int check_val = curr_op.result_value;
            int res_val = 0;
            for (int j = 0; j < curr_op.op_values.Count; j++)
            {
                res_val += curr_op.op_values[j];
            }
            Console.WriteLine(check_val + ": from Sum() -> " + res_val);
            if (res_val == curr_op.result_value)
            {
                Console.WriteLine("found a valid op");
                valid_ops.Add(curr_op);
            }

            // check for a straight '*'
            res_val = 1;
            for (int j = 0; j < curr_op.op_values.Count; j++)
            { 
                res_val *= curr_op.op_values[j];
            }
            Console.WriteLine(check_val + ": from Mult() -> " + res_val);
            if (res_val == curr_op.result_value)
            {
                Console.WriteLine("found a valid op");
                valid_ops.Add(curr_op);
            }

            // more checks here
            // try a (* and +) and (+ and *)
        }
    }

    public void Print()
    {
        for (int i = 0; i < all_ops.Count; i++)
        {
            Console.Write("result " + all_ops[i].result_value + ": ");
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