using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityProject.DB.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Correo es requerido")]
        [DisplayName("Correo Electronico")]
        [EmailAddress(ErrorMessage = "Esto debe ser un correo")]
        public string? UserEmail { get; set; }

        [Required(ErrorMessage ="Nombre es requerido")]
        [DisplayName("Nombre")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Apellido es requerido")]
        [DisplayName("Apellido")]
        public string? UserLastName { get; set; }

        [Required(ErrorMessage = "Contraseña es requerido")]
        [DisplayName("Contraseña")]
        [DataType(DataType.Password)]
        public string? UserPassword { get; set; }

        [Required(ErrorMessage = "Rol es requerido")]
        [DisplayName("Rol")]
        public int RolID { get; set; }

        [ForeignKey("RolID")]
        public virtual Rol Rol { get; set; }
    }
}
