using System;

namespace IiSM_Lab3
{
    class Program
    {
        private static double[] array = new double[1000];
        private static double[] log_array = new double[1000];
        private static double[] laplas_array = new double[1000];

        //Логистическое LG(a,b), a = 2, b = 3; 
        public static void log_distribution()
        {
            double m = 2;
            double D = Math.Pow(3.14, 2) / 3;
            double k = 3;
            Random r = new Random();
            for (int i = 0; i < log_array.Length; i++)
            {
                double y = r.NextDouble();
                log_array[i] = m + k*Math.Log(y/(1 - y));
            }
            Array.Sort(log_array);
            writeArrayToFile(log_array, "log_array.txt");
        }

        //Лапласа L(a), a = 2
        public static void laplas_distribution()
        {
            double lambda = 2.0;
            Random r = new Random();
            for (int i = 0; i < log_array.Length; i++)
            {
                double y = r.NextDouble();
                if (y > 0 && y <= 0.5)
                {
                    laplas_array[i] = (1/lambda)*Math.Log(2*y);
                }
                if (y > 0.5 && y <= 1)
                {
                    laplas_array[i] = (-1) * (1 / lambda) * Math.Log(2 * (1 - y));
                }
            }
            Array.Sort(laplas_array);
            writeArrayToFile(laplas_array, "laplas_array.txt");
        }

        public static double log_F1(double x)
        {
            double p = (-1) * (x - 2) / 3;
            return Math.Pow(1 + Math.Pow(Math.Exp(1), p), -1);
        }

        public static double laplas_F1(double x)
        {
            if (x < 0)
            {
                return 0.5 * Math.Exp(2 * x);
            }
            else
            {
                return 1 - (0.5) * Math.Exp((-1) * 2 * x);
            }
        }

        private static void LogKolmagorovTest(double[] array)
        {
            Array.Sort(array);
            int num = array.Length;
            double Dn = 0.0;
            double Dn0 = 0.0;
            for (int i = 0; i < array.Length; i++)
            {
                Dn0 = Math.Abs((i + 1) / (double)(num) - log_F1(array[i]));
                Dn = Math.Max(Dn, Dn0);
            }
            Console.WriteLine("KolmagorovTest : " + Math.Sqrt(1000.0) * Dn);
            Console.ReadLine();
        }

        private static void LaplasKolmagorovTest(double[] array)
        {
            Array.Sort(array);
            int num = array.Length;
            double Dn = 0.0;
            double Dn0 = 0.0;
            for (int i = 0; i < array.Length; i++)
            {
                Dn0 = Math.Abs((i + 1) / (double)(num) - laplas_F1(array[i]));
                Dn = Math.Max(Dn, Dn0);
            }
            Console.WriteLine("KolmagorovTest : " + Math.Sqrt(1000.0) * Dn);
            Console.ReadLine();
        }

        private static void writeArrayToFile(double[] array, string fileName)
        {
            System.IO.StreamWriter textFile = new System.IO.StreamWriter(@"C:\Users\LEO\Documents\Visual Studio 2012\Projects\IiSM_Lab3\" + fileName);
            for (int i = 0; i < array.Length; i++)
            {
                textFile.WriteLine("a*[" + i + "] = " + array[i]);
            }
            textFile.Close();
        }

        static void Main(string[] args)
        {
            log_distribution();
            LogKolmagorovTest(log_array);
            laplas_distribution();
            LaplasKolmagorovTest(laplas_array);
        }
    }
}
