

namespace Shop.Common.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using Services;

    public class ProductsViewModel : MvxViewModel
    {
        private List<Product> products;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private readonly IMvxNavigationService navigationService;
        private MvxCommand addProductCommand;


        public ProductsViewModel(
           IApiService apiService,
           IDialogService dialogService,
           IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.LoadProducts();
        }

        public ICommand AddProductCommand
        {
            get
            {
                this.addProductCommand = this.addProductCommand ?? new MvxCommand(this.AddProduct);
                return this.addProductCommand;
            }
        }

        

        public List<Product> Products
        {
            get => this.products;
            set => this.SetProperty(ref this.products, value);
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            this.LoadProducts();
        }



        private async void AddProduct()
        {
            await this.navigationService.Navigate<AddProductViewModel>();
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
