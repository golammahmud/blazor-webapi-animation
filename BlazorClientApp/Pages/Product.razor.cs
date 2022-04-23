using BlazorClientApp.Services;
using Microsoft.AspNetCore.Components;
using Models.App.ViewModels;

namespace BlazorClientApp.Pages
{
    public class ProductBase:ComponentBase
    {
        [Inject]
        public IProductService productService { get; set; }
        protected bool _loading { get; set; }
        protected bool _hidePosition { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Products = (await productService.GetProducts()).ToList();
        }
    }
}
