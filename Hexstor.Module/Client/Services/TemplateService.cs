using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using System.Net;

namespace Hexstor.Module.Template.Services
{
    public class TemplateService : ResponseServiceBase, IService
    {
        public TemplateService(IHttpClientFactory http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("Template");

        public async Task<(List<Models.Template>,HttpStatusCode)> GetTemplatesAsync()
        {
            var url = $"{Apiurl}";
            (var data, var response) = await GetJsonWithResponseAsync<List<Models.Template>>(url);
            return (data, response.StatusCode);      
        }

        public async Task<(Models.Template,HttpStatusCode)> GetTemplateAsync(int TemplateId)
        {
            var url = $"{Apiurl}/{TemplateId}";
            (var data, var response) = await GetJsonWithResponseAsync<Models.Template>(url);
            return (data, response.StatusCode);        
        }

        public async Task<(Models.Template,HttpStatusCode)> AddTemplateAsync(Models.Template Template)
        {
            var url = $"{Apiurl}";
            (var data, var response) = await PostJsonWithResponseAsync<Models.Template>(url,Template);
            return (data, response.StatusCode);        
        }

        public async Task<(Models.Template,HttpStatusCode)> UpdateTemplateAsync(Models.Template Template)
        {
            var url = $"{Apiurl}/{Template.TemplateId}";
            (var data, var response) = await PutJsonWithResponseAsync<Models.Template>(url,Template);
            return (data, response.StatusCode);        
        }

        public async Task<HttpStatusCode> DeleteTemplateAsync(int TemplateId)
        {
            var url = $"{Apiurl}/{TemplateId}";
            var response  = await DeleteWithResponseAsync(url);
            return response.StatusCode;
        }
    }
}
