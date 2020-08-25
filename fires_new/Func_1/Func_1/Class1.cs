using System;

namespace Func_1
{
    public class Func_1
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
            if (c == 0) { stage = 0; }
            else if (c >= Find_digital(c, procent, cn)) { stage = 2; }
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
            if (c >= Find_digital(c, 25, cn)) { stage = 2; }
            else if (c == 0) { stage = 0; }
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
