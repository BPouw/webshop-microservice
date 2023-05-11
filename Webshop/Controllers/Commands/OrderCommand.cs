using Microsoft.AspNetCore.Mvc;
using Domain;

namespace Webshop.Controllers.Commands;

public class OrderCommand : ControllerBase
{
    public OrderCommand()
    {
        
    }
    
    [HttpPost("Order")]
    public async Task<IActionResult> PostOrder(Product order)
    {
        return BadRequest();
    }
}