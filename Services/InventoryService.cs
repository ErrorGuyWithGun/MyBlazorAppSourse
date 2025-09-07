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
        { this._httpClient = httpClient; }

        public async Task<List<InventoryModel>> LoadPublicInventory()
        {
            var response = await _httpClient.GetAsync("api/Inventory/GetPublicInventory");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<InventoryModel>>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            }
            return new List<InventoryModel>();
        }

        public async Task<List<InventoryModel>> LoadInventory(Guid userId)
        {
            var response = await _httpClient.GetAsync($"api/Inventory/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<InventoryModel>>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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

        public async Task<List<ItemModel>> LoadItem(Guid inventoryId)
        {
            var response = await _httpClient.GetAsync($"api/Inventory/GetItem/{inventoryId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ItemModel>>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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

        public async Task<List<InventoryModel>> LoadAllInventory()
        {
            var response = await _httpClient.GetAsync("api/Inventory/GetAllInventory");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<InventoryModel>>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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
            var response = await _httpClient.GetAsync($"api/Inventory/GetDiscussion/{inventoryId}");
            if (response.IsSuccessStatusCode) {
                var content= await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<DiscussionModel>>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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
