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
        #region простой интеграл
        public static double Standard_f(double x)
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
            return randomSample;
        }

        public static double Standard_integral()
        {
            int size = 10000;
            double integrale = 0.0;
            for (int i = 0; i < size; i++)
            {
                double x = var_x();
                integrale += Standard_f(x);
            }
            return integrale/size;
        }
#endregion 

        #region двойной интеграл
        public static double Double_f(double x, double y)
        {
            if ((x < 0) || (x > 1) || (y < 0) || (y > 2))
            {
                return 0;
            }
            else
            {
                return (Math.Pow(x, 2) + Math.Pow(y, 2));
            }
        }

        public static double Double_var(int upper)
        {
            var cu = new ContinuousUniform(0, upper);
            double randomSample = cu.Sample();
            return randomSample;
        }

        public static double Double_integral()
        {
            int size = 10000;
            double integrale = 0.0;
            for (int i = 0; i < size; i++)
            {
                double x = Double_var(1);
                double y = Double_var(2);
                integrale += Double_f(x, y);
            }
            return 2 * integrale / size;
        }
        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine("Standard integral  :  " + Standard_integral());
            Console.WriteLine("Double integral  :  " + Double_integral());
            Console.ReadLine();
        }
    }
}
