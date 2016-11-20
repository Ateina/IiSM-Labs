using System;

namespace IiSM_Lab1
{
    class Program
    {
        #region variables
        private static int size = 1000;
        private static double [] alfa = new double [size];
        private static double [] alfa_start = new double [size];
        private static double[] a = new double[size];
        private static double[] b = new double[size];
        private static double[] b_start = new double[size];
        private static double[] c = new double[size];
        static int beta = 19;
        private static double M = Math.Pow(2, 31);
        private static int K = 48;
        private static double[] V = new double[K];
        static double delta = 16.9;
        #endregion

        #region methods
        private static void MCG()
        {
            Console.WriteLine("MCG");
            alfa_start = CountStartAlfa();
            alfa = CountAlfa(alfa_start);
            writeArrayToFile(alfa, "MCG.txt");
        }

        private static void MacLaren_Marsaglia()
        {
            Console.WriteLine("MacLaren_Marsaglia");
            b_start = CountStartAlfa();
            b = CountAlfa(b_start);
            c = createArrayByRandom();
            int renew_index = 47;
            for (int i = 0; i < V.Length; i++)
            {
                V[i] = b[i];
            }
            for (int i = 0; i < a.Length; i++)
            {
                int j = (int)(c[i] * K);
                a[i] = V[j];
                renew_index++;
                if (renew_index < 1000)
                {
                    V[j] = b[renew_index];
                }
            }
            writeArrayToFile(a, "Maclaren-Marsaglia.txt");
        }
        #endregion

        #region tests
        private static void PearsonTest(double[] array)
        {
            double[] m = new double[10];
            m = createMiArray(array);
            double sum = 0;
            double p = 0.1;
            for (int i = 0; i < m.Length; i++)
            {
                sum = sum + (Math.Pow(m[i] - size * p, 2.0)) / (size * p);
            }
            Console.WriteLine("Pearson Test : " + sum);
        }

        private static void KolmagorovTest(double[] array)
        {
            Array.Sort(array);
            double Dn = 0.0;
            double Dn0 = 0.0;
            double Dn1 = 0.0;
            for (int i = 0; i < array.Length; i++)
            {
                double double_i = (double)i;
                Dn0 = Math.Abs(F0(array[i]) - (double_i + 1.0) / 1000.0);
                Dn1 = Math.Abs(F0(array[i]) - (double_i) / 1000.0);
                Dn = max_three(Dn, Dn0, Dn1);
            }
            Console.WriteLine("KolmagorovTest : " + Math.Sqrt(1000.0) * Dn);
        }
        #endregion

        #region help-functions
        private static double[] CountStartAlfa()
        {
            double[] tmp = new double[1000];
            tmp[0] = 19;
            for (int i = 0; i < tmp.Length - 1; i++)
            {
                tmp[i + 1] = (beta * tmp[i]) % M;
            }
            return tmp;
        }

        private static double[] CountAlfa(double[] array_start)
        {
            double[] tmp = new double[1000]; 
            for (int i = 0; i < alfa.Length; i++)
            {
                tmp[i] = array_start[i] / M;
            }
            return tmp;
        }

        private static void writeArrayToFile(double[] array, string fileName)
        {
            System.IO.StreamWriter textFile = new System.IO.StreamWriter(@"C:\Users\LEO\Documents\Visual Studio 2012\Projects\IiSM_Lab1\" + fileName);
            for (int i = 0; i < array.Length; i++)
            {
                textFile.WriteLine("a*[" + i + "] = " + array[i]);
            }
            textFile.Close();
        }

        private static double[] createArrayByRandom()
        {
            double[] tmp = new double[1000];
            Random r = new Random();
            for (int i = 0; i < alfa.Length; i++)
            {
                tmp[i] = r.NextDouble(); ;
            }
            return tmp;
        }

        private static double[] createMiArray(double[] array)
        {
            double[] tmp = new double[10];
            for (int i = 0; i < tmp.Length; i++)
            {
                tmp[i] = 0;
            }
            for (int j = 0; j < array.Length; j++)
            {
                double var = array[j]*10;
                int index_m = (int) var;
                tmp[index_m] = ++tmp[index_m];
            }
            return tmp;
        }

        private static double max_three(double a1, double a2, double a3)
        {
            double tmp = 0.0;
            if (a1 < a2)
            {
                tmp = a2;
            }
            else
            {
                tmp = a1;
            }
            if (tmp < a3)
            {
                return a3;
            }
            else
                return tmp;
        }

        private static double F0(double h)
        {
            if (h > 1.0)
            {
                return 1.0;
            }
            if (h < 0.0)
            {
                return 0.0;
            }
            else
            {
                return h;
            }
        }
        #endregion

        static void Main(string[] args)
        {
            //Мультипликативный конгруэнтный метод
            MCG();
            PearsonTest(alfa);
            KolmagorovTest(alfa);
            //Метод Макларена-Марсальи
            MacLaren_Marsaglia();
            PearsonTest(a);
            KolmagorovTest(a);
            Console.ReadLine();
        }
    }
}
