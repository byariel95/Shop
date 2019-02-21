

namespace Shop.Web.Models
{
    using Microsoft.AspNetCore.Http;
    using Data.Entities;
    using System.ComponentModel.DataAnnotations;

    public class ProductViewModel :Product

    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

    }
}
