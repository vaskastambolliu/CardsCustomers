using System;
using System.Collections.Generic;

namespace CardsCustomers.Models;

public partial class Currency
{
    public int IdCurrency { get; set; }

    public string CurrencyName { get; set; } = null!;
}
