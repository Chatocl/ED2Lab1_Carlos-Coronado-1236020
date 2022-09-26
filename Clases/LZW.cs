using System;
using System.Collections.Generic;
using System.Text;

namespace Clases
{
    public class LZW
    {
        public List<int> Comprimir(string Text) 
        {
            Dictionary<string, int> Dic = new Dictionary<string, int>();
            List<int> RCom = new List<int>();
            string Aux = "";
            //Llenar el dic 
            for (int a = 0; a < 256; a++)
            {
                Dic.Add(((char)a).ToString(), a);
            }
            //Comprimir y agregar las cadenas de caracteres que no esten en el DIc
            foreach (char Caracter in Text)
            {
                string Aux2 = Aux + Caracter;
                if (Dic.ContainsKey(Aux2)) 
                {
                    Aux = Aux2;
                }
                else
                {
                    //Guardar el valor comprimido
                    RCom.Add(Dic[Aux]);
                    //Agregar cadena nueva en el Dic
                    Dic.Add(Aux2, Dic.Count);
                    Aux=Caracter.ToString();
                }
         
            }
            if (!string.IsNullOrEmpty(Aux))
            {
                RCom.Add(Dic[Aux]);
            }
            return RCom;
        }
        public string Descomprimir(List<int> Text) 
        {
            Dictionary<int,string> Dic = new Dictionary<int, string>();
            string Aux2 = "";
            //Llenar el dic 
            for (int b = 0; b < 256; b++)
            {
                Dic.Add(b,((char)b).ToString());
            }
            string Aux = Dic[Text[0]];
            Text.RemoveAt(0);
            StringBuilder RDes = new StringBuilder(Aux);
            //Descomprimir la lista en el mensaje 
            foreach (int pos in Text)
            {
                Aux2 = null;
                if (Dic.ContainsKey(pos))
                {
                    Aux2 = Dic[pos];
                }
                else if(pos == Dic.Count)
                {
                    Aux2 = Aux + Aux[0];
                }

                RDes.Append(Aux2);
                //Agregar cadena nueva en el Dic
                Dic.Add(Dic.Count,Aux+Aux2[0]);
                Aux = Aux2;
            }

            return RDes.ToString();
        }
    }
}
