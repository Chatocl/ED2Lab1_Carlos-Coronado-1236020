using System;
using System.Collections.Generic;
using System.Text;

namespace Clases
{       //Nodos Huffman usa basico
        internal class HuffmanNode<T> : IComparable
        {
            internal HuffmanNode(double prob, T value)
            {
                Prob = prob;
                LChild = RChild = Parent = null;
                Value = value;
                Hoja = true;
            }

            internal HuffmanNode(HuffmanNode<T> lchild, HuffmanNode<T> rchild)
            {
                LChild = lchild;
                RChild = rchild;
                Prob = lchild.Prob + rchild.Prob;
                lchild.Binari = true;
                rchild.Binari = false;
                lchild.Parent = rchild.Parent = this;
                Hoja = false;
            }

            internal HuffmanNode<T> LChild { get; set; }
            internal HuffmanNode<T> RChild { get; set; }
            internal HuffmanNode<T> Parent { get; set; }
            internal T Value { get; set; }
            internal bool Hoja { get; set; }

            internal bool Binari { get; set; }

            internal int Bit
            {
                get { return Binari ? 0 : 1; }
            }

            internal bool IsRoot
            {
                get { return Parent == null; }
            }

            internal double Prob { get; set; }

            public int CompareTo(object obj)
            {
                return -Prob.CompareTo(((HuffmanNode<T>)obj).Prob);
            }
        }
        public class Huffman<T> where T : IComparable
        {
            private readonly Dictionary<T, HuffmanNode<T>> DicHoja = new Dictionary<T, HuffmanNode<T>>();
            private readonly HuffmanNode<T> Raiz;

            public Huffman(IEnumerable<T> values)
            {
                var counts = new Dictionary<T, int>();
                var Monticulo = new Monticulo<HuffmanNode<T>>();
                int VCount = 0;
                //Contar y dar valor a las letras
                foreach (T value in values)
                {
                    if (!counts.ContainsKey(value))
                    {
                        counts[value] = 0;
                    }
                    counts[value]++;
                    VCount++;
                }
                //Almacenar en monticulo
                foreach (T value in counts.Keys)
                {
                    var node = new HuffmanNode<T>((double)counts[value] / VCount, value);
                    Monticulo.Add(node);
                    DicHoja[value] = node;
                }
                //Ordenar el monticulo 
                while (Monticulo.Count > 1)
                {
                    HuffmanNode<T> leftSon = Monticulo.Pop();
                    HuffmanNode<T> rightSon = Monticulo.Pop();
                    var parent = new HuffmanNode<T>(leftSon, rightSon);
                    Monticulo.Add(parent);
                }

                Raiz = Monticulo.Pop();
                Raiz.Binari = false;
            }

            public void AuxCodificar(T value, List<int> Encode)
            {
                if (!DicHoja.ContainsKey(value))
                {
                    throw new ArgumentException("Valor incorrecto");
                }
                HuffmanNode<T> AuxNodo = DicHoja[value];
                var AuxCodi = new List<int>();
                while (!AuxNodo.IsRoot)
                {
                    AuxCodi.Add(AuxNodo.Bit);
                    AuxNodo = AuxNodo.Parent;
                }

                AuxCodi.Reverse();
                Encode.AddRange(AuxCodi);
            }

            public List<int> Codificar(IEnumerable<T> values)
            {
                var AuxList = new List<int>();

                foreach (T value in values)
                {
                    AuxCodificar(value, AuxList);
                }
                return AuxList;
            }

            public T AuxDecodificar(List<int> bitString, ref int pos)
            {
                HuffmanNode<T> AuxNodo = Raiz;
                while (!AuxNodo.Hoja)
                {
                    if (pos > bitString.Count)
                    {
                        throw new ArgumentException("Valor incorrecto");
                    }
                    AuxNodo = bitString[pos++] == 0 ? AuxNodo.LChild : AuxNodo.RChild;
                }
                return AuxNodo.Value;
            }

            public List<T> Decodificar(List<int> bitString)
            {
                int Pos = 0;
                var AuxList = new List<T>();

                while (Pos != bitString.Count)
                {
                    AuxList.Add(AuxDecodificar(bitString, ref Pos));
                }
                return AuxList;
            }
    }

}
