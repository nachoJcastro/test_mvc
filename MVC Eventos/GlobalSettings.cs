using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace MVC_Eventos
{
    public class GlobalSettings
    {
        public static HttpClient WebApiClient = new HttpClient();

        static GlobalSettings()
        {
            WebApiClient.BaseAddress = new Uri("http://localhost:50248/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue ("application/json"));
        }
    }
}