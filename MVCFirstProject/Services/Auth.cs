using Microsoft.AspNetCore.Mvc;
using MVCFirstProject.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using MVCFirstProject.Serializer;

namespace MVCFirstProject.Services
{
    public class Auth
    {
        private static HttpClient httpClient = new HttpClient();
        private string content;
        #region Parameters
        private const string redredirect_uri = "https://stackoverflow.com/oauth/login_success";
        private const string client_id = "21122";
        #endregion
        public Auth(HttpClient _client)
        {
            httpClient = _client;
        }

        public async Task<IEnumerable<Tags>> GetAuthResult()
        {

            using(httpClient)
            {
                httpClient.BaseAddress = new Uri(redredirect_uri);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("scope", String.Empty);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("client_id", client_id);

                HttpResponseMessage responseMessage = await httpClient.GetAsync("https://stackoverflow.com/oauth/dialog");

                if (responseMessage.IsSuccessStatusCode)
                    content = responseMessage.Content.ReadAsStringAsync().Result;
                else return null;
            }

            return new SerializerResponse(content).DeserializeTagsResponse();
        }
    }
}
