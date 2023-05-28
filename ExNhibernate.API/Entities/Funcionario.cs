namespace ExNhibernate.API.Entities
{
    public class Funcionario
    {
        public virtual long Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual int Idade { get; set; }
        public virtual double Salario { get; set; }
    }
}
