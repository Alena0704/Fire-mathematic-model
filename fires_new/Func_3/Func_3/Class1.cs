using System;

namespace Func_3
{
    public class Func_3
    {
        public static int Change_Stage(int ccs, int[] snc, int procent)
        {
            int stage = ccs;
            int cn = Count_neiboughs(snc);
            int c = 0;

            for (int i = 0; i < 8; i++)
            {
                if (snc[i] > 1 && snc[i] < 5)
                    c++;
            }
            if (c == 0) { stage = 1; }
            else if (c > Find_digital(c, procent, cn)) { stage = 4; }
            return stage;
        }

        public static int Change_Stage(int ccs, int[] snc)
        {
            int stage = ccs;
            int cn = Count_neiboughs(snc);
            int c = 0;

            for (int i = 0; i < 8; i++)
            {
                if (snc[i] > 1 && snc[i] < 5)
                    c++;
            }
            if (c == 0) { stage = 1; }
            else if (c > Find_digital(c, 75, cn)) { stage = 4; }
            return stage;
        }

        private static int Find_digital(int c, int procent, int count_neibough)
        {
            return count_neibough / 100 * procent;
        }
        private static int Count_neiboughs(int[] ar)
        {
            int c = 0;
            for (int i = 0; i < ar.Length; i++)
            {
                if (ar[i] != -1)
                    c++;
            }
            return c;
        }
    }
}
