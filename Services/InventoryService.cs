using BlazorApp1.Components.Inventory;
using MyBlazorAppSourse.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace MyBlazorAppSourse.Services
{
    public class InventoryService
    {
        private readonly HttpClient _httpClient;

        public InventoryService(HttpClient httpClient)
        {_httpClient = httpClient; }

        public async Task<List<InventoryModel>> LoadPublicInventory(string searchTerm = null, string sortBy = null)
        {
            var response = await _httpClient.GetFromJsonAsync<List<InventoryModel>>($"api/Inventory/GetPublicInventory?searchTerm={searchTerm}&sortBy={sortBy}");

            if (response != null)
            {
                return response;

            }
            return new List<InventoryModel>();
        }

        public async Task<List<InventoryModel>> LoadInventory(Guid userId, string searchTerm = null, string sortBy = null)
        {
            var response = await _httpClient.GetFromJsonAsync<List<InventoryModel>>($"api/Inventory/{userId}?searchTerm={searchTerm}f&sortBy={sortBy}");

            if (response != null)
            {
                return response;

            }
            return new List<InventoryModel>();
        }

        public async Task<bool> CreateInventory(InventoryModel inventory)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Inventory/CreateInventory", inventory);
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteInventory(Guid inventoryId, string userId)
        {
            var response = await _httpClient.DeleteAsync($"api/Inventory/DeleteInventory/{inventoryId}/{userId}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> EditInventory(InventoryModel inventory)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Inventory/EditInventory", inventory);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ShareInventory(InventoryAccessModel access, string userId)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Inventory/InventoryUserAccess/{userId}", access);
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<List<ItemModel>> LoadItem(Guid inventoryId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<ItemModel>>($"api/Inventory/GetItem/{inventoryId}");
            if (response != null)
            {
                return response;
            }
            return new List<ItemModel>();
        }

        public async Task<bool> CreateItem(ItemModel inventory)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Inventory/CreateItem", inventory);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> DeleteItem(Guid inventorid, Guid Itemid)
        {
            var response = await _httpClient.DeleteAsync($"api/Inventory/DeleteItem/{inventorid}/{Itemid}");
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> EditItem(ItemModel item)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Inventory/EditItem", item);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<List<InventoryModel>> LoadAllInventory(string searchTerm = null, string sortBy = null)
        {
            var response = await _httpClient.GetFromJsonAsync<List<InventoryModel>>($"api/Inventory/GetAllInventory?searchTerm={searchTerm}&sortBy={sortBy}");

            if (response != null)
            {
                return response;

            }
            return new List<InventoryModel>();
        }

        public async Task<string> GetCategory(Guid categoryId)
        {
            var response = await _httpClient.GetAsync($"api/Inventory/GetCategory/{categoryId}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();

            return "None";
        }
        public async Task<List<CategoryModel>> GetAllCategory()
        {
            var response = await _httpClient.GetAsync("api/Inventory/GetAllCategory");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<CategoryModel>>();
            }
            return new List<CategoryModel>();
        }

        public async Task<List<DiscussionModel>> GetDiscussions(Guid inventoryId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<DiscussionModel>>($"api/Inventory/GetDiscussion/{inventoryId}");
            if (response != null) {
                return response;
            }

            return new List<DiscussionModel>();
        }

        public async Task<bool> CreateDiscussions(DiscussionModel discussion)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Inventory/CreateDiscussion", discussion);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
