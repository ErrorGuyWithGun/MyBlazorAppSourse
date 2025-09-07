
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MyBlazorAppSourse.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Nodes;


namespace MyBlazorAppSourse.Services
{
    public class UserService : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;
        public UserService(
            HttpClient httpClient, 
            ILocalStorageService localStorage, 
            NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _navigationManager = navigationManager;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            var expiration = await _localStorage.GetItemAsync<DateTime>("tokenExpiration");
            var roles = await _localStorage.GetItemAsync<string>("userRoles");


            if (string.IsNullOrEmpty(token) || expiration < DateTime.UtcNow)
            {
              
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var email = await _localStorage.GetItemAsync<string>("userEmail");

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, email),
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Role, roles)
            };

       
            var identity = new ClaimsIdentity(claims, "Token");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public async Task<bool> Login(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Authentication/login", loginModel);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(responseContent);
                var root = doc.RootElement;

                var token = root.GetProperty("token").GetString();
                var expiration = root.GetProperty("expiration").GetDateTime();
                string roles = root.GetProperty("role").GetString();
            
                await _localStorage.SetItemAsync("authToken", token);
                await _localStorage.SetItemAsync("tokenExpiration", expiration);
                await _localStorage.SetItemAsync("userEmail", loginModel.Email);
                await _localStorage.SetItemAsync("userRoles", roles);

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                return true;
            }
            return false;
        }


        public async Task<bool> Register(RegisterUser registerUser) 
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Authentication?role=User", registerUser);
            if (response.IsSuccessStatusCode)
            {
               
                return true;
            }
            return false;
        }

        public async Task<CurrentUser> GetCurrentUser()
        {
            string email = await _localStorage.GetItemAsync<string>("userEmail");
            if (email != null) 
            { 
                var user = await _httpClient.GetFromJsonAsync<CurrentUser>($"api/Authentication/email/{email}");
                return new CurrentUser
                {   Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    Role = await _localStorage.GetItemAsync<string>("userRoles"),
                    Token = await _localStorage.GetItemAsync<string>("authToken"),
                    Expiration = await _localStorage.GetItemAsync<DateTime>("tokenExpiration")
                }; 
            }   
            else 
             return new CurrentUser(); 
        }

        public async Task logOut()
        {
            await _localStorage.RemoveItemAsync("userEmail");
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("tokenExpiration");
            await _localStorage.RemoveItemAsync("userRoles");
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<bool> ForgotPassword(string Email)
        {
            
            var response = await _httpClient.PostAsJsonAsync("api/Authentication/ForgotPassword", Email);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }
        public async Task<bool> ResetPassword(ResetPasswordModel resetPassword) 
        {
            var response = await _httpClient.PostAsJsonAsync("api/Authentication/ResetPassword", resetPassword);
            if (response.IsSuccessStatusCode) 
            {
                return true;
            }
            return false;
        }
        public async Task<bool> GetIfUserExist(string email) 
        {
            var responseMessage = await _httpClient.GetAsync($"api/Authentication/email/{email}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> GetIfNameExist(string username) 
        {
            var responseMessage = await _httpClient.GetAsync($"api/Authentication/name/{username}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<UserDTOModel> GetIfIdExist(string id) 
        {
            var responseMessage = await _httpClient.GetAsync($"api/Authentication/id/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var user = await _httpClient.GetFromJsonAsync<UserDTOModel>($"api/Authentication/id/{id}");
                return new UserDTOModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    Roles = user.Roles,
                };

            }
            return new UserDTOModel();
        }

        public async Task CheckUserStatus(CurrentUser currentUser)
        {
            if (currentUser != null && currentUser.IsActive == false)
            {
                _navigationManager.NavigateTo("/Block");
            }
        }

    }


}