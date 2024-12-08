using System;

namespace TP_ATP
{
    internal class Posicao
    {
        private int linha;
        private int coluna;
        public Posicao(int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }
        public int Linha
        {
            get { return linha; }
            set { linha = value; }
        }
        public int Coluna
        {
            get { return coluna; }
            set {  coluna = value; }
        }
    }
}
