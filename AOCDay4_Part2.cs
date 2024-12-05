/**
  
 
                    _\/_
                     /\
                     /\
                    /  \
                    /~~\o
                   /o   \
                  /~~*~~~\
                 o/    o \
                 /~~~~~~~~\~`
                /__*_______\
                     ||
                   \====/
                    \__/

 
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class AOCDay4_Part2
{
    private string inputString;
    private string searchStr;
    private string searchStrRev;
    private int total;
    private int rows;
    private int cols;
    private char[,] the_map;
    private char[,] map_copy;
    private List<(int row, int col)> dims;

    public AOCDay4_Part2()
    {
        LoadInputString();
        searchStr = "MAS";
        searchStrRev = "SAM";
        total = 0;
    }

    private void LoadInputString()
    {
        foreach (string line in File.ReadAllLines("C:\\Users\\jglatts\\Documents\\AOC24\\input_day4.txt"))
        {
            if (line.Length > 0 && !string.IsNullOrEmpty(line))
            {
                Console.WriteLine(line);
                inputString += line + "\n";
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

        for (int i = 0; i < rows; i++)
        {
            if (i == (rows - 1))
                break;

            for (int j = 0; j < cols; j++)
            {
                dims = new List<(int row, int col)>();
                if (the_map[i, j] == searchStrRev[0])
                {
                    checkReversedStr(i, j);
                }

                else if (the_map[i, j] == searchStr[0])
                {
                    checkRegStr(i, j);  
                }
            }
        }
        
        printFindings();
    }

    private bool checkReversedStr(int i, int j)
    {
        dims.Add((i, j));
        bool is_valid = true;
        int copy_i = i + 1;
        int copy_j = j + 1;
        int copy_i_right = 0;
        int copy_j_right = 0;
        int k;

        for (k = 1; k < searchStrRev.Length; k++)
        {
            if (copy_i >= (rows - 1) || copy_j >= cols)
            {
                return false;
            }
            else if (searchStrRev[k] != the_map[copy_i, copy_j])
            {
                return false;
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
            copy_i_right = i + 1;
            copy_j_right = j + 2;

            if (i >= (rows - 1) || copy_i_right >= (rows - 1) || copy_j_right >= cols)
                return false;

            if (the_map[i, copy_j_right] == searchStrRev[0])
            {
                dims.Add((i, copy_j_right));
                copy_j_right--;
                bool is_valid_right = true;
                for (k = 1; k < searchStrRev.Length; k++)
                {
                    if (copy_j_right < 0 || copy_i_right < 0 || copy_i_right >= (rows - 1) || copy_j_right >= cols)
                    {
                        is_valid_right = false;
                        break;
                    }
                    if (the_map[copy_i_right, copy_j_right] != searchStrRev[k])
                    {
                        is_valid_right = false;
                        break;
                    }
                    else
                    {
                        dims.Add((copy_i_right, copy_j_right));
                        copy_i_right++;
                        copy_j_right--;
                    }
                }
                if (is_valid_right)
                {
                    int idx = 0;
                    total += 1;
                    for (k = 0; k < dims.Count; k++)
                    {
                        if (idx < searchStrRev.Length)
                            map_copy[dims[k].row, dims[k].col] = searchStrRev[idx++];
                        else
                        {
                            idx = 0;
                            map_copy[dims[k].row, dims[k].col] = searchStrRev[idx++];
                        }
                    }
                    dims.Clear();
                }
            }
            else if (the_map[i, copy_j_right] == searchStr[0])
            {
                dims.Add((i, copy_j_right));
                copy_j_right--;
                bool is_valid_2 = true;
                for (k = 1; k < searchStr.Length; k++)
                {
                    if (copy_j_right < 0 || copy_i_right < 0 || copy_i_right >= (rows - 1) || copy_j_right >= cols)
                    {
                        is_valid_2 = false;
                        break;
                    }
                    if (the_map[copy_i_right, copy_j_right] != searchStr[k])
                    {
                        is_valid_2 = false;
                        break;
                    }
                    else
                    {
                        dims.Add((copy_i_right, copy_j_right));
                        copy_i_right++;
                        copy_j_right--;
                    }
                }
                if (is_valid_2)
                {
                    int idx = 0;
                    total += 1;
                    for (k = 0; k < dims.Count; k++)
                    {
                        if (idx < searchStr.Length)
                            map_copy[dims[k].row, dims[k].col] = searchStr[idx++];
                        else
                        {
                            idx = 0;
                            map_copy[dims[k].row, dims[k].col] = searchStr[idx++];
                        }
                    }
                    dims.Clear();
                }
            }
        }
        return true;
    }
    

    private bool checkRegStr(int i, int j)
    {
        dims.Add((i, j));
        bool is_valid = true;
        int copy_i = i + 1;
        int copy_j = j + 1;
        int copy_i_right = 0;
        int copy_j_right = 0;
        int k;

        for (k = 1; k < searchStr.Length; k++)
        {
            if (copy_i >= (rows - 1) || copy_j >= cols)
            {
                return false;
            }
            else if (searchStr[k] != the_map[copy_i, copy_j])
            {
                return false;
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
            copy_i_right = i + 1;
            copy_j_right = j + 2;
            if (i >= (rows - 1) || copy_i_right >= (rows - 1) || copy_j_right >= cols)
                return false;

            if (the_map[i, copy_j_right] == searchStr[0])
            {
                dims.Add((i, copy_j_right));
                copy_j_right--;
                bool is_valid_right = true;
                for (k = 1; k < searchStr.Length; k++)
                {
                    if (copy_j_right < 0 || copy_i_right < 0 || copy_i_right >= (rows - 1) || copy_j_right >= cols)
                    {
                        is_valid_right = false;
                        break;
                    }
                    if (the_map[copy_i_right, copy_j_right] != searchStr[k])
                    {
                        is_valid_right = false;
                        break;
                    }
                    else
                    {
                        dims.Add((copy_i_right, copy_j_right));
                        copy_i_right++;
                        copy_j_right--;
                    }
                }
                if (is_valid_right)
                {
                    int idx = 0;
                    total += 1;
                    for (k = 0; k < dims.Count; k++)
                    {
                        if (idx < searchStr.Length)
                            map_copy[dims[k].row, dims[k].col] = searchStr[idx++];
                        else
                        {
                            idx = 0;
                            map_copy[dims[k].row, dims[k].col] = searchStr[idx++];
                        }
                    }
                    dims.Clear();
                }
            }

            else if (the_map[i, copy_j_right] == searchStrRev[0])
            {
                dims.Add((i, copy_j_right));
                copy_j_right--;
                bool is_valid_2 = true;
                for (k = 1; k < searchStrRev.Length; k++)
                {
                    if (copy_j_right < 0 || copy_i_right < 0 || copy_i_right >= (rows - 1) || copy_j_right >= cols)
                    {
                        is_valid_2 = false;
                        break;
                    }
                    if (the_map[copy_i_right, copy_j_right] != searchStrRev[k])
                    {
                        is_valid_2 = false;
                        break;
                    }
                    else
                    {
                        dims.Add((copy_i_right, copy_j_right));
                        copy_i_right++;
                        copy_j_right--;
                    }
                }
                if (is_valid_2)
                {
                    int idx = 0;
                    total += 1;
                    for (k = 0; k < dims.Count; k++)
                    {
                        if (idx < searchStrRev.Length)
                            map_copy[dims[k].row, dims[k].col] = searchStrRev[idx++];
                        else
                        {
                            idx = 0;
                            map_copy[dims[k].row, dims[k].col] = searchStrRev[idx++];
                        }
                    }
                    dims.Clear();
                }
            }

        }
        return true;
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
        AOCDay4_Part2 a = new AOCDay4_Part2();
        a.Solve();
    }

}
