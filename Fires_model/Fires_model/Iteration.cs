using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace проба_json
{
    class Iteration
    {
        public int iteration;
        public int burnt, unburnt, burning, trance;
        public Cells[,] cells;
        public Iteration(int x, int y)
        {
            cells = new Cells[x, y];

        }

        public void Add_cell(int x, int y, int c, int time_all)
        {
            cells[x, y] = new Cells(x, y, c);
            cells[x, y].time_all = time_all;
            switch (c)
            {
                case 0:
                    unburnt++;
                    break;
                case 1:
                    burning++;
                    break;
                case 2:
                    burning++;
                    break;
                case 3:
                    burning++;
                    break;
                case 4:
                    burnt++;
                    break;
            }
        }

        public int Get_cell(int x, int y)
        {
            return cells[x, y].stage;
        }

        public void Change_iteration(Iteration past_iter, int t)
        {
            int qx = cells.GetLength(0);
            int qy = cells.GetLength(1);
            Parallel.For(0, qx, (i) =>
            {
                Parallel.For(0, qy, (j) =>
                {
                    //    for (int i=0; i<qx; i++)
                    //{
                    //    for (int j = 0; j < qy; j++)
                    //    { 

                    int[] neib = past_iter.Get_neibs(i, j);
                    Add_cell(i, j, 0, t);
                    cells[i, j].stage = past_iter.cells[i, j].Change_stage(neib);
                    switch (cells[i, j].stage)
                    {
                        case 0:
                            unburnt++;
                            break;
                        case 1:
                            burning++;
                            break;
                        case 2:
                            burning++;
                            break;
                        case 3:
                            burning++;
                            break;
                        case 4:
                            burnt++;
                            break;
                    }

                    if (cells[i, j].stage != past_iter.cells[i, j].stage)
                        trance++;
                    else
                        cells[i, j].time = past_iter.cells[i, j].time;
                });

            });
        }

        int[] Get_neibs(int i, int j)
        {
            int qx = cells.GetLength(0);
            int qy = cells.GetLength(1);
            int[] neib = { -1, -1, -1, -1, -1, -1, -1, -1 };
            if (i == 0 && j != qy - 1 && j != 0)
            {
                neib[1] = cells[i + 1, j].stage;
                neib[2] = cells[i, j - 1].stage;
                neib[3] = cells[i, j + 1].stage;
                neib[6] = cells[i + 1, j - 1].stage;
                neib[7] = cells[i + 1, j + 1].stage;
            }
            else if (i != 0 && i != qx - 1 && j == 0)
            {
                neib[0] = cells[i - 1, j].stage;
                neib[1] = cells[i + 1, j].stage;
                neib[3] = cells[i, j + 1].stage;
                neib[5] = cells[i - 1, j + 1].stage;
                neib[7] = cells[i + 1, j + 1].stage;
            }
            else if (i == qx - 1 && j != qy - 1 && j != 0)
            {
                neib[0] = cells[i - 1, j].stage;
                neib[2] = cells[i, j - 1].stage;
                neib[3] = cells[i, j + 1].stage;
                neib[4] = cells[i - 1, j - 1].stage;
                neib[5] = cells[i - 1, j + 1].stage;

            }
            else if (i != 0 && i != qx - 1 && j == qy - 1)
            {
                neib[0] = cells[i - 1, j].stage;
                neib[1] = cells[i + 1, j].stage;
                neib[2] = cells[i, j - 1].stage;
                neib[4] = cells[i - 1, j - 1].stage;
                neib[6] = cells[i + 1, j - 1].stage;

            }
            else if (i == 0 && j == 0)
            {
                neib[1] = cells[i + 1, j].stage;
                neib[3] = cells[i, j + 1].stage;

                neib[7] = cells[i + 1, j + 1].stage;

            }
            else if (i == 0 && j == qy - 1)
            {
                neib[1] = cells[i + 1, j].stage;
                neib[2] = cells[i, j - 1].stage;

                neib[6] = cells[i + 1, j - 1].stage;

            }
            else if (i == qx - 1 && j == 0)
            {
                neib[0] = cells[i - 1, j].stage;
                neib[1] = -1;
                neib[2] = -1;
                neib[3] = cells[i, j + 1].stage;
                neib[4] = -1;
                neib[5] = cells[i - 1, j + 1].stage;
                neib[6] = -1; neib[7] = -1;
            }
            else if (i == qx - 1 && j == qy - 1)
            {
                neib[0] = cells[i - 1, j].stage;
                neib[2] = cells[i, j - 1].stage;
                neib[4] = cells[i - 1, j - 1].stage;

            }
            else
            {
                neib[0] = cells[i - 1, j].stage; neib[1] = cells[i + 1, j].stage;
                neib[2] = cells[i, j - 1].stage; neib[3] = cells[i, j + 1].stage;
                neib[4] = cells[i - 1, j - 1].stage; neib[5] = cells[i - 1, j + 1].stage;
                neib[6] = cells[i + 1, j - 1].stage; neib[7] = cells[i + 1, j + 1].stage;
            }
            return neib;
        }
    }
}
