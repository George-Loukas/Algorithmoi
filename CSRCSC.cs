using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAlgorithms.mymaths
{
    class CSRCSC
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

    }
}
