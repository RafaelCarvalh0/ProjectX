using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Text;

namespace ProjectX.Services.Client
{
    public class ClientHelper
    {
        private HttpClient client = new();
        private Uri uriBase;

        public ClientHelper()
        {
            uriBase = new Uri("https://localhost:7100/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public enum RequestType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public async Task<dynamic> CallWebService(string Endpoint, RequestType method, object request = default(object))
        {
            try
            {
                string uri = uriBase + Endpoint;

                HttpResponseMessage httpResponse = new HttpResponseMessage();

                switch (method)
                {
                    case RequestType.GET:
                        httpResponse = await client.GetAsync(uri);
                        break;
                    case RequestType.POST:
                        httpResponse = await client.PostAsJsonAsync(uri, request);
                        break;
                    case RequestType.PUT:
                        httpResponse = await client.PutAsJsonAsync(uri, request);
                        break;
                    case RequestType.DELETE:
                        httpResponse = await client.DeleteAsync(uri);
                        break;
                    default:
                        break;
                }

                if (!httpResponse.IsSuccessStatusCode)
                {
                    if ((int)httpResponse.StatusCode == 401)
                    {
                        throw new Exception(httpResponse.ReasonPhrase);
                    }

                    var contentResult = await httpResponse.Content.ReadAsStringAsync();
                    if ((int)httpResponse.StatusCode >= 400 && (int)httpResponse.StatusCode <= 500)
                        throw new Exception(contentResult);

                    throw new Exception("Ocorreu algum erro não esperado!");
                }

                var json = await httpResponse.Content.ReadAsStringAsync();

                if (json != null && json.Length > 0)
                {
                    var resultCore = JsonConvert.DeserializeObject(json);

                    return resultCore;
                }
                else
                {
                    if (string.IsNullOrEmpty(json))
                        return null;

                    int statusCode = (int)httpResponse.StatusCode;

                    if (statusCode == 204 || statusCode == 200)
                        return true;
                    else
                        return false;
                }

                //if (method == RequestType.GET)
                //{
                //    response = await client.GetAsync(uri);
                //    dynamic result = await response.Content.ReadAsStringAsync();

                //    return result;
                //}
                //else if (method == RequestType.POST)
                //{
                //    response = await client.PostAsJsonAsync(uri, request);
                //    dynamic result = await response.Content.ReadAsStringAsync();

                //    return result;
                //}
                //else if (method == RequestType.PUT)
                //{
                //    response = await client.PutAsJsonAsync(uri, request);
                //    dynamic result = await response.Content.ReadAsStringAsync();

                //    return response;
                //}
                //else if (method == RequestType.DELETE)
                //{
                //    response = await client.DeleteAsync(uri);
                //    dynamic result = await response.Content.ReadAsStringAsync();

                //    return result;
                //}

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
