using CopaFilmes.Model.Common.WebApi.Enums;
using CopaFilmes.Model.WebApi.Models.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CopaFilmes.Common.WebApi.Util
{
    public class ApiRequestCenter<U, T>
        where T : class
    {
        #region Properties

        public TimeSpan TimeOut { get; private set; }

        private Dictionary<Enums.EnumRequestType, Func<ApiRequest<U>, string, Task<T>>> _dicRequest;

        #endregion

        #region Constructors

        public ApiRequestCenter()
        {
            TimeOut = TimeSpan.FromMinutes(10);
            _dicRequest = new Dictionary<Enums.EnumRequestType, Func<ApiRequest<U>, string, Task<T>>>
            {
                {
                    Enums.EnumRequestType.Delete,
                    async (apiRequest, apiFormat)
                        => await Execute(apiRequest, apiFormat,
                            async (client, model) => await client.DeleteAsync(model.Url))
                },
                {
                    Enums.EnumRequestType.Get,
                    async (apiRequest, apiFormat)
                        => await Execute(apiRequest, apiFormat,
                            async (client, model) => await client.GetAsync(model.Url))
                },
                {
                    Enums.EnumRequestType.Post,
                    async (apiRequest, apiFormat)
                        => await Execute(apiRequest, apiFormat,
                            async (client, model) => await client.PostAsJsonAsync(model.Url, model.Body))
                },
                {
                    Enums.EnumRequestType.Put,
                    async (apiRequest, apiFormat)
                        => await Execute(apiRequest, apiFormat,
                            async (client, model) => await client.PutAsJsonAsync(model.Url, model.Body))
                }
            };
        }

        #endregion

        #region Methods

        public async Task<T> Send(ApiRequest<U> apiRequest, string apiFormat = ApiFormatType.JSON)
            => await _dicRequest[apiRequest.RequestType](apiRequest, apiFormat);

        private void MountHeader(HttpClient client, ApiRequest<U> apiRequest, string apiFormat)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(apiFormat));
            client.Timeout = TimeOut;

            client.DefaultRequestHeaders.Add("token", apiRequest.Token);
            client.DefaultRequestHeaders.Add("sessionid", apiRequest.SessionId);
        }

        private async Task<T> Execute(ApiRequest<U> apiRequest, string apiFormat,
            Func<HttpClient, ApiRequest<U>, Task<HttpResponseMessage>> function)
        {
            using (var client = new HttpClient())
            {
                MountHeader(client, apiRequest, apiFormat);

                var response = await function(client, apiRequest);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<T>();
            }
        }

        #endregion
    }
}
