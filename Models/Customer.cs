using System;
using System.Collections.Generic;

namespace CardsCustomers.Models;

public partial class Customer
{
    public int IdCustomer { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public int Points { get; set; }

    public DateTime InsertDate { get; set; }

    public int IdUserAdmin { get; set; }

    public bool? Deleted { get; set; }

    public string TimeStampIdCard { get; set; } = null!;
    public string? Gender { get; set; }
}
