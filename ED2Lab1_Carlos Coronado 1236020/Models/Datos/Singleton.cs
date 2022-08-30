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
        public Clases.Arbol_AVL<Persona> ArbolAVL = new Clases.Arbol_AVL<Persona>();
     
    }
}
