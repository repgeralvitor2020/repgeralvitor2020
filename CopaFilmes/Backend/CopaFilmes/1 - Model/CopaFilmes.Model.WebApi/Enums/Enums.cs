using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CopaFilmes.Model.Common.WebApi.Enums
{
    public class Enums
    {
        public enum EnumApiResponse
        {
            Ok = 0,
            Error = 1,
            Exception = 2
        }

        public enum EnumRequestType
        {
            Delete = 0,
            Get = 1,
            Post = 2,
            Put = 3
        }
    }
}
