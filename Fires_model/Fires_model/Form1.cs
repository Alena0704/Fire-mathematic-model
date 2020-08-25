using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace проба_json
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Experience exp;
        DataGridView gridView;

        private void runExperimentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int qx = Convert.ToInt32(w.Text) / Convert.ToInt32(dw.Text);
            int qy = Convert.ToInt32(h.Text) / Convert.ToInt32(dh.Text);
            if (exp != null)
                gridView.Dispose();
            exp = new Experience(qx, qy);
            exp.Time = Convert.ToInt32(time.Text);
            DrawTable(qx, qy);
        }
        void DrawTable(int qx, int qy)
        {
            gridView = new DataGridView();
            int width_wind = this.Width - 30;
            int height_wind = this.Height - 45;
            gridView.AllowUserToAddRows = false;
            gridView.AllowUserToResizeColumns = false;
            gridView.AllowUserToResizeRows = false;
            gridView.ColumnHeadersVisible = false;
            gridView.RowHeadersVisible = false;
            gridView.Width = 20 * qx + 5;
            gridView.Height = 20 * qy + 5;
            gridView.Top = 30;
            gridView.Left = 15;
            for (int i = 0; i < qy; i++)
            {
                gridView.Columns.Add(i.ToString(), i.ToString());
                gridView.Columns[i].Width = width_wind / qx;
            }
            for (int j = 0; j < qx; j++)
            {
                gridView.Rows.Add();
                gridView.Rows[j].Height = height_wind / qy;
            }
            gridView.Width = width_wind / qx * qx + 5;
            gridView.Height = height_wind / qy * qy + 5;
            exp.Add_iteration(0);

            Random rnd = new Random();
            Parallel.For(0, qx, (i) =>
            {
                Parallel.For(0, qy, (j) =>
                {
                    int c = rnd.Next(0, 5);
                    exp.Add_cell(0, i, j, c);
                    gridView[j, i].Value = c.ToString();
                    gridView[j, i].Style.BackColor = change_color(c);
                });

            });
            gridView.CellValueChanged += dh_TextChanged;
            gridView.Parent = this;



            this.Refresh();
        }
        private void dh_TextChanged(object sender, EventArgs e)
        {
            int n;
            int i = gridView.CurrentCell.RowIndex;
            int j = gridView.CurrentCell.ColumnIndex;
            //for (int i = 0; i < exp.W; i++)
            //{
            //    for (int j = 0; j < exp.H; j++)
            //    {
            if ((gridView[j, i].Value).ToString().Length > 1 || !int.TryParse(gridView[j, i].Value.ToString(), out n) || Convert.ToInt32(gridView[j, i].Value.ToString()) < 0 || Convert.ToInt32(gridView[j, i].Value) > 5)
                gridView[j, i].Value = "0";

            exp.Update_cell(0, i, j, Convert.ToInt32(gridView[j, i].Value));
            gridView[j, i].Style.BackColor = change_color(Convert.ToInt32(gridView[j, i].Value));
            //    }
            //}
        }
        Color change_color(int i)
        {
            switch (i)
            {
                case 0:
                    return Color.White;
                case 1:
                    return Color.Yellow;
                case 2:
                    return Color.Salmon;
                case 3:
                    return Color.Red;
                case 4:
                    return Color.SteelBlue;
                case 5:
                    return Color.Gray;
                default: return Color.Black;

            }
        }

        private void resetAllCellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < exp.W; i++)
            {
                for (int j = 0; j < exp.H; j++)
                {
                    exp.Update_cell(0, i, j, 0);
                    gridView[j, i].Value = 0;
                    gridView[j, i].Style.BackColor = change_color(0);
                }
            }
            //gridView.DataSource = exp.Get_cells(exp.curr_time);
        }
        void get_datas()
        {
            Parallel.For(0, exp.W, (i) =>
            {

                Parallel.For(0, exp.H, (j) =>
                {
                    gridView[j, i].Value = exp.Get_cell(exp.CurrTime, i, j).ToString();
                    gridView[j, i].Style.BackColor = change_color(Convert.ToInt32(gridView[j, i].Value));
                });

            });
        }
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exp.Time = Convert.ToInt32(time.Text);
            exp.CurrTime = 0;
            Parallel.For(0, exp.W, (i) =>
            {

                Parallel.For(0, exp.H, (j) =>
                {
                    exp.Add_cell(0, i, j, Convert.ToInt32(gridView[j, i].Value));
                });

            });
            toolStripLabel1.Text = "Running experiment...";


            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (exp.CurrTime != 0 && (exp.CurrTime == exp.Time - 1 || exp.Comparison(exp.CurrTime, exp.CurrTime - 1)))
            {
                toolStripLabel1.Text = "End of experiment";
                timer1.Stop();
            }
            else
            {
                toolStripLabel1.Text = "Running simulator...";
                exp.Change_stage();
                toolStripLabel1.Text = "Load results...";
                get_datas();
                Tact_of_tacts.Text = exp.CurrTime.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
