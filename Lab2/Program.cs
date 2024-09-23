using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(190, 36);
            const double STARTX1 = 0;
            const double ENDX1 = 3.01;
            const double STARTX2 = 0;
            const double ENDX2 = 2.01;
            const double STEP = 0.1;

            int rows = (int)(ENDX1 / STEP) + 1;
            int columns = (int)(ENDX2 / STEP) + 1;

            double[,] f12Values = new double[rows, columns];
            double[,] f21Values = new double[rows, columns];

            int rowIndex = 0;
            for (double x1 = STARTX1; x1 <= ENDX1; x1 += STEP, rowIndex++)
            {
                int colIndex = 0;
                for (double x2 = STARTX2; x2 <= ENDX2; x2 += STEP, colIndex++)
                {
                    f12Values[rowIndex, colIndex] = F12(x1, x2);
                    f21Values[rowIndex, colIndex] = F21(x1, x2);
                }
            }

            Console.WriteLine("f12(x1, x2):\n");
            Console.Write("x1\\x2\t");
            for (double x2 = STARTX2; x2 <= ENDX2; x2 += STEP)
            {
                Console.Write($"{x2:F1}\t");
            }
            Console.WriteLine("MIN");

            double maxOfMins12 = double.MinValue;
            for (int i = 0; i < rows; i++)
            {
                double minInRow = double.MaxValue;
                Console.Write($"{STARTX1 + i * STEP:F1}\t");
                for (int j = 0; j < columns; j++)
                {
                    double value = f12Values[i, j];
                    Console.Write($"{value:F2}\t");
                    if (value < minInRow)
                    {
                        minInRow = value;
                    }
                }
                Console.WriteLine($"{minInRow:F2}");
                if (minInRow > maxOfMins12)
                {
                    maxOfMins12 = minInRow; 
                }
            }
            Console.WriteLine("\nf21(x1, x2):\n");

            Console.Write("x1\\x2\t");
            for (double x2 = STARTX2; x2 <= ENDX2; x2 += STEP)
            {
                Console.Write($"{x2:F1}\t");
            }

            Console.WriteLine();
            for (int i = 0; i < rows; i++)
            {
                Console.Write($"{STARTX1 + i * STEP:F1}\t");
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{f21Values[i, j]:F2}\t");
                }
                Console.WriteLine();
            }

            Console.Write("\nMIN");
            double maxOfMins21 = double.MinValue;
            for (int j = 0; j < columns; j++)
            {
                double minInColumn = double.MaxValue;
                for (int i = 0; i < rows; i++)
                {
                    double value = f21Values[i, j];
                    if (value < minInColumn)
                    {
                        minInColumn = value;
                    }
                }
                Console.Write($"\t{minInColumn:F2}");
                if (minInColumn > maxOfMins21)
                {
                    maxOfMins21 = minInColumn;
                }
            }
            Console.WriteLine($"\n\nMaxOfMinsF12: {maxOfMins12}\nMaxOfMinsF21: {maxOfMins21}");


            double[,] f12Diff = new double[rows, columns];
            double[,] f21Diff = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    f12Diff[i, j] = f12Values[i, j] - maxOfMins12;
                    f21Diff[i, j] = f21Values[i, j] - maxOfMins21;
                }
            }

            Console.WriteLine("\nf12 - maxOfMins12:");
            Console.Write("x1\\x2\t");
            for (double x2 = STARTX2; x2 <= ENDX2; x2 += STEP)
            {
                Console.Write($"{x2:F1}\t");
            }
            Console.WriteLine();

            for (int i = 0; i < rows; i++)
            {
                Console.Write($"{STARTX1 + i * STEP:F1}\t");
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{f12Diff[i, j]:F2}\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nf21 - maxOfMins21:");
            Console.Write("x1\\x2\t");
            for (double x2 = STARTX2; x2 <= ENDX2; x2 += STEP)
            {
                Console.Write($"{x2:F1}\t");
            }
            Console.WriteLine();

            for (int i = 0; i < rows; i++)
            {
                Console.Write($"{STARTX1 + i * STEP:F1}\t");
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{f21Diff[i, j]:F2}\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.Write("x1\\x2\t");
            for (double x2 = STARTX2; x2 <= ENDX2; x2 += STEP)
            {
                Console.Write($"{x2:F1}\t");
            }
            Console.WriteLine();
            string[,] resultTable = new string[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (f12Diff[i, j] >= 0 && f21Diff[i, j] >= 0)
                    {
                        resultTable[i, j] = Math.Max(f12Diff[i, j], f21Diff[i, j]).ToString("F2");
                    }
                    else
                    {
                        resultTable[i, j] = "-----";
                    }
                }
            }

            for (int i = 0; i < rows; i++)
            {
                Console.Write($"{STARTX1 + i * STEP:F1}\t");
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{resultTable[i, j]}\t");
                }
                Console.WriteLine();
            }

            double? minValue = null;
            double minX1 = -1, minX2 = -1;
            for (int i = 0; i < resultTable.GetLength(0); i++)
            {
                for (int j = 0; j < resultTable.GetLength(1); j++)
                {
                    if (resultTable[i, j] != "-----")
                    {
                        if (double.TryParse(resultTable[i, j], out double number))
                        {
                            if (minValue == null || number < minValue)
                            {
                                minValue = number;
                                minX1 = i * STEP;
                                minX2 = j * STEP;
                            }
                        }
                    }
                }
            }
            if (minValue.HasValue)
            {
                Console.WriteLine($"\nOptimal solutions: {minValue.Value}");
                Console.WriteLine($"x1 = {minX1}, x2 = {minX2}");
            }
            else
            {
                Console.WriteLine("No numbers found in array");
            }

        }
        static double F12(double x1, double x2)
        {
            return 5 * Math.Pow(x1, 2) - 12 * x1 * x2 + 3 * Math.Pow(x2, 2) + 15;
        }
        static double F21(double x1, double x2)
        {
            return (15 * Math.Pow(x2, 2) - 6 * Math.Pow(x2, 3) - 7 * x2 + 10)
                * (5 * Math.Pow(x1, 2) - 8 * x1 + 7);
        }
    }
}
