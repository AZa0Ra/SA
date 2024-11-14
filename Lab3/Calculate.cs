using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab3.Program;

namespace Lab3
{

    public class Calculate
    {

        public delegate double CalculateHatsMethod(int row, int col, double[,] array, double time);

        public delegate double CalculateABYMethod(int row, int col, double[,] array);

        private static bool CheckCondition(double number)
        {
            return number > 0 && number <= 1;
        }

        public static double CalculateA(int row, int col, double[,] gamma)
        {
            return CheckCondition(Program.ALPHA_HAT[row, col]) ? 0.5 * (Program.IT_HAT[row, col] + Program.IP_HAT[row,col]) * Program.ALPHA_HAT[row,col] : 0;
        }
        public static double CalculateB(int row, int col, double[,] gamma)
        {
            return CheckCondition(Program.ALPHA_HAT[row, col]) ? Math.Exp(Program.IT_HAT[row, col]) * Math.Pow(10, -4) / Math.Pow(1 + Program.ALPHA_HAT[row,col], 2) : 0;
        } 
        public static double CalculateY(int row, int col, double[,] array)
        {
            return CheckCondition(Program.ALPHA_HAT[row, col]) ? 1.5 * Math.Exp(-0.5 * (Program.IT_HAT[row,col] + Program.ID_HAT[row,col]) ) * Program.ALPHA_HAT[row,col] : 0;
        }
        public static List<double> CalculateTime()
        {
            var result = new List<double>();
            for (double time = T_START; time <= T_MAX; time += STEP)
            {
                result.Add(time);
            }
            return result;
        }
        public static bool CheckStrictCondition(double number)
        {
            return number >= 0 && number <= 1;
        }
        public static double[,] CalculateResults(int min, int max, int value, double[,] probability)
        {
            double[,] result = new double[probability.GetLength(0), probability.GetLength(0)];
            for (int row = 0; row < probability.GetLength(0); row++)
            {
                for (int col = 0; col < probability.GetLength(1); col++)
                {
                    double prob = probability[row, col];
                    result[row, col] = (prob >= min && prob < max) ? TIME[row] : value;
                }
            }
            return result;
        }
        public static double[,] CalculateABY(CalculateABYMethod method, double[,] array)
        {
            double[,] result = new double[SITUATIONS, FACTORS];
            for (int row = 0; row < SITUATIONS; row++)
            {
                for (int col = 0; col < FACTORS; col++)
                {
                    result[row, col] = method(row, col, array);
                }
            }
            return result;
        }
        public static void OutputArray(double[,] array)
        {
            for (int row = 0; row < array.GetLength(0); row++)
            {
                for (int col = 0; col < array.GetLength(1); col++)
                {
                    Console.Write($"{array[row, col]:F6} \t");
                }
                Console.WriteLine();
            }
        }

        public static double[,] CalculateIpdt(Func<int, int, double, double> method, int line)
        {
            int rows = TIME.Count;
            double[,] result = new double[rows, FACTORS];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < FACTORS; col++)
                {
                    result[row, col] = method(line, col, TIME[row]);
                }
            }

            return result;
        }

        public static double[,] CalculateProbability(double[,] alpha, int line, double[,] ip, double[,] id, double[,] it, double[,] beta, double[,] gamma)
        {
            int rows = TIME.Count;
            double[,] result = new double[rows, FACTORS];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < FACTORS; col++)
                {
                    result[row, col] = 1 - Math.Log(1 + alpha[line, col] * ip[row, col] * id[row, col] * it[row, col]
                        * (1 + alpha[line, col]) * (1 + beta[line, col]) * (1 + gamma[line, col]))
                        / Math.Log(2);
                }
            }

            return result;
        }
    }
}
