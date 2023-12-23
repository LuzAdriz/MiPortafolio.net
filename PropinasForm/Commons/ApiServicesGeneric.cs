using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Commons
{
    public class ApiServicesGeneric
    {
        public string baseApiUrl { get { return "https://localhost:7224"; } }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public (bool, string, IEnumerable<T>) getListaGeneric<T>(string endPoint)
        {
            var listaReturn = Enumerable.Empty<T>();
            var client = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{this.baseApiUrl}/api/{endPoint}"),
                Headers =
                {
                    {HttpRequestHeader.Accept.ToString(),"application/json" },
                },
            };

            var response = client.SendAsync(httpRequestMessage).Result;
            if (response == null)
            {
                return (true, "No se pudo establecer conexion con el api", listaReturn);
            }
            var stringResponse = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return (true, stringResponse, listaReturn);
            }
            else
            {
                listaReturn = JsonSerializer.Deserialize<IEnumerable<T>>(stringResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return (false, string.Empty, listaReturn);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public (bool, string) insertModelGeneric<T>(T model, string endPoint)
        {
            var client = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{this.baseApiUrl}/api/{endPoint}"),
                Headers =
                {
                    {HttpRequestHeader.Accept.ToString(),"application/json" },
                },
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var response = client.SendAsync(httpRequestMessage).Result;
            if (response == null)
            {
                return (true, "No se pudo establecer conexion con el api");
            }
            var stringResponse = response.Content.ReadAsStringAsync().Result;

            return ((response.StatusCode != HttpStatusCode.OK), stringResponse);
        }

        public (bool, string) UpdateModelGeneric<T>(T model, string endPoint)
        {
            var client = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{this.baseApiUrl}/api/{endPoint}"),
                Headers =
                {
                    {HttpRequestHeader.Accept.ToString(),"application/json" },
                },
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var response = client.SendAsync(httpRequestMessage).Result;
            if (response == null)
            {
                return (true, "No se pudo establecer conexion con el api");
            }
            var stringResponse = response.Content.ReadAsStringAsync().Result;

            return ((response.StatusCode != HttpStatusCode.OK), stringResponse);
        }

        public (bool, string) DeleteModelGeneric<T>(T model, string endPoint)
        {
            var id = model.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyValuePair), true) != null).FirstOrDefault();
            var client = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{this.baseApiUrl}/api/{endPoint}?id={id.GetValue(model)}"),
                Headers =
                {
                    {HttpRequestHeader.Accept.ToString(),"application/json" },
                },
            };

            var response = client.SendAsync(httpRequestMessage).Result;
            if (response == null)
            {
                return (true, "No se pudo establecer conexion con el api");
            }
            var stringResponse = response.Content.ReadAsStringAsync().Result;

            return ((response.StatusCode != HttpStatusCode.OK), stringResponse);
        }
    }
}
