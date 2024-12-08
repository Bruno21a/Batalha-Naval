using System;

namespace TP_ATP
{
    internal class JogadorComputador
    {
        public char[,] tabuleiro;
        public int pontuacao;
        public int numTirosDados;
        Posicao[] posTirosDados;
        public JogadorComputador(int linhas, int colunas)
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
        public Posicao EscolherAtaque()
        {
            Random random = new Random();
            Posicao ataque;
            bool jafoi;
            do
            {
                int linha = random.Next(0, tabuleiro.GetLength(0));
                int coluna = random.Next(0, tabuleiro.GetLength(1));
                ataque = new Posicao(linha, coluna);
                jafoi = true;
                for (int i = 0; i < numTirosDados; i++)
                {
                    if (posTirosDados[i].Linha == linha && posTirosDados[i].Coluna == coluna)
                    {
                        jafoi = false; break;
                    }
                }
            }
            while (!jafoi);
            posTirosDados[numTirosDados++] = ataque;
            return ataque;
        }
        public bool ReceberAtaque(Posicao ataque)
        {
            if (tabuleiro[ataque.Linha, ataque.Coluna] == 'A')
            {
                if (tabuleiro[ataque.Linha, ataque.Coluna] != 'X' && tabuleiro[ataque.Linha, ataque.Coluna] != 'T')
                {
                    tabuleiro[ataque.Linha, ataque.Coluna] = 'T';
                    pontuacao++;
                    return true;
                }
            }
            else
            {
                tabuleiro[ataque.Linha, ataque.Coluna] = 'X';
                return false;
            }
            return false;
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
    }
}
