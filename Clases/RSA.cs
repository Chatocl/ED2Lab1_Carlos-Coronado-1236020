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
        public string LLavePublica()
        {
            string llave = "";
            int N = 0;
            int Z = 0;

            int Primo1 = Primo();
            int Primo2 = Primo();

            return llave;
        }
        public string LLavePrivada()
        {
            string llave = "";
            int Primo1 = Primo();
            int Primo2 = Primo();
            llave = Primo1.ToString() + "," + Primo2.ToString();
            return llave;
        }
        internal int Primo()
        {
            Random r = new Random();
            bool EsPrimo = false;
            int a = 0;
            int num = 0;
            while (!EsPrimo)
            {
                num = r.Next(0, 524288);
                for (int i = 1; i < (num + 1); i++)
                {
                    if (a % i == 0)
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
