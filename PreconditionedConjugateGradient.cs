using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAlgorithms.mymaths
{
    class PreconditionedConjugateGradient
    {
        public static double[] Solve(double[,] A, double[] b,double[,] M) // M: preconditioner
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Αρχή Conjugate Gradient CG");
            Console.WriteLine("Μητρώο Α: ");
            Matrices.PrintMatrix(A);
            Console.WriteLine("\nΔιάνυσμα b: ");
            Vectors.PrintDoubleVector(b);

            //Ορισμοί
            int m = A.GetLength(0);
            int it = 0, maxit = 0;
            double[] xt = new double[m];    //Initial guess x1 = zeros[]
            double[] xtplus1 = new double[m];
            double[] y = new double[m];
            double[] rtplus1 = new double[m];
            double[] rt = new double[m];    // Initial Residual r1 = b - A * x1
            double[] dt = new double[m];     // Direction vector, initially d1 = r1
            double[] dtplus1 = new double[m]; // Direction vector of next step (t+1)
            double[] Axt = new double[m];
            double[] Adt = new double[m];
            double[] atAdt = new double[m];
            double[] atdt = new double[m];
            double[] st = new double[m];
            double[] stplus1 = new double[m];
            double at;
            double betatplus1;
            double[] betatplus1dt = new double[m];
            double maxerror;
            double[,] C = new double[m, m];
            double NormalizedResidualNorm;
            ////

            Console.WriteLine("\nΠληκτρολόγισε τις μέγιστες επαναλήψεις: ");
            maxit = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nAποδεκτό σφάλμα. Πλήκτρολόγισε το n ( Σφάλμα = 10^-n) : ");
            maxerror = double.Parse(Console.ReadLine());

            maxerror = (Math.Pow(10, -maxerror));
            Console.WriteLine("\nTο αποδεκτό σφάλμα: " + maxerror);

            Axt = MatricesVectors.MatrixVectorMultiplication(A, xt);          // b-Ax1=rt εδω κάνω το A*x1
            rt = Vectors.SubVectors(b, Axt);                                  // εδώ ολοκληρώνω το b-A*x1
            dt = MatricesVectors.MatrixVectorMultiplication(M, rt);           // d1 =M* r1
            Vectors.EqualVector(ref dt, ref st);
            while (it <= maxit)
            {
                Console.WriteLine("\nΕπανάληψη " + it + " από " + maxit);

                // Main solution here
                at = step(rt, dt, st,A); // From orthogonality condition 2

                Adt = MatricesVectors.MatrixVectorMultiplication(A, dt);
                atAdt = Vectors.ScalarVectorMultiplication(Adt, at);
                atdt = Vectors.ScalarVectorMultiplication(dt, at);
                xtplus1 = Vectors.AddVectors(xt, atdt);              // x(t+1) = x(t) + at * dt
                rtplus1 = Vectors.SubVectors(rt, atAdt);         // r(t+1) = r(t) - at * A * dt
                stplus1 =MatricesVectors.MatrixVectorMultiplication(M,rtplus1);
                betatplus1 = beta(rtplus1, rt,st, stplus1); // From orthogonality condition 1

                betatplus1dt = Vectors.ScalarVectorMultiplication(dt, betatplus1);    // betatplus1 * dt
                dtplus1 = Vectors.AddVectors(stplus1, betatplus1dt);      // dtplus1 = rtplus1 + betatplus1 * dt

                it++;
                m = it;

                //Έλεγχος Προσσέγγισης
                NormalizedResidualNorm = (Vectors.ProductVectors(rtplus1, stplus1)) / Vectors.ProductVectors(rt,st); // ||r(t+1)/r(t)||

                if (NormalizedResidualNorm < maxerror)  //||r(t+1)/r(t)|| < error
                {
                    it = maxit + 2;
                }

                Vectors.EqualVector(ref xtplus1, ref xt);
                Vectors.EqualVector(ref rtplus1, ref rt);
                Vectors.EqualVector(ref dtplus1, ref dt);
                Vectors.EqualVector(ref stplus1, ref st);
            }

            Console.WriteLine("\n\nΕπαναλήψεις :" + m);

            Console.WriteLine("\nx Προσσέγγισης: ");

            Vectors.PrintDoubleVector(xtplus1);

            Console.WriteLine("\nΤέλος Conjugate Gradient (CG)");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
            return xtplus1;
        }
        static double step(double[] rt, double[] dt,double[] st, double[,] A)
        {
            // Ορισμοί
            double at;
            double rtTst;
            double[] Adt = new double[A.GetLength(0)];
            double dtTAdt;

            // Υπολογισμός step at
            rtTst = Vectors.ProductVectors(rt,st); // rt' * st
            Adt = MatricesVectors.MatrixVectorMultiplication(A, dt);  // A * dt
            dtTAdt = Vectors.ProductVectors(dt, Adt); // dt' * A * dt
            at = rtTst / dtTAdt;    // at = (rt' * st) / (dt' * A * dt)

            return at;
        }
        static double beta(double[] rtplus1, double[] rt,double[] st,double[] stplus1)
        {
            // Ορισμοί
            double betatplus1;
            double rtplus1Tstplus1;
            double rtTst;

            // Υπολογισμός step at
            rtplus1Tstplus1 = Vectors.ProductVectors(rtplus1, stplus1); // rtplus1' * stplus1
            rtTst = Vectors.ProductVectors(rt, st);   // rt' * rt

            betatplus1 = rtplus1Tstplus1 / rtTst; // betatplus1 = (rtplus' * rtplus) / (rt' * rt)

            return betatplus1;
        }
        static double[,] JacobiPreconditioner(double[,] A)
        {
            double[,] M = new double[A.GetLength(0),A.GetLength(1)];
            for (int i = 0; i < A.GetLength(0); i++)
                if (A[i, i] != 0)
                    M[i, i] = 1 / A[i, i];
            return M;
        }
        public static void test()
        {
            Console.WriteLine("[][][][][][][][][][][][][][][][][][][][][][][][]");
            Console.WriteLine("\nΤεστ στην Conjugate Gradient (CG)\n");
            double[,] A = { { 16, 8, 4 }, { 8, 29, 12 }, { 4, 12, 41 } };
            double[] x = Vectors.GenerateVector(A.GetLength(0));
            double[] b = MatricesVectors.MatrixVectorMultiplication(A, x);
            double[,] M = JacobiPreconditioner(A);
            Console.WriteLine("Θετικά ορισμένο συμμετρικό Μητρώο Α");
            Matrices.PrintMatrix(A);
            Console.WriteLine("\nΤυχαίο διάνυσμα x");
            Vectors.PrintDoubleVector(x);
            Console.WriteLine("\nΔιάνυσμα b που προκύπτει");
            Vectors.PrintDoubleVector(b);
            double[] xtest = Solve(A, b, M);
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
            Console.WriteLine("\nΤΕΛΟΣ - Τεστ Conjugate Gradient (CG)\n");
            Console.WriteLine("[][][][][][][][][][][][][][][][][][][][][][][][]");
            Console.ReadKey();
        }
    }
}
