using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CopaFilmes.Business.WebApi.CopaFilmes;
using CopaFilmes.Common.WebApi.ApiConfig;
using CopaFilmes.Common.WebApi.Util;
using CopaFilmes.Model.Common.WebApi.Enums;
using CopaFilmes.Model.WebApi.Models.Common;
using CopaFilmes.Model.WebApi.Models.CopaFilmes;
using Microsoft.AspNetCore.Mvc;

namespace CopaFilmes.WebApi.Controllers
{
    public class HomeController : ControllerApiBase
    {
        #region Properties

        private FilmeBS _bs;

        #endregion

        #region Constructor

        public HomeController() : base()
        {
            _bs = new FilmeBS();
        }

        #endregion

        #region Get

        [HttpGet]
        [Route("ObterFilmes")]
        public async Task<IActionResult> ObterFilmes()
        {
            try
            {
                var filmes = await new ApiRequestCenter<string, List<Filme>>()
                                       .Send(new ApiRequest<string>()
                                       {
                                           Url = "http://copafilmes.azurewebsites.net/api/filmes",
                                           RequestType = Enums.EnumRequestType.Get
                                       });

                filmes = filmes.OrderByDescending(f => f.Ano)
                               .ThenBy(f => f.Titulo)
                               .ToList();

                return await GetResponse(filmes);
            }
            catch (Exception ex)
            {
                return await ThrowException(ex);
            }
        }

        #endregion

        #region Post

        [HttpPost]
        [Route("GerarCampeonato")]
        public async Task<IActionResult> GerarCampeonato([FromBody] List<Filme> filmes)
        {
            try
            {
                var filmesVencedores = new List<Filme>();

                if (_bs.ValidarFilmes(filmes, out string erro))
                    filmesVencedores = _bs.GerarCampeonato(filmes);

                return await GetResponse(filmesVencedores, erro);
            }
            catch (Exception ex)
            {
                return await ThrowException(ex);
            }
        }

        #endregion
    }
}
