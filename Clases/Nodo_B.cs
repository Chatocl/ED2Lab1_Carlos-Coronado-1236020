using System;
using System.Collections.Generic;
using System.Text;

namespace Clases
{
    public  class Nodo_B<T> where T : IComparable<T>
    {
       public T[] Claves;
       public int NumClaves;

       public Nodo_B<T>[] Hijos; 
       public int NumHijos;

       public bool EsHoja;

        public Nodo_B() 
        {
        }
        public Nodo_B(int min) 
        {
            Claves = new T[2*min];
            Hijos = new Nodo_B<T>[2*min+1];
            NumClaves = 0;
            NumHijos = 0;
            EsHoja = true;
        }
         

    }
}
