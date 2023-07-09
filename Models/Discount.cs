using System;
using System.Collections.Generic;

namespace CardsCustomers.Models;

public partial class Discount
{
    public int IdDiscount { get; set; }

    public string DiscountName { get; set; } = null!;

    public int PointsNeeded { get; set; }

    public int DiscountPercent { get; set; }

    public DateTime InsertDate { get; set; }
    public int? IdUserAdmin { get; set; }
}
