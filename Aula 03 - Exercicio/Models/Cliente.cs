using System.ComponentModel.DataAnnotations;

namespace Aula_03___Exercicio.Models
{
    public class Cliente
    {

        public long Id { get; set; }

        //[Required(ErrorMessage = "O CPF � obrigat�rio, insira no formato: 000000000-n")]
        //[MaxLength(12, ErrorMessage = "O CPF, nesse caso, tem no m�ximo 12 caracteres (considerando o tra�o)\n[000000000-1]")]
        //[MinLength(11, ErrorMessage = "O CFP precisa ter no minimo 11 caracteres.")]
        public string? Cpf { get; set; }


        //[Required(ErrorMessage = "Digite um nome, ele � obrigat�rio.")]
        //[StringLength(maximumLength: 100, MinimumLength = 3)] //daria para usar assim no cpf tamb�m
        public string Nome { get; set; }


        //[Required(ErrorMessage = "Este � um campo obrigat�rio")]
        public DateTime DataNascimento { get; set; }


        public int Idade => DateTime.Now.Year - DataNascimento.Year;
    }
}