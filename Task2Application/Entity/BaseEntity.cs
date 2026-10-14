using System;
using System.Collections.Generic;
using System.Text;

namespace Task2Application.Entity;

public abstract class BaseEntity 
{
    public int Id { get; private set; }
    public bool IsDeleted { get; private set; } = false;

    public void SoftDelete()
    {
        IsDeleted = true;
    }

}