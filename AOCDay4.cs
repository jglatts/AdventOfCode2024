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
            if (line.Length > 0 && !string.IsNullOrEmpty(line))
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
        if (the_map[i, j] == checkStr[0])
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

        // check for horiztonal and vertical matches
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                doCheckHorz(i, j, searchStr);
                doCheckHorz(i, j, searchStrRev);
                doCheckVert(i, j, searchStr);
                doCheckVert(i, j, searchStrRev);
            }
        }
        diagLookUpNorm();    
        diagLookUpRev();    
        printFindings();
    }


    private void diagLookUpRev()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (the_map[i, j] == searchStrRev[0])
                {
                    int copy_i = i + 1;
                    int copy_j = j + 1;
                    bool is_valid = true;
                    List<(int row, int col)> dims = new List<(int row, int col)>();
                    dims.Add((i, j));
                    for (int k = 1; k < searchStrRev.Length; k++)
                    {
                        if (copy_i >= rows || copy_j >= cols)
                        {
                            is_valid = false;
                            break;
                        }
                        if (searchStrRev[k] != the_map[copy_i, copy_j])
                        {
                            is_valid = false;
                            break;
                        }
                        else
                        {
                            dims.Add((copy_i, copy_j));
                            copy_i++;
                            copy_j++;
                        }
                    }
                    if (is_valid)
                    {
                        int idx = 0;
                        for (int k = 0; k < dims.Count; k++)
                        {
                            map_copy[dims[k].row, dims[k].col] = searchStrRev[idx++];
                        }
                        total += 1;
                    }

                    int copy_i_left = i + 1;
                    int copy_j_left = j - 1;
                    bool is_valid_left = true;
                    List<(int row, int col)> dims_left = new List<(int row, int col)>();
                    dims_left.Add((i, j));
                    for (int k = 1; k < searchStrRev.Length; k++)
                    {
                        if (copy_i_left >= rows || copy_j_left < 0 || copy_j_left >= cols)
                        {
                            is_valid_left = false;
                            break;
                        }
                        if (searchStrRev[k] != the_map[copy_i_left, copy_j_left])
                        {
                            is_valid_left = false;
                            break;
                        }
                        else
                        {
                            dims_left.Add((copy_i_left, copy_j_left));
                            copy_i_left++;
                            copy_j_left--;
                        }
                    }
                    if (is_valid_left)
                    {
                        int k = 0;
                        int idx = 0;
                        for (k = 0; k < dims_left.Count; k++)
                        {
                            map_copy[dims_left[k].row, dims_left[k].col] = searchStrRev[idx++];
                        }
                        total += 1;
                    }
                }
            }
        }
    }

    private void diagLookUpNorm()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (the_map[i, j] == searchStr[0])
                {
                    int copy_i = i + 1;
                    int copy_j = j + 1;
                    bool is_valid = true;
                    List<(int row, int col)> dims = new List<(int row, int col)>();
                    dims.Add((i, j));
                    for (int k = 1; k < searchStr.Length; k++)
                    {
                        if (copy_i >= rows || copy_j >= cols)
                        {
                            is_valid = false;
                            break;
                        }
                        if (searchStr[k] != the_map[copy_i, copy_j])
                        {
                            is_valid = false;
                            break;
                        }
                        else
                        {
                            dims.Add((copy_i, copy_j));
                            copy_i++;
                            copy_j++;
                        }
                    }
                    if (is_valid)
                    {
                        int idx = 0;
                        for (int k = 0; k < dims.Count; k++)
                        {
                            map_copy[dims[k].row, dims[k].col] = searchStr[idx++];
                        }
                        total += 1;
                    }

                    int copy_i_left = i + 1;
                    int copy_j_left = j - 1;
                    bool is_valid_left = true;
                    List<(int row, int col)> dims_left = new List<(int row, int col)>();
                    dims_left.Add((i, j));
                    for (int k = 1; k < searchStr.Length; k++)
                    {
                        if (copy_i_left >= rows || copy_j_left < 0 || copy_j_left >= cols)
                        {
                            is_valid_left = false;
                            break;
                        }
                        if (searchStr[k] != the_map[copy_i_left, copy_j_left])
                        {
                            is_valid_left = false;
                            break;
                        }
                        else
                        {
                            dims_left.Add((copy_i_left, copy_j_left));
                            copy_i_left++;
                            copy_j_left--;
                        }
                    }
                    if (is_valid_left)
                    {
                        int idx = 0;
                        for (int k = 0; k < dims_left.Count; k++)
                        {
                            map_copy[dims_left[k].row, dims_left[k].col] = searchStr[idx++];
                        }
                        total += 1;
                    }
                }
            }
        }
    }

    public void printFindings()
    {
        Console.WriteLine(total);
        for (int i = 0; i < rows; i++)
        {
            string str = "row #" + i + ") ";
            Console.Write(str.PadRight(15, ' '));
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
