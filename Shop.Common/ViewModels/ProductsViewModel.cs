

namespace Shop.Common.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using Services;

    public class ProductsViewModel : MvxViewModel
    {
        private List<Product> products;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;

        public List<Product> Products
        {
            get => this.products;
            set => this.SetProperty(ref this.products, value);
        }

        public ProductsViewModel(
            IApiService apiService,
            IDialogService dialogService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var response = await this.apiService.GetListAsync<Product>(
                "https://shopzulu.azurewebsites.net",
                "/api",
                "/Products",
                "bearer",
                token.Token);

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            this.Products = (List<Product>)response.Result;
            this.products = this.products.OrderBy(p => p.Name).ToList();
        }
    }

}
