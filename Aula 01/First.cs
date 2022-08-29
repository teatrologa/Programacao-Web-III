/*Construa um cadastro completo (CRUD) de clientes.
Neste cadastro, o cliente deve possuir cpf, nome, data de nascimento e idade.*/

namespace Aula_01
{
    public class First
    {
        public string Cpf { get; set; }
        public string Name { get; set; }
        public DateTime Nascimento { get; set; }
        public int Idade => (DateTime.Now.Year - Nascimento.Year);
    }
}
