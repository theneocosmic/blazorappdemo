using System.Net.Http.Json;
using System.Text.Json;

namespace blazorappdemo;
public class CategoryService:ICategoryService
{

    public readonly HttpClient _client;
    private readonly JsonSerializerOptions _options;
    public CategoryService(HttpClient client)
    {
        _client = client;
        _options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
    }

    public async Task<List<Category>> Get()
    {
        Console.WriteLine("Ejecutando Get");
        var response = await _client.GetAsync("v1/categories");
        var content = await response.Content.ReadAsStringAsync();

        if(!response.IsSuccessStatusCode)
            throw new ApplicationException(content);

        return JsonSerializer.Deserialize<List<Category>>(content, _options);
    }

    public async Task Add(Category category)
    {
        var response = await _client.PostAsync("v1/categories", JsonContent.Create(category));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new ApplicationException(content);
    }

    public async Task Update(int CategoryId,Category category){
        var response = await _client.PutAsync($"v1/categories/{CategoryId}", JsonContent.Create(category));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new ApplicationException(content);
    }

    public async Task Delete(int CategoryId){
        var response = await _client.DeleteAsync($"v1/categories/{CategoryId}");
        var content = await response.Content.ReadAsStringAsync();
        if(!response.IsSuccessStatusCode)
            throw new ApplicationException(content);
    }

}

public interface ICategoryService 
{
    Task<List<Category>> Get();
}