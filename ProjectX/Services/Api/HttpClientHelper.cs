using Newtonsoft.Json;
using ProjectX.Services.Api;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Services {
    public class HttpClientHelper {
        #region Properties
        private HttpClient _client;
        private StringContent _content;
        private Encoding _encoding;
        private string _mediaType;
        private string _endpoint;
        #endregion

        #region Constructors
        /// <summary>
        /// Http Client Helper
        /// </summary>
        /// <param name="client"></param>
        public HttpClientHelper(HttpClient client) {
            //ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicy) => {
            //    if (sslPolicy == SslPolicyErrors.None)
            //        return true;

            //    if (sslPolicy == SslPolicyErrors.RemoteCertificateChainErrors &&
            //       ((HttpWebRequest)sender).RequestUri.AbsoluteUri.Equals("a trusted URL"))
            //        return true;

            //    return false;
            //};
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            //HttpClient client = new HttpClient(handler);
            //client.DefaultRequestHeaders.Add("Accept", "application/json");
            //HttpClientHandler _httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            //_client = new HttpClient(_httpClientHandler);
            _client = new HttpClient(handler);

            this._client.BaseAddress = AppSettings.Caminho;
            this._client.DefaultRequestHeaders.Accept.Clear();
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_client = client;            
            _encoding = Encoding.UTF8;
            _mediaType = "application/json";
        }

        public HttpClientHelper() {

        }
        #endregion

        #region Serialize
        /// <summary>
        /// Serialize object to JSON
        /// </summary>
        /// <param name="o">Objeto a ser serializado</param>
        /// <returns>Objeto serializado no tipo string</returns>
        public string Serialize<T>(T o)
            => JsonConvert.SerializeObject(o);
        #endregion

        #region Deserialize
        /// <summary>
        /// Deserialize JSON to object
        /// </summary>
        /// <param name="Json">Json a ser desserializado</param>
        /// <returns>Objeto desserializado no tipo T</returns>
        public T Deserialize<T>(string Json)
            => JsonConvert.DeserializeObject<T>(Json);
        #endregion

        #region With Content Serialized
        /// <summary>
        /// Convert to Json the object to be send
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public HttpClientHelper WithContentSerialized<T>(T o) {
            _content = new StringContent(Serialize(o), _encoding, _mediaType);
            return this;
        }
        #endregion

        #region With Content
        /// <summary>
        /// Convert tje object to be send
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public HttpClientHelper WithContent(object o) {
            _content = new StringContent(o.ToString(), _encoding, _mediaType);
            return this;
        }
        #endregion

        #region Set End Point
        /// <summary>
        /// Set End Point
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public HttpClientHelper SetEndpoint(string endpoint) {
            //_endpoint = "https://www.itmej.com:49500/" + endpoint;

            ///aqui
            _endpoint = "https://localhost:7100/" + endpoint;
            //_endpoint = "http://rabrune.brazilsouth.cloudapp.azure.com:5001/" + endpoint;

            return this;
        }
        #endregion

        #region Set Token
        /// <summary>
        /// Set Token
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public HttpClientHelper SetToken(string token) {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", token);
            return this;
        }
        #endregion

        #region Http Methods
        /// <summary>
        /// Get Async
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAsync()
            => await _client.GetAsync(_endpoint);

        /// <summary>
        /// Post Async
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync()
            => await _client.PostAsync(_endpoint, _content);

        /// <summary>
        /// Put Async
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutAsync()
            => await _client.PutAsync(_endpoint, _content);

        /// <summary>
        /// Delete Async
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteAsync()
            => await _client.DeleteAsync(_endpoint);
        #endregion
    }
}
