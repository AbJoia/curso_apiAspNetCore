using System.ComponentModel.DataAnnotations;

namespace src.Api.Domain.Dtos
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email é campo obtigatório para login.")]
        [EmailAddress(ErrorMessage = "Email em formato inválido.")]
        [StringLength(100, ErrorMessage = "Campo deve ter na máximo {1} caracteres.")]
        public string Email { get; set; }
    }
}