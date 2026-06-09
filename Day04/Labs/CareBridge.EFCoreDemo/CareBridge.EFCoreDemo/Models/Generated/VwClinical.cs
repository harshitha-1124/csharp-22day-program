using System;
using System.Collections.Generic;

namespace CareBridge.EFCoreDemo.Models.Generated;

public partial class VwClinical
{
    public int PatientId { get; set; }

    public string FullName { get; set; } = null!;

    public int EncounterId { get; set; }

    public string EncounterType { get; set; } = null!;

    public string Diagnosis { get; set; } = null!;
}
