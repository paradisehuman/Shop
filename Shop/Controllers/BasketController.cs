using Microsoft.AspNetCore.Mvc;
using Shop.Application.Contracts;

namespace Shop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketController(IBasketService basketService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateBasket(Guid customerId)
    {
        await basketService.CreateBasketAsync(customerId);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> AddItemToBasket(Guid basketId, Guid productId, int quantity)
    {
        await basketService.AddItemToBasketAsync(basketId, productId, quantity);
        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveItemFromBasket(Guid basketId, Guid productId)
    {
        await basketService.RemoveItemFromBasketAsync(basketId, productId);
        return Ok();
    }
}