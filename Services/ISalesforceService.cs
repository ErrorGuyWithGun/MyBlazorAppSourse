using MyBlazorAppSourse.Models;

namespace MyBlazorAppSourse.Service
{
    public interface ISalesforceService
    {
        Task<List<SalesforceAccount>> GetAccountsAsync();
        Task<SalesforceAccount> CreateAccountAsync(SalesforceAccount account);
    }
}
