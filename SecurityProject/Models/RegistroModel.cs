using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SecurityProject.Models
{
    public class RegistroModel
    {
        [Required(ErrorMessage = "Correo es requerido")]
        [DisplayName("Correo Electronico")]
        [EmailAddress(ErrorMessage = "Esto debe ser un correo")]
        public string? UserEmail { get; set; }

        [Required(ErrorMessage = "Nombre es requerido")]
        [DisplayName("Nombre")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Apellido es requerido")]
        [DisplayName("Apellido")]
        public string? UserLastName { get; set; }

        [Required(ErrorMessage = "Contraseña es requerido")]
        [DisplayName("Contraseña")]
        [DataType(DataType.Password)]
        public string? UserPassword { get; set; }

        [Required(ErrorMessage = "Confirme la contraseña")]
        [DisplayName("Confirmar Contraseña")]
        [DataType(DataType.Password)]
        [Compare("UserPassword", ErrorMessage = "La contraseña no es la misma")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Rol es requerido")]
        [DisplayName("Rol")]
        public int RolID { get; set; }
    }
}
