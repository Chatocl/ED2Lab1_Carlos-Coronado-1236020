using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clases
{
    public class Arbol_AVL<T> : NonLinearDataStructureBase<T> where T : IComparable<T>
    {
        private Nodo<T> Raiz = new Nodo<T>();
        private Nodo<T> temp = new Nodo<T>();
        private List<T> listaOrdenada = new List<T>();


        public void Add(T value)
        {
            Inser(value);
        }
        public int ObtenerFE(Nodo<T> n)
        {

            if (n == null)
            {
                return -1;
            }
            else
            {
                return n.FE;
            }

        }
        protected override void Delete(Nodo<T> nodo)
        {
            if (nodo.Izquierdo.Valor == null && nodo.Derecho.Valor == null) // Caso 1
            {
                nodo.Valor = nodo.Derecho.Valor;
            }
            else if (nodo.Derecho.Valor == null) // Caso 2
            {
                nodo.Valor = nodo.Izquierdo.Valor;
                nodo.Derecho = nodo.Izquierdo.Derecho;
                nodo.Izquierdo = nodo.Izquierdo.Izquierdo;
            }
            else // Caso 3
            {
                if (nodo.Izquierdo.Valor != null)
                {
                    temp = Derecha(nodo.Izquierdo);
                }
                else
                {
                    temp = Derecha(nodo);
                }
                nodo.Valor = temp.Valor;
            }

        }
        public T Remove(T deleted)
        {
            Nodo<T> busc = new Nodo<T>();
            busc = Get(Raiz, deleted);
            if (busc != null)
            {
                Delete(busc);
            }
            return deleted;
        }
        private Nodo<T> Derecha(Nodo<T> nodo)
        {
            if (nodo.Derecho.Valor == null)
            {
                if (nodo.Izquierdo.Valor != null)
                {
                    return Derecha(nodo.Izquierdo);
                }
                else
                {
                    Nodo<T> temporal = new Nodo<T>();
                    temporal.Valor = nodo.Valor;
                    nodo.Valor = nodo.Derecho.Valor;
                    return temporal;
                }
            }
            else
            {
                return Derecha(nodo.Derecho);
            }
        }
        protected override Nodo<T> Get(Nodo<T> nodo, T value)
        {
            if (value.CompareTo(nodo.Valor) == 0)
            {
                return nodo;
            }
            else if (value.CompareTo(nodo.Valor) == -1)
            {
                if (nodo.Izquierdo.Valor == null)
                {
                    return null;
                }
                else
                {
                    return Get(nodo.Izquierdo, value);
                }
            }
            else
            {
                if (nodo.Derecho.Valor == null)
                {
                    return null;
                }
                else
                {
                    return Get(nodo.Derecho, value);
                }
            }
        }
        public Nodo<T> InsertarAVL(Nodo<T> nodo, Nodo<T> tempo) //Se inserta el valor en el arbol y se verifico si está ordenado
        {
            try
            {
                Nodo<T> nuevoNodo = tempo;

                if (nodo.Valor.CompareTo(tempo.Valor) == -1)
                {
                    if (tempo.Izquierdo.Valor == null)
                    {
                        tempo.Izquierdo = nodo;

                    }
                    else
                    {

                        tempo.Izquierdo = InsertarAVL(nodo, tempo.Izquierdo);

                        if ((ObtenerFE(tempo.Izquierdo) - ObtenerFE(tempo.Derecho) == 2))
                        {
                            if (nodo.Valor.CompareTo(tempo.Izquierdo.Valor) == -1)
                            {
                                nuevoNodo = RotIzq(tempo);
                            }
                            else
                            {
                                nuevoNodo = RotDIzq(tempo);
                            }
                        }

                    }
                }
                else if (nodo.Valor.CompareTo(tempo.Valor) == 1)
                {

                    if (tempo.Derecho.Valor == null)
                    {
                        tempo.Derecho = nodo;
                    }
                    else
                    {


                        tempo.Derecho = InsertarAVL(nodo, tempo.Derecho);
                        if ((ObtenerFE(tempo.Derecho) - ObtenerFE(tempo.Izquierdo) == 2))
                        {
                            if (nodo.Valor.CompareTo(tempo.Derecho.Valor) == 1)
                            {
                                nuevoNodo = RotDer(tempo);
                            }
                            else
                            {
                                nuevoNodo = RotDDER(tempo);
                            }
                        }

                    }
                }
                //altura
                if ((tempo.Izquierdo == null) && (tempo.Derecho != null))
                {
                    tempo.FE = tempo.Derecho.FE + 1;
                }
                else if ((tempo.Izquierdo != null) && (tempo.Derecho == null))
                {
                    tempo.FE = tempo.Izquierdo.FE + 1;
                }
                else
                {
                    tempo.FE = Math.Max(ObtenerFE(tempo.Izquierdo), ObtenerFE(nodo.Derecho)) + 1;
                }
                return nuevoNodo;
            }
            catch
            {
                throw;
            }
        }
        public Nodo<T> CrearNodoAVL(T valor)
        {
            Nodo<T> nodo = new Nodo<T>();
            nodo.Valor = valor;
            nodo.FE = 0;
            nodo.Izquierdo = new Nodo<T>();
            nodo.Derecho = new Nodo<T>();
            return nodo;

        }
        public void Inser(T value)
        {
            try
            {
                Nodo<T> nuevo = CrearNodoAVL(value);

                if (Raiz.Valor == null)
                {
                    Raiz = nuevo;
                }
                else
                {
                    Raiz = InsertarAVL(nuevo, Raiz);

                }


            }
            catch
            {
                throw;
            }
        }
        protected override Nodo<T> Insert(Nodo<T> nodo, T value)
        {
            try
            {
                Nodo<T> nuevo = CrearNodoAVL(value);

                if (nodo == null)
                {
                    nodo = nuevo;
                }
                else
                {
                    nodo = InsertarAVL(nuevo, nodo);

                }
                return nodo;

            }
            catch
            {
                throw;
            }
        }
        public Nodo<T> RotIzq(Nodo<T> nodo)
        {

            Nodo<T> aux = nodo.Izquierdo;

            nodo.Izquierdo = aux.Derecho;
            aux.Derecho = nodo;
            nodo.FE = Math.Max(ObtenerFE(nodo.Izquierdo), ObtenerFE(nodo.Derecho)) + 1;//Devuelve el mayor
            aux.FE = Math.Max(ObtenerFE(aux.Izquierdo), ObtenerFE(aux.Derecho)) + 1;// Devuelve el mayor
            return aux;
        }

        public Nodo<T> RotDer(Nodo<T> nodo)
        {

            Nodo<T> aux = nodo.Derecho;
            nodo.Derecho = aux.Izquierdo;
            aux.Izquierdo = nodo;
            nodo.FE = Math.Max(ObtenerFE(nodo.Izquierdo), ObtenerFE(nodo.Derecho)) + 1;//Devuelve el mayor
            aux.FE = Math.Max(ObtenerFE(aux.Izquierdo), ObtenerFE(aux.Derecho)) + 1;// Devuelve el mayor
            return aux;
        }

        public Nodo<T> RotDIzq(Nodo<T> nodo)// Rotación Doble Izquierda
        {
            Nodo<T> tem = new Nodo<T>();
            nodo.Izquierdo = RotDer(nodo.Izquierdo);
            tem = RotIzq(nodo);
            return tem;

        }

        public Nodo<T> RotDDER(Nodo<T> nodo)// Rotación Doble Derecho
        {

            Nodo<T> tem = new Nodo<T>();
            nodo.Derecho = RotIzq(nodo.Derecho);
            tem = RotDer(nodo);
            return tem;

        }

        private void InOrder(Nodo<T> nodo)
        {
            if (nodo.Valor != null)
            {
                InOrder(nodo.Izquierdo);
                listaOrdenada.Add(nodo.Valor);
                InOrder(nodo.Derecho);
            }
        }

        public List<T> ObtenerLista()
        {
            listaOrdenada.Clear();
            InOrder(Raiz);
            return listaOrdenada;
        }

        public List<T> Obtener(Func<T, bool> Predicate)
        {
            List<T> prov = new List<T>();
            ObtenerLista();
            for (int i = 0; i < listaOrdenada.Count(); i++)
            {
                if (Predicate(listaOrdenada[i]))
                {
                    prov.Add(listaOrdenada[i]);
                }
            }
            return prov;
        }


        //**************************************************************************************************
    }
}
