using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBankPOO.Model
{
    public class Conta
    {

        public decimal Saldo { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }

        public Conta(string nome, string cpf, string senha, decimal saldo)
        {

            Saldo = saldo;
            Nome = nome;
            Cpf = cpf;
            Senha = senha;
        }
      



        public decimal VerSaldo()
        {
            return Saldo;
        }

        public void DepositarDinheiro(decimal valorDeposito)
        {
            Saldo += valorDeposito;
        }

        public void SacarDinheiro(decimal totalSaque)
        {
            if (Saldo < totalSaque)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNÃO A SALDO SUFICIENTE");
                Console.ResetColor();
            }
            else
            {
                Saldo -= totalSaque;
            }
        }

        public void TransferirDinheiro(Conta contaEnvio, Conta contaDestino, decimal valorTransf)
        {
            contaEnvio.Saldo -= valorTransf;
            contaDestino.Saldo += valorTransf;
        }





    }
}
