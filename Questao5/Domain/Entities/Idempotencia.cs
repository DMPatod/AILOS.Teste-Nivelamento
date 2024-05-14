using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;

namespace Questao5.Domain.Entities
{
    public class Idempotencia
    {
        public Idempotencia() { }
        private Idempotencia(string chave_idempotencia, string requisicao, string resultado)
        {
            Chave_Idempotencia = chave_idempotencia;
            Requisicao = requisicao;
            Resultado = resultado;
        }

        public string Chave_Idempotencia { get; private set; }
        public string Requisicao { get; private set; }
        public string Resultado { get; private set; }

        public static Idempotencia Criar(CadastrarMovimentacaoRequest requisicao, bool successo, string mensagem)
        {
            return new Idempotencia(
                Guid.NewGuid().ToString(),
                JsonConvert.SerializeObject(requisicao),
                JsonConvert.SerializeObject(new { Successo = successo, Mensagem = mensagem }));
        }
    }
}
