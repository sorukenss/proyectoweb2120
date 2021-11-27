using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class  Persona
    {
           [Key]
        public string Identification { get; set; }
        public string Nombre { get; set; }
        public string Apellido {get; set;}
        public string Sexo { get; set; }
        public int Edad { get; set; }
        public string Usuario { get; set; }
        public string password { get; set; }
        public string Foto { get; set; }

        public string Direccion { get; set; }

        public  string Telefono { get; set; }

        public string Correo { get; set; }

        public bool Estado { get; set; }

        public string TipoDeIdentificacion { get; set;}
    }
}
