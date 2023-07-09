using System;
using System.Collections.Generic;

namespace CardsCustomers.Models;

public partial class UserRole
{
    public int IdUserRole { get; set; }

    public string Role { get; set; }

    public DateTime InsertDate { get; set; }

}
