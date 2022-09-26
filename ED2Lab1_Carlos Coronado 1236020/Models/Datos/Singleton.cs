using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2Lab1_Carlos_Coronado_1236020.Models.Datos
{
    public class Singleton
    {
        private static Singleton _instance = null;
        public static Singleton Instance
        {
           get
           {
               if (_instance == null) _instance = new Singleton();
               return _instance;
           }
        }

        //public Clases.Arbol_B<Persona> ArbolB = new Clases.Arbol_B<Persona>(5);
        public int bandera;
        string Companies = "";
        public List<Persona> Aux = new List<Persona>();
        public Clases.Arbol_AVL<Persona> ArbolAVL = new Clases.Arbol_AVL<Persona>();
        private const string companies = "Parker Inc Conn - Huels Hickle Ziemann and Legros Nolan LLC McDermott Cummerata and Thompson Welch - Shields O'Hara and Sons Parisian Gleichner and Collins Moore and Sons Gottlieb - Sporer Schiller Fadel and Gislason Rowe LLC Tremblay Inc Schuppe, D'Amore and Hilpert Dickinson - Nikolaus Vandervort - Weimann Jenkins - Douglas Lind Inc Connelly - Swaniawski Block and Sons Emard LLC Daugherty Group Hermiston Lakin and Jacobi Swift - Volkman Wunsch LLC Witting - Becker Jones Grady and Breitenberg Ziemann - Borer Upton LLC Barrows and Sons Maggio LLC Daniel - Franey Stehr - Langosh Gaylord, Schiller and Murray Pollich and Sons Schuster, Olson and Doyle Turner LLC Jacobs - Farrell Lakin - Altenwerth Mante - Lesch Kub and Sons Mayer, Block and Gaylord Pagac - Bosco Wisozk - Strosin Cassin, Kreiger and McKenzie Corkery - Rosenbaum Marvin - Legros Gislason Group Mertz Casper and Hirthe Ziemann and Sons 1 22 333 4444 55555 666666 7777777 88888888 9999999999 0000000000";
        public Clases.Huffman<char> Codifi = new Clases.Huffman<char>(companies);
        public Clases.LZW CodiCartas = new Clases.LZW();
    }
}