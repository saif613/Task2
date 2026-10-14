using System;
using System.Collections.Generic;
using System.Text;

namespace Task2Application.DTOs;

public record ProductResponseDto(
int Id,
string ProductName,
int? CategoryId,
string? CategoryName,
decimal? UnitPrice,
short? UnitsInStock,
byte[] RowVersion
);
