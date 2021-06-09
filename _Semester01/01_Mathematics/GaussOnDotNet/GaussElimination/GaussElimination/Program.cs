using System;

namespace GaussElimination
{
    class Program
    {
        static void Main(string[] args)
        {
            // PENDING: Check if the matrix is singular
            //          Check if the matrix is zero
            //          Check if the matrix is _____

            double[,] A0 = new double[2, 3] { { 2, 3, 4 }, { 5, 6, 7 } };
            double[,] A1 = new double[2, 3] { { 0.24836937, 0.12553841, 0.69173313 }, 
                {0.56901761, 0.13038817, 0.75507039} };
            double[,] A2 = new double[2, 3] { { 0.0004, 1.402, 1.406 }, { 0.4003, -1.502, 2.501 } };
            double[,] A3 = new double[2, 3] { { 2, 3, 4 }, { 5, 6, 7 } };


            for (int i = 0; i < A2.GetLength(0); i++)
            {
                for (int j = 0; j < A2.GetLength(1); j++)
                {
                    Console.Write(A2[i, j] + "\t");
                }
                Console.WriteLine();
            }
            GaussPivotedSig(A2, 4);
            GaussUnPivotedSig(A2, 4);
            GaussPivotedSig(A2, 5);
            GaussUnPivotedSig(A2, 5);
            GaussPivotedSig(A2, 6);
            GaussUnPivotedSig(A2, 6);

            double[,] rndA = rndMatrix(2, 3);

            for (int i = 0; i < rndA.GetLength(0); i++)
            {
                for (int j = 0; j < rndA.GetLength(1); j++)
                {
                    Console.Write(rndA[i, j] + "\t");
                }
                Console.WriteLine();
            }
            GaussPivotedSig(rndA, 4);
            GaussUnPivotedSig(rndA, 4);
            GaussPivotedSig(rndA, 5);
            GaussUnPivotedSig(rndA, 5);
            GaussPivotedSig(rndA, 6);
            GaussUnPivotedSig(rndA, 6);

            Console.WriteLine();
            Console.ReadKey();
        }

        public static double[,] rndMatrix(int row, int col)
        {
            Random random = new Random();
            random.NextDouble();
            double[,] A = new double[2, 3];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    A[i, j] = random.NextDouble();
                }
            }

            return A;
        }
        static double SetSig(double value, int digits)
        {
            digits = digits - 1;
            int log10 = (int)Math.Floor(Math.Log10(Math.Abs(value)));
            double exp = Math.Pow(10, log10);
            value /= exp;
            value = Math.Round(value, digits);
            value *= exp;

            return value;
        }
        public static double[] GaussPivotedSig(double[,] A, int d)
        {

            double[,] B = new double[2,3];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    B[i, j] = SetSig(A[i, j],d);
                }
            }

            double maxElement = B[0, 0];
            int maxRow = 0;
            for (int i = 1; i < 2; i++)
            {
                if (Math.Abs(B[i, 0]) > Math.Abs(maxElement))
                {
                    maxElement = B[i, 0];
                    maxRow = i;
                }
            }


            double[,] temp = new double[2, 3];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp[i, j] = B[Math.Abs(i - maxRow), j];
                }
            }

            B = temp;

            double ratio = SetSig(B[1, 0] / B[0, 0], d);

            for (int i = 0; i < 3; i++)
            {
                B[1, i] = SetSig(B[1, i] - SetSig(ratio * B[0, i], d), d);
            }

            double[] x = new double[] { 0, 0 };
            x[1] = SetSig(B[1, 2] / B[1, 1], d);
            x[0] = SetSig(SetSig((B[0, 2] - SetSig((x[1] * B[0, 1]),d)),d) / B[0, 0], d);

            Console.WriteLine("------ Pivoted with Significant Digits = " + d + " --------");
            Console.WriteLine("X1 = " + (decimal)x[0] + " X2 = " + (decimal)x[1]);

            return x;
        }
        public static double[] GaussUnPivotedSig(double[,] A, int d)
        {

            double[,] B = new double[2, 3];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    B[i, j] = SetSig(A[i, j], d);
                }
            }

            double maxElement = B[0, 0];
            int maxRow = 0;
            
            double ratio = SetSig(B[1, 0] / B[0, 0], d);

            for (int i = 0; i < 3; i++)
            {
                B[1, i] = SetSig(B[1, i] - SetSig(ratio * B[0, i], d), d);
            }

            double[] x = new double[] { 0, 0 };
            x[1] = SetSig(B[1, 2] / B[1, 1], d);
            x[0] = SetSig(SetSig((B[0, 2] - SetSig((x[1] * B[0, 1]), d)), d) / B[0, 0], d);

            Console.WriteLine("------ Not pivoted with Significant Digits = "+ d + " --------");
            Console.WriteLine("X1 = " + (decimal)x[0] + " X2 = " + (decimal)x[1]);

            return x;
        }
        







        public static double[] GaussEliminationUnPivoted(double[,] A)
        {
            double[,] B = A;
            double maxElement = B[0, 0];
            int maxRow = 0;


            double ratio = B[1, 0] / B[0, 0];

            for (int i = 0; i < 3; i++)
            {
                B[1, i] = B[1, i] - ratio * B[0, i];
            }

            double[] x = new double[] { 0, 0 };
            x[1] = B[1, 2] / B[1, 1];
            x[0] = (B[0, 2] - (x[1] * B[0, 1])) / B[0, 0];

            Console.WriteLine("X1 = " + x[0] + " X2 = " + x[1]);

            return x;
        }


        public static double[] GaussEliminationPivoted(double[,] A)
        {
            double[,] B = A;
            double maxElement = B[0, 0];
            int maxRow = 0;
            for (int i = 1; i < 2; i++)
            {
                if (Math.Abs(B[i, 0]) > Math.Abs(maxElement))
                {
                    maxElement = B[i, 0];
                    maxRow = i;
                }
            }


            double[,] temp = new double[2, 3];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp[i, j] = B[Math.Abs(i - maxRow), j];
                }
            }

            B = temp;

            double ratio = B[1, 0] / B[0, 0];

            for (int i = 0; i < 3; i++)
            {
                B[1, i] = B[1, i] - ratio * B[0, i];
            }

            double[] x = new double[] { 0, 0 };
            x[1] = B[1, 2] / B[1, 1];
            x[0] = (B[0, 2] - (x[1] * B[0, 1])) / B[0, 0];

            Console.WriteLine("X1 = " + x[0] + " X2 = " + x[1]);

            return x;
        }

    }
}
