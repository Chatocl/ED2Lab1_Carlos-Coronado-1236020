using System;
using System.Collections.Generic;


namespace Clases
{
    public class Arbol_B <T>: Nodo_B<T> where T : IComparable<T>
    {
        internal int min;
        internal int max;
        internal Nodo_B<T> Root = new Nodo_B<T>();

        public Arbol_B(int Grado) 
        {
            if (Grado<=0)
            {
                throw new Exception("Grado del arbol incorrecto");
            }
            else
            {
                min = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Grado/2)));
                max = Grado;
                Root = new Nodo_B<T>(min);
            }
        }

        public void Insertar(T Valor) 
        {
            Nodo_B<T> Temp = new Nodo_B<T>();
            Nodo_B<T> Aux = new Nodo_B<T>();
            Temp = Root;

            if (Temp.NumClaves==(2*min-1))
            {
               
                Root = Aux;
                Aux.EsHoja = false;
                Aux.NumClaves = 0;
                Aux.Hijos[0] = Temp;
                Division(Aux, 0, Temp);
                Ayu_Insertar(Aux, Valor);
            }
            else
            {
                Ayu_Insertar(Aux, Valor);
            }
        
        }
        internal void Ayu_Insertar(Nodo_B<T> Nodo , T Valor) 
        {

            if (Nodo.EsHoja)
            {
                int a;
                a = Nodo.NumClaves;

                while (a>=1 && Valor.CompareTo(Nodo.Claves[a-1]) <= 0)
                {
                    Nodo.Claves[a] = Nodo.Claves[a - 1];
                    a--;
                }
                Nodo.Claves[a] = Valor;
                Nodo.NumClaves++;
            }
            else
            {
                int b = 0;
                while (b<Nodo.NumClaves && Valor.CompareTo(Nodo.Claves[b]) > 0)
                {
                    b++;
                }
                if (Nodo.Hijos[b].NumClaves == (2*min-1) )
                {
                    Division(Nodo, b, Nodo.Hijos[b]);
                    if (Valor.CompareTo(Nodo.Claves[b])>0)
                    {
                        b++;
                    }
                    Ayu_Insertar(Nodo.Hijos[b], Valor);
                }
            }

        }
        
        internal void Division(Nodo_B<T> PNodo, int Posicion, Nodo_B<T> HNodo) 
        {
            Nodo_B<T> Aux = new Nodo_B<T>();
            Aux.EsHoja = HNodo.EsHoja;
            Aux.NumClaves = (min-1);
            for (int a = 0; a <(min-1) ; a++)
            {
                Aux.Claves[a]= HNodo.Claves[a+min];
            }

            if (HNodo.EsHoja==false)
            {
                for (int b = 0; b < min; b++)
                {
                    Aux.Hijos[b]= HNodo.Hijos[b+min];
                }
            }

            HNodo.NumClaves = min - 1;
            for (int c = PNodo.NumClaves; c < Posicion; c++)
            {
                PNodo.Hijos[(c+1)]= PNodo.Hijos[c];
            }

            PNodo.Hijos[Posicion + 1] = Aux;
            for (int d = PNodo.NumClaves; d < Posicion; d--)
            {
                PNodo.Claves[(d + 1)] = PNodo.Claves[d];
            }

            PNodo.Claves[Posicion] = HNodo.Claves[min - 1];
            PNodo.NumClaves++;
        }

        public List<T> ObtenerLista() 
        {
            List<T> Aux = new List<T>();
            InOrder(Root, Aux);
            return Aux;

        }
        internal void InOrder(Nodo_B<T> Aux, List<T> Lista) 
        {
            int a;
            for (a = 0; a < Aux.NumClaves; a++)
            {
                if (Aux.Hijos[a]!=null)
                {
                    InOrder(Aux.Hijos[a], Lista);
                }
                Lista.Add(Aux.Claves[a]);
            }

            if (Aux.Hijos[a] != null)
            {
                InOrder(Aux.Hijos[a], Lista);
            }
        }
    }
}
