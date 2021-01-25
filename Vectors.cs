using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAlgorithms.mymaths
{
    class Vectors
    {
        public static double[] GetVector()
        {
            Console.WriteLine("Διάσταση διανύσματος:");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine(" ");
            Console.WriteLine("Η διάσταση που πλήκτρολογήσατε είναι: " + n);
            double[] a = new double[n];
            Console.WriteLine("Πληκτρολογίστε τις συντεταγμένες του διανύσματος.");
            for (int i = 0; i < n; i++)
            {
                int k = i + 1;
                Console.WriteLine(k + "η Συντεταγμένη:");
                a[i] = double.Parse(Console.ReadLine());
                Console.WriteLine("Η " + k + "η συντεταγμένη είναι:" + a[i]);
                Console.WriteLine(" ");
            }
            return (a);
        } //Λήψη διανύσματος από χρήστη
        public static double[] AddVectors(double[] a, double[] b)
        {
            if (a.Length > b.Length)
            {
                int n = a.Length;
                double[] c = new double[n];
                c = a;
                int m = b.Length;
                for (int i = 0; i < m; i++)
                {
                    c[i] = c[i] + b[i];
                }
                return (c);
            }
            else if (a.Length < b.Length)
            {
                int n = b.Length;
                double[] c = new double[n];
                c = b;
                int m = a.Length;
                for (int i = 0; i < m; i++)
                {
                    c[i] = c[i] + a[i];
                }
                return (c);
            }
            else
            {
                int m = b.Length;
                double[] c = new double[m];
                for (int i = 0; i < m; i++)
                {
                    c[i] = a[i] + b[i];
                }
                return (c);
            }
        } //Πρόσθεση διανυσμάτων
        public static double[] SubVectors(double[] a, double[] b)
        {
            //Έλεγχος διαστάσεων δωθέντων διανυσμάτων.
            if (a.Length > b.Length)
            {
                //Αν το κάποιο διάνυσμα έχει περισσότερες διαστάσεις, τότε απαιτούνται περισσότερες επαναλήψεις
                int n = a.Length;
                double[] c = new double[n];
                c = a;
                int m = b.Length;
                for (int i = 0; i < m; i++)
                {
                    c[i] = c[i] - b[i];
                }
                return (c);
            }
            else if (a.Length < b.Length)
            {
                int n = b.Length;
                double[] c = new double[n];
                c = b;
                int m = a.Length;
                for (int i = 0; i < m; i++)
                {
                    c[i] = c[i] - a[i];
                }
                return (c);
            }
            else
            {
                int m = b.Length;
                double[] c = new double[m];
                for (int i = 0; i < m; i++)
                {
                    c[i] = a[i] - b[i];
                }
                return (c);
            }
        } //Αφαίρεση διανυσμάτων
        public static double ProductVectors(double[] a, double[] b)
        {
            double sum = 0;
            int n = a.Length;
            for (int i = 0; i < n; i++)
            {
                sum += a[i] * b[i];
            }
            return (sum);
        } //Εσωτερικό γίνόμενο διανυσμάτων
        public static void PrintDoubleVector(double[] x)
        {
            int d = x.Length;
            Console.Write("(");
            for (int i = 0; i < d; i++)
            {
                if (i == (d - 1))
                {
                    Console.Write(x[i]);

                }
                else
                {
                    Console.Write(x[i] + " , ");
                }
            }
            Console.Write(")");
            Console.WriteLine();
        } //Εκτυπώνει double[] διάνυσμα
        public static void PrintIntVector(int[] x)
        {
            int d = x.Length;
            Console.Write("(");
            for (int i = 0; i < d; i++)
            {
                if (i == (d - 1))
                {
                    Console.Write(x[i]);

                }
                else
                {
                    Console.Write(x[i] + ",");
                }
            }
            Console.Write(")");
            Console.WriteLine();
        } //Εκτυπώνει int[] διάνυσμα
        public static double[] ScalarVectorMultiplication(double[] a, double s)
        {
            int d = a.Length;
            double[] c = new double[d];
            if (s == 0)
            {
                for (int i = 0; i < d; i++)
                {
                    c[i] = 0;
                }
            }
            else
            {

                for (int i = 0; i < d; i++)
                {
                    c[i] = a[i] * s;
                }
            }
            return (c);
        } // Πολλαπλασιασμός αριθμού με διάνυσμα
        public static double Norm2(double[] a)
        {
            double norm2 = 0;
            for (int i = 0; i < a.Length; i++)
            {
                norm2 = norm2 + a[i] * a[i];
            }
            norm2 = Math.Sqrt(norm2);
            return (norm2);
        } // Εύρεση Ευκλείδιας Νόρμας (Norm2)
        public static void VectorEqualityCheck(double[] vec1, double[] vec2)
        {
            int n1 = vec1.Length;
            int n2 = vec2.Length;
            int flag=1;
            /* Comparing two vectors for equality */

            if (n1 != n2)
            {
                Console.Write("The Vectors Cannot be compared :\n");
            }
            else
            {
                Console.Write("The Vectors can be compared : \n");
                for (int i = 0; i < n1; i++)
                { 
                        if (Math.Round((vec1[i] - vec2[i]),4) != 0)
                        {
                            flag = 0;
                            break;
                        }
                }
                if (flag == 1)
                    Console.Write("Two Vectors are equal.\n\n");
                else
                {
                    Console.Write("Two Vectors are not equal\n\n");
                    Console.Write("Vector 1:\n");
                    PrintDoubleVector(vec1);
                    Console.Write("Vector 2:\n");
                    PrintDoubleVector(vec2);
                }
            }
        }
        public static void EqualVector(ref double[] vec1, ref double[] vec2)
        {
            int n1 = vec1.Length;
            int n2 = vec2.Length;
            for(int i = 0; i < n1; i++)
                vec2[i] = vec1[i];
        } //vec1 -> vec2
        public static double[] GenerateVector(int n)
        {
            double[] b = new double[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                    b[i] = random.Next(-50, 500);
            }
            return b;
        } // generates vector with diamension n (n is given)
    }
}
