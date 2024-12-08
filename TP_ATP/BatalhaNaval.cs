using System;
using System.IO;

namespace TP_ATP
{
    internal class BatalhaNaval
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite seu nome completo: ");
            string nomecompleto = Console.ReadLine();
            JogadorHumano hum = new JogadorHumano(10, 10, nomecompleto);
            JogadorComputador comp = new JogadorComputador(10, 10);
            Console.WriteLine(hum.Nickname);
            Console.WriteLine("Quantas embarcações deseja adicionar?");
            int qntemb = int.Parse(Console.ReadLine());
            for (int i = 0; i < qntemb; i++)
            {
                Console.WriteLine($"Digite o nome da {i + 1}° embarcação que deseja adicionar: ");
                string nomeE = Console.ReadLine();
                Console.WriteLine($"Digite agora o tamanho da {i + 1}° embarcação: ");
                int tamanhoE = int.Parse(Console.ReadLine());
                Console.WriteLine($"Digite a linha que deseje colocar a {i + 1}° embarcação: ");
                int linhaE = int.Parse(Console.ReadLine());
                Console.WriteLine($"Digite a coluna que deseje colocar a {i + 1}° embarcação: ");
                int colunaE = int.Parse(Console.ReadLine());
                Embarcacao E = new Embarcacao(nomeE, tamanhoE);
                Posicao EE = new Posicao(linhaE, colunaE);
                hum.AdicionarEmbarcacao(E, EE);
                nomeE = null;
                tamanhoE = 0;
                linhaE = 0;
                colunaE = 0;
            }
            StreamReader arq = new StreamReader("frotaComputador.txt");
            string linha;
            try
            {
                while ((linha = arq.ReadLine()) != null)
                {
                    string[] partes = linha.Split(';');
                    if (partes.Length >= 3)
                    {
                        string nomeEmbarcacao = partes[0];
                        int linhainicial = int.Parse(partes[1]);
                        int colunainicial = int.Parse(partes[2]);
                        Embarcacao embarcacao = new Embarcacao(nomeEmbarcacao, ObterTamanhoEmbarcacao(nomeEmbarcacao));
                        Posicao posicao = new Posicao(linhainicial, colunainicial);
                        comp.AdicionarEmbarcacao(embarcacao, posicao);
                    }
                }
            }
            catch { }
            arq.Close();
            bool jogoEmAndamento = true;
            string[] jogadas = new string[100]; 
            int contadorJogadas = 0; 
            while (jogoEmAndamento)
            {
                Console.WriteLine($"{hum.Nickname}, é sua vez de atacar!");
                hum.ImprimirTabuleiroAdversario();
                Posicao posicaoAtaque = hum.EscolherAtaque();
                bool acerto = comp.ReceberAtaque(posicaoAtaque);
                string jogada = $"{hum.Nickname} atacou {posicaoAtaque.Linha},{posicaoAtaque.Coluna}. Resultado: " + (acerto ? "Acertou!" : "Errou!");
                jogadas[contadorJogadas] = jogada; 
                contadorJogadas++; 
                if (acerto)
                {
                    Console.WriteLine("Você acertou uma embarcação!");
                    if (comp.Pontuacao == 0)
                    {
                        Console.WriteLine($"{hum.Nickname} venceu o jogo!");
                        jogadas[contadorJogadas] = $"{hum.Nickname} venceu o jogo!"; 
                        contadorJogadas++; 
                        jogoEmAndamento = false;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Você errou o alvo!");
                }
                if (jogoEmAndamento)
                {
                    Console.WriteLine("Agora é a vez do computador!");
                    comp.ImprimirTabuleiroAdversario();
                    Posicao posicaoAtaqueComputador = comp.EscolherAtaque();
                    acerto = hum.ReceberAtaque(posicaoAtaqueComputador);
                    jogada = $"Computador atacou {posicaoAtaqueComputador.Linha},{posicaoAtaqueComputador.Coluna}. Resultado: " + (acerto ? "Acertou!" : "Errou!");
                    jogadas[contadorJogadas] = jogada; 
                    contadorJogadas++; 
                    if (acerto)
                    {
                        Console.WriteLine("O computador acertou uma embarcação!");
                        if (hum.Pontuacao == 0)
                        {
                            Console.WriteLine("O computador venceu o jogo!");
                            jogadas[contadorJogadas] = "O computador venceu o jogo!"; 
                            contadorJogadas++; 
                            jogoEmAndamento = false;
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("O computador errou o alvo!");
                    }
                }
            }
            using (StreamWriter writer = new StreamWriter("jogadas.txt"))
            {
                for (int i = 0; i < contadorJogadas; i++)
                {
                    writer.WriteLine(jogadas[i]); 
                }
            }
        }
        static private int ObterTamanhoEmbarcacao(string nomeEmbarcacao)
        {
            switch (nomeEmbarcacao)
            {
                case "Submarino": return 1;
                case "Hidroavião": return 2;
                case "Cruzador": return 3;
                case "Encouraçado": return 4;
                case "Porta-aviões": return 5;
                default: return 0;
            }
        }
    }
}
