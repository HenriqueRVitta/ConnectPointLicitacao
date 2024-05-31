using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licitacao.Models
{
    public class Modelo
    {
        public int TotalErros { get; set; }
        public string TotalLicitacoes { get; set; }
        public int Paginas { get; set; }
        public int LicitacoesPorPagina {get;set;}
        public int LicitacoesNestaPagina { get; set; }
        public List<DadosModelo> Licitacoes { get; set; } 
    }

    public class DadosModelo
    {
        public string Id_licitacao { get; set; }
        public string Titulo { get; set; }
        public string Municipio_IBGE { get; set; }
        public string Uf { get; set; }
        public string Orgao { get; set; }
        public string Abertura_datetime { get; set; }
        public string Objeto { get; set; }
        public string Link { get; set; }
        public string LinkExterno { get; set; }
        public string Municipio { get; set; }
        public string Abertura { get; set; }
        public string AberturaComHora { get; set; }
        public string Id_tipo { get; set; }
        public string Tipo { get; set; }
        public string Valor { get; set; }
        public string Id_portal { get; set; }
    }
}


