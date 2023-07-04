using System;
using System.Collections.Generic;

namespace CardsCustomers.Models;

public partial class PointsPerMoney
{
    public int IdPointsPerMoney { get; set; }

    public decimal PointsPerMoneyValue { get; set; }

    public DateTime InsertDate { get; set; }

    public int IdUserAdminInsert { get; set; }

    public bool Deleted { get; set; }
}
