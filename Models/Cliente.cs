using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CuscuzBank.Models
{
    public class Cliente : PessoaFisica
    {
        public required string Id { get; set; }
        public required Endereco Endereco { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public double RendaMensal { get; set; }
    }
}
