using Oqtane.Enums;
using Oqtane.Models;
using Oqtane.Services;
using Oqtane.Shared;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Hexstor.Module.Template.Services
{
    public  partial class ResponseServiceBase : ServiceBase
    {
        private readonly SiteState _siteState;
        protected ResponseServiceBase(IHttpClientFactory factory, SiteState siteState) : base(factory, siteState)
        {
            _siteState = siteState;
        }

        protected async Task<(T,HttpResponseMessage)> GetJsonWithResponseAsync<T>(string uri)
        {
            var response = await GetHttpClient().GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None);
            if (await CheckResponse(response, uri) && ValidateJsonContent(response.Content))
            {
                return (await response.Content.ReadFromJsonAsync<T>(), response);
                
            }
            return (default(T), response);
        }

        protected async Task<(T, HttpResponseMessage)> PostJsonWithResponseAsync<T>(string uri, T data)
        {
            var response = await GetHttpClient().PostAsJsonAsync<T>(uri,data);
            if (await CheckResponse(response, uri) && ValidateJsonContent(response.Content))
            {
                return (await response.Content.ReadFromJsonAsync<T>(), response);

            }
            return (default(T), response);
        }

        protected async Task<(T, HttpResponseMessage)> PutJsonWithResponseAsync<T>(string uri, T data)
        {
            var response = await GetHttpClient().PutAsJsonAsync<T>(uri, data);
            if (await CheckResponse(response, uri) && ValidateJsonContent(response.Content))
            {
                return (await response.Content.ReadFromJsonAsync<T>(), response);

            }
            return (default(T), response);
        }

        protected async Task<HttpResponseMessage> DeleteWithResponseAsync(string uri)
        {
           var response = await GetHttpClient().DeleteAsync(uri);
       
           return response;
        }

        protected async Task LogErrorRead(string uri, string message, params object[] args)
        {
            await Log(uri, LogFunction.Read.ToString(), LogLevel.Error.ToString(), message, args);
         
        }
        protected async Task LogErrorCreate(string uri, string message, params object[] args)
        {
            await Log(uri, LogFunction.Create.ToString(), LogLevel.Error.ToString(), message, args);

        }

        protected async Task LogErrorUpdate(string uri, string message, params object[] args)
        {
            await Log(uri, LogFunction.Update.ToString(), LogLevel.Error.ToString(), message, args);

        }

        private async Task Log(string uri, string method, string status, string message, params object[] args)
        {
            if (_siteState.Alias != null && !uri.StartsWith(CreateApiUrl("Log")))
            {
                var log = new Log();
                log.SiteId = _siteState.Alias.SiteId;
                log.PageId = null;
                log.ModuleId = null;
                log.UserId = null;
                log.Url = uri;
                log.Category = GetType().AssemblyQualifiedName;
                log.Feature = Utilities.GetTypeNameLastSegment(log.Category, 0);
                log.Function = method;
                log.Level = status;
                log.Message = message;
                log.MessageTemplate = "";
                log.Properties = JsonSerializer.Serialize(args);
                await PostJsonAsync(CreateApiUrl("Log"), log);
            }
        }

        private async Task<bool> CheckResponse(HttpResponseMessage response, string uri)
        {
            if (response.IsSuccessStatusCode)
            {
                // if response from api call is not from an api url then the route was not mapped correctly
                if (uri.Contains("/api/") && !response.RequestMessage.RequestUri.AbsolutePath.Contains("/api/"))
                {
                    await Log(uri, response.RequestMessage.Method.ToString(), LogLevel.Error.ToString(), "Request {Uri} Not Mapped To An API Controller Method", uri);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (response.StatusCode != HttpStatusCode.NoContent && response.StatusCode != HttpStatusCode.NotFound)
                {
                    await Log(uri, response.RequestMessage.Method.ToString(), LogLevel.Error.ToString(), "Request {Uri} Failed With Status {StatusCode} - {ReasonPhrase}", uri, response.StatusCode, response.ReasonPhrase);
                }
                return false;
            }
        }

        private static bool ValidateJsonContent(HttpContent content)
        {
            var mediaType = content?.Headers.ContentType?.MediaType;
            return mediaType != null && mediaType.Equals("application/json", StringComparison.OrdinalIgnoreCase);
        }
    }
}
