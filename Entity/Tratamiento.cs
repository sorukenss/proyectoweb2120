using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Tratamiento
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoTratamiento{get; set;}
        public string Recomendacion{get; set;}
        public string Diagnostico{get; set;}

        
        [NotMapped]
        public List<Medicamento> Medicamentos{get; set;} 
    }
}