using Microsoft.Azure.AppService.ApiApps.Service;
using ProjectX.Models.Authentication;
using ProjectX.Services.Api;
using Rabrune.Services.Authentication;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProjectX.Services.Authentication {
    public class AuthenticationFactory : IAuthenticationFactory {
        #region Properties
        HttpClientHandler _httpClientHandler = new HttpClientHandler();
        private readonly HttpClient _client;

        private const string ERROR_MESSAGE = "Ocorreu um erro. Tente novamente.";
        #endregion

        public AuthenticationFactory() {
            _httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            _client = new HttpClient(_httpClientHandler);

            this._client.BaseAddress = AppSettings.Caminho;
            this._client.DefaultRequestHeaders.Accept.Clear();
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region Login

        public async Task<TokenResult> Login(UserLogin user) {
            try {
                var response = await PostAsync("api/Token/Get/", user);

                var contentResult = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode) {
                    if (((int)response.StatusCode) >= 400 && ((int)response.StatusCode) < 500)
                        return null;

                    throw new Exception(ERROR_MESSAGE);
                }

                return new HttpClientHelper().Deserialize<TokenResult>(contentResult);
            } catch (Exception e) {
                throw e;
            }
        }

        private string GetCharacters(int aLength) {
            var chars = new[] { "a", "b", "c", "d", "e", "f", "g", " " };
            var returnval = string.Join(string.Empty,
                Enumerable.Range(0, aLength).Select(it => chars[it % chars.Length]).ToArray());
            //Assert.That(returnval.Length, EqualTo(aLength), "GetCharacters() did not return correct length  string");
            return returnval;
        }
        #endregion

        #region Post Async
        /// <summary>
        /// Call Post Async
        /// </summary>
        /// <param name="endPoint">Set End Point</param>
        /// <param name="obj">Object to serealize</param>
        public async Task<HttpResponseMessage> PostAsync<T>(string endPoint, T obj)
            => await
            (string.IsNullOrWhiteSpace(AppSettings.Acesso) ?
            new HttpClientHelper(_client)
                .SetEndpoint($"{endPoint}")
                .WithContentSerialized(obj)
                .PostAsync() :
            new HttpClientHelper(_client)
                .SetEndpoint($"{endPoint}")
                .WithContentSerialized(obj)
                .SetToken(AppSettings.Acesso)
                .PostAsync());
        #endregion

        #region Put Async
        /// <summary>
        /// Call Put Async
        /// </summary>
        /// <param name="endPoint">Set End Point</param>
        /// <param name="token">Token JWT</param>
        /// <param name="obj">Object to serealize</param>
        public async Task<HttpResponseMessage> PutAsync<T>(string endPoint, T obj)
            => await new HttpClientHelper(_client)
                .SetEndpoint($"{endPoint}")
                .WithContentSerialized(obj)
                .SetToken(AppSettings.Acesso)
                .PutAsync();
        #endregion

        #region Get Async
        /// <summary>
        /// Call Get Async
        /// </summary>
        /// <param name="endPoint">Set End Point</param>
        /// <param name="token">Token JWT</param>
        public async Task<HttpResponseMessage> GetAsync(string endPoint)
            => await new HttpClientHelper(_client)
                .SetEndpoint($"{endPoint}")
                .SetToken(AppSettings.Acesso)
                .GetAsync();
        #endregion

        #region Delete Async
        /// <summary>
        /// Call Get Async
        /// </summary>
        /// <param name="endPoint">Set End Point</param>
        /// <param name="token">Token JWT</param>
        public async Task<HttpResponseMessage> DeleteAsync(string endPoint)
            => await new HttpClientHelper(_client)
                .SetEndpoint($"{endPoint}")
                .SetToken(AppSettings.Acesso)
                .DeleteAsync();
        #endregion


    }
}
