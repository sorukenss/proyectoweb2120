using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity{ 

    public class UtilidadConsultorio{

        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoUtilidad { get; set; }

        public double ValorCita { get; set; }

        public double Ganancia { get; set; }

        


    }
}