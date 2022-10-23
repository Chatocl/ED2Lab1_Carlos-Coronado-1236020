using System;
using System.Collections.Generic;
using System.Text;

namespace Clases
{
    public class Cesar
    {
        public string Cifrar(string code, string key)
        {
            Dictionary<int, char> Dic = new Dictionary<int, char>();
            //nuevo abecedario
            for (int i = 0; i < key.Length; i++)
            {
                if (!Dic.ContainsValue(key[i]))
                {
                    Dic.Add(Dic.Count, key[i]);
                }
            }
            for (int i = 0; i < 256; i++)
            {
                if (!Dic.ContainsValue(Convert.ToChar(i)))
                {
                    Dic.Add(Dic.Count, Convert.ToChar(i));
                }
            }
            //cifrado
            string cifrado = "";
            foreach (char l in code)
            {
                cifrado += Dic[Convert.ToInt32(l)];
            }
            return cifrado;
        }
        public string Descifrar(string decode, string key)
        {
            Dictionary<char, int> cipher = new Dictionary<char, int>();
            //nuevo abecedario
            for (int i = 0; i < key.Length; i++)
            {
                if (!cipher.ContainsValue(key[i]))
                {
                    cipher.Add(key[i], cipher.Count);
                }
            }
            for (int i = 0; i < 256; i++)
            {
                if (!cipher.ContainsKey(Convert.ToChar(i)))
                {
                    cipher.Add(Convert.ToChar(i), cipher.Count);
                }
            }
            //descifrado
            string descifrado = "";
            foreach (char l in decode)
            {
                descifrado += Convert.ToChar(cipher[l]);
            }

            return descifrado;
        }
    }
}
