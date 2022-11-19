

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SecurityProject.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Correo es requerido")]
        [DisplayName("Correo Electronico")]
        [EmailAddress(ErrorMessage = "Esto debe ser un correo")]
        public string? UserEmail { get; set; }

        [Required(ErrorMessage = "Contraseña es requerido")]
        [DisplayName("Contraseña")]
        [DataType(DataType.Password)]
        public string? UserPassword { get; set; }
    }
}
