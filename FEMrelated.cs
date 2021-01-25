using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAlgorithms.mymaths
{
    class FEMrelated
    {
        public static double[,] GetTransformationMatrix(double[] coordsStart, double[] coordsEnd)
        {
            double l = Math.Sqrt(Math.Pow(coordsStart[0] - coordsEnd[0], 2) + Math.Pow(coordsStart[1] - coordsEnd[1], 2));
            double sn = (coordsEnd[1] - coordsStart[1]) / l;
            double cn = (coordsEnd[0] - coordsStart[0]) / l;
            return new double[,]
            {
                { cn, sn, 0, 0, 0, 0 },
                { -sn, cn, 0, 0, 0, 0 },
                { 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, cn, sn, 0 },
                { 0, 0, 0, -sn, cn, 0 },
                { 0, 0, 0, 0, 0, 1 },
            };
        }  // 6 βαθμοί ελευθερίας
        public static double[,] GetTransformationMatrixTranspose(double[] coordsStart, double[] coordsEnd)
        {
            double l = Math.Sqrt(Math.Pow(coordsStart[0] - coordsEnd[0], 2) + Math.Pow(coordsStart[1] - coordsEnd[1], 2));
            double sn = (coordsEnd[1] - coordsStart[1]) / l;
            double cn = (coordsEnd[0] - coordsStart[0]) / l;
            return new double[,]
            {
                { cn, -sn, 0, 0, 0, 0 },
                { sn, cn, 0, 0, 0, 0 },
                { 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, cn, -sn, 0 },
                { 0, 0, 0, sn, cn, 0 },
                { 0, 0, 0, 0, 0, 1 },
            };
        }  // 6 βαθμοί ελευθερίας
        public static double[,] GetBeamStiffness(double[] coordsStart, double[] coordsEnd, double E, double I, double A)
        {
            double l = Math.Sqrt(Math.Pow(coordsStart[0] - coordsEnd[0], 2) + Math.Pow(coordsStart[1] - coordsEnd[1], 2));
            double t = E * A / l;
            double a = 4 * E * I / l;
            double b = 2 * E * I / l;
            double c = 6 * E * I / l / l;
            double d = 12 * E * I / l / l / l;
            double[,] K = new double[,]
            {
                { t, 0, 0, -t, 0, 0 },
                { 0, d, c, 0, -d, c },
                { 0, c, a, 0, -c, b },
                { -t, 0, 0, t, 0, 0 },
                { 0, -d, -c, 0, d, -c },
                { 0, c, b, 0, -c, a },
            };

            return Matrices.MatrixMultiplication(GetTransformationMatrixTranspose(coordsStart, coordsEnd), Matrices.MatrixMultiplication(K, GetTransformationMatrix(coordsStart, coordsEnd)));
        } // 6 βαθμοί ελευθερίας
        public static double[,] GetBeamMass(double[] coordsStart, double[] coordsEnd, double rho, double A)
        {
            double l = Math.Sqrt(Math.Pow(coordsStart[0] - coordsEnd[0], 2) + Math.Pow(coordsStart[1] - coordsEnd[1], 2));
            double t = 0.5 * rho * A * l;
            double a = 0.01 * rho * A * l * l * l;
            double[,] M = new double[,]
            {
                { t, 0, 0, 0, 0, 0 },
                { 0, t, 0, 0, 0, 0 },
                { 0, 0, a, 0, 0, 0 },
                { 0, 0, 0, t, 0, 0 },
                { 0, 0, 0, 0, t, 0 },
                { 0, 0, 0, 0, 0, a },
            };

            return Matrices.MatrixMultiplication(GetTransformationMatrixTranspose(coordsStart, coordsEnd), Matrices.MatrixMultiplication(M, GetTransformationMatrix(coordsStart, coordsEnd)));
        }
        public static (double, double, double, double, double, double) GetBeamConstants()
        {
            const double E = 2.1e6;
            const double A = 0.01;
            const double I = 8.3e-6;
            const double rho = 100;
            const double dt = 0.01;
            const int steps = 1000;
            return (E, A, I, rho, dt, steps);
        } // E,A,I,rho,dt,steps
        static double[,] SolveDiagonal(double a, double[,] A, double[,] b)
        {
            double[,] result = new double[A.GetLength(0), 1];
            for (int i = 0; i < A.GetLength(0); i++)
            {
                result[i, 0] = b[i, 0] / A[i, i] / a;
            }

            return result;
        }
        
    }
}
