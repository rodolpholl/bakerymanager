using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace BakeryManager.InfraEstrutura.Helpers
{
    public  class JSONHelper : IDisposable
    {
        public string URI { get; set; }
        private WebClient client { get; set; }

        public JSONHelper()
        {
            URI = string.Empty;
            StartupWebClient();
        }
        
        public JSONHelper(string pURI)
        {
            URI = pURI;
            StartupWebClient();
        }

        private void StartupWebClient()
        {
            client = new WebClient();
            client.Headers.Add("User-Agent", "Nobody");
        }

        public T ParseJsonStringToObject<T>(string Json) where T : class, new()
        {
            return JsonConvert.DeserializeObject<T>(Json);
        }

        public T ParseObjectByURI<T>(string pURI) where T : class, new()
        {
         
            var response = client.DownloadString(new Uri(pURI));

            var obj = ParseJsonStringToObject<T>(response);

            return obj;
        }

        public T ParseObjectByURI<T>() where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(URI))
                throw new Exception("Erro a converter a string JSON. A propriedade URI deve ser informada!");
            else
                return ParseObjectByURI<T>(URI);
        }

        public void Dispose()
        {
            client.Dispose();
            
        }
    }
}
