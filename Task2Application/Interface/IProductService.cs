
using Task2Application.DTOs;

namespace Task2Application.Interface;

public interface IProductService
{
    Task<IReadOnlyList<ProductResponseDto>> GetProductsAsync(
        int page,
        int pageSize);

    Task<bool> UpdateProductAsync(
        int id,
        UpdateProductDto dto);

    Task<bool> SoftDeleteProductAsync(int id);
}
