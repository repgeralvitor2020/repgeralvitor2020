using CopaFilmes.Model.Common.WebApi.Enums;
using CopaFilmes.Model.Common.WebApi.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CopaFilmes.Common.WebApi.ApiConfig
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ControllerApiBase : ControllerBase
    {
        #region Constructor

        public ControllerApiBase() : base() { }

        #endregion

        #region Methods

        private Enums.EnumApiResponse GetStatusResponse(string message = "")
            => string.IsNullOrEmpty(message) ? Enums.EnumApiResponse.Ok : Enums.EnumApiResponse.Error;

        protected async Task<IActionResult> GetResponse<T>(T data, string message = "")
            where T : class
            => await Task.FromResult(Ok(new ApiResponse<T>()
            {
                Data = data,
                Message = message,
                Status = GetStatusResponse(message)
            }));

        protected async Task<IActionResult> GetResponse(string message = "")
            => await Task.FromResult(Ok(new ApiResponse()
            {
                Message = message,
                Status = GetStatusResponse(message)
            }));

        protected async Task<IActionResult> ThrowException(Exception ex)
            => await Task.FromResult(BadRequest(new ApiResponse()
            {
                Message = string.Concat(ex.Message, " - ", ex.StackTrace),
                Status = Enums.EnumApiResponse.Exception
            }));

        #endregion
    }
}
