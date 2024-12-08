using System;

namespace TP_ATP
{
    internal class JogadorHumano
    {
        public char[,] tabuleiro;
        public int pontuacao;
        public int numTirosDados;
        public Posicao[] posTirosDados;
        public string nickname;
        public JogadorHumano(int linhas, int colunas, string nomeCompleto)
        {
            tabuleiro = new char[linhas, colunas];
            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                   tabuleiro[i, j] = 'A';
                }
            }
            pontuacao = 0;
            numTirosDados = 0;
            posTirosDados = new Posicao[linhas * colunas];
            nickname = GerarNickname(nomeCompleto);
        }
        public char[,] Tabuleiro
        {
            get { return tabuleiro; }
            set { tabuleiro = value; }
        }
        public int Pontuacao
        {
            get { return pontuacao; }
            set { pontuacao = value; }
        }
        public int NumTirosDados
        {
            get { return numTirosDados; }
            set { numTirosDados = value; }
        }
        public Posicao[] PosTirosDados
        {
            get { return posTirosDados; }
            set { posTirosDados = value; }
        }
        public string Nickname
        {
            get { return nickname; }
            set { nickname = value; }
        }
        public string GerarNickname(string nomeCompleto)
        {
            string[] nomes = nomeCompleto.Split(' ');
            string ultimonome = nomes[nomes.Length - 1];
            string nickname = ultimonome;
            for (int i = 0; i < nomes.Length - 1; i++)
            {
                nickname += nomes[i][0];
            }
            return nickname.ToLower();
        }
        private bool JaAtacado(Posicao ataque)
        {
            for (int i = 0; i < NumTirosDados; i++)
            {
                if (PosTirosDados[i].Linha == ataque.Linha && PosTirosDados[i].Coluna == ataque.Coluna)
                {
                    return true;
                }
            }
            return false;
        }
        public Posicao EscolherAtaque()
        {
            while (true)
            {
                Console.WriteLine("Digite a linha do ataque (0 a 9): ");
                if (!int.TryParse(Console.ReadLine(), out int linha))
                {
                    Console.WriteLine("Entrada inválida. Por favor, digite um número.");
                    continue;
                }

                Console.WriteLine("Digite a coluna do ataque (0 a 9): ");
                if (!int.TryParse(Console.ReadLine(), out int coluna))
                {
                    Console.WriteLine("Entrada inválida. Por favor, digite um número.");
                    continue;
                }

                if (linha >= 0 && linha < Tabuleiro.GetLength(0) && coluna >= 0 && coluna < Tabuleiro.GetLength(1))
                {
                    Posicao ataque = new Posicao(linha, coluna);

                    if (!JaAtacado(ataque))
                    {
                        PosTirosDados[NumTirosDados++] = ataque;
                        return ataque;
                    }
                    else
                    {
                        Console.WriteLine("Você já atacou essa posição!");
                    }
                }
                else
                {
                    Console.WriteLine("Posição inválida! Tente novamente.");
                }
            }
        }
        public bool ReceberAtaque(Posicao ataque)
        {
            if (tabuleiro[ataque.Linha, ataque.Coluna] == 'A')
            {
                tabuleiro[ataque.Linha, ataque.Coluna] = 'X';
                return false;
            }
            else if (tabuleiro[ataque.Linha, ataque.Coluna] != 'X' && tabuleiro[ataque.Linha, ataque.Coluna] != 'T')
            {
                tabuleiro[ataque.Linha, ataque.Coluna] = 'T';
                pontuacao++;
                return true;
            }
            return false;
        }
        public bool AdicionarEmbarcacao(Embarcacao embarcacao, Posicao posicaoInicial)
        {
            for (int i = 0; i < embarcacao.Tamanho; i++)
            {
                int linha = posicaoInicial.Linha;
                int coluna = posicaoInicial.Coluna + i;
                if (linha < 0 || linha >= tabuleiro.GetLength(0) || coluna < 0 || coluna < tabuleiro.GetLength(1))
                {
                    return false;
                }
                if (tabuleiro[linha, coluna] != 'A')
                {
                    return false;
                }
            }
            for (int i = 0; i < embarcacao.Tamanho; i++)
            {
                int linha = posicaoInicial.Linha;
                int coluna = posicaoInicial.Coluna + i;
                tabuleiro[linha, coluna] = embarcacao.Nome[0];
            }
            return true;
        }
        public void ImprimirTabuleiroJogador()
        {
            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    char unidade = tabuleiro[i, j];
                    Console.Write(unidade + " ");
                }
                Console.WriteLine();
            }
        }
        public void ImprimirTabuleiroAdversario()
        {
            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    char unidade = tabuleiro[i, j];
                    if (unidade == 'X' || unidade == 'T')
                    {
                        Console.Write(unidade + " ");
                    }
                    else
                    {
                        Console.Write("A ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
