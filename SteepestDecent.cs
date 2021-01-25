using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAlgorithms.mymaths
{
    class SteepestDecent
    {
        public static double[] Solve(double[,] A, double[] b)
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Αρχή Steepest Decent (SD)");
            Console.WriteLine("Μητρώο Α: ");
            Matrices.PrintMatrix(A);
            Console.WriteLine("\nΔιάνυσμα b: ");
            Vectors.PrintDoubleVector(b);
            int m = A.GetLength(0);
            int it = 0, maxit = 0;
            double[] x = new double[m];
            double[] xt = new double[m];
            double[] y = new double[m];
            double[] rt = new double[m];
            double[] r = new double[m];
            double[] temp = new double[m];
            double a;
            double maxerror;
            double[,] C = new double[m, m];
            Console.WriteLine("\nΠληκτρολόγισε τις μέγιστες επαναλήψεις: ");
            maxit = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nAποδεκτό σφάλμα. Πλήκτρολόγισε το n ( Σφάλμα = n*10^-6) : ");
            maxerror = double.Parse(Console.ReadLine());
            maxerror = maxerror * (Math.Pow(10, -6));
            Console.WriteLine("\nTο αποδεκτό σφάλμα: " + maxerror);
            while (it <= maxit )
            {
                Console.WriteLine("\nΕπανάληψη " + it+1 + " από " + maxit);
                // Main solution here
                r = MatricesVectors.MatrixVectorMultiplication(A,x); // b-Ax=rt εδω κάνω το A*x
                r = Vectors.SubVectors(b, r);                       // εδώ ολοκληρώνω το b-A*x
                a = step(r, A);
                temp = Vectors.ScalarVectorMultiplication(r, a);
                xt = Vectors.AddVectors(x, temp);                  // x(t+1)=xt+at*r
                it++;
                m = it;
                if (Vectors.Norm2(Vectors.SubVectors(xt, x)) < maxerror)
                    it = maxit + 2;
                
                Vectors.EqualVector(ref xt, ref x);
            }
            Console.WriteLine("\n\nΕπαναλήψεις :" + m);
            it = maxit + 2;
            Console.WriteLine("\nΣφάλμα: " + Vectors.Norm2(Vectors.SubVectors(xt, x)));
            Console.WriteLine("\nx Προσσέγγισης: ");
            Vectors.PrintDoubleVector(xt);
            Console.WriteLine("\nΤέλος Steepest Decent (SD)");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
            return x;
        }
        static double step(double[] r,double[,] A)
        {
            double a;
            a = Vectors.ProductVectors(r, r); // r^T *r
            double[] temp = MatricesVectors.MatrixVectorMultiplication(A, r);
            a = a / (Vectors.ProductVectors(r, temp));
            return a;
        }
        public static void test()
        {
            Console.WriteLine("[][][][][][][][][][][][][][][][][][][][][][][][]");
            Console.WriteLine("\nΤεστ στην Steepest Decent\n");
            double[,] A = { { 16, 8, 4 }, { 8, 29, 12 }, { 4, 12, 41 } };
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
                if (Math.Round((x[i]) - Math.Round(xtest[i])) == 0)
                {
                    c = i + 1;
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
            Console.WriteLine("\nΤΕΛΟΣ - Τεστ Steepest Decent\n");
            Console.WriteLine("[][][][][][][][][][][][][][][][][][][][][][][][]");
            Console.ReadKey();
        }
    }
}
