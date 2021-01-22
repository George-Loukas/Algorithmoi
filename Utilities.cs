using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAlgorithms.mymaths
{
    class Utilities
    {
        public static void BubbleSorting(ref double[] A)
        {
            double temp;
            for (int j = 0; j <= A.GetLength(0) - 2; j++)
            {
                for (int i = 0; i <= A.GetLength(0) - 2; i++)
                {
                    if (A[i] < A[i + 1])
                    {
                        temp = A[i + 1];
                        A[i + 1] = A[i];
                        A[i] = temp;
                    }
                }
            }
        }
        public static void Swap(ref double A,ref double B)
        {
            double temp;
            temp = A;
            A = B;
            B = temp;

        }
        public static void SwapRows(int i,int m,ref double[,] A)
        {
            for (int cols = 0; cols < A.GetLength(1); cols++)
            {
                Swap(ref A[i, cols],ref A[m, cols]);
            }
            
        }

    }
}
