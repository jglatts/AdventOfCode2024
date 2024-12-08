using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class AOCDay6
{
    private string inputString;
    private List<List<char>> the_map;
    private readonly char obstruction;
    private readonly char guard;
    private List<(int row, int col)> obs_positions;
    private (int row, int col) guard_position;
    
    public enum GuardDirection { 
        UP,
        DOWN,
        LEFT,
        RIGHT,
    };

    public AOCDay6()
    {
        the_map = new List<List<char>>();
        obs_positions = new List<(int row, int col)>();
        obstruction = '#';
        guard = '^';
        LoadInputString();
    }

    private void LoadInputString()
    {
        inputString = "";
        string[] all_lines = File.ReadAllLines("..\\inputs\\test_input6.txt");
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
                    Console.Write(line[j] + " ");
                    temp_list.Add(line[j]);
                }
                the_map.Add(temp_list);
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        Console.WriteLine("rows " + the_map.Count + ", cols " + the_map[0].Count);
        Console.WriteLine("guard pos " + guard_position.row + ", " + guard_position.col);
    }

    public void Solve()
    {
        /*
            will need to employ a graph searh (DFS|BFS) 
         */
        (int row, int col) curr_guard_pos = guard_position;
        bool obs_check = false;
        int row_above = curr_guard_pos.row - 1;
        if (row_above < 0)
        {
            // will need to do more here
            Console.Write("guard is at row 0");
            return;
        }

        drawMap(curr_guard_pos);
        while (!obs_check)
        {
            char check = the_map[row_above][curr_guard_pos.col];
            if (check != obstruction)
            {
                row_above--;
                if (row_above < 1)
                {
                    Console.Write("guard is at row " + row_above);
                    guard_position.row = row_above + 1;
                    break;
                }
                drawMap((row_above, curr_guard_pos.col));
            }
            else
            { 
                obs_check = true;
            }
        }

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
        Console.WriteLine("guard pos " + curr_guard_pos.row + ", " + curr_guard_pos.col);
        the_map[curr_guard_pos.row][curr_guard_pos.col] = '^';

        for (int i = 0; i < the_map.Count; i++)
        {
            for (int j = 0; j < the_map[i].Count; j++)
            {
                Console.Write(the_map[i][j] + " ");
            }
            Console.WriteLine();
        }

        Console.WriteLine();
    }


    public void Print()
    {
        Console.WriteLine(inputString);
    }

    public static void Main(string[] args)
    {
        AOCDay6 a = new AOCDay6();
        a.Solve();
    }

}
