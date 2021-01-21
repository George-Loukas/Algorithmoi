using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAlgorithms.mymaths
{
    class MatricesVectors
    {
        public static double[] CsrAx( double[,] A, double[] x)
        {
            (double[] Values, int[] ColIndices, int[] RowOffsets) = mymaths.CSRCSC.CsrParameters(A);
            double[] y = new double[x.Length];
            int d = x.Length;
            for (int i = 0; i <= d - 1; i++)
            {
                for (int j = RowOffsets[i]; j <= RowOffsets[i + 1] - 1; j++)
                {
                    y[i] = y[i] + Values[j] * x[ColIndices[j]];
                }
            }
            return (y);
        }
        public static double[] CsrAΤx(double[,] A, double[] x)
        {
            (double[] Values, int[] ColIndices, int[] RowOffsets) = mymaths.CSRCSC.CsrParameters(A);
            double[] y = new double[x.Length];
            int d = x.Length;
            for (int i = 0; i <= d - 1; i++)
            {
                y[i] = 0;
            }
            for (int i = 0; i <= d - 1; i++)
            {
                for (int j = RowOffsets[i]; j <= RowOffsets[i + 1] - 1; j++)
                {
                    y[ColIndices[j]] = y[ColIndices[j]] + Values[j] * x[i];

                }
            }
            return (y);
        }
        public static double[] MatrixVectorMultiplication(double[,] A,double[] x)
        {
            double[] y=new double[x.Length];
            int cA = A.GetLength(1);
            int rA = A.GetLength(0);
            if (cA != x.Length)
            {
                Console.WriteLine("error: Λάθος διαστάσεις πίνακα-διανύσματος.");
            }
            else
            {
                for (int i = 0; i < rA; i++)
                {
                        double temp = 0;
                        for (int k = 0; k < cA; k++)
                        {
                             temp += A[i, k] * x[k];
                        }
                        y[i] = temp;
                }
            }
            return y;
        }
        public static (double[,], double[]) GenerateMatrixVector()
        {
            double[,] A=mymaths.Matrices.GenerateSqrMatrix();
            Random random = new Random();
            double[] x = new double[A.GetLength(0)];
            for (int i = 0; i < x.Length; i++)
            {
                x[i]= random.Next(-10, 10);
            }
            return (A,x);
        }
        public static double[,] HyperMatrix(double[,] A,double[] y)
        {
            int n = y.Length;
            double[,] a = new double[n,n+1];
            
            for (int i = 0; i < n ; i++)
            {
                a[i, n] = y[i];
                for (int j = 0; j < n; j++)
                {

                    a[i, j] = A[i, j];
                }
            }

            return a;
        }
    }
}
