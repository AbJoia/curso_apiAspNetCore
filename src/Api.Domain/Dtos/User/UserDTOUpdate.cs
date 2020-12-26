using System;
using System.ComponentModel.DataAnnotations;

namespace src.Api.Domain.Dtos.User
{
    public class UserDTOUpdate
    {
        [Required(ErrorMessage = "Id é obrigatório.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength (60, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Email é campo obirgatório.")]
        [EmailAddress (ErrorMessage = "Email em formato inválido.")]
        [StringLength (100, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
        public string Email { get; set; }
    }
}