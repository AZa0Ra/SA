using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    internal class Program
    {
        static void Main()
        {
            double[,] table = {
            { 1, 2, 3, 5, 6, 8 },
            { 0.5, 1, 2, 3, 4, 6 },
            { 0.33, 0.5, 1, 2, 3, 5 },
            { 0.2, 0.33, 0.5, 1, 2, 4 },
            { 0.17, 0.25, 0.33, 0.5, 1, 3 },
            { 0.13, 0.17, 0.2, 0.25, 0.33, 1 }
        };

            int rowCount = table.GetLength(0);
            int colCount = table.GetLength(1);

            double[] Vi = new double[rowCount];
            Console.WriteLine("Vi values:");
            for (int i = 0; i < rowCount; i++)
            {
                Vi[i] = GeometricMean(table, i);
                Console.WriteLine($"Vi[{i + 1}] = {Vi[i]:F2}");
            }

            double totalVi = Vi.Sum();
            Console.WriteLine($"\nSum Vi values:\n{totalVi:F2}");
            double[] Pi = Vi.Select(v => v / totalVi).ToArray();

            Console.WriteLine("\nPi values:");
            for (int i = 0; i < Pi.Length; i++)
            {
                Console.WriteLine($"Pi[{i + 1}] = {Pi[i]:F2}");
            }

            double[] En1 = new double[rowCount];
            Console.WriteLine("\nEn1 values:");
            for (int i = 0; i < rowCount; i++)
            {
                En1[i] = 0;
                for (int j = 0; j < colCount; j++)
                {
                    En1[i] += table[i, j] * Pi[j];
                }
                Console.WriteLine($"En1[{i + 1}] = {En1[i]:F6}"); 
            }

            double[] En2 = new double[rowCount];
            Console.WriteLine("\nEn2 values:");
            for (int i = 0; i < rowCount; i++)
            {
                En2[i] = En1[i] / Pi[i];
                Console.WriteLine($"En2[{i + 1}] = {En2[i]:F6}"); 
            }


            double yMax = En2.Average();
            Console.WriteLine($"\nyMax = {yMax:F6}");

            double UI = (yMax - rowCount) / (rowCount - 1);
            Console.WriteLine($"\nUI = {UI:F6}");

            double WI = 1.24;
            Console.WriteLine($"WI = {WI}");

            double WU = UI / WI;
            Console.WriteLine($"\nWU = {WU:F6}");
        }

        static double GeometricMean(double[,] matrix, int row)
        {
            int colCount = matrix.GetLength(1);
            double product = 1.0;
            for (int j = 0; j < colCount; j++)
            {
                product *= matrix[row, j];
            }
            return Math.Pow(product, 1.0 / colCount);
        }
    }
}
