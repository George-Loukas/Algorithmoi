using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAlgorithms.mymaths
{
    class SOR
    {
        public static double[] Solve(double[,] A, double[] b)
        {
            // Successive Over - Relaxation Method
            // x(t + 1) = (1 - w) x(t) + w xGS(t + 1)
            // Gauss-Seidel is derived from SOR for w = 1
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Αρχή Successive Over - Relaxation");
            Console.WriteLine("Μητρώο Α: ");
            Matrices.PrintMatrix(A);
            Console.WriteLine("\nΔιάνυσμα b: ");
            Vectors.PrintDoubleVector(b);
            int m = A.GetLength(0);
            int it = 0, maxit = 0;
            double[,] l = new double[m, m];
            double[,] u = new double[m, m];
            double[,] d = new double[m, m];
            double[] x = new double[m];
            double[] xt = new double[m];
            double[] y1 = new double[m];
            double[] y2 = new double[m];
            double[] y = new double[m];
            double maxerror;
            double error;
            double w;
            double[,] C = new double[m, m];
            double[,] minuswD = new double[m, m];
            double[,] wU = new double[m, m];
            Console.WriteLine("\nΠληκτρολόγισε τις μέγιστες επαναλήψεις: ");
            maxit = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nAποδεκτό σφάλμα. Πλήκτρολόγισε το n ( Σφάλμα = 10^-n) : ");
            maxerror = double.Parse(Console.ReadLine());
            maxerror = (Math.Pow(10, -maxerror));
            Console.WriteLine("\nTο αποδεκτό σφάλμα: " + maxerror);
        start:
            Console.WriteLine("\nΠληκτρολόγησε τον συντελεστή χαλάρωσης w, πρέπει 0 < w < 2");
            w = double.Parse(Console.ReadLine());
            if (w > 0 && w < 1)
            {
                Console.WriteLine("\nUnder Relaxation w = " + w);
                //Under-relaxation (𝜔<1) usually helps if the iterative method doesn’t converge, but it decreases the convergence rate.
                //Gauss - Seidel always converges for positive definite matrices, thus there is no point in under - relaxation.

            }
            else if (w > 1 && w < 2)
            {
                Console.WriteLine("\nOver Relaxation w = " + w);
            }
            else if (w == 1)
            {
                Console.WriteLine("\nGauss Seidel, w = 1");
            }
            else
            {
                Console.WriteLine("\nOver Relaxation w = " + w);
                Console.WriteLine("\nΟ συντελεστής χαλάρωσης δεν είναι αποδεκτός.");
                goto start;
            }
            Console.WriteLine("\nO συντελεστής χαλάρωσης είναι w = : " + w);

            (l, u, d) = Matrices.LUD(A);
            while (it <= maxit)
            {
                Console.WriteLine("\nΕπανάληψη " + it + " από " + maxit);
                Vectors.EqualVector(ref xt, ref x);
                ////(D - w * L) * x(t+1) = (w * U + (1-w) * D) * x(t) + w * b
                C = Matrices.SubMatrix(d, MultiplyScalarWithMatrix(l, w)); //(D - w * L)
                minuswD = MultiplyScalarWithMatrix(d, 1 - w); //(1-w) * D
                wU = MultiplyScalarWithMatrix(u, w);  //w * U
                y1 = MatricesVectors.MatrixVectorMultiplication(Matrices.AddMatrix(wU, minuswD), x);  // (w * U + (1-w) * D) * x(t)
                y2 = Vectors.ScalarVectorMultiplication(b, w);  // w * b
                y = Vectors.AddVectors(y1, y2);       //(w * U + (1-w) * D) * x(t) + w * b
                xt = Substitutions.LUFrontSubstitution(C, y);
                //error calc
                double[] temp = Vectors.SubVectors(xt, x);
                double temp1 = Vectors.Norm2(temp);
                error = Math.Abs(temp1);
                if (error < maxerror)
                {
                    Console.WriteLine("\nΕπαναλήψεις :" + it);
                    it = maxit + 2;
                    Console.WriteLine("\nΣφάλμα: " + error);

                }
                it++;
            }
            Console.WriteLine("\nx Προσσέγγισης: ");
            Vectors.PrintDoubleVector(xt);
            Console.WriteLine("\nΤέλος Successive Over Relaxation");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
            return xt;
        }
        static double[,] MultiplyScalarWithMatrix(double[,] U,double w)
        {
            double[,] A = new double[U.GetLength(0),U.GetLength(1)];
            for (int i = 0; i < U.GetLength(0); i++)
                for (int j = 0; j < U.GetLength(1); j++)
                    A[i, j] = w * U[i, j];
            return A;           
        }
        public static void test()
        {
            Console.WriteLine("[][][][][][][][][][][][][][][][][][][][][][][][]");
            Console.WriteLine("\nΤεστ στην Successive Over-Relaxation\n");
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
            Console.WriteLine("\nΤΕΛΟΣ - Τεστ Successive Over - Relaxation\n");
            Console.WriteLine("[][][][][][][][][][][][][][][][][][][][][][][][]");
            Console.ReadKey();
        }
    }
}
