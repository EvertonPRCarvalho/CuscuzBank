using System.ComponentModel.DataAnnotations;

namespace CuscuzBank.Models
{
    public class Endereco
    {

        [Key]
        public required string Id { get; set; }
        public string? Logradouro { get; set; }
        public int Numero { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Bairro { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Cidade { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Estado { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? CEP { get; set; }
       
    }
}
