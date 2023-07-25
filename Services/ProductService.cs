using System.Net.Http.Json;
using System.Text.Json;

namespace blazorappdemo;
public class ProductService:IProductService
{
    public readonly HttpClient _client;
    private readonly JsonSerializerOptions _options;
    public ProductService(HttpClient client)
    {
        _client = client;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<Product>> Get()
    {
        var response = await _client.GetAsync("v1/products");
        var content = await response.Content.ReadAsStringAsync();
        
        if(!response.IsSuccessStatusCode)
            throw new ApplicationException(content);

        return await JsonSerializer.DeserializeAsync<List<Product>>(await response.Content.ReadAsStreamAsync(),_options);
    }

   public async Task<Product> GetById(int productId)
    {
        var response = await _client.GetAsync($"v1/products/{productId}");
        var content = await response.Content.ReadAsStringAsync();
        if(!response.IsSuccessStatusCode)
            throw new ApplicationException(content);

        return await JsonSerializer.DeserializeAsync<Product>(await response.Content.ReadAsStreamAsync(),_options);
    }

    public async Task Add(Product product)
    {
        var response = await _client.PostAsync("v1/products", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new ApplicationException(content);
    }

    public async Task Update(int ProductId,Product product)
    {
        var response = await _client.PutAsync($"v1/products/{ProductId}",JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();
        if(!response.IsSuccessStatusCode)
            throw new ApplicationException(content);
    }

    public async Task Delete(int productId)
    {
        var response = await _client.DeleteAsync($"v1/products/{productId}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new ApplicationException(content);

    }
}

public interface IProductService
{
    Task<List<Product>> Get();
    Task<Product> GetById(int productId);
    Task Add(Product product);
    Task Update(int ProductId,Product product);
    Task Delete(int productId);
}