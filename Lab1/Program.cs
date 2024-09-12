using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const double xStart = 1.0;
            const double xEnd = 5.0;
            const double step = 0.1;
            const double f1 = 0.2;
            const double f2 = 1.0;

            List<double> f1Values = new List<double>();
            List<double> f2Values = new List<double>();
            List<bool> comparisonResults = new List<bool>();
            List<double> maxValues = new List<double>();
            List<double> minValues = new List<double>();
            List<double> F1 = new List<double>();
            List<double> F2 = new List<double>();

            Func<double, double> f1x = (x) => 0.8 * Math.Exp(-2 * Math.Pow(x - 3, 2));
            Func<double, double> f2x = (x) => 10 - 6 * x + Math.Pow(x, 2);

            for (double x = xStart; x <= xEnd; x += step)
            {
                double f1Val = f1x(x);
                double f2Val = f2x(x);

                f1Values.Add(f1Val);
                f2Values.Add(f2Val);

                bool isConditionMet = (f1Val >= f1) && (f2Val >= f2);
                comparisonResults.Add(isConditionMet);

                if (isConditionMet)
                {
                    F1.Add(f1Val / f1);
                    F2.Add(f2Val / f2);
                    maxValues.Add(Math.Max(f1Val / f1, f2Val / f2));
                    minValues.Add(Math.Min(f1Val / f1, f2Val / f2));
                }
                else
                {
                    F1.Add(double.NaN);
                    F2.Add(double.NaN);
                    maxValues.Add(double.NaN);
                    minValues.Add(double.NaN);
                }
            }

            double globalMinMaxValue = double.PositiveInfinity;
            double globalMaxMinValue = double.NegativeInfinity;
            int minMaxIndex = -1;
            int maxMinIndex = -1;

            for (int i = 0; i < maxValues.Count; i++)
            {
                if (!double.IsNaN(maxValues[i]) && maxValues[i] < globalMinMaxValue)
                {
                    globalMinMaxValue = maxValues[i];
                    minMaxIndex = i;
                }

                if (!double.IsNaN(minValues[i]) && minValues[i] > globalMaxMinValue)
                {
                    globalMaxMinValue = minValues[i];
                    maxMinIndex = i;
                }
            }

            Console.WriteLine("x\tf1(x)\tf2(x)\tCondition\tF1\t\tF2\t\tMax(f1, f2)\tMin(f1, f2)");
            for (int i = 0; i < f1Values.Count; i++)
            {
                double x = xStart + i * step;
                Console.WriteLine($"{x:F2}\t{f1Values[i]:F4}\t{f2Values[i]:F4}\t{comparisonResults[i]}\t\t{F1[i]:F4}\t\t{F2[i]:F4}\t\t{maxValues[i]:F4}\t\t{minValues[i]:F4}");
            }
            if (minMaxIndex != -1 && maxMinIndex != -1)
            {
                double xMinMax = xStart + minMaxIndex * step;
                double xMaxMin = xStart + maxMinIndex * step;

                Console.WriteLine($"\nMin number of maxValues: {globalMinMaxValue:F4} at x = {xMinMax:F2}");
                Console.WriteLine($"\nMax number of minValues: {globalMaxMinValue:F4} at x = {xMaxMin:F2}");
            }
            else
            {
                Console.WriteLine("\nNo values ​​were found");
            }
        }
    }
}
