using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAlgorithms.mymaths
{
    class Substitutions
    {
        public static double[] BackSubstitution(double[,] A) //Το Α πρέπει να είναι το υπερμητρώο.
        {
            int n = A.GetLength(0);
            double[] x = new double[A.GetLength(0)];
            double sum;
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

        public static double[] LUFrontSubstitution(double[,] L, double[] b) 
        {
            int n = L.GetLength(0);
            double[] y = new double[n];
            double[] x=new double[n];
            double sumL;
            for (int i = 0; i < n; i++)
            {
                sumL = 0;
                for (int j = 0; j < i; j++)
                {
                    sumL = sumL + L[i, j] * y[j];
                }
                y[i] = (1 / L[i, i]) * (b[i] - sumL);
            }

            return y;

        } //Χρήση μόνο στην LU, ή Cholesky(πρέπει να επαληθευτεί)

        public static double[] LUBackSubstitution(double[,] U, double[] b) 
        {
            int n = U.GetLength(0);
            for (int i = n - 1; i >= 0; i--)
            {
                for (int j = n - 1; j > i; j--)
                {
                    b[i] = b[i] - U[i,j] * b[j];
                }
                b[i] = b[i] / U[i,i];
            }


            return b;
        } //Χρήση μόνο στην LU, ή Cholesky(πρέπει να επαληθευτεί)
    }
}

