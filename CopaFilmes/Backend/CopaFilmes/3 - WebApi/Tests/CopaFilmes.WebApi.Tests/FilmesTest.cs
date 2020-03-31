using CopaFilmes.Business.WebApi.CopaFilmes;
using CopaFilmes.Common.WebApi.Util;
using CopaFilmes.Model.Common.WebApi.Enums;
using CopaFilmes.Model.WebApi.Models.Common;
using CopaFilmes.Model.WebApi.Models.CopaFilmes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CopaFilmes.WebApi.Tests
{
    [TestClass]
    public class FilmesTest
    {
        [TestMethod]
        public void ObterFilmes()
        {
            try
            {
                var filmes = ObterFilmesSelecionados(0);

                Assert.AreEqual(16, filmes.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [TestMethod]
        public void GerarCampeonatoVazio()
        {
            try
            {
                var filmes = new List<Filme>();

                var filmesVencedores = new List<Filme>();

                new FilmeBS().ValidarFilmes(filmes, out string erro);

                Assert.AreNotEqual(string.Empty, erro);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [TestMethod]
        public void GerarCampeonatoCompleto()
        {
            try
            {
                var filmes = ObterFilmesSelecionados();

                var filmesVencedores = new List<Filme>();

                new FilmeBS().ValidarFilmes(filmes, out string erro);

                Assert.AreEqual(string.Empty, erro);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [TestMethod]
        public void GerarCampeonatoIncompleto()
        {
            try
            {
                var filmes = ObterFilmesSelecionados();
                filmes.ElementAt(0).Ano = -1;

                var filmesVencedores = new List<Filme>();

                new FilmeBS().ValidarFilmes(filmes, out string erro);

                Assert.AreNotEqual(string.Empty, erro);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private List<Filme> ObterFilmesSelecionados(int nrFilmes = 8)
        {
            return new ApiRequestCenter<string, List<Filme>>()
                                       .Send(new ApiRequest<string>()
                                       {
                                           Url = "http://copafilmes.azurewebsites.net/api/filmes",
                                           RequestType = Enums.EnumRequestType.Get
                                       })
                                       .GetAwaiter()
                                       .GetResult()
                                       .Skip(nrFilmes)
                                       .ToList();
        }
    }
}
