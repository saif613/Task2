using System;
using System.Collections.Generic;
using System.Text;

namespace Task2Application.Entity;

public class Category : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    public ICollection<Product> Products { get; private set; } = new List<Product>();



    public Category(string name, string? description = null) => (Name, Description) = (name, description);
    private Category() { }


}
