using System;
using System.Collections.Generic;
using System.Text;

namespace Clases
{
    public class Nodo<T> where T : IComparable<T>
    {
        public Nodo<T> Izquierdo { get; set; }
        public Nodo<T> Derecho { get; set; }
        public T Valor { get; set; }
        public int FE { get; set; }

    }
}
