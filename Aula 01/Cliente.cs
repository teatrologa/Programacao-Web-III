/*Construa um cadastro completo (CRUD) de clientes.
Neste cadastro, o cliente deve possuir cpf, nome, data de nascimento e idade.*/

using System.ComponentModel.DataAnnotations;

namespace Aula_01
{
    public class Cliente
    {
        [Required]
        public string Cpf { get; set; }


        [Required(ErrorMessage = "Digite um nome, ele é obrigatório.")]
        public string Name { get; set; }


        [Required]
        public DateTime Nascimento { get; set; }


        public int Idade => (DateTime.Now.Year - Nascimento.Year);
    }
}
