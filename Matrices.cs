using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAlgorithms.mymaths
{
    class Matrices
    {
        public static (double[,],int) ReadSqrMatrix()
        {
            Console.WriteLine("Διάσταση Τετραγωνικού Μητρώου");
            int d = int.Parse(Console.ReadLine());

            double[,] A = new double[d, d];
            Console.WriteLine("Καταχώρηση τιμών μητρώου A:");
            for (int i = 0; i < d; i++)
            {
                for (int j = 0; j < d; j++)
                {
                    Console.WriteLine("Συνιστώσα μητρώου : Α(" + i + "," + j + ")");
                    A[i, j] = double.Parse(Console.ReadLine());
                    Console.WriteLine(" ");
                }
            }
            return (A,d);
        }
        public static void PrintMatrix(double[,] A)
        {
            int rowsA = A.GetLength(0);
            int colsA = A.GetLength(1);
            for (int i = 0; i < rowsA; i++)
            {
                Console.Write("|");
                for (int j = 0; j < colsA; j++)
                {
                    Console.Write(A[i, j] + " ");
                }
                Console.Write("|");
                Console.WriteLine("");
            }
        }
        public static double[,] MatrixMultiplication(double[,] A, double[,] B)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);
            int rB = B.GetLength(0);
            int cB = B.GetLength(1);
            double temp = 0;
            double[,] C = new double[rA, cB];
            if (cA != rB)
            {
                Console.WriteLine("error: Λάθος διαστάσεις.");
            }
            else
            {
                for (int i = 0; i < rA; i++)
                {
                    for (int j = 0; j < cB; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < cA; k++)
                        {
                            temp += A[i, k] * B[k, j];
                        }
                        C[i, j] = temp;
                    }
                }
            }
         return C;

        }
        public static double[,] Transpose(double[,] A)
        {
            double[,] AT = new double[A.GetLength(1), A.GetLength(0)];
            int r = A.GetLength(0);
            int c = A.GetLength(1);
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    AT[j, i] = A[i, j];
                }
            }
            return AT;
        }
        public static double MatrixDet3x3(double[,] A)
        {
            double det=0;
            for (int i = 0; i < 3; i++)
                det = det + (A[0, i] * (A[1, (i + 1) % 3] * A[2, (i + 2) % 3] - A[1, (i + 2) % 3] * A[2, (i + 1) % 3]));

            Console.WriteLine("The Determinant of the matrix is: " + det);
            return det;
        }
        public static void SparseMatrixCheck(double[,] arr1)
        {
            int r = arr1.GetLength(0);
            int c = arr1.GetLength(1);
            int ctr = 0;
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    if (arr1[i, j] == 0)
                    {
                        ++ctr;
                    }
                }
            }
            if (ctr > ((r * c) / 2))
            {
                Console.Write("The given matrix is sparse matrix. \n");
            }
            else
                Console.Write("The given matrix is not a sparse matrix.\n");

            Console.Write("There are {0} number of zeros in the matrix.\n\n", ctr);

        }
        public static void MatrixEqualityCheck(double[,] arr1, double[,] brr1)
        {
            int i, j, r1, c1, r2, c2, flag = 1;
            r1 = arr1.GetLength(0); 
            c1 = arr1.GetLength(1);
            r2 = brr1.GetLength(0);
            c2 = brr1.GetLength(1);
            /* Comparing two matrices for equality */

            if (r1 != r2 && c1 != c2)
            {
                Console.Write("The Matrices Cannot be compared :\n");
            }
            else
            {
                Console.Write("The Matrices can be compared : \n");
                for (i = 0; i < r1; i++)
                {
                    for (j = 0; j < c2; j++)
                    {
                        if (Math.Round(arr1[i, j]-brr1[i, j],4) != 0)
                        {
                            flag = 0;
                            break;
                        }
                    }
                }
                if (flag == 1)
                    Console.Write("Two matrices are equal.\n\n");
                else
                    Console.Write("Two matrices are not equal\n\n");
            }
        }
        public static double[,] GenerateSqrMatrix()
        {


            Random random = new Random();
            int n;
            Console.WriteLine("Δημιουργία Πίνακα");
            Console.WriteLine("θες Τυχαία (1) ή Εσύ (2);");
            int c = int.Parse(Console.ReadLine());
            if (c == 2)
            {
                Console.WriteLine("Πόσες διαστάσεις να είναι ο πίνακας;");
                n = int.Parse(Console.ReadLine());
            }
            else if (c == 1)
            {
                n = Math.Abs(random.Next(3, 5));
            }
            else
            {
                Console.WriteLine("Πλήκτρολόγησε 1 ή 2 ρε γκαζμά.");
                n = 0;
            }
            double[,] A = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i,j]= random.Next(-10,10);
                }
            }
            return A;
        }
    }
}
