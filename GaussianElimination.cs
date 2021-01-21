using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAlgorithms.mymaths
{
    class GaussianElimination
    {
        public static double[] Solve(double[,] a, double[] y)
        {
            double[] x = new double[y.Length];
            int n = a.GetLength(0); 
            double[,] A = new double[n, n + 1];
            double sum;
            A = mymaths.MatricesVectors.HyperMatrix(a, y);
            n = n - 1;
            for (int k = 0; k <= n-1; k++)
            {
                for(int i = k+1; i <= n ; i++)
                {
                    for(int j = k + 1; j <= n + 1; j++)
                    {
                        A[i,j] = A[i,j] - A[i, k] / A[k, k] * A[k, j];
                    }
                }
            }
            x[n] =Math.Round(A[n, n + 1] / A[n, n],4) ;
            
            for(int k = n - 1; k >=0 ; k--) 
            {
                sum = 0;
                for (int j = k + 1; j <= n; j++)
                { 
                    sum = sum + A[k, j] * x[j];
                    
                }

                x[k] = Math.Round(1 / A[k, k] * (A[k, n + 1] - sum),4);

            }
            return x;
        }        

        public static double[] SolvePP(double[,] a,double[] y)
        {
            double[] x = new double[y.Length];
            int n = y.Length;
            double[,] A = mymaths.MatricesVectors.HyperMatrix(a, y);
            double sum;
            //Γίνεται το elimination
            for (int j = 0; j < n-1; j++)
            {
                ColumnsSorting(ref A, j);
                EliminationCol(ref A, j);
            }

            //Back Substitution
            n = n - 1;
            x[n] = Math.Round(A[n, n + 1] / A[n, n], 4);
            for (int k = n - 1; k >= 0; k--)
            {
                sum = 0;
                for (int j = k + 1; j <= n; j++)
                {
                    sum = sum + A[k, j] * x[j];

                }

                x[k] = Math.Round(1 / A[k, k] * (A[k, n + 1] - sum), 4);

            }
                return x;
        }

        private static void ColumnsSorting(ref double[,] A,int col)
        {
            for (int j = 0; j <= A.GetLength(0) - 2; j++)
            {
                for (int i = 0; i <= A.GetLength(0) - 2; i++)
                {
                    if ( Math.Abs(A[i,col])  <  Math.Abs( A[i + 1,col] ))
                    {
                        if (i>=col)
                        {
                            mymaths.Utilities.SwapRows(i, i + 1, ref A);
                        }

                    }
                }
            }
        }
        private static void EliminationCol(ref double[,] A,int cols)
        {
            double c;
            for(int rows = cols+1; rows < A.GetLength(0); rows++)
            {
                if (A[cols, cols] != 0)
                {
                    c = (-1) * (A[rows, cols] / A[cols, cols]);
                    if (c != 0)
                    {                  
                        MultiWholeRow(ref A, cols, c);
                        AddRows(ref A, cols, rows);
                    }

                }
            }
        }
        private static void MultiWholeRow(ref double[,] A, int row, double c)
        {
            for (int cols = 0; cols < A.GetLength(1); cols++)
            {
                A[row, cols] = Math.Round(A[row, cols] * c,2);
            }

        }
        private static void AddRows(ref double[,] A, int row1, int row2)
        {
            for (int cols = 0; cols < A.GetLength(1); cols++)
            {
                A[row2, cols] = A[row2, cols] + A[row1, cols];
            }
        }
    }
}