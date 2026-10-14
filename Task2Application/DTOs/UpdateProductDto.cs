using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Task2Application.DTOs;

public record UpdateProductDto
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public required string ProductName { get; init; }

    public int? CategoryId { get; init; }

    [Range(typeof(decimal), "0", "9999999999999999.99")]
    public decimal? UnitPrice { get; init; }

    [Range(0, 32767)]
    public short? UnitsInStock { get; init; }

    [Required]
    public required byte[] RowVersion { get; init; }
}
