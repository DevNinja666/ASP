using ASP_NET_03._Razor_Pages_Product_Site.Models;
using ASP_NET_03._Razor_Pages_Product_Site.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_NET_03._Razor_Pages_Product_Site.Pages;

public class ProductModel : PageModel
{
    private readonly ProductService _productService;

    public Product? Product { get; set; }

    public ProductModel(ProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        Product = await _productService.GetProductById(id);

        if (Product is null)
            return NotFound();

        return Page();
    }
}
