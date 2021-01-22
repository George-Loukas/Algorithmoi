using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAlgorithms.mymaths
{
    class Skyline
    {
        public static void test()
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
            A[0, 4] = 6;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (A[i,j]!=A[i,i])
                    A[j, i] = A[i, j];
                }
            }
            Console.WriteLine("Ο πίνακας Α: ");
            Matrices.PrintMatrix(A);
            Console.ReadLine();

            (double[] values,int[] diagoffset)= MatrixCompression.Skyline(A);
            Console.WriteLine("values: \n");
            Vectors.PrintDoubleVector(values);
            Console.WriteLine("diagoffset: \n");
            Vectors.PrintIntVector(diagoffset);
            Console.ReadLine();


        }
    }
}
