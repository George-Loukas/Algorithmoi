using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAlgorithms.mymaths
{
    class MatrixCompression
    {
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
        } // Έρευση πλήθους μη μηδενικών στοιχείων

        // ----- Συμπιέσεις Sparce Πινάκων.
        public static (double[], int[], int[]) CsrParameters(double[,] A)
        {
            int nonzeroentries = nnm(A);
            int k = 0;
            int l = 0;
            bool flag = false;
            double[] values = new double[nonzeroentries];
            int[] colindices = new int[nonzeroentries];
            int[] rowoffset = new int[A.GetLength(0) + 1];
            rowoffset[A.GetLength(0)] = nonzeroentries;
            for (int i = 0; i < A.GetLength(0); i++)
            {
                flag=true; // όταν είναι true, σημαίνει ότι μπήκε σε καινούργια γραμμη
                for (int j = 0; j < A.GetLength(1); j++)
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
            bool flag = false;
            double[] values = new double[nonzeroentries];
            int[] rowindices = new int[nonzeroentries];
            int[] coloffset = new int[A.GetLength(1) + 1];
            coloffset[A.GetLength(1)] = nonzeroentries;
            for (int j = 0; j < A.GetLength(1); j++)
            {
                flag = true; // όταν είναι true, σημαίνει ότι μπήκε σε καινούργια γραμμη
                for (int i = 0; i < A.GetLength(0); i++)
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
        public static (double[],int[]) SkylineSym(double[,] A)
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
        } //row major
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
        // ----- ΤΕΛΟΣ Συμπιέσεις Sparce Πινάκων.

        // ----- Search: παίρνει τη συμπιεσμένη μορφή και τα ζητούμενα i,j και επιστρέφει τη τιμή A[i,j]
        public static double BandedSymSearch(double[,] banded,int row, int col) 
        {
            double result=0;
            if (col < banded.GetLength(0))
                result = banded[col, row];
            else if (row == col)
                result = banded[0, row];
            return result;
        }
        public static double CsrSearch(double[] values,int[] colindices,int[] rowoffsets,int row, int col)
        {
            double result = 0;
            int temp = rowoffsets[row]; // first non zero in this row
            int num =rowoffsets[row+1]- rowoffsets[row];  //number of non zero in this row
            for(int i = 0; i < num; i++)
            {
                if (colindices[temp + i] == col)
                    result = values[temp + i];
            }
            return result;
        }
        public static double CscSearch(double[] values, int[] rowindices, int[] coloffsets, int row, int col)
        {
            double result = 0;
            int temp = coloffsets[col]; // first non zero in this row
            int num = coloffsets[col + 1] - coloffsets[col];  //number of non zero in this row
            for (int i = 0; i < num; i++)
            {
                if (rowindices[temp + i] == row)
                    result = values[temp + i];
            }
            return result;
        }
        public static double CooSearch(double[] values, int[] rows, int[] columns, int row, int col)
        {
            double result = 0;
            for (int i = 0; i < rows.Length; i++)
                if (rows[i] == row && columns[i] == col)
                    result = values[i];
            return result;
        } //row major
        public static double SkylineDiagSearch(double[] values,int[] diagoffset,int row,int col)
        {
            double result=0;
            int n = diagoffset.Length - 1; // διαστάσεις του πίνακα Α (το diagoffset στη τελευταια θέση εχει nnz όποτε αφαιρώ 1)
            int h = diagoffset[col + 1] - diagoffset[col]; // βρίσκω το υψος της στηλης με τα nnz

            if (col - row < h)
            {
                if (row == col)
                    result = values[diagoffset[col]];
                else if (row > col)
                {
                    result = 0;
                }
                else
                    result = values[diagoffset[col] + col - row];
            }
            else
                result = 0;

            return result;
        }  // Αν ο πίνακας έχει στοιχεία μόνο πανω από την διαγώνιο
        // ----- Τέλος Search

        // ----- Test των Search Μεθόδων
        public static void testCsrSearch()
        {
            double[,] A = { { 9, 0, 3, 0 }, { 0, 8, 0, 0 }, { 0, 2, 6, 0 }, { 1, 0, 0, 5 } };
            mymaths.Matrices.PrintMatrix(A);
            Console.WriteLine("");
            (double[] values, int[] colindices, int[] rowoffset) = mymaths.MatrixCompression.CsrParameters(A);
            mymaths.Vectors.PrintDoubleVector(values);
            mymaths.Vectors.PrintIntVector(colindices);
            mymaths.Vectors.PrintIntVector(rowoffset);
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    Console.WriteLine("\n i: " + i + " j: " + j);
                    //int i = Convert.ToInt32(Console.ReadLine());
                    //int j = Convert.ToInt32(Console.ReadLine());
                    double result = mymaths.MatrixCompression.CsrSearch(values, colindices, rowoffset, i, j);
                    Console.Write("\n" + result);
                    Console.ReadKey();
                }

        }
        public static void testCscSearch()
        {
            double[,] A = { { 9, 0, 3, 0 }, { 0, 8, 0, 0 }, { 0, 2, 6, 0 }, { 1, 0, 0, 5 } };
            mymaths.Matrices.PrintMatrix(A);
            Console.WriteLine("");
            (double[] values, int[] rowindices, int[] coloffset) = mymaths.MatrixCompression.CscParameters(A);
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    Console.WriteLine("\n i: " + i + " j: " + j);
                    //int i = Convert.ToInt32(Console.ReadLine());
                    //int j = Convert.ToInt32(Console.ReadLine());
                    double result = mymaths.MatrixCompression.CsrSearch(values, rowindices, coloffset, i, j);
                    Console.Write("\n" + result);
                    Console.ReadKey();
                }

        }
        public static void testBandedSearch()
        {

            double[,] A = mymaths.Matrices.GenerateSqrMatrix();
            A = mymaths.Matrices.MakeSymmMatrix(A);
            mymaths.Matrices.PrintMatrix(A);
            Console.WriteLine("");
            double[,] bandA = mymaths.MatrixCompression.BandedSym(A);
            mymaths.Matrices.PrintMatrix(bandA);
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    Console.WriteLine("");
                    Console.WriteLine("\n i: " + i + " j: " + j);
                    //int i = Convert.ToInt32(Console.ReadLine());
                    //int j = Convert.ToInt32(Console.ReadLine());
                    double result = mymaths.MatrixCompression.BandedSymSearch(A, i, j);
                    Console.WriteLine("\n" + result);
                    Console.ReadKey();
                }
        }
        public static void testCooSearch()
        {
            double[,] A = { { 9, 0, 3, 0 }, { 0, 8, 0, 0 }, { 0, 2, 6, 0 }, { 1, 0, 0, 5 } };
            mymaths.Matrices.PrintMatrix(A);
            Console.WriteLine("");
            double[] values;
            int[] rows,columns;
            (values, rows, columns) = mymaths.MatrixCompression.COO(A);
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    Console.WriteLine("\n i: " + i + " j: " + j);
                    //int i = Convert.ToInt32(Console.ReadLine());
                    //int j = Convert.ToInt32(Console.ReadLine());
                    double result = mymaths.MatrixCompression.CooSearch(values, rows, columns, i, j);
                    Console.Write("\n" + result);
                    Console.ReadKey();
                }

        }
        public static void testSkylineDiagSearch()
        {
            double[,] A = { { 9, 0, 3, 0, 0},
                            { 0, 8, 0, 4, 0}, 
                            { 0, 0, 6, 0, 0}, 
                            { 0, 0, 0, 5, 2},
                            { 0, 0, 0, 0, 1} };
            mymaths.Matrices.PrintMatrix(A);
            Console.WriteLine("");
            double[] values;
            int[] diagoffsets;
            (values, diagoffsets) = mymaths.MatrixCompression.SkylineSym(A);
            Vectors.PrintDoubleVector(values);
            Vectors.PrintIntVector(diagoffsets);
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    Console.WriteLine(" i: " + i + " j: " + j);
                    //int i = Convert.ToInt32(Console.ReadLine());
                    //int j = Convert.ToInt32(Console.ReadLine());
                    double result = mymaths.MatrixCompression.SkylineDiagSearch(values, diagoffsets, i, j);
                    Console.WriteLine(result);
                    Console.ReadKey();
                }

        }
        // ----- Tέλος Test των Search Μεθόδων

        public static int[] BooleanMatrixLcreation(double[,] A)
        {
            // Ο boolean πίνακας L έχει σε κάθε του γραμμή ένα μοναδικό άσσο σε κάποια στήλη για αυτό σε κάθε γραμμή
            // θα διατρέξω όλες τις στήλες με for loop και όταν βρώ τον άσο, εξάγω τον αριθμό της στήλης.

            int n = A.GetLength(0);
            int m = A.GetLength(1);

            int[] CompressedL = new int[n];
            int k = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (A[i, j] == 1)
                    {
                        CompressedL[k] = j;
                        k++;
                    }
                }
            }
            return CompressedL;
        }
        public static void BooleanMatrixLcreationTest()
        {
            double[,] L = new double[,] {{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                         {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                         {0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                         {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0} };

            int NumberOfGlobalDOFs = L.GetLength(1);

            int[] CompressedL = BooleanMatrixLcreation(L);

            Console.WriteLine("Ο boolean L σε compressed format:");
            Vectors.PrintIntVector(CompressedL);
            Console.ReadLine();

            double[] x1 = new double[] { 1, 0, 152, 0, 5, 1, 8, 1, 0, 13, 1, 1 };

            double[] y1 = LMultiplyWithVector(CompressedL, x1);

            Console.WriteLine("L (compressed) * x1 = ");
            Vectors.PrintDoubleVector(y1);
            Console.ReadLine();

            double[] x2 = new double[] { 1, 1, 1, 1 };

            double[] y2 = LTransopeMultiplyWithMatrix(CompressedL, NumberOfGlobalDOFs, x2);

            Console.WriteLine("L' (compressed) * x2 = ");
            Vectors.PrintDoubleVector(y2);
            Console.ReadLine();
        }
        public static double[] LMultiplyWithVector(int[] CompressedL, double[] x)
        {
            int n = CompressedL.Length;
            int k;
            double[] y = new double[n];
            for (int i = 0; i < n; i++)
            {
                k = CompressedL[i];
                y[i] = x[k];
            }

            return y;
        }
        public static double[] LTransopeMultiplyWithMatrix(int[] CompressedL, int NumberOfGlobalDOFs, double[] x)
        {
            double[] y = new double[NumberOfGlobalDOFs];
            int m = CompressedL.Length;
            for (int i = 0; i < NumberOfGlobalDOFs; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (CompressedL[j] == i)
                    {
                        y[i] = x[i];
                    }
                }
            }

            return y;
        }

    }
}
