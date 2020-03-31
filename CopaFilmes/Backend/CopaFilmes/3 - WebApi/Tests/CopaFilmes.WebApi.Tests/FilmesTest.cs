using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CopaFilmes.WebApi.Tests
{
    [TestClass]
    public class FilmesTest
    {
        [TestInitialize]
        public void Init()
        {
            ObterFilmes();
            GerarCampeonatoVazio();
            GerarCampeonatoCompleto();
            GerarCampeonatoIncompleto();
        }

        [TestMethod]
        public void ObterFilmes()
        {

        }

        [TestMethod]
        public void GerarCampeonatoVazio()
        {
            
        }

        [TestMethod]
        public void GerarCampeonatoCompleto()
        {

        }

        [TestMethod]
        public void GerarCampeonatoIncompleto()
        {

        }
    }
}
