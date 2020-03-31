using System;
using System.Collections.Generic;
using System.Text;

namespace CopaFilmes.Model.WebApi.Models.CopaFilmes
{
    public class Filme
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public double Nota { get; set; }
        public bool Ativo = true;
    }
}
