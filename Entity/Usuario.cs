using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Usuario
    {
        [Key]
        [Column(TypeName = "varchar(30)")]
        public string User { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string Password { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string Nombre { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string Apellidos { get; set; }
        [NotMapped]
        public string Token { get; set; }
        [Column(TypeName = "varchar(8)")]
        public string Rol { get; set; }
        public string Estado { get; set; }
        public string IdPersona { get; set; }
         
    }
}