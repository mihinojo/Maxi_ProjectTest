using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MaxiEmployeeInsurance.Services
{
    public class AgentParameters<TEntity>
    {
        public AgentParameters(string urlBase, string uri, Method metodo, string tipoMedia, TEntity entidad, string token, string tokenScheme)
        {
            UrlBase = new Uri(urlBase);
            Uri = uri;
            Metodo = metodo;
            TipoMedia = tipoMedia;
            Entidad = entidad;
            Token = token;
            TokenScheme = tokenScheme;
            Credentials = null;
        }

        public AgentParameters(string urlBase, string uri, Method metodo, string tipoMedia, string token, string tokenScheme)
        {
            UrlBase = new Uri(urlBase);
            Uri = uri;
            Metodo = metodo;
            TipoMedia = tipoMedia;
            Token = token;
            TokenScheme = tokenScheme;
            Credentials = null;
        }

        public AgentParameters(string urlBase, string uri, Method metodo, string tipoMedia, TEntity entidad)
        {
            UrlBase = new Uri(urlBase);
            Uri = uri;
            Metodo = metodo;
            TipoMedia = tipoMedia;
            Entidad = entidad;
            Token = string.Empty;
            TokenScheme = string.Empty;
            Credentials = null;
        }

        public AgentParameters(string urlBase, string uri, Method metodo, string tipoMedia)
        {
            UrlBase = new Uri(urlBase);
            Uri = uri;
            Metodo = metodo;
            TipoMedia = tipoMedia;
            Token = string.Empty;
            TokenScheme = string.Empty;
            Credentials = null;
        }

        public AgentParameters(string urlBase, string uri, string tipoMedia, TEntity entidad)
        {
            UrlBase = new Uri(urlBase);
            Uri = uri;
            TipoMedia = tipoMedia;
            Entidad = entidad;
            Token = string.Empty;
            TokenScheme = string.Empty;
            Credentials = null;
        }

        public AgentParameters(string urlBase, string uri, string tipoMedia, TEntity entidad, String attributes)
        {
            UrlBase = new Uri(urlBase);
            Uri = uri;
            TipoMedia = tipoMedia;
            Entidad = entidad;
            Token = string.Empty;
            TokenScheme = string.Empty;
            Credentials = null;
            ElasticAttributes = attributes;
        }

        public AgentParameters(string urlBase, string uri, string tipoMedia, TEntity entidad, int beneficiaryId)
        {
            UrlBase = new Uri(urlBase);
            Uri = uri;
            TipoMedia = tipoMedia;
            Entidad = entidad;
            Token = string.Empty;
            TokenScheme = string.Empty;
            Credentials = null;
            BeneficiaryId = beneficiaryId;
        }

        public AgentParameters(string urlBase, string uri, string tipoMedia)
        {
            if (urlBase != null)
            {
                UrlBase = new Uri(urlBase);
            }
          
            Uri = uri;
            TipoMedia = tipoMedia;
            Token = string.Empty;
            TokenScheme = string.Empty;
            Credentials = null;
        }

        public NetworkCredential Credentials { get; set; }
        public Uri UrlBase { get; set; }
        public string Uri { get; set; }
        public TEntity Entidad { get; set; }
        public Method Metodo { get; set; }
        public string TipoMedia { get; set; }
        public string Token { get; set; }
        public string TokenScheme { get; set; }
        public int BeneficiaryId { get; set; }
        public string ElasticAttributes { get; set; }
        public string Language { get; set; }

        public void AddNetworkCredentials(string credentialUser, string credentialPassword)
            => Credentials = new NetworkCredential(credentialUser, credentialPassword);

        public void RemoveNetworkCredentials()
            => Credentials = null;

        public void AddToken(string accessToken)
        {
            Token = accessToken;
            TokenScheme = Services.TokenScheme.Bearer;
        }

        public void AddLanguage(string language)
        {
            Language = language;
        }

        public void RemoveToken(string accessToken)
        {
            Token = string.Empty;
            TokenScheme = string.Empty;
        }
    }

    public static class TokenScheme
    {
        public static readonly string Bearer = "Bearer";
    }
}
