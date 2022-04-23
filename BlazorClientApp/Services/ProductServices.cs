using System.Net.Http.Json;
using Models.App.ViewModels;

namespace BlazorClientApp.Services
{   
    public interface IProductService
    {
      
        Task<IEnumerable<ProductViewModel>> GetProducts();
        Task<ProductViewModel> CreateProduct(ProductViewModel product);
    }
    public class ProductServices : IProductService
    {
        private string message = string.Empty;
        private readonly HttpClient httpClient;

        public ProductServices(HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
        }
        public async Task<IEnumerable<ProductViewModel>> GetProducts()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<ProductViewModel[]>("api/product");
                
            }
            catch
            {
                throw new ApplicationException($"data retrive failed");
            }
        }

        public async Task<ProductViewModel> CreateProduct(ProductViewModel product)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<ProductViewModel>("api/Product", product);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<ProductViewModel>();
                }
                else
                {
                    message = "data saved unsuccefully !!";
                    return null;
                }
            }
            catch
            {
                throw new ApplicationException($"error:{message}");
            }
        }
    }
}
