using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        public Movimento() { }
        public Movimento(string id, string idContaCorrente, DateTime dataMovimento, TipoMovimento tipoMovimento, double valor)
        {
            Id = id;
            IdContaCorrente = idContaCorrente;
            DataMovimento = dataMovimento;
            TipoMovimento = tipoMovimento;
            Valor = valor;
        }

        public string Id { get; private set; }
        public string IdContaCorrente { get; private set; }
        public DateTime DataMovimento { get; private set; }
        public TipoMovimento TipoMovimento { get; private set; }
        public double Valor { get; private set; }

        public static Movimento Criar(string id, string numeroContaCorrente, DateTime dateTime, TipoMovimento tipoMovimento, double valorMovimentacao)
        {
            return new Movimento(id, numeroContaCorrente, dateTime, tipoMovimento, valorMovimentacao);
        }
    }
}
