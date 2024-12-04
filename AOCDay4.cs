using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class AOCDay4
{
    private string inputString;
    private string searchStr;
    private string searchStrRev;
    private int total;
    private int rows;
    private int cols;
    private char[,] the_map;
    private char[,] map_copy;

    public enum DIRECTION 
    {
        HORIZONTAL,
        VERTICAL
    };

    public AOCDay4()
    {
        LoadInputString();
        searchStr = "XMAS";
        searchStrRev = "SAMX";
        total = 0;
    }

    private void LoadInputString()
    {
        foreach (string line in File.ReadAllLines("C:\\Users\\jglatts\\Documents\\AOC24\\input_day4.txt"))
        {
            if (line.Length > 0 && !line.Equals(" "))
                inputString += line + "\n";
        }
    }

    private void doCheckHorz(int i, int j, string checkStr)
    {
        if (the_map[i, j] == checkStr[0])
        {
            bool is_valid = true;
            int copy = j + 1;
            List<(int row, int col)> dims = new List<(int row, int col)>();
            dims.Add((i, j));
            for (int k = 1; k < checkStr.Length; k++)
            {
                if (copy >= cols)
                {
                    is_valid = false;
                    break;
                }
                if (the_map[i, copy] != checkStr[k])
                {
                    is_valid = false;
                    break;
                }
                else
                {
                    dims.Add((i, copy));
                    copy++;
                }
            }
            if (is_valid)
            {
                int idx = 0;
                for (int k = 0; k < dims.Count; k++)
                {
                    map_copy[dims[k].row, dims[k].col] = checkStr[idx++];
                }
                total += 1;
            }
        }
    }

    private void doCheckVert(int i, int j, string checkStr)
    {
        if (the_map[i, j] == searchStr[0])
        {
            int copy = i + 1;
            bool is_valid = true;
            List<(int row, int col)> dims = new List<(int row, int col)>();
            dims.Add((i, j));
            for (int k = 1; k < checkStr.Length; k++)
            {
                if (copy >= rows)
                {
                    is_valid = false;
                    break;
                }
                if (the_map[copy, j] != checkStr[k])
                {
                    is_valid = false;
                    break;
                }
                else
                {
                    dims.Add((copy, j));
                    copy++;
                }
            }
            if (is_valid)
            {
                int idx = 0;
                for (int k = 0; k < dims.Count; k++)
                {
                    map_copy[dims[k].row, dims[k].col] = checkStr[idx++];
                }
                total += 1;
            }
        }
    }

    public void Solve()
    {
        string[] vals = inputString.Split('\n');
        rows = vals.Length;
        cols = vals[0].Length;
        Console.WriteLine("rows " + rows + " - cols " + cols);
        the_map = new char[rows, cols];
        map_copy = new char[rows, cols];

        // fill the 2D map
        for (int i = 0; i < vals.Length; i++)
        {
            for (int j = 0; j < vals[i].Length; j++)
            {
                the_map[i, j] = vals[i][j];
                map_copy[i, j] = '.';
            }
        }

        // check for horiztonal matches
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                doCheckHorz(i, j, searchStr);
                doCheckHorz(i, j, searchStrRev);
            }
        }

        // check vertical matches
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                doCheckVert(i, j, searchStr);
                doCheckVert(i, j, searchStrRev);
            }
        }

        Console.WriteLine(total);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(map_copy[i, j]);
            }
            Console.WriteLine("");
        }
    }

    public void Print()
    {
        Console.WriteLine(inputString);
    }

    public static void Main(string[] args)
    {
        AOCDay4 a = new AOCDay4();
        a.Solve();
    }

}
