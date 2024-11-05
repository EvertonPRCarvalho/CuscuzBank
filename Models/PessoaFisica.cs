using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuscuzBank.Models
{
    /// <summary>
    /// Para criação desta classe foi utilizado o exemplo presente no site:https://apicenter.estaleiro.serpro.gov.br/documentacao/consulta-cpf/pt/demonstracao/
    /// <summary>
    public class PessoaFisica
    {
        [Key]
        public string? Cpf { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Situacao { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataNascimento { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Obito { get; set; }
    }
}
