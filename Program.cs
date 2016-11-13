using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Random;
using MathNet.Numerics.Distributions;

namespace IiSM4Lab
{
    class Program
    {
        public static double f(double x)
        {
        if (x < 1)
            {
                return 0;
            }
        else
            {
                return Math.Pow(Math.Sin(x), 5);
            }
        }

        public static double var_x() 
        {
            var exp = new Exponential(1);
            double randomSample = exp.Sample();
            //Console.WriteLine(exp + "   " + randomSample);
            return randomSample;
        }

        public static double integral()
        {
            int size = 10000;
            double integrale = 0.0;
            for (int i = 0; i < size; i++)
            {
                double x = var_x();
                integrale += f(x);
            }
            return integrale/size;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(integral());
            Console.ReadLine();
        }
    }
}
