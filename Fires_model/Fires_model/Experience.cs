using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace проба_json
{
    class Experience
    {
        int id_exp;
        int columns, rows, stages;
        int time;
        int curr_time = 0;
        Iteration[] iter;

        public DataGridView dg;

        public Experience(int w, int h)
        {
            columns = w;
            rows = h;

        }
        public int CurrTime
        {
            set
            {
                curr_time = value;
                iter[value] = new Iteration(columns, rows);
            }
            get { return curr_time; }
        }
        public int Time
        {
            set
            {
                time = value;
                iter = new Iteration[time];
            }
            get { return time; }
        }

        public int W
        {
            get
            {
                return columns;
            }
        }
        public int H
        {
            get { return rows; }
        }
        public int Id
        {
            set { id_exp = value; }
            get { return id_exp; }
        }

        public void Add_iteration(int t)
        {
            iter[t] = new Iteration(columns, rows);
            iter[t].iteration = t;
        }

        public int Get_cell(int it, int x, int y)
        {
            return iter[it].Get_cell(x, y);
        }

        public void Add_cell(int it, int x, int y, int c)
        {
            iter[it].Add_cell(x, y, c, time);
        }

        public void Update_cell(int it, int x, int y, int c)
        {
            iter[it].cells[x, y].stage = c;
        }

        public void Change_stage()
        {
            curr_time++;
            Add_iteration(curr_time);
            iter[curr_time].Change_iteration(iter[curr_time - 1], time);
        }

        public bool Comparison(int t1, int t2)
        {
            if (iter[t1].cells.Equals(iter[t2].cells))
                return true;
            else return false;
        }
    }
}
