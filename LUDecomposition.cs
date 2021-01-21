using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAlgorithms.mymaths
{
    class LUDecomposition
    {

        public static void TestLU()
        {
            double[,] U, L,A;
            double[] b,x,y;
            (A,x)=mymaths.MatricesVectors.GenerateMatrixVector();
            b = mymaths.MatricesVectors.MatrixVectorMultiplication(A, x); //A * x= b
            (L, U) = LUcreation(A);
            mymaths.Matrices.PrintMatrix(A);
            Console.WriteLine("^A^");
            mymaths.Matrices.PrintMatrix(L);
            Console.WriteLine("^L^");
            mymaths.Matrices.PrintMatrix(U);
            Console.WriteLine("^U^");
            Console.ReadKey();
            y = mymaths.Substitutions.LUFrontSubstitution(L, b);
            Console.WriteLine("y (L*y=b)");
            mymaths.Vectors.PrintDoubleVector(y);
            x = mymaths.Substitutions.LUBackSubstitution(U, y);
            Console.WriteLine("x (U*x=y)");
            mymaths.Vectors.PrintDoubleVector(x);
            Console.WriteLine("^U^");
            Console.WriteLine("Comparation with right solution.\n");
            double[] xexpected = mymaths.GaussianElimination.Solve(A, b);
            mymaths.Vectors.PrintDoubleVector(xexpected);
            mymaths.Vectors.VectorEqualityCheck(x, xexpected);
           

        }
        public static (double[,], double[,]) LUcreation(double[,] a)
        {
            int n = a.GetLength(0);
            double[,] l, u;
            l = new double[n, n];
            u = new double[n, n];

            int i,j,k;
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    if (j < i)
                        l[j,i] = 0;
                    else
                    {
                        l[j,i] = a[j,i];
                        for (k = 0; k < i; k++)
                        {
                            l[j,i] = l[j,i] - l[j,k] * u[k,i];
                        }
                    }
                }
                for (j = 0; j < n; j++)
                {
                    if (j < i)
                        u[i,j] = 0;
                    else if (j == i)
                        u[i,j] = 1;
                    else
                    {
                        u[i,j] = a[i,j] / l[i,i];
                        for (k = 0; k < i; k++)
                        {
                            u[i,j] = u[i,j] - ((l[i,k] * u[k,j]) / l[i,i]);
                        }
                    }
                }
            }
            return (l, u);
        }
        public static void LUtoA(ref double[,] a)
        {
            int n = a.GetLength(0);
            double[,] l, u;
            l = new double[n, n];
            u = new double[n, n];

            int i, j, k;
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    if (j < i)
                        l[j, i] = 0;
                    else
                    {
                        l[j, i] = a[j, i];
                        for (k = 0; k < i; k++)
                        {
                            l[j, i] = l[j, i] - l[j, k] * u[k, i];
                        }
                        a[j, i] = l[j, i];
                        if (i == j)
                        {
                            a[j, i] = a[j, i] - 1;
                        }
                    }
                }
                for (j = 0; j < n; j++)
                {
                    if (j < i)
                    {
                        u[i, j] += 0;
                        a[i, j] += 0;
                    }
                    else if (j == i)
                    {
                        u[i, j] += 1;
                        a[i, j] += 1;
                    }
                    else
                    {
                        u[i, j] = a[i, j] / l[i, i];
                        for (k = 0; k < i; k++)
                        {
                            u[i, j] = u[i, j] - ((l[i, k] * u[k, j]) / l[i, i]);
                        }
                        a[i, j] += u[i, j];
                    }
                }
            }
            Console.WriteLine("Οι πίνακες L,U αποθηκεύτηκαν στον Α");
            Console.WriteLine("O Α");
            mymaths.Matrices.PrintMatrix(a);
            Console.WriteLine("O L");
            mymaths.Matrices.PrintMatrix(l);
            Console.WriteLine("O U");
            mymaths.Matrices.PrintMatrix(u);
        } //ουσιαστικά είναι η άσκηση 4 από το word. Αποθηκεύει στον Α τους LU (η διαγώνιος είναι μονο του L *έχω αφαιρέσει τους άσσους)

    }
}

