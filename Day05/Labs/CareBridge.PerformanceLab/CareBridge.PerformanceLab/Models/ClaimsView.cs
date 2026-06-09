using System;
using System.Collections.Generic;

namespace CareBridge.PerformanceLab.Models;

public partial class ClaimsView
{
    public int ClaimId { get; set; }

    public decimal BilledAmount { get; set; }

    public decimal? ReimbursedAmt { get; set; }

    public string Status { get; set; } = null!;
}
