using Microsoft.AspNetCore.Mvc;
using Shop.Application.Contracts;
using Shop.Domain.Entities;

namespace Shop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddProduct(string picture, string title, string description, decimal price, int stock)
    {
        await productService.CreateProductAsync(picture, title, description, price, stock);
        return Ok();
    }
}