using System;
using System.Collections.Generic;
using System.Text;

namespace Task2Application.Entity;

public class Product : BaseEntity
{
    public string ProductName { get; private set; } = null!;
    public int? CategoryId { get; private set; }
    public decimal? UnitPrice { get; private set; }
    public short? UnitsInStock { get; private set; }
    public Category? Category { get; private set; }

    public byte[] RowVersion { get; private set; } = null!;

    public Product(string productName, int? categoryId = null, decimal? unitPrice = null, short? unitsInStock = null) => (ProductName, CategoryId, UnitPrice, UnitsInStock) = (productName, categoryId, unitPrice, unitsInStock);
    private Product() { }


    public void Update(
        string productName,
        int? categoryId,
        decimal? unitPrice,
        short? unitsInStock)
    {
        ProductName = productName;
        CategoryId = categoryId;
        UnitPrice = unitPrice;
        UnitsInStock = unitsInStock;
    }
}
