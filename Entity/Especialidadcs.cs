using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Especialidad
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoEspecialidad { get; set; }
        public string NombreEspecialidad { get; set; }
      
        
    }
}