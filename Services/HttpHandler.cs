
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MyBlazorAppSourse.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.Versioning;
using System.Security.Claims;
using System.Text.Json;

namespace MyBlazorAppSourse.Services
{
    public class HttpHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorageService;

        public HttpHandler(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorageService.GetItemAsync<string>("authToken");
            if (!string.IsNullOrEmpty(token)) 
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
