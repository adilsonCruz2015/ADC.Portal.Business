

using Solucoes.Auxiliares.Seguranca;
using System;
using System.Text.RegularExpressions;

namespace ADC.Portal.Dominio.Seguranca
{
    public class Token
    {
        private static string ChaveBase = "ADC.Portal.Business";
        public Encriptacao Encriptacao { get; set; }

        Token(string chave)
        {
            Encriptacao = new Encriptacao();
            Encriptacao.Chave = chave;
            Encriptacao.Senha = "gcS5A0uLBKkhD08xwX";
        }

        public static string EncriptarTexto(string value)
        {
            return EncriptarTexto(value, ChaveBase);
        }

        public static string EncriptarTexto(string value, string chave)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("O valor não pode ser vazio ou nulo.", "value");

            if (string.IsNullOrEmpty(chave))
                throw new ArgumentException("O valor não pode ser vazio ou nulo.", "chave");

            Token tk = new Token(chave);
            return tk.Encriptacao.EncriptarTexto(value);
        }

        public static string EncriptarTexto(string value, DateTime timeout)
        {
            return EncriptarTexto(value, ChaveBase, timeout);
        }

        public static string EncriptarTexto(string value, string chave, DateTime timeout)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("O valor não pode ser vazio ou nulo.", "value");

            if (string.IsNullOrEmpty(chave))
                throw new ArgumentException("O valor não pode ser vazio ou nulo.", "chave");

            if (object.Equals(timeout, null))
                throw new ArgumentException("O valor é obrigatório.", "timeout");

            Token tk = new Token(chave);
            return tk.Encriptacao.EncriptarTexto(string.Format("{0}|{1}", timeout.ToString(), value));
        }

        public static string DecriptarTexto(string value)
        {
            return DecriptarTexto(value, ChaveBase);
        }

        public static string DecriptarTexto(string value, string chave)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(chave))
                return string.Empty;

            Token tk = new Token(chave);
            string expressao = @"^([0-9]{1,2}/[0-9]{1,2}/[0-9]{2,}[ 0-9:\.]*)(\|)(.*)$";
            string resultado = tk.Encriptacao.DecriptarTexto(value);
            bool dataLimite = Regex.IsMatch(resultado, expressao);
            DateTime data = dataLimite ? Convert.ToDateTime(Regex.Replace(resultado, @"^([^\|]+)(\|)(.*)$", "$1")) : new DateTime();

            if (data != new DateTime() && DateTime.Compare(DateTime.Now, data) > 0)
            {
                resultado = string.Empty;
            }
            else if (dataLimite)
            {
                resultado = Regex.Replace(resultado, expressao, "$3");
            }

            return resultado;
        }
    }
}
