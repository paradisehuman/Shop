using Microsoft.AspNetCore.Mvc;
using Shop.Application.Contracts;

namespace Shop.Controllers;

public class CheckoutController(ICheckoutService checkoutService) : ControllerBase
{
    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout(Guid basketId, Guid customerId)
    {
        await checkoutService.CheckoutAsync(basketId, customerId);

        return Ok("Purchase completed and discount for next purchase granted.");
    }
}