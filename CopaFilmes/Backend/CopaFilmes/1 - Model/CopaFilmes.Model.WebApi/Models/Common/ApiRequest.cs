using CopaFilmes.Model.Common.WebApi.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CopaFilmes.Model.WebApi.Models.Common
{
    public class ApiRequest<T>
    {
        public string SessionId { get; set; }
        public string Token { get; set; }
        public string Url { get; set; }
        public T Body { get; set; }
        public Enums.EnumRequestType RequestType { get; set; }
    }
}
