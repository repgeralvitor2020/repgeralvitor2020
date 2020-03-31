using System;
using System.Collections.Generic;
using System.Text;
using static CopaFilmes.Model.Common.WebApi.Enums.Enums;

namespace CopaFilmes.Model.Common.WebApi.Models.Common
{
    public class ApiResponse<TData> : ApiResponse
        where TData : class
    {
        public TData Data { get; set; }

        public ApiResponse() { }
    }

    public class ApiResponse
    {
        public EnumApiResponse Status { get; set; }

        public string Message { get; set; }

        public ApiResponse() { }
    }
}
