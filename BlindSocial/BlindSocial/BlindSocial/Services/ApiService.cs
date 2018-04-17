using BlindSocial.Helpers;
using BlindSocial.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BlindSocial.Services
{
    public class ApiService
    {
        protected string baseAddress;
        protected string securityToken;
        protected HttpClient m_HttpClient;

        public ApiService(string baseAddress, string securityToken)
        {
            this.baseAddress = Settings.BaseAPIUrl + baseAddress;
            // Storing the security token in a class property of type string    
            this.securityToken = securityToken.StartsWith("Bearer") ? securityToken.Substring(7) : securityToken;
            m_HttpClient = CreateHttpClient();
        }

        protected HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient()
            {
                MaxResponseContentBufferSize = 9999999,
                Timeout = new TimeSpan(0, 2, 0),
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(securityToken))
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + securityToken);

            return httpClient;
        }

        public async Task<RootObject> Analize(string url)
        {
            try
            {
                // Get the response from the server url and REST path for the data  
                var response = await m_HttpClient.PostAsync(new Uri(baseAddress + "?url=" + url),
                    new StringContent("", Encoding.UTF8, "application/json"));

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException("Access Denied");

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<RootObject>(await response.Content.ReadAsStringAsync());

                throw new WebException(response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                // TODO:        
                throw ex;
            }
        }


        public async Task<string> Translate(string text)
        {
            try
            {
                // Get the response from the server url and REST path for the data  
                var response = await m_HttpClient.PostAsync(new Uri(Settings.TranslateAPIUrl),
                    new StringContent($"{{'q': '{text}', 'target': 'es'}}", Encoding.UTF8, "application/json"));

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException("Access Denied");

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());

                throw new WebException(response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                // TODO:        
                throw ex;
            }
        }
    }
}
