using System;

namespace iism2
{
    class Program
    {
        private static double[] bin_array = new double[1000];
        private static double[] bernulli_array = new double[1000];

        #region distribution
        //бином
        public static void binom_distribution()
        {
            double p = 0.25;
            int m = 5;
            Random r = new Random();
            int tmp = 0;
            for (int i = 0; i < bin_array.Length; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    double x = r.NextDouble();
                    if (x <= p)
                    {
                        tmp++;
                    }
                }
                bin_array[i] = tmp;
                tmp = 0;
            }
            Array.Sort(bin_array);
            writeArrayToFile(bin_array, "binom.txt");
            double[] m_bin = countMiArray(bin_array);
            test_binom_distribution(bin_array, m_bin);
        }

        //бернулли
        public static void bernulli_distribution()
        {
            double p = 0.7;
            Random r = new Random();
            for (int i = 0; i < bernulli_array.Length; i++)
            {
                double x = r.NextDouble();
                if (x <= p)
                {
                    bernulli_array[i] = 1;
                }
                if (x > p)
                {
                    bernulli_array[i] = 0;
                }
            }
            writeArrayToFile(bernulli_array, "bernulli.txt");
            double[] m_bern = countMiArrayBern(bernulli_array);
            test_bernulli_distribution(bernulli_array, m_bern);
        }
        #endregion

        #region tests
        public static void test_bernulli_distribution(double[] array, double[] m)
        {
            double[] p = { 0.3, 0.7 };
            double sum = 0;
            for (int i = 0; i < 2; i++) 
            {
                double a = Math.Pow(m[i] - array.Length * p[i], 2);
                sum = sum + a / (array.Length * p[i]);
            }
            Console.WriteLine("Pirson Bernulli: " + sum);
        }

        public static void test_binom_distribution(double[] array, double[] m)
        {
            double[] p = { 0.75, 0.25 };
            double[] m1 = { m[0] + m[1] + 0.5*m[2], 0.5*m[2] + m[3] + m[4] + m[5] };
            double sum = 0;
            for (int i = 0; i < 2; i++)
            {
                double a = Math.Pow(m1[i] - array.Length * p[i], 2);
                sum = sum + a / (array.Length * p[i]);
            }
            Console.WriteLine("Pirson Binom: "+ sum);
        }
        #endregion

        #region func
        public static double[] countMiArray(double[] array)
        {
            double[] m = new double[6];
            for (int i = 0; i < 6; i++)
            {
                m[i] = 0;
            }
            for (int i = 0; i < array.Length; i++)
            {
                int index = (int)array[i];
                m[index]++;
            }
            return m;
        }

        public static double[] countMiArrayBern(double[] array)
        {
            double[] m = new double[2];
            for (int i = 0; i < 2; i++)
            {
                m[i] = 0;
            }
            for (int i = 0; i < array.Length; i++)
            {
                int index = (int)array[i];
                m[index]++;
            }
            return m;
        }

        private static void writeArrayToFile(double[] array, string fileName)
        {
            System.IO.StreamWriter textFile = new System.IO.StreamWriter(@"C:\Users\LEO\Documents\Visual Studio 2015\Projects\Lab2iism\" + fileName);
            for (int i = 0; i < array.Length; i++)
            {
                textFile.WriteLine("a*[" + i + "] = " + array[i]);
            }
            textFile.Close();
        }

        private static double C(int n, int m)
        {
            return 0.0;
        }
        #endregion


        static void Main(string[] args)
        {
            bernulli_distribution();
            binom_distribution();
            Console.ReadLine();
        }
    }
}
