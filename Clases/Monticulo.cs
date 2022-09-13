using System;
using System.Collections.Generic;
using System.Text;

namespace Clases
{
    public class Monticulo<T> where T : IComparable
    {
        protected List<T> Heap = new List<T>();

        public int Count
        {
            get { return Heap.Count; }
        }
        protected virtual void SetAt(int i, T val)
        {
            Heap[i] = val;
        }

        protected bool RChildEST(int i)
        {
            return RChildPos(i) < Heap.Count;
        }

        protected bool LChildEST(int i)
        {
            return LChildPos(i) < Heap.Count;
        }

        protected int PPos(int i)
        {
            return (i - 1) / 2;
        }

        protected int LChildPos(int i)
        {
            return 2 * i + 1;
        }

        protected int RChildPos(int i)
        {
            return 2 * (i + 1);
        }

        protected T ArrayVal(int i)
        {
            return Heap[i];
        }

        protected T Parent(int i)
        {
            return Heap[PPos(i)];
        }

        protected T Left(int i)
        {
            return Heap[LChildPos(i)];
        }

        protected T Right(int i)
        {
            return Heap[RChildPos(i)];
        }

        public void Add(T val)
        {
            Heap.Add(val);
            SetAt(Heap.Count - 1, val);
            UpHeap(Heap.Count - 1);
        }

        public T Peek()
        {
            if (Heap.Count == 0)
            {
                throw new IndexOutOfRangeException("No hay valores en el monticulo");
            }

            return Heap[0];
        }

        public T Pop()
        {
            if (Heap.Count == 0)
            {
                throw new IndexOutOfRangeException("No hay valores en el monticulo");
            }

            T valRet = Heap[0];

            SetAt(0, Heap[Heap.Count - 1]);
            Heap.RemoveAt(Heap.Count - 1);
            DownHeap(0);
            return valRet;
        }

        protected void Swap(int i, int j)
        {
            T valHold = ArrayVal(i);
            SetAt(i, Heap[j]);
            SetAt(j, valHold);
        }

        protected void UpHeap(int i)
        {
            while (i > 0 && ArrayVal(i).CompareTo(Parent(i)) > 0)
            {
                Swap(i, PPos(i));
                i = PPos(i);
            }
        }

        protected void DownHeap(int i)
        {
            while (i >= 0)
            {
                int iContinue = -1;

                if (RChildEST(i) && Right(i).CompareTo(ArrayVal(i)) > 0)
                {
                    iContinue = Left(i).CompareTo(Right(i)) < 0 ? RChildPos(i) : LChildPos(i);
                }
                else if (LChildEST(i) && Left(i).CompareTo(ArrayVal(i)) > 0)
                {
                    iContinue = LChildPos(i);
                }

                if (iContinue >= 0 && iContinue < Heap.Count)
                {
                    Swap(i, iContinue);
                }

                i = iContinue;
            }
        }
    }
}
