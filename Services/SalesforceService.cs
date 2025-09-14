using System.Collections.Generic;
using System.Net.Http.Json;
using MyBlazorAppSourse.Models;
using MyBlazorAppSourse.Service;

namespace MyBlazorAppSourse.Services
{
    public class SalesforceService
    {
        private readonly HttpClient _httpClient;

        public SalesforceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SalesforceAccount>> GetAccountsAsync()
        {
           var response = _httpClient.GetFromJsonAsync<List<SalesforceAccount>>("api/salesforce/GetAccounts");
            if (response != null)
            {
                return await response;

            }
            return new List<SalesforceAccount>();
     
        }

        public async Task<SalesforceAccount> CreateAccountAsync(SalesforceAccount account)
        {
            var response = await _httpClient.PostAsJsonAsync("api/salesforce/CreateAccount", account);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<SalesforceAccount>();

        }
    }
}
