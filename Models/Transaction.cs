using System;
using System.Collections.Generic;

namespace CardsCustomers.Models;

public partial class Transaction
{
    public int IdTransaction { get; set; }

    public int? IdCustomer { get; set; }

    public string? IdCard { get; set; }

    public int? Points { get; set; }

    public int? Purchase { get; set; }

    public int? Discount { get; set; }

    public decimal? NewValue { get; set; }

    public int? Balance { get; set; }

    public DateTime InsertDate { get; set; }

    public int IdUserAdmin { get; set; }

    public bool? Deleted { get; set; }
    public int? PointsFromCurTrans { get; set; }
}
