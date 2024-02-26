using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MaxiEmployeeInsurance.Services
{
    public class AgentService<TEntity, TResponse>
        where TEntity : class
        where TResponse : class
    {
        public TResponse? resp;
        public async Task<TResponse> GetAsync(AgentParameters<TEntity> param)
        { param.Metodo = Method.Get; await RequestAsync(param); return resp; }
        public async Task<TResponse> PostAsync(AgentParameters<TEntity> param)
       { param.Metodo = Method.Post; await RequestAsync(param); return resp; }
        public async Task<TResponse> PutAsync(AgentParameters<TEntity> param)
        { param.Metodo = Method.Put; await RequestAsync(param); return resp; }
        public async Task<TResponse> DeleteAsync(AgentParameters<TEntity> param)
        { param.Metodo = Method.Delete; await RequestAsync(param); return resp; }

        private HttpClient NewHtttpClient(NetworkCredential? credentials = null)
        {
            return credentials == null
                ? new HttpClient()
                : new HttpClient(new HttpClientHandler { Credentials = credentials },true);
        }

        private async Task RequestAsync(AgentParameters<TEntity> param)
        {
            resp = null;
            using (var cliente = NewHtttpClient(param.Credentials))
            {
                if (!string.IsNullOrEmpty(param.Language))
                {
                    cliente.DefaultRequestHeaders.Add("Language", param.Language);
                }
                cliente.Timeout = TimeSpan.FromSeconds(240);
                cliente.BaseAddress = param.UrlBase;
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(param.TipoMedia));
                if (!string.IsNullOrEmpty(param.Token) && !string.IsNullOrEmpty(param.TokenScheme))
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(param.TokenScheme, param.Token);
                try
                {
                    HttpResponseMessage response;

                    switch (param.Metodo)
                    {
                        case Method.Get:
                            response = await cliente.GetAsync(param.Uri,HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                            break;
                        case Method.Post:
                            response = param.Entidad != null
                                ? await cliente.PostAsJsonAsync(param.Uri, param.Entidad).ConfigureAwait(false)
                                : await cliente.PostAsJsonAsync(param.Uri, new { }).ConfigureAwait(false);
                            break;
                        case Method.Put:
                            response = param.Entidad != null
                                ? await cliente.PutAsJsonAsync(param.Uri, param.Entidad).ConfigureAwait(false)
                                : await cliente.PutAsJsonAsync(param.Uri, new { }).ConfigureAwait(false);
                            break;
                        case Method.Delete:
                            response = await cliente.DeleteAsync(param.Uri).ConfigureAwait(false);
                            break;
                        default:
                            response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                            break;
                    }

                    if (response.IsSuccessStatusCode)
                        resp = await response.Content.ReadAsAsync<TResponse>();
                    else if (response.StatusCode.Equals(HttpStatusCode.Unauthorized))
                        resp = await response.Content.ReadAsAsync<TResponse>();
                    else if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
                        resp = await response.Content.ReadAsAsync<TResponse>();
                    else
                    {
                        cliente.CancelPendingRequests();
                        cliente.Dispose();
                    }
                }
                catch (Exception)
                {
                    cliente.CancelPendingRequests();
                    cliente.Dispose();
                }
            }
        }

    }

}
