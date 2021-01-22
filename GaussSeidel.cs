using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAlgorithms.mymaths
{
    class GaussSeidel
    {
        public static double[] Solve(double[,] A, double[] b)
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Αρχή Gauss Seidel(GS)");
            Console.WriteLine("Μητρώο Α: ");
            Matrices.PrintMatrix(A);
            Console.WriteLine("\nΔιάνυσμα b: ");
            Vectors.PrintDoubleVector(b);
            int m = A.GetLength(0);
            int it=0,maxit=0;
            double[,] l = new double[m, m];
            double[,] u = new double[m, m];
            double[,] d = new double[m, m];
            double[] x = new double[m];
            double[] xt = new double[m];
            double[] y = new double[m];
            double maxerror;
            double error;
            double[,] C = new double[m, m];
            Console.WriteLine("\nΠληκτρολόγισε τις μέγιστες επαναλήψεις: ");
            maxit = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nAποδεκτό σφάλμα. Πλήκτρολόγισε το n ( Σφάλμα = n*10^-6) : ");
            maxerror = double.Parse(Console.ReadLine());
            maxerror = maxerror * (Math.Pow(10, -6));
            Console.WriteLine("\nTο αποδεκτό σφάλμα: "+maxerror);

            (l, u, d) = Matrices.LUD(A);
            while (it<=maxit)
            {
                Console.WriteLine("\nΕπανάληψη "+it+" από "+maxit);
                Vectors.EqualVector(ref xt,ref x);
                // (D-L)*x(t+1)=U*x(t)+b
                C = mymaths.Matrices.SubMatrix(d,l);//D-L
                y = Vectors.AddVectors(MatricesVectors.MatrixVectorMultiplication(u, x), b); // U*x(t)+b
                xt = Substitutions.LUFrontSubstitution(C, y);
                //error calc
                double[] temp=Vectors.SubVectors(xt, x);
                double temp1 =  Vectors.Norm2(temp);
                error = Math.Abs(temp1);
                if(error<maxerror)
                {
                    Console.WriteLine("\nΕπαναλήψεις :"+it);
                    it = maxit + 2;
                    Console.WriteLine("\nΣφάλμα: "+ error);

                }
                it++;
            }
            Console.WriteLine("\nx Προσσέγγισης: ");
            Vectors.PrintDoubleVector(x);
            Console.WriteLine("\nΤέλος Gauss Seidel(GS)");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
            return x;
        }
        public static void test()
        {
            Console.WriteLine("[][][][][][][][][][][][][][][][][][][][][][][][]");
            Console.WriteLine("\nΤεστ στην Gauss Seidel\n");
            double[,] A = { { 16, -8, -4 }, { -8, -29, 12 }, { -4, 12, -41 } };
            double[] x = Vectors.GenerateVector(A.GetLength(0));
            double[] b = MatricesVectors.MatrixVectorMultiplication(A, x);
            Console.WriteLine("Θετικά ορισμένο συμμετρικό Μητρώο Α");
            Matrices.PrintMatrix(A);
            Console.WriteLine("\nΤυχαίο διάνυσμα x");
            Vectors.PrintDoubleVector(x);
            Console.WriteLine("\nΔιάνυσμα b που προκύπτει");
            Vectors.PrintDoubleVector(b);
            double[] xtest = Solve(A, b);
            int c = 0;
            for (int i = 0; i < x.Length; i++)
            {
                if (Math.Round((x[i])-Math.Round( xtest[i])) == 0)
                {
                    c=i+1;
                }
            }
            if (c == A.GetLength(0)) 
            {
                Console.WriteLine("\n ΕΠΙΤΥΧΗΣ \n");
                Console.WriteLine("x Προσσέγγισης \n");
            }
            else
                Console.WriteLine("\n AΠΟΤΥΧΙΑ \n");
            Vectors.PrintDoubleVector(x);
            Console.WriteLine("\nΤΕΛΟΣ - Τεστ Gauss Seidel\n");
            Console.WriteLine("[][][][][][][][][][][][][][][][][][][][][][][][]");
            Console.ReadKey();
        }
    }
}
