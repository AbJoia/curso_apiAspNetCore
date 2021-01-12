using System;
using System.ComponentModel.DataAnnotations;

namespace src.Api.Domain.Dtos.Municipio
{
    public class MunicipioDtoUpdate
    {
        [Required(ErrorMessage = "Id é campo obrigatório.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome de Município é campo obrigatório.")]
        [StringLength(60, ErrorMessage = "Nome de Município deve ter no máximo {1} caracteres.")]      
        public string Nome { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Código IBGE Inválido.")]
        public int CodIBGE { get; set; }

        [Required(ErrorMessage = "Código UF é campo obrigatório.")]        
        public Guid UfId { get; set; } 
    }
}