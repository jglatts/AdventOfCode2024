﻿using System;
using System.IO;
using System.Collections.Generic;

public enum GuardDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
};

public class Vertex
{
    public int row;
    public int col;
}

public class AOCDay6
{

    private string inputString;
    private List<List<char>> the_map;
    private readonly char obstruction;
    private readonly char guard;
    private List<(int row, int col)> obs_positions;
    private List<(int row, int col)> marked_positions;
    private Dictionary<Vertex, int> seen_amount;
    private (int row, int col) guard_position;
    private GuardDirection guard_direction;

    public AOCDay6()
    {
        the_map = new List<List<char>>();
        obs_positions = new List<(int row, int col)>();
        marked_positions = new List<(int row, int col)>();
        seen_amount = new Dictionary<Vertex, int>();
        obstruction = '#';
        guard = '^';
        guard_direction = GuardDirection.UP;
        LoadInputString();
    }

    private void loadMap()
    {
        string real_in = "..\\inputs\\input_day6.txt";
        string test_in = "..\\inputs\\test_input6.txt";
        //string[] all_lines = File.ReadAllLines(test_in);
        string[] all_lines = File.ReadAllLines(real_in);
        for (int i = 0; i < all_lines.Length; i++)
        {
            try
            {
                List<char> temp_list = new List<char>();
                string line = all_lines[i];
                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j] == guard)
                    {
                        guard_position = (i, j);
                    }
                    else if (line[j] == obstruction)
                    {
                        obs_positions.Add((i, j));
                    }
                    temp_list.Add(line[j]);
                }
                the_map.Add(temp_list);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }

    private void LoadInputString()
    {
        inputString = "";
        loadMap();
        Console.WriteLine("rows " + the_map.Count + ", cols " + the_map[0].Count);
        Console.WriteLine("guard pos " + guard_position.row + ", " + guard_position.col + "\n");
    }

    public void Solve()
    {
        (int row, int col) curr_guard_pos = guard_position;
        bool obs_check = false;
        bool is_done = false;
        bool is_in_final_check = false;
        int row_above = curr_guard_pos.row - 1;
        if (row_above < 0)
        {
            // will need to do more here
            Console.Write("guard is at row 0 - errr");
            return;
        }

        //drawMap(curr_guard_pos);
        marked_positions.Add((curr_guard_pos.row, curr_guard_pos.col));
        
        // loop detection is somewhat working
        // the reported value (5469) is too-hi
        // 
        while (!is_done)
        {
            while (!obs_check)
            {
                if (row_above >= the_map.Count)
                {
                    Console.WriteLine("err with row_above" + row_above);
                    //drawMap((row_above - 1, curr_guard_pos.col));
                    is_done = true;
                    break;
                }
                if (curr_guard_pos.col >= the_map[0].Count || curr_guard_pos.col < 0)
                {
                    Console.WriteLine("err with col_pos" + row_above);
                    is_done = true;
                    break;
                }
                char check = the_map[row_above][curr_guard_pos.col];
                if (check != obstruction)
                {
                    if (guard_direction == GuardDirection.UP)
                    {
                        //drawMap((row_above, curr_guard_pos.col));
                        if (!marked_positions.Contains((row_above, curr_guard_pos.col)))
                            marked_positions.Add((row_above, curr_guard_pos.col));

                        if (addToMap(row_above, curr_guard_pos.col))
                        {   
                            // loop deteced
                            is_done= true;
                            break;
                        }
                        if (row_above == 0)
                        { 
                            curr_guard_pos.col++;
                            //row_above++;
                            guard_direction = GuardDirection.RIGHT; 
                            break;
                        }
                        row_above--;
                    }
                    else if (guard_direction == GuardDirection.DOWN)
                    {
                        if (row_above >= the_map.Count)
                        {
                            Console.WriteLine("guard is at row " + row_above);
                            guard_position.row = row_above - 1;
                            break;
                        }
                        if (!marked_positions.Contains((row_above, curr_guard_pos.col)))
                            marked_positions.Add((row_above, curr_guard_pos.col));

                        if (addToMap(row_above, curr_guard_pos.col))
                        {
                            // loop deteced
                            is_done = true;
                            break;
                        }
                        //drawMap((row_above, curr_guard_pos.col));
                        row_above++;
                    }
                    else if (guard_direction == GuardDirection.LEFT)
                    {
                        if (curr_guard_pos.col < 0)
                        {
                            Console.WriteLine("error curr_guard == 0 and directoin == lefts");
                            return;
                        }
                        if (!marked_positions.Contains((row_above, curr_guard_pos.col)))
                            marked_positions.Add((row_above, curr_guard_pos.col));

                        if (addToMap(row_above, curr_guard_pos.col))
                        {
                            // loop deteced
                            
                            is_done = true;
                            break;
                        }
                        //drawMap((row_above, curr_guard_pos.col));
                        curr_guard_pos.col -= 1;
                    }
                    else if (guard_direction == GuardDirection.RIGHT)
                    {
                        if (!marked_positions.Contains((row_above, curr_guard_pos.col)))
                            marked_positions.Add((row_above, curr_guard_pos.col));

                        if (addToMap(row_above, curr_guard_pos.col))
                        {
                            // loop deteced
                            is_done = true;
                            break;
                        }
                        if (curr_guard_pos.col >= the_map[0].Count)
                        {
                            Console.WriteLine("error curr_guard");
                            curr_guard_pos.col--;
                            obs_check = true;
                            is_done = true;
                        }
                        //drawMap((row_above, curr_guard_pos.col));
                        curr_guard_pos.col += 1;
                    }
                }
                else
                {
                    // found an obstruction, check which direction to change to 
                    //Console.WriteLine("found an obstruction and guard is at " + guard_direction);
                    //drawMap((row_above, curr_guard_pos.col));
                    if (is_in_final_check)
                    {
                        is_done = true;
                        break;
                    }
                    if (guard_direction == GuardDirection.DOWN && row_above == (the_map.Count))
                    {
                        // this is the break condition, we are in DOWN state and last row
                        Console.WriteLine("in final step!");
                        is_in_final_check = true;
                    }
                    if (guard_direction == GuardDirection.UP)
                    {
                        guard_direction = GuardDirection.RIGHT;
                        guard_position.col += 1;
                        curr_guard_pos.col += 1;
                        row_above++;
                    }
                    else if (guard_direction == GuardDirection.RIGHT)
                    {
                        guard_direction = GuardDirection.DOWN;
                        row_above++;
                        curr_guard_pos.col--;
                    }
                    else if (guard_direction == GuardDirection.DOWN)
                    {
                        guard_direction = GuardDirection.LEFT;
                        curr_guard_pos.col--;
                        row_above--;
                    }
                    else
                    {
                        guard_direction = GuardDirection.UP;
                        row_above--;
                        curr_guard_pos.col++;
                    }
                }
            }
        }

        if (row_above == the_map.Count)
            row_above--;
        else if (row_above < 0)
            row_above = 0;
        if (curr_guard_pos.col == the_map[0].Count)
            curr_guard_pos.col--;
        else if (curr_guard_pos.col < 0)
            curr_guard_pos.col = 0;

        drawMap((row_above, curr_guard_pos.col));
        string final_str = "guard visited " + marked_positions.Count + " distinct spots" + " finished at (" + row_above + ", " + curr_guard_pos.col + ")";
        Console.WriteLine(final_str);
    }

    public bool addToMap(int row_above, int col)
    {
        Vertex v = new Vertex() { row = row_above, col = col };
        Vertex key = null;
        bool ret = false;
        
        if (mapContainsVertex(v))
        {
            key = getMapKey(v);
            if (seen_amount[key] > 2)
            {
                Console.WriteLine("loop detected! (" + row_above + ", " + col + ") has been seen " + seen_amount[key] + " times");
                ret = true;
            }
            seen_amount[getMapKey(v)] += 1;
        }
        else
        {
            seen_amount.Add(v, 1);
        }

        if (key != null)
            Console.WriteLine("(" + row_above + ", " + col + ") has been seen " + seen_amount[key] + " times");
        
        return ret;
    }

    public Vertex getMapKey(Vertex v)
    {
        foreach (var item in seen_amount)
        {
            Vertex check = item.Key;
            if (check.row == v.row && check.col == v.col)
                return check;
        }
        return null;
    }

    public bool mapContainsVertex(Vertex v)
    {
        foreach (var item in seen_amount)
        {
            Vertex check = item.Key;
            if (check.row == v.row && check.col == v.col)
                return true;
        }

        return false;
    }

    public void drawMap((int row, int col) curr_guard_pos)
    {
        for (int i = 0; i < the_map.Count; i++)
        {
            for (int j = 0; j < the_map[i].Count; j++)
            {
                the_map[i][j] = '.';
            }
        }
        for (int i = 0; i < obs_positions.Count; i++)
        {
            the_map[obs_positions[i].row][obs_positions[i].col] = '#';
        }
        for (int i = 0; i < marked_positions.Count; i++)
        {
            the_map[marked_positions[i].row][marked_positions[i].col] = 'X';
        }

        Console.WriteLine("guard pos " + curr_guard_pos.row + ", " + curr_guard_pos.col);

        if (guard_direction == GuardDirection.UP)
            the_map[curr_guard_pos.row][curr_guard_pos.col] = '^';
        else if (guard_direction == GuardDirection.RIGHT)
            the_map[curr_guard_pos.row][curr_guard_pos.col] = '>';
        else if (guard_direction == GuardDirection.LEFT)
            the_map[curr_guard_pos.row][curr_guard_pos.col] = '<';
        else if (guard_direction == GuardDirection.DOWN)
            the_map[curr_guard_pos.row][curr_guard_pos.col] = 'v';

        for (int i = 0; i < the_map.Count; i++)
        {
            string s = "row (" + i + ")";
            Console.Write(s.PadRight(10, ' '));
            for (int j = 0; j < the_map[i].Count; j++)
            {
                Console.Write(the_map[i][j] + "");
            }
            Console.WriteLine();
        }

        /*
        foreach (var items in seen_amount)
        { 
            Console.WriteLine(" (" + items.Key.row + ", " + items.Key.col + ") has been seen " + items.Value + " times");
        }
        */

        Console.WriteLine();
    }


    public void Print()
    {
        Console.WriteLine(inputString);
    }

    public static void Main(string[] args)
    {
        AOCDay6 a = new AOCDay6();
        var watch = System.Diagnostics.Stopwatch.StartNew();
        a.Solve();
        watch.Stop();
        Console.WriteLine("\n\nsolved in " +  + (watch.ElapsedMilliseconds) + "ms\n" + (watch.ElapsedMilliseconds/1000) + "sec");
    }

}