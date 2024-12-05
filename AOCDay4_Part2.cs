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
        foreach (string line in File.ReadAllLines("C:\\Users\\jglatts\\Documents\\AOC24\\test_input4.txt"))
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


        /*
         * 
         *          need to find diagnoal matches, ie.
         *              S X S
         *                A
         *              M   M 
         *              
         *               or
         * 
         *              M X S
         *                A
         *              M   S 
         * 
         *          or any other combinations of MAS|SAM
         */

        for (int i = 0; i < rows; i++)
        {
            if (i == (rows - 1))
                break;
            //Console.WriteLine("solving");
            for (int j = 0; j < cols; j++)
            {
                dims = new List<(int row, int col)>();
                if (the_map[i, j] == searchStrRev[0])
                {
                    dims.Add((i, j));
                    bool is_valid = true;
                    int copy_i = i + 1;
                    int copy_j = j + 1; 
                    int copy_i_right = 0;
                    int copy_j_right = 0;
                    int k;
                    // see if we have a diag match
                    for (k = 1; k < searchStrRev.Length; k++)
                    {
                        if (copy_i >= (rows-1) || copy_j >= cols)
                        {
                            is_valid = false;
                            break;
                        }
                        else if (searchStrRev[k] != the_map[copy_i, copy_j])
                        {
                            is_valid = false;
                            break;
                        }
                        else {
                            dims.Add((copy_i, copy_j));
                            copy_i++;
                            copy_j++;
                        }
                    }

                    if (is_valid)
                    {
                        // found a match on ride side, check left
                        copy_i_right = i + 1;
                        copy_j_right = j + 2;

                        try
                        {
                            if (i >= (rows-1) || copy_i_right >= (rows - 1) || copy_j_right >= cols)
                                break;

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
                                }
                            }
                        
                        }
                        catch (Exception ex)
                        {
                            string err_str = "dims: " + dims[k].row + ", " + dims[k].col + "\n";
                            err_str += "copy-left " + copy_i + ", " + copy_j + "\n";
                            err_str += "copy-right " + copy_i_right + ", " + copy_j_right + "\n";
                            Console.WriteLine("failed in is_valid 2nd check\n" + err_str +  ex.Message + "\n" + ex.StackTrace);
                        }
                    }
                }
            }
        }
        
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

                else if (the_map[i, j] == searchStr[0])
                { 
                    // more checking, ooo what fun!
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
        AOCDay4_Part2 a = new AOCDay4_Part2();
        a.Solve();
    }

}
