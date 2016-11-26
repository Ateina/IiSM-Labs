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

        #region система
        static double[,] a = new double[,] {{0.7, -0.2},
                                            {0.5, -0.5}};
        static double[] f = { 1, 2 };
        static double[] pi = {0.5, 0.5};
        public static double[,] P = new double [2,2];
        
        static int N = 10000;
        static int m = 100000;

        //static int[] i = new int[N + 1];
        //static double[] Q = new double[N + 1];
        //static double[] ksi = new double[m];
        static double alpha;

        static double countVariable(int[] h)
        {
            int[] i = new int[N + 1];
            double[] ksi = new double[m];
            double[] Q = new double[N + 1];
            for (int e = 0; e < 2; e++)
            {
                for (int j = 0; j < 2; j++)
                {
                    P[e, j] = 0.5;
                }
            }

            double x = 0.0;
            Random r = new Random();
            //m реализаций цепи маркова
            //вспомогательная цепь маркова
            for (int k = 0; k < m; k++)
            {
                alpha = r.NextDouble();
                if (alpha < 0.5)
                    i[0] = 0;
                else i[0] = 1;
                for (int j = 1; j <= N; j++)
                {
                    alpha = r.NextDouble();
                    if (alpha < 0.5)
                        i[j] = 0;
                    else i[j] = 1;
                }
                //веса
                if (pi[0] > 0)
                    Q[0] = h[i[0]] / pi[i[0]];
                else Q[0] = 0;
                for (int j = 1; j <= N; j++)
                {
                    int t1 = i[j - 1];
                    int t2 = i[j];
                    if (P[t1, t2] > 0.0)
                    {
                        Q[j] = Q[j - 1] * a[t1, t2] / P[t1, t2];
                    }
                    else Q[j] = 0;
                }
                //СВ
                for (int j = 0; j <= N; j++)
                    ksi[j] += Q[j] * f[i[j]];
            }
            for (int j = 0; j < m; j++)
                x += ksi[j];
            //Arrays.fill(i, 0);
            //Arrays.fill(Q, 0.0);
            //Arrays.fill(ksi, 0.0);
            return x / m;
        }

        public static void Resolve_System()
        {
            double x = countVariable(new int[]{1, 0});
            Console.WriteLine("x = " + x);
            double y = countVariable(new int[]{0, 1});
            Console.WriteLine("y = " + y);
            Console.WriteLine();
        }
        #endregion

        static void Main(string[] args)
        {
            //Console.WriteLine("Standard integral  :  " + Standard_integral());
            //Console.WriteLine("Double integral  :  " + Double_integral());

            Resolve_System();
            Console.ReadLine();
        }
    }
}
