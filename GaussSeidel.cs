using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAlgorithms.mymaths
{
    class GaussSeidel
    {
        public static double[] Solve2(double[,] A, double[] b)
        {
            int m = A.GetLength(0);
            double[,] l = new double[m, m];
            double[,] u = new double[m, m];
            double[,] d = new double[m, m];
            double[] y = new double[m];
            (l, u, d) = LUD(A);
            Console.WriteLine(" ");

            mymaths.Matrices.PrintMatrix(A);

            (double[,], double [,], double [,]) LUD(double[,] a)
            {
                int n = a.GetLength(0);
                double[,] L = new double[n, n];
                double[,] U = new double[n, n];
                double[,] D = new double[n, n];
                for(int i = 0; i < n; i++)
                {
                    for(int j = i+1; j < n; j++)
                    {
                        U[i, j] = A[i, j];
                        L[j, i] = A[j, i];
                    }
                }
                for (int j = 0; j < n; j++)
                {
                    D[j,j]=A[j, j];
                }
                Console.WriteLine(" ");
                mymaths.Matrices.PrintMatrix(U);
                Console.WriteLine(" ");

                mymaths.Matrices.PrintMatrix(L);
                Console.WriteLine(" ");

                mymaths.Matrices.PrintMatrix(D);


                return (L, U, D);
            }

            return y;
        }
    }
}
