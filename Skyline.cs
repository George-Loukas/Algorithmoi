using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms2020
{
    public static class Skyline
    {
        //Skyline does not store any excess zeros like banded format, because each column can have a different height.

        //It is also called variable-band matrix.

        //There is also a skyline format for asymetric matrices. 

        //The bold zeros inside the active columns are at first glance not necessary.
        //However, when applying the Cholesky factorization, they will be overwritten. 
        //This is called fill-in.

        //SKY is recommended when solving symmetric positive definite systems with the 
        //direct method based on Cholesky or LDL factorization :
        //No fill-in will occur at the zero entries outside the active columns (structural zeros). 
        //The entries stored in SKY are exactly those needed for the factorization → memory efficiency.
        //We only need to operate inside the active columns → computation time efficiency.

        //Skyline for symmetric A
        //Skyline: 2 arrays (1D) are needed. Values(nnz), DiagOffsets(n+1)

        public static (double[], int[]) SkylineArrays(double[,] A)
        {
            int n = A.GetLength(0);
            (int nnz,int[] max) = SkylineNNZ(A);
            double[] SkylineValues = new double[nnz];
            int[] SkylineDiagOffsets = new int[n + 1];
            bool p;
            int k = 0;
            int[] maxh = new int[n];
            SkylineValues[0] = A[0, 0];
            for (int j = 1; j < n; j++)
            {
                p = true;
                for (int i = j; i > j - maxh[j] + 1 ; i--)
                {
                    SkylineValues[k] = A[i, j];
                    Console.WriteLine(" " + i + " " + j + " " + k);
                    if (p)
                    {
                        p = false;
                        SkylineDiagOffsets[j] = k;
                    }
                    k++;
                }

            }
            SkylineDiagOffsets[n] = nnz;

            Console.WriteLine("The non zero entries' values array is");
            Utilities.PrintVectorDouble(SkylineValues);
            Console.ReadLine();

            Console.WriteLine("The Diagonal offsets array is");
            Utilities.PrintVectorInt(SkylineDiagOffsets);
            Console.ReadLine();

            return (SkylineValues, SkylineDiagOffsets);
        }

        public static (int, int[]) SkylineNNZ(double[,] A)
        {

            int nnz, i;
            nnz = 0;
            int[] maxh = new int[A.GetLength(0)];
            
            for (int j = 0; j < A.GetLength(0); j++)
            {
                i = 0;
                while (i <= j)
                {
                    if (A[i,j] != 0)
                    {
                        maxh[j] = j - i;
                        nnz += j - i + 1; // ??????????????????
                        goto start;
                    }
                    i++;
                }
            start:;
            }

            Console.WriteLine("The value of nnz for Skyline is " + nnz);
            Console.WriteLine("The array maxh is ");
            Utilities.PrintVectorInt(maxh);
            Console.ReadLine();

            return (nnz, maxh);
        }

        public static void TestSkylineArrays()
        {
            double[,] A = new double[5, 5];
            A[0, 0] = 1;
            A[0, 1] = 1;
            A[0, 2] = 0;
            A[0, 3] = 3;
            A[0, 4] = 0;
            A[1, 1] = 5;
            A[1, 2] = 0;
            A[1, 3] = 0;
            A[1, 4] = 0;
            A[2, 2] = 4;
            A[2, 3] = 6;
            A[2, 4] = 4;
            A[3, 3] = 7;
            A[3, 4] = 0;
            A[4, 4] = 5;
            for (int i = 0; i < 5; i++)
            {
                for (int j = i; j < 0; j--)
                {
                    A[i, j] = A[j, i];
                }
            }

            Console.WriteLine("Ο Πίνακας Α που θέλουμε αποθηκεύσουμε σε Skyline Form");
            Utilities.PrintMatrix(A);
            Console.WriteLine("");

            (double[] Values,int[] DiadOffsets) = SkylineArrays(A);
        }

        //Devire Cholesky L matrix from Skyline Format Storage
        //public static double[,] CholeskyLSkyline(double[] SkylineValues, int[] SkylineDiagOffsets)
        //{
        //    int n = SkylineDiagOffsets.Length - 1;
        //    int[] h = new int[n];
        //    int[] m = new int[n];
        //    for (int j = 0; j < n; j++)
        //    {
        //        h[j] = SkylineDiagOffsets[j + 1] - SkylineDiagOffsets[j] - 1;
        //        m[j] = j - h[j];
        //    }
        //    double[,] L = new double[n, n];
        //    double sum;
        //    for (int i = 0; i <= n; i++)
        //    { 
        //        for (int j = m[i]; j >= i; j--)
        //        {
        //            sum = sum + SkylineValues[j] * SkylineValues[j];
        //            L[i, j] = (1 / SkylineValues[i]) * (SkylineValues[j] - sum);
        //        }
        //        L[i,i] = 

        //            L[i,i] = Math.Sqrt(SkylineValues[j] - sum);
        //        }
        //        else
        //        {
        //            sum = 0;
        //            for (int j = m[i]; j >= i - 1; j--)
        //            {
        //            sum = sum + SkylineValues[j] * SkylineValues[j];
        //            }
        //            L[i, j] = Math.Sqrt(SkylineValues[j] - sum);

        //    }
                
        //        {
        //            sum = sum + SkylineValues[j] * SkylineValues[j];
        //        }

        //    }

        }
    }
