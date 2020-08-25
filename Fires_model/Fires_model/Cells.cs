using System;
using Func_0;
using Func_1;
using Func_2;
using Func_3;
using Func_4;


namespace проба_json
{
    class Cells
    {
        public int x = 0;
        public int y = 0;
        public int stage = 0;
        public int time;
        public int time_all;
        public Cells(int x1, int y1, int stage1)
        {
            x = x1;
            y = y1;
            stage = stage1;
        }

        public int Change_stage(int[] neib)
        {
            int c = 0;
            switch (stage)
            {
                case 0:
                    c = Func_0.Func_0.Change_Stage(stage, neib, 50);
                    if (c != stage)
                        time = 0;
                    else time++;
                    break;
                case 1:
                    c = Func_1.Func_1.Change_Stage(stage, neib, 35);
                    if (c != stage)
                        time = 0;
                    else time++;
                    break;
                case 2:
                    c = Func_2.Func_2.Change_Stage(stage, neib, 35);
                    if (c != stage)
                        time = 0;
                    else
                    {
                        time++;
                        if (time >= time_all * 20 / 100)
                        {
                            c = 3;
                            time = 0;
                        }
                    }
                    break;
                case 3:
                    c = Func_3.Func_3.Change_Stage(stage, neib, 10);
                    if (c != stage)
                        time = 0;
                    else
                    {
                        time++;
                        if (time >= time_all * 30 / 100)
                        {
                            c = 3;
                            time = 0;
                        }
                    }
                    break;
                case 4:
                    c = Func_4.Func_4.Change_Stage(stage, neib, 15);
                    if (c != stage)
                        time = 0;
                    else
                    {
                        time++;
                        if (time >= time_all * 50 / 100)
                        {
                            c = 3;
                            time = 0;
                        }
                    }
                    break;
            }
            return c;
        }
    }
}
