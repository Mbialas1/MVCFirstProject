using Microsoft.AspNetCore.Mvc;
using MVCFirstProject.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace MVCFirstProject.Services
{
    public class Auth
    {
        private static HttpClient httpClient = new HttpClient();
        private string content;
        #region Parameters
        private const string redredirect_uri = "https://test";
        private const string client_id = "testmateusza-cf76326bcd57420c5e7d7db136c03a85";
        #endregion
        public Auth(HttpClient _client)
        {
            httpClient = _client;
        }

        public async Task<String> GetAuthResult()
        {
            HttpResponseMessage responseMessage = null;
            using (httpClient)
            {
                httpClient.BaseAddress = new Uri(redredirect_uri);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("response_type", "code");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("client_id", client_id);

                responseMessage = await httpClient.GetAsync("https://konto.furgonetka.pl/oauth/authorize");

                if (responseMessage.IsSuccessStatusCode)
                    content = responseMessage.Content.ReadAsStringAsync().Result;
                else return null;
            }

            return content;
        }
    }
}
