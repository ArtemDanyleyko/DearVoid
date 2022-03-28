using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSE.Client.Common
{
    public class CallServer
    {

        public CallServer()
        {

        }

        public async Task<string> GetStringAsync(string url)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();

#if DEBUG
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
#endif
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var client = new HttpClient(httpClientHandler))
            {
                //   var response = await client.PostAsync(url, content);
                return await client.GetStringAsync(url);

            }
        }

        public async Task<TOut> PostAsync<TIn, TOut>(string url, TIn toPost)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();

            //#if DEBUG
            //            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            //#endif
            string json = JsonConvert.SerializeObject(toPost);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var client = new HttpClient(httpClientHandler))
            {
                var ret = await client.PostAsync(url, httpContent);
                string contents = await ret.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TOut>(contents);
            }
        }
    }
}
