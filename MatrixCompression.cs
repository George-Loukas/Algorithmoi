using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAlgorithms.mymaths
{
    class MatrixCompression
    {
        public static (double[], int[], int[]) CsrParameters(double[,] A)
        {
            int nonzeroentries = nnm(A);
            int k = 0;
            int l = 0;
            int d = A.GetLength(0);
            bool flag = false;
            double[] values = new double[nonzeroentries];
            int[] colindices = new int[nonzeroentries];
            int[] rowoffset = new int[d + 1];
            rowoffset[d] = nonzeroentries;
            for (int i = 0; i < d; i++)
            {
                flag=true; // όταν είναι true, σημαίνει ότι μπήκε σε καινούργια γραμμη
                for (int j = 0; j < d; j++)
                {
                    if (A[i, j] != 0)
                    {
                        values[k] = A[i, j];
                        colindices[k] = j;
                        if (flag)
                        {
                            rowoffset[l] = k;
                            l++;
                            flag= false; // Μόλις γίνει false, δεν θα ξαναμπεί στην if, οπότε θα αποθηκευτεί μόνο η θέση του πρωτου μη μηδενικου. 
                        }           // Για να ξαναμπεί στην if πρέπει να τελειώσει η λούπα των j(στηλών) και να αλλάξει το i που δειχνει τη γραμμη.
                        k++;
                    }
                }
            }
            return (values, colindices, rowoffset);
        }
        public static (double[], int[], int[]) CscParameters(double[,] A)
        {
            int nonzeroentries = nnm(A);
            int k = 0;
            int l = 0;
            int d = A.GetLength(0);
            bool flag = false;
            double[] values = new double[nonzeroentries];
            int[] rowindices = new int[nonzeroentries];
            int[] coloffset = new int[d + 1];
            coloffset[d] = nonzeroentries;
            for (int j = 0; j < d; j++)
            {
                flag = true; // όταν είναι true, σημαίνει ότι μπήκε σε καινούργια γραμμη
                for (int i = 0; i < d; i++)
                {
                    if (A[i, j] != 0)
                    {
                        values[k] = A[i, j];
                        rowindices[k] = i;
                        if (flag)
                        {
                            coloffset[l] = k;
                            l++;
                            flag = false; // Μόλις γίνει false, δεν θα ξαναμπεί στην if, οπότε θα αποθηκευτεί μόνο η θέση του πρωτου μη μηδενικου. 
                        }           // Για να ξαναμπεί στην if πρέπει να τελειώσει η λούπα των j(στηλών) και να αλλάξει το i που δειχνει τη γραμμη.
                        k++;
                    }
                }
            }
            mymaths.Vectors.PrintDoubleVector(values);
            mymaths.Vectors.PrintIntVector(rowindices);
            mymaths.Vectors.PrintIntVector(coloffset);


            return (values, rowindices, coloffset);
        }
        public static (double[],int[]) Skyline(double[,] A)
        {
            double[] values;
            int[] diagoffset;
            (values, diagoffset) = Val(A);

             (int,int[]) NNZ(double[,] a)
            {
                bool flag = false;
                int numbersnonzero;
                numbersnonzero = 0;
                int[] b = new int[A.GetLength(0)]; // Max Height in column
                for (int j = 0; j < A.GetLength(0); j++)
                {
                    flag = false;
                    for (int i= 0; i <= j; i++)
                    {
                        if (A[i, j] != 0 && !flag)
                        {
                            flag = true;
                            b[j] = i;
                        }
                        if (i <= j && flag)
                            numbersnonzero++;
                    }

                }
                Console.WriteLine(numbersnonzero);
                Vectors.PrintIntVector(b);
                Console.ReadLine();
                return (numbersnonzero,b);
            } // Check ok
             (double[],int[]) Val(double[,] b)
            {
                int n = b.GetLength(0);
                double[] val;
                int[] diagoff;
                int nnz,k=0;
                int[] maxh;
                (nnz,maxh)= NNZ(b);
                val = new double[nnz];
                diagoff = new int[n+1];
                diagoff[n] = nnz;
                bool flag1;
                for (int j = 0; j < n; j++) //columns count
                {
                    flag1 = true;
                    for(int i = j; i >= maxh[j]; i--)
                    {
                        if (flag1)
                        {
                            flag1 = false;
                            diagoff[j] = k;
                        }
                        val[k] = A[i, j];
                        k++;
                    }
                }
                return (val,diagoff);
            }
            return (values, diagoffset);
        }
        public static int nnm(double[,] A)
        {
            int d = A.GetLength(0);
            int counter = 0;
            for (int i = 0; i < d; i++)
            {
                for (int k = 0; k < d; k++)
                {
                    if (A[i, k] != 0)
                    {
                        counter = counter + 1;
                    }
                }
            }
            Console.WriteLine(" ");
            Console.WriteLine("Το μητρώο περιέχει " + counter + " μη μηδενικές εισαγωγές");
            return (counter);
        }
        public static (double[],int[],int[]) COO(double[,] A)
        {
            double[] values = new double[nnm(A)];
            int[] rows = new int[nnm(A)];
            int[] columns = new int[nnm(A)];
            int k = 0;
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1) ; j++)
                {
                    if (A[i, j] != 0)
                    {
                        values[k]= A[i, j];
                        rows[k] = i;
                        columns[k] = j;
                        k++;

                    }
                }

            }
            return (values, rows, columns);
        }
        public static double[,] BandedSym(double[,] A)
        {
            int bandwith=3;
            int n = A.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                for (int j = n - 1; j > i; j--)
                {
                    if (A[i, j] != 0 && (j - i + 1) > bandwith)
                    {
                        bandwith = j - i + 1;
                    }
                }
            }
            double[,] band = new double[bandwith, n];
                for (int col = 0; col < bandwith; col++)
                {
                    for (int row = 0; row < n; row++)
                    {
                        if(row + col  < n)
                            band[col, row] = A[row, row+col];
                        else
                            band[col, row] = 0;

                    }
                }
            return band;
        }

    }
}
