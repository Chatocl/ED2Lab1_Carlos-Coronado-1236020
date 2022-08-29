using System;
using ED2Lab1_Carlos_Coronado_1236020.Models.Datos;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2Lab1_Carlos_Coronado_1236020.Models
{
    public class Persona: IComparable<Persona>
    {

        public string Nombre { get; set; }
        public int DPi { get; set; }
        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime? FDB { get; set; }
        public string Direccion { get; set; }
        public int CompareTo(Persona Otro) 
        {
            if(Otro == null) 
            {
                return 0; 
            }
            else
            {
                return this.DPi.CompareTo(Otro.DPi);
            }
        
        }
    }
}
