using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    internal class Program
    {
        public const int T_START = 0;
        public const int T_MAX = 6;
        public const double STEP = 0.5;
        public const int SITUATIONS = 4;
        public const int FACTORS = 7;

        public static List<double> TIME = Calculate.CalculateTime();

        public static readonly double[,] ALPHA_HAT = {
            {0.5, 0.6, 0.65, 0.5, 0, 0.7, 0.6},
            {0, 0, 0.6, 0.7, 0, 0, 0.4},
            {0, 0, 0.7, 0.7, 0.4, 0.55, 0.65},
            {0, 0, 0.75, 0.6, 0.4, 0.5, 0}
        };

        public static readonly double[,] IP_HAT = {
            {0.6, 0.7, 0.4, 0.8, 0, 0.7, 0.6},
            {0, 0, 0.5, 0.6, 0, 0, 0.5},
            {0, 0, 0.4, 0.4, 0.4, 0.8, 0.6},
            {0, 0, 0.6, 0.3, 0.35, 0.6, 0}
        };

        public static readonly double[,] ID_HAT = {
            {0.7, 0.8, 0.4, 0.7, 0, 0.7, 0.7},
            {0, 0, 0.3, 0.8, 0, 0, 0.8},
            {0, 0, 0.3, 0.8, 0.4, 0.6, 0.6},
            {0, 0, 0.5, 0.7, 0.3, 0.7, 0}
        };

        public static readonly double[,] IT_HAT = {
            {0.8, 0.8, 0.6, 0.8, 0, 0.8, 0.9},
            {0, 0, 0.7, 0.9, 0, 0, 0.6},
            {0, 0, 0.5, 0.8, 0.5, 0.7, 0.75},
            {0, 0, 0.8, 0.75, 0.55, 0.8, 0}
        };
        static void Main(string[] args)
        {
            double[,] gamma = Calculate.CalculateABY(Calculate.CalculateY, new double[3, 3]);
            double[,] alpha = Calculate.CalculateABY(Calculate.CalculateA, gamma);
            double[,] beta = Calculate.CalculateABY(Calculate.CalculateB, gamma);

            Console.WriteLine("Alpha");
            Calculate.OutputArray(alpha);
            Console.WriteLine("Beta");
            Calculate.OutputArray(beta);
            Console.WriteLine("Gamma");
            Calculate.OutputArray(gamma);
            List<double[,]> ip_S = new List<double[,]>();
            List<double[,]> id_S = new List<double[,]>();
            List<double[,]> it_S = new List<double[,]>();
            List<double[,]> probability = new List<double[,]>();
            List<double[,]> results = new List<double[,]>();

            for (int index = 0; index < SITUATIONS; index++)
            {
                ip_S.Add(Calculate.CalculateIpdt((row, col, time) =>
                {
                    double value = 10 * IP_HAT[row, col] * (Math.Log10(1 + alpha[row, col]) * Math.Pow(time + 1, 2));
                    return Calculate.CheckStrictCondition(value) ? value : 1;
                }, index));

                id_S.Add(Calculate.CalculateIpdt((row, col, time) =>
                {
                    double value = (Math.Pow(1 + 0.5 * beta[row, col] + gamma[row, col], 2)) / (Math.Pow(1 + ID_HAT[row, col], 2) + 0.4 * alpha[row, col]);
                    return Calculate.CheckStrictCondition(value) ? value : 1;
                }, index ));

                it_S.Add(Calculate.CalculateIpdt((row, col, time) =>
                {
                    double value = 0.05 * IT_HAT[row,col] * ((2 + Math.Pow(10, -2) * alpha[row, col]) * (1- 3 * beta[row, col] * time));
                    return Calculate.CheckStrictCondition(value) ? value : 1;
                }, index));

                probability.Add(Calculate.CalculateProbability(alpha, index, ip_S[index], id_S[index], it_S[index], beta, gamma));

                Console.WriteLine("\nIP_S" + (index + 1));
                Calculate.OutputArray(ip_S[index]);

                Console.WriteLine("\nID_S" + (index + 1));
                Calculate.OutputArray(id_S[index]);

                Console.WriteLine("\nIT_S" + (index + 1));
                Calculate.OutputArray(it_S[index]);

                Console.WriteLine("\nProbability" + (index + 1));
                Calculate.OutputArray(probability[index]);

                results.Add(Calculate.CalculateResults(0, 1, 1, probability[index]));

                Console.WriteLine("\nResults" + (index + 1));
                Calculate.OutputArray(results[index]);
            }
        }
    }
}
