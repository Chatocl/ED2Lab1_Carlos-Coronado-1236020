using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Clases
{
    public class RSA
    {
        public List<int> LLaves()
        {
            Random r = new Random();
            Random r2 = new Random();
            List<int> Valores  = new List<int>();
            List<int> Kval = new List<int>();
            int Primo1 = Primo();
            int Primo2 = Primo();
            string llave = "";
            int Aux = 0;
            int Aux2 = 0;
            int N = 0;
            int Z = 0;
            int K = 0;
            int J = 0;
           

            N = Primo1 * Primo2;
            Z = (Primo1-1) * (Primo2-1);
            for (int i = 1; i <Z; i++)
            {
                if (MCD(i,Z)==1)
                {
                    Kval.Add(i);
                }
            }
            Aux = r.Next(0, Kval.Count-1);
            K = Kval[Aux];
            Aux2 = r2.Next(0, Kval.Count-1);
            J = Kval[Aux2];
            Valores.Add(N);
            Valores.Add(K);
            Valores.Add(J);
            return Valores;
        }
        private int MCD(int num1,int num2) 
        {
            int a=Math.Max(num1,num2);
            int b=Math.Min(num1,num2);
            int res = 0;
            do
            {
                res = b;
                b = a % b;
                a = res;
            } while (b!=0);
            return res;
        }
        private int Primo()
        {
            Random r = new Random();
            bool EsPrimo = false;
            int a = 0;
            int num = 0;
            while (!EsPrimo)
            {
                num = r.Next(0,1024);
                a = 0;
                for (int i = 1; i < (num + 1); i++)
                {
                    if (num % i == 0)
                    {
                        a++;
                    }
                }
                if (a != 2)
                {
                    EsPrimo = false;
                }
                else
                {
                    EsPrimo = true;
                }

            }
            return num;
        }
    }
}
