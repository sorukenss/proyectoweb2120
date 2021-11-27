using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{

    public class Chat
    {

        [Key]
        public string CodigoChat { get; set; }
        public string CodigoPaciente { get; set; }

        public string CodigoPsicologo { get; set; }

        public string IdEnviado { get; set;}

        public string Mensaje { get; set; }
    }



}