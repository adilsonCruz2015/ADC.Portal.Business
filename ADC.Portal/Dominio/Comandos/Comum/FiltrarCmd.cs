using Solucoes.Auxiliares;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace ADC.Portal.Dominio.Comandos.Comum
{
    public abstract class FiltrarCmd 
    {
        public FiltrarCmd()
        {
            this.Limpar();
        }

        public virtual void Limpar()
        {
            this.Maximo = 300;
            this.Pagina = 1;
        }

        protected int _maximo;
        [Display(Name = "Máximo de registros")]
        public virtual int Maximo
        {
            get { return _maximo; }
            set { _maximo = value <= 0 ? 1 : value; }
        }

        protected int _pagina;
        [Display(Name = "Página atual")]
        public virtual int Pagina
        {
            get { return _pagina; }
            set { _pagina = value <= 0 ? 1 : value; }
        }

        [Display(Name = "Palavra chave")]
        [Description("Informe palavras chave")]
        public virtual string PalavraChave { get; set; }
       

        public IList<string> DesmebrarPalavraChave()
        {
            List<string> resultado = new List<string>();

            if (string.IsNullOrWhiteSpace(this.PalavraChave))
                return resultado;

            string texto = this.PalavraChave;
            texto = Regex.Replace(texto, @"[`|'|;]", " ").Trim();
            texto = Regex.Replace(texto, @"[ ]{2,}", " ").Trim();

            if (!string.IsNullOrWhiteSpace(texto))
            {
                MatchCollection agrupar = Regex.Matches(texto, "(\"[^\"]+\")", RegexOptions.IgnoreCase);
                if (agrupar.Count > 0)
                {
                    foreach (Match item in agrupar)
                    {
                        texto = texto.Replace(item.Groups[1].Value, "");
                        string novo = item.Groups[1].Value.Replace("\"", "");
                        resultado.Add(novo);
                        resultado.Add(ParaTexto.CodificarHtml(novo));

                    }
                    texto = Regex.Replace(texto, @"[`|'|;]", " ").Trim();
                    texto = Regex.Replace(texto, @"[ ]{2,}", " ").Trim();
                }
                if (!string.IsNullOrWhiteSpace(texto))
                {
                    string[] separar = texto.Split(' ');
                    resultado.Add(texto);
                    resultado.Add(ParaTexto.CodificarHtml(texto));
                    foreach (string item in separar)
                    {
                        if (item.Length > 0)
                        {
                            resultado.Add(item);
                            resultado.Add(ParaTexto.CodificarHtml(item));
                        }
                    }
                }
            }

            return resultado.Where(x => x.Length > 2).Distinct().ToList();
        }
    }

    public abstract class FiltrarCmd<TClasse> : FiltrarCmd
        where TClasse : class
    {
        public abstract TClasse DefinirMaximo(int valor);

        public abstract TClasse DefinirPagina(int valor);

        public abstract TClasse DefinirPalavraChave(string valor);
    }
}
