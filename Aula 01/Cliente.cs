/*Construa um cadastro completo (CRUD) de clientes.
Neste cadastro, o cliente deve possuir cpf, nome, data de nascimento e idade.*/

using System.ComponentModel.DataAnnotations;

namespace Aula_01
{
    public class Cliente
    {
        [Required(ErrorMessage = "O CPF é obrigatório, insira no formato: 000000000-n")]
        [MaxLength(12, ErrorMessage = "O CPF, nesse caso, tem no máximo 12 caracteres (considerando o traço)\n[000000000-1]")]
        [MinLength(11, ErrorMessage = "O CFP precisa ter no minimo 11 caracteres.")]
        public string ?Cpf { get; set; }


        [Required(ErrorMessage = "Digite um nome, ele é obrigatório.")]
        [StringLength(maximumLength: 100, MinimumLength = 3)] //daria para usar assim no cpf também
        public string ?Name { get; set; }


        [Required(ErrorMessage = "Este é um campo obrigatório")]
        public DateTime Nascimento { get; set; }


        public int Idade => (DateTime.Now.Year - Nascimento.Year);
    }
}
