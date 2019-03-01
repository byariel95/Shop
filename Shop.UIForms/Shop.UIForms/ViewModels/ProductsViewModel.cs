




namespace Shop.UIForms.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Common.Models;
    using Shop.Common.Services;
    using Xamarin.Forms;

    public class ProductsViewModel: BaseViewModel
    {
        private ApiService apiService;
        private ObservableCollection<Product> products;
        private bool isRefreshing;
        public ObservableCollection<Product> Products
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            this.IsRefreshing = true;
            var response = await this.apiService.GetListAsync<Product>(
                "https://shopzulu.azurewebsites.net",
                "/api",
                "/Products");

            this.IsRefreshing =false;
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error",response.Message,"Acept");
                return;

            }
            var myProducts = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(myProducts);

        }
    }

}
