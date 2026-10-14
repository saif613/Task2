
using Microsoft.EntityFrameworkCore;
using Task2Application.DTOs;
using Task2Application.Interface;
using Task2Application.Persistence;

namespace Task2Application.Services;

public class ProductService : IProductService
{
    private readonly TaskDb _context;

    public ProductService(TaskDb context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ProductResponseDto>> GetProductsAsync(
        int page, int pageSize)
    {
        if (page < 1)
            throw new ArgumentException("Page must be >= 1.");

        if (pageSize < 1 || pageSize > 100)
            throw new ArgumentException(
                "PageSize must be between 1 and 100.");

        return await _context.Products
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProductResponseDto(
                p.Id,
                p.ProductName,
                p.CategoryId,
                p.Category == null ? null : p.Category.Name,
                p.UnitPrice,
                p.UnitsInStock,
                p.RowVersion))
            .ToListAsync();
    }

    public async Task<bool> UpdateProductAsync(
        int id, UpdateProductDto dto)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
            return false;

        if (dto.CategoryId.HasValue)
        {
            var categoryExists = await _context.Categories
                .AnyAsync(c => c.Id == dto.CategoryId.Value);

            if (!categoryExists)
                throw new ArgumentException(
                    "The specified category does not exist.");
        }

        _context.Entry(product)
            .Property(p => p.RowVersion)
            .OriginalValue = dto.RowVersion;

        product.Update(
            dto.ProductName,
            dto.CategoryId,
            dto.UnitPrice,
            dto.UnitsInStock);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> SoftDeleteProductAsync(int id)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
            return false;

        product.SoftDelete();

        await _context.SaveChangesAsync();

        return true;
    }
}
