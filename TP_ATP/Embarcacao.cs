using System;

namespace TP_ATP
{
    internal class Embarcacao
    {
        private string nome;
        private int tamanho;
        public Embarcacao(string nome, int tamanho)
        {
            this.nome = nome;
            this.tamanho = tamanho;
        }
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public int Tamanho
        {
            get { return tamanho; }
            set {  tamanho = value; }
        }
    }
}
