using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class HistorialClinico
    {
         [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoHistorial{get; set;}
        public List<Cita> citas { get; set; }

        public List<Tratamiento> tratamientos { get; set; }
    }
}