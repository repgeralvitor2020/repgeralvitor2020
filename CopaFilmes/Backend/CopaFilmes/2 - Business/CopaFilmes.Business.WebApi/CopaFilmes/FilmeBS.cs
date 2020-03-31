using CopaFilmes.Model.WebApi.Models.CopaFilmes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CopaFilmes.Business.WebApi.CopaFilmes
{
    public class FilmeBS
    {
        public List<Filme> GerarCampeonato(List<Filme> filmes)
        {
            filmes = filmes.OrderBy(f => f.Titulo).ToList();

            var totalFilmes = filmes.Count;
            for (var i=0; i<(totalFilmes / 2); i++)
                CompararFilmes(filmes[i], filmes[totalFilmes - (i + 1)]);

            filmes.RemoveAll(f => !f.Ativo);

            if (filmes.Count == 2)
                filmes = filmes.OrderByDescending(f => f.Nota)
                               .ThenBy(f => f.Titulo)
                               .ToList();
            else
                filmes = GerarCampeonato(filmes);

            return filmes;
        }

        private void CompararFilmes(Filme filmeA, Filme filmeB)
        {
            if (filmeA.Nota < filmeB.Nota)
                filmeA.Ativo = false;
            else if (filmeA.Nota > filmeB.Nota || filmeA.Nota == filmeB.Nota)
                filmeB.Ativo = false;
        }

        public bool ValidarFilmes(List<Filme> filmes, out string erro)
        {
            erro = "";

            if (filmes.Count != 8)
                erro += NovaLinha("Não contém 8 filmes");

            foreach (var filme in filmes)
                ValidarFilme(filme, ref erro);

            return string.IsNullOrEmpty(erro);
        }

        private void ValidarFilme(Filme filme, ref string erro)
        {
            if (string.IsNullOrEmpty(filme.Id))
                erro += NovaLinha("Contém filme sem Id válido");
            if (DateTime.MinValue.Year > filme.Ano)
                erro += NovaLinha("Contém filme sem Ano válido");
            if (string.IsNullOrEmpty(filme.Titulo))
                erro += NovaLinha("Contém filme sem Título válido");
        }

        private string NovaLinha(string texto)
            => string.Concat(texto, Environment.NewLine);
    }
}
