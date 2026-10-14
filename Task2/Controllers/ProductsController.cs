
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task2Application.DTOs;
using Task2Application.Interface;

namespace Task2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductResponseDto>>>
        GetProducts(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
    {
        try
        {
            var products = await _productService.GetProductsAsync(
                page, pageSize);

            return Ok(products);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(
        int id,
        [FromBody] UpdateProductDto dto)
    {
        try
        {
            var updated = await _productService.UpdateProductAsync(
                id, dto);

            if (!updated)
                return NotFound($"Product with ID {id} was not found.");

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict(
                "The product was modified by another user. Reload it and try again.");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            var deleted = await _productService.SoftDeleteProductAsync(id);

            if (!deleted)
                return NotFound($"Product with ID {id} was not found.");

            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict(
                "The product was modified by another user. Please try again.");
        }
    }
}
