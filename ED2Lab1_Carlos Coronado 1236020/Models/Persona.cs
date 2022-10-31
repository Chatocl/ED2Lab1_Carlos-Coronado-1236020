using System;
using ED2Lab1_Carlos_Coronado_1236020.Models.Datos;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2Lab1_Carlos_Coronado_1236020.Models
{
    public class Persona : IComparable<Persona>
    {

        public string name { get; set; }
        public string dpi { get; set; }
        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime? datebirth { get; set; }
        public string address { get; set; }
        public string[] companies { get; set; }
        public string recluiter { get; set; }
        public string[] CarRecomen { get; set; }
        public string[] Conv { get; set; }
        public string[] LlavesPriv { get; set; }
        public List<int>[] CarCod { get; set; }
        public string LLavePub { get; set; }
        public bool CODI { get; set; }
        public bool DECODI { get; set; }
        public bool CODI2 { get; set; }
        public bool DECODI2 { get; set; }
        public bool CODI3 { get; set; }
        public bool DECODI3 { get; set; }
        public string[] codificacion { get; set; }
        public string[] decodificacion { get; set; }
        public int CompareTo(Persona Otro) 
        {
            if(Otro == null) 
            {
                return 0; 
            }
            else
            {
                return this.dpi.CompareTo(Otro.dpi);
            }
        
        }
    }
}
