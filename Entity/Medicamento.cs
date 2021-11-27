using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Medicamento
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoMedicamento { get; set; }
        public string Nombre{get;set;}
        public string Instrucciones{get; set;}

        public string HoraDeTomar { get; set; }
    }
}