using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ProjectX.Services.Api.Application
{
    public class ApplicationFactory : IApplicationFactory
    {
        HttpClientHandler _httpClientHandler = new HttpClientHandler();
        private readonly HttpClient _client;
        private const string ERROR_MESSAGE = "An unexpected error has occurred. Please try again.";
        private static string _token;
        public ApplicationFactory()
        {
            _httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            _client = new HttpClient(_httpClientHandler);

            this._client.BaseAddress = AppSettings.Caminho;
            this._client.DefaultRequestHeaders.Accept.Clear();
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            GetToken();
        }

        private void GetToken()
        {
            ClaimsIdentity identity = AppSettings.Diretivas as ClaimsIdentity;
            //_token = $"{IdentityExtensions.GetTokenType(identity)} {IdentityExtensions.GetToken(identity)}";
            _token = "Wathever";
        }

        public async Task<dynamic> CallWebService(string endPoint, RequestTypeEnum requestTypeEnum, object obj = null)
        {
            try
            {
                var result = new JObject();
                HttpResponseMessage httpResponse = new HttpResponseMessage();

                switch (requestTypeEnum)
                {
                    case RequestTypeEnum.GET:
                        httpResponse = await GetAsync(endPoint);
                        break;
                    case RequestTypeEnum.POST:
                        httpResponse = await PostAsync(endPoint, obj);
                        break;
                    case RequestTypeEnum.PUT:
                        httpResponse = await PutAsync(endPoint, obj);
                        break;
                    case RequestTypeEnum.DELETE:
                        httpResponse = await DeleteAsync(endPoint);
                        break;
                    case RequestTypeEnum.POST_ANONIMOUS:
                        httpResponse = await PostAnonimousAsync(endPoint, obj);
                        break;
                    default:
                        break;
                }

                if (!httpResponse.IsSuccessStatusCode)
                {
                    if (((int)httpResponse.StatusCode) == 401)
                    {
                        throw new Exception(httpResponse.ReasonPhrase);
                    }

                    var contentResult = await httpResponse.Content.ReadAsStringAsync();
                    if (((int)httpResponse.StatusCode) >= 400 && ((int)httpResponse.StatusCode) <= 500)
                        throw new Exception(contentResult);

                    throw new Exception(ERROR_MESSAGE);
                }

                var json = await httpResponse.Content.ReadAsStringAsync();

                if (json != null && json.Length > 0)
                {
                    var resultCore = JsonConvert.DeserializeObject(json);

                    return resultCore;
                }
                else
                {
                    int statusCode = (int)httpResponse.StatusCode;

                    if (statusCode == 204 || statusCode == 200)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region Post Anonimous Async
        /// <summary>
        /// Call Post Async
        /// </summary>
        /// <param name="endPoint">Set End Point</param>
        /// <param name="obj">Object to serealize</param>
        public async Task<HttpResponseMessage> PostAnonimousAsync<T>(string endPoint, T obj)
            => await
            new HttpClientHelper(_client)
                .SetEndpoint($"{endPoint}")
                .WithContentSerialized(obj)
                .PostAsync();
        #endregion

        #region Post Async
        /// <summary>
        /// Call Post Async
        /// </summary>
        /// <param name="endPoint">Set End Point</param>
        /// <param name="obj">Object to serealize</param>
        private async Task<HttpResponseMessage> PostAsync<T>(string endPoint, T obj)
            => await new HttpClientHelper(_client)
                .SetEndpoint($"{endPoint}")
                .WithContentSerialized(obj)
                .SetToken(_token)
                .PostAsync();
        #endregion

        #region Put Async
        /// <summary>
        /// Call Put Async
        /// </summary>
        /// <param name="endPoint">Set End Point</param>
        /// <param name="obj">Object to serealize</param>
        private async Task<HttpResponseMessage> PutAsync<T>(string endPoint, T obj)
            => await new HttpClientHelper(_client)
                .SetEndpoint($"{endPoint}")
                .WithContentSerialized(obj)
                .SetToken(_token)
                .PutAsync();
        #endregion

        #region Get Async
        /// <summary>
        /// Call Get Async
        /// </summary>
        /// <param name="endPoint">Set End Point</param>
        private async Task<HttpResponseMessage> GetAsync(string endPoint)
            => await new HttpClientHelper(_client)
                .SetEndpoint($"{endPoint}")
                .SetToken(_token)
                .GetAsync();
        #endregion

        #region Delete Async
        /// <summary>
        /// Call Get Async
        /// </summary>
        /// <param name="endPoint">Set End Point</param>
        private async Task<HttpResponseMessage> DeleteAsync(string endPoint)
            => await new HttpClientHelper(_client)
                .SetEndpoint($"{endPoint}")
                .SetToken(_token)
                .DeleteAsync();
        #endregion
    }
}
