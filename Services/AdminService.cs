using MyBlazorAppSourse.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;


namespace MyBlazorAppSourse.Services
{
    public class AdminService
    {
        private readonly HttpClient _httpClient;

        public AdminService(HttpClient httpClient) {  _httpClient = httpClient; }
        public async Task<List<User>> LoadAllUsers()
        {
            var response = await _httpClient.GetAsync("api/Admin/GetAllUser");
            
            if (response.IsSuccessStatusCode)
            {
                
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<List<User>>(content, options);
            }
            return new List<User>();
        }

        public async Task<List<User>> LoadUsers(int startIndex, int endIndex)
        {
            var response = await _httpClient.GetAsync($"api/Admin/GetUser?startIndex={startIndex}&endIndex={endIndex}");
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<List<User>>(content, options);
            }
            return new List<User>();
        }

        public async Task<string> LoadRole(string email)
        {
            var response = await _httpClient.GetAsync($"api/Admin/GetUserRole?email={email}");
            if (response.IsSuccessStatusCode) 
            {
                var content = await response.Content.ReadAsStringAsync();

                return content;
            }
            return "";
        }

        public async Task<EditModel> EditUser(EditModel user)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Admin/EditUser", user);
            if (response.IsSuccessStatusCode) 
            {
                return await response.Content.ReadFromJsonAsync<EditModel>();
            }
            return user;
        }
            
        public async Task<int> GetUserCount()
        {
            var response = await _httpClient.GetAsync("api/Admin/GetUserCount");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<int>();
            }
            return 0;
        }

        public async Task<bool> DeleteUser(string email)
        { var responses = await _httpClient.DeleteAsync($"api/Admin/DeleteUserByEmail/{email}");
            if (responses.IsSuccessStatusCode) 
            {
                return true;
            }
            return false;
        }
    }
}
