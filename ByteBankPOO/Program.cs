using ByteBankPOO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;

namespace ByteBankProject
{

    public class Program
    {
       
        static void ShowMenu()
        {
            
            Console.WriteLine("\n\n============= MENU PRINCIPAL ================================");
            Console.WriteLine("\n1 - Inserir Novo Usuário");
            Console.WriteLine("2 - Deletar Usuário");
            Console.WriteLine("3 - Detalhes do Usuário");
            Console.WriteLine("4 - Movimentações de conta (DEPOSITO, SAQUE E TRANSFERENCIA)");
            Console.WriteLine("5 - Informações sobre Bytebank");
            Console.WriteLine("0 - Fazer Logout\n");
            Console.WriteLine("=============================================================\n");
            Console.Write("Digite a opção desejada: ");
        }

        static void ShowSubMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n=====MOVIMENTAÇÃO DE CONTA=====");
            Console.WriteLine("\n1 - Depositar");
            Console.WriteLine("2 - Sacar");
            Console.WriteLine("3 - Transferir");
            Console.WriteLine("0 - Menu Principal\n");
            Console.WriteLine("===============================");
            Console.ResetColor();

        }

        static void CriarUsuario(List<Conta> contasU)
        {
            
            Console.Clear();
            Console.WriteLine("\n\nSeja Bem Vindo ao sistema de cadastros do BYTE BANK\n");
            Console.Write("Primeiramenta como gostaria de ser chamado: ");
            string nomeUsuario = Console.ReadLine();
            Console.Write("Insira os numeros do seu CPF: ");
            string cpfUsuario = Console.ReadLine();
            Console.Write("Por Favor, insira uma senha: ");
            string senhaUsuario = Console.ReadLine();
            decimal saldoUsuario = 0;
            contasU.Add(new Conta(nomeUsuario, cpfUsuario, senhaUsuario, saldoUsuario));
            Console.Clear();
            Console.WriteLine("\n\nSeu cadastro foi completado com Sucesso");
            Console.WriteLine($"\n{nomeUsuario}, seja bem vindo ao BYTE BANK");
            Console.WriteLine("\nRetornando ao menu principal\n");
            Console.WriteLine("=============================================================\n");
            ShowMenu();
            
        }

        static void DeletarUsuario(List<Conta> contasU)
        {
            Console.Clear();
            Console.WriteLine("\n\nBem vindo a area de encerramento de conta BYTE BANK");
            Console.WriteLine("Você realmente deseja continuar com o encerramento?");
            Console.WriteLine("Digite 1 para SIM | Digite 2 para NÃO");
            int userOption = int.Parse(Console.ReadLine());
            if (userOption == 2)
            {
                Console.WriteLine("\nObrigado por continuar com a gente");
                Console.WriteLine("BYTE BANK agradece a preferência");
                Console.WriteLine("Para sua comodidade estamos retornando ao Menu Principal");
                Console.WriteLine("---------------------------------------------------------\n");
                ShowMenu();
            }
            else if (userOption == 1)
            {
                Console.WriteLine("Tudo bem, vamos continuar com o encerramento!");
                Console.Write($"\nPor favor, digite o CPF: ");
                string cpfDeletar = Console.ReadLine();
                Conta cpfDeletarIndex = contasU.Find(c => c.Cpf == cpfDeletar);
                contasU.Remove(cpfDeletarIndex);

                while (cpfDeletarIndex == null)
                {

                    Console.WriteLine("\nCPF NÃO ENCONTRADO NO BANCO DE DADOS, TENTE NOVAMENTE");
                    Console.Write("Digite novamente o CPF: ");
                    cpfDeletar = Console.ReadLine();
                    cpfDeletarIndex = contasU.Find(c => c.Cpf == cpfDeletar);
                    contasU.Remove(cpfDeletarIndex);

                }

                Console.WriteLine($"\nSr(a) {cpfDeletarIndex.Nome} com CPF: {cpfDeletarIndex.Cpf}, sua conta foi encerrada com sucesso");
                Console.WriteLine("Retornando ao Menu Principal\n\n");
                ShowMenu();
            }


        }

        static void DetalharUsuario(List<Conta> contasU)
        {
            Console.Clear();
            Console.WriteLine("\nBem vindo ao seu perfil no BYTE BANK");
            Console.Write($"\nPor favor, digite o CPF: ");
            string cpfProcurarUsuario = Console.ReadLine();
            Conta cpfProcuararIndex = contasU.Find(c => c.Cpf == cpfProcurarUsuario);
            int tentativas = 0;

            while (cpfProcuararIndex == null)
            {
                if (tentativas == 3)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n\nCPF NÃO FOI ENCOTNRADO APÓS 3 TENTATIVAS, RETORNANDO AO MENU INICIAL\n\n");
                    Console.ResetColor();
                    ShowMenu();
                    break;
                }

                Console.WriteLine("\nCPF NÃO ENCONTRADO NO BANCO DE DADOS, TENTE NOVAMENTE");
                Console.Write("Digite novamente o CPF: ");
                cpfProcurarUsuario = Console.ReadLine();
                cpfProcuararIndex = contasU.Find(c => c.Cpf == cpfProcurarUsuario);

                tentativas++;
            }
            foreach (Conta detalhe in contasU)
            {
                if (cpfProcuararIndex == detalhe)
                {
                    Console.WriteLine("\nCONTA ENCONTRADA");
                    Console.WriteLine($"\nSr(a), {detalhe.Nome}, com CPF {detalhe.Cpf}, seu saldo atual é de R$ {detalhe.Saldo:F2}\n");
                    Console.WriteLine("Para sua comodidade, vamos retornar ao Menu Principal\n\n");
                    ShowMenu();

                }

            }

        }

        static Conta LogarUsuario(List<Conta> contasU)
        {
            Console.Clear();
            Conta login;
            Console.WriteLine("Bem vindo ao setor de movimentações bancárias do BYTE BANK\n");
            Console.WriteLine("Para prosseguir, faça seu login!\n\n");
            do
            {
                
                Console.Write("Insira seu CPF: ");
                string cpfLogin = Console.ReadLine();
                Console.Write("Insira sua senha: ");
                string senhaLogar = Console.ReadLine();
                login = contasU.Find(d => d.Cpf == cpfLogin);
                login = contasU.Find(d => d.Senha == senhaLogar);
                if (login == null)
                {
                    Console.WriteLine("\n\nDesculpe, CPF ou SENHA inválidos\n\n");
                }

            } while (login == null);
            return login;

        }

        static void Depositar(Conta usuarioLogado)
        {
            Console.Clear();
            Console.Write("\nQual o valor que você deseja depositar? R$ ");
            decimal valorDeposito = decimal.Parse(Console.ReadLine());
            usuarioLogado.DepositarDinheiro(valorDeposito);
            Console.WriteLine($"\nDeposito de R${valorDeposito:F2}, foi efetuado com sucesso na sua conta");
            Console.WriteLine($"Seu novo saldo é de R$ {usuarioLogado.Saldo:F2}\n\n");

        }

        static void Sacar(Conta usuarioLogado)
        {
            Console.Clear();
            Console.Write("\nQual o valor que você deseja sacar? R$ ");
            decimal valorSaque = decimal.Parse(Console.ReadLine());
            usuarioLogado.SacarDinheiro(valorSaque);
            if (valorSaque > usuarioLogado.Saldo)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nSAQUE NÃO FOI REALIZADO");
                Console.WriteLine($"\nSEU SALDO CONTINUA: R$ {usuarioLogado.Saldo}");
                Console.ResetColor();

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nSaque de R${valorSaque:F2}, foi efetuado com sucesso na sua conta");
                Console.WriteLine($"Seu novo saldo é de R$ {usuarioLogado.Saldo:F2}\n\n");
                Console.ResetColor();

            }

        }

        static void Transferir(List<Conta> contasU, Conta usuarioLogado)
        {
            Console.Clear();
            Conta cpfInicio = usuarioLogado;
            
            
            Console.Write("Digite o CPF do favorecido:  ");
            string encontraCpf = (Console.ReadLine());
            Conta cpfFinal = contasU.Find(x => x.Cpf == encontraCpf);
            
            while (cpfFinal == cpfInicio)
            {
                Console.WriteLine("\nCONTA DESTINATARIA É IGUAL A SUA CONTA");
                Console.Write("DIGITE UMA NOVA CONTA DESTINATARIA: ");
                encontraCpf = (Console.ReadLine());
                cpfFinal = contasU.Find(x => x.Cpf == encontraCpf);
                
                if (cpfFinal == null) // aumentar a segurança nesse ponto
                {
                    Console.WriteLine("OPERAÇÃO INVALIDA, CONTA NÃO ENCONTRADA");
                    Console.Write("\nDigite novamente o CPF: ");
                    encontraCpf = (Console.ReadLine());
                    cpfFinal = contasU.Find(x => x.Cpf == encontraCpf);
                    Console.WriteLine();
                }
            }

            while (cpfFinal == null)
            {
                Console.WriteLine("OPERAÇÃO INVALIDA, CONTA NÃO ENCONTRADA");
                Console.Write("\nDigite novamente o CPF: ");
                encontraCpf = Console.ReadLine(); 
                cpfFinal = contasU.Find(x => x.Cpf == encontraCpf);
                
            }
            
            Console.Write($"Sr.(a) {cpfInicio.Nome.ToUpper()}, qual será o valor a transferir? R$ ");
            decimal valorTransferencia = decimal.Parse(Console.ReadLine());
            cpfFinal.TransferirDinheiro(cpfInicio, cpfFinal, valorTransferencia);

            Console.WriteLine("\n\nTRANSFERENCIA FEITA COM SUCESSO\n");
            Console.WriteLine($"VALOR TOTAL TRANSFERIDO FOI R${valorTransferencia:F2}");
            Console.WriteLine($"TRANSFERENCIA FEITA POR: {cpfInicio.Nome.ToUpper()}, com CPF:{cpfInicio.Cpf}");
            Console.WriteLine($"TRANSFERENCIA RECEBIDA POR: {cpfFinal.Nome.ToUpper()}, com CPF:{cpfFinal.Cpf}");

        }

        static void DetalharBanco(List<Conta> contasU)
        {
            Console.Clear();
            foreach (Conta x in contasU)
            {
                
                Console.WriteLine($"Titular: {x.Nome.ToUpper()} || CPF: {x.Cpf} || Saldo: R$ {x.Saldo:F2}");
                Console.WriteLine();
            }
            
        }
        static decimal DetalharBanco2(List<Conta> contasU)
        {
            decimal somar = 0;

            foreach (Conta j in contasU)
            {
                somar += j.Saldo;
            }
            Console.WriteLine($"BYTE BANK ADMINISTRA UM TOTAL DE R$ {somar:F2}");
            return somar;
            
        }



        public static void Main(string[] args)
        {

            List<Conta> contasU = new List<Conta>();

            int opcaoMenuPrincipal;
            int opcaoSubMenu;


            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("============= BYTE BANK PROJECT =============================");
            Console.WriteLine();
            ShowMenu();

            do
            {
                opcaoMenuPrincipal = Convert.ToInt32(Console.ReadLine());

                switch (opcaoMenuPrincipal)
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\n\nPROGRAMA ENCERRADO");
                        Console.WriteLine("Foi um prazer lhe atender");
                        Console.WriteLine("Até Breve");
                        Console.ResetColor();
                        break;
                    case 1:
                        CriarUsuario(contasU);
                        break;
                    case 2:
                        DeletarUsuario(contasU);
                        break;
                    case 3:
                        DetalharUsuario(contasU);
                        break;
                    case 4:


                        Conta usuarioLogado = LogarUsuario(contasU);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nLOGIN FEITO COM SUCESSO\n");
                        Console.ResetColor();
                        do
                        {
                            ShowSubMenu();
                            opcaoSubMenu = int.Parse(Console.ReadLine());
                            while (opcaoSubMenu > 4 && opcaoSubMenu < 0)
                            {
                                Console.WriteLine("Opção inexistente, selecione uma opçao valida!");
                                opcaoSubMenu = int.Parse(Console.ReadLine());
                            }


                            switch (opcaoSubMenu)
                            {
                                case 0:
                                    Console.WriteLine("\nLOGOUT FEITO COM SUCESSO");
                                    Console.WriteLine("RETORNANDO AO MENU PRINCIPAL\n\n");
                                    ShowMenu();
                                    break;
                                case 1:
                                    Console.WriteLine("\nSETOR DE DEPÓSITOS: ");
                                    Depositar(usuarioLogado);
                                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                                    Console.WriteLine("\nPara conluir o depósito, aperte ENTER");
                                    Console.ReadKey();
                                    Console.Clear();
                                    Console.ResetColor();
                                    break;
                                case 2:
                                    Console.WriteLine("\nSETOR DE SAQUES: ");
                                    Sacar(usuarioLogado);
                                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                                    Console.WriteLine("\nPara conluir o saque, aperte ENTER");
                                    Console.ReadKey();
                                    Console.Clear();
                                    Console.ResetColor();
                                    break;
                                case 3:
                                    Console.WriteLine("\nSETOR DE TRANSFERÊNCIAS: ");
                                    Transferir(contasU, usuarioLogado);
                                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                                    Console.WriteLine("\nPara conluir o saque, aperte ENTER");
                                    Console.ReadKey();
                                    Console.Clear();
                                    Console.ResetColor();
                                    break;


                            }


                        } while (opcaoSubMenu != 0);
                        break;
                    case 5:
                        Console.WriteLine("Essas são as contas listadas no nosso BYTE BANK: \n");
                        DetalharBanco(contasU);
                        Console.WriteLine();
                        DetalharBanco2(contasU);
                        ShowMenu();
                        break;
                }




            } while (opcaoMenuPrincipal != 0);

            Console.ReadKey();
        }
    }
}