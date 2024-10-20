using Microsoft.AspNetCore.Mvc;
using Shop.Application.Contracts;

namespace Shop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddCustomer(string firstName)
    {
        await customerService.CreateCustomerAsync(firstName);
        return Ok();
    }
}