using System;
using System.Collections.Generic;

namespace CardsCustomers.Models;

public partial class UserAdmin
{
    public int IdUserAdmin { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!;

    public DateTime InsertDate { get; set; }

    public DateTime ChangePasswordDate { get; set; }
}
