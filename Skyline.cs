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
            double[,] A = { { 1, 1, 0, 3, 6},
                            { 0, 5, 0, 0, 0},
                            { 0, 0, 4, 6, 4},
                            { 0, 0, 0, 7, 0},
                            { 0, 0, 0, 0, 5}};
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

            (double[] values,int[] diagoffset)= MatrixCompression.SkylineSym(A);
            Console.WriteLine("values: \n");
            Vectors.PrintDoubleVector(values);
            Console.WriteLine("diagoffset: \n");
            Vectors.PrintIntVector(diagoffset);
            Console.ReadLine();


        }
    }
}
