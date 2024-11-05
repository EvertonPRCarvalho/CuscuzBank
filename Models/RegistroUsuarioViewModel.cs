using System.ComponentModel.DataAnnotations;

namespace CuscuzBank.Models
{
    public class RegistroUsuarioViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public required string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public required string ConfirmPassword { get; set; }
    }
}
