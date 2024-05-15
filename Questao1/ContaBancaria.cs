namespace Questao1
{
    class ContaBancaria
    {
        public ContaBancaria(int numeroConta, string titular)
        {
            NumeroConta = numeroConta;
            Titular = titular;
        }
        public ContaBancaria(int numeroConta, string titular, double deposito)
        {
            NumeroConta = numeroConta;
            Titular = titular;
            Deposito(deposito);
        }
        public int NumeroConta { get; }
        public string Titular { get; set; }
        public double Saldo { get; private set; } = 0;
        public void Deposito(double valor)
        {
            Saldo += valor;
        }
        public void Saque(double valor)
        {
            Saldo -= (valor + TaxaSaque);
        }

        private readonly double TaxaSaque = 3.5;

        public override string ToString()
        {
            return $"Conta {NumeroConta}, Titular: {Titular}, Saldo: $ {Saldo:F2}";
        }

    }
}
