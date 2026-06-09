//////using CareBridge.PerformanceLab.Models;
//////using CareBridge.PerformanceLab.Models;
//////using var db = new CareBridgeContext();
//////while (true)
//////{
//////    Console.Clear();
//////    Console.WriteLine("=================================");
//////    Console.WriteLine(" CAREBRIDGE PERFORMANCE LAB");
//////    Console.WriteLine("=================================");
//////    Console.WriteLine("1. View Patient");
//////    Console.WriteLine("2. View Patient Encounters");
//////    Console.WriteLine("3. Exit");
//////    Console.WriteLine();

//////    Console.Write("Choose Option: ");

//////    string? choice = Console.ReadLine();

//////    switch (choice)
//////    {
//////        case "1":
//////            ShowPatient();
//////            break;

//////        case "2":
//////            ShowEncounters();
//////            break;

//////        case "3":
//////            return;

//////        default:
//////            Console.WriteLine("Invalid Option");
//////            break;
//////    }

//////    Console.WriteLine();
//////    Console.WriteLine("Press any key to continue...");
//////    Console.ReadKey();
//////}
//////static void ShowPatient()
//////{
//////    using var db = new CareBridgeContext();
//////    var patient =
//////        db.Patients
//////          .FirstOrDefault(p => p.Mrn == "MRN999998");

//////    if (patient == null)
//////    {
//////        Console.WriteLine("Patient not found.");
//////        return;
//////    }

//////    Console.WriteLine();
//////    Console.WriteLine("PATIENT DETAILS");
//////    Console.WriteLine("----------------------------");

//////    Console.WriteLine($"Patient Id : {patient.PatientId}");
//////    Console.WriteLine($"MRN        : {patient.Mrn}");
//////    Console.WriteLine($"Name       : {patient.FullName}");
//////    Console.WriteLine($"City       : {patient.City}");
//////    Console.WriteLine($"Active     : {patient.IsActive}");
//////}
//////static void ShowEncounters()
//////{
//////    using var db = new CareBridgeContext();
//////    var patient =
//////        db.Patients
//////          .FirstOrDefault(p => p.Mrn == "MRN999998");

//////    if (patient == null)
//////    {
//////        Console.WriteLine("Patient not found.");
//////        return;
//////    }

//////    var encounters =
//////        db.Encounters
//////          .Where(e => e.PatientId == patient.PatientId)
//////          .ToList();

//////    Console.WriteLine();
//////    Console.WriteLine("PATIENT ENCOUNTERS");
//////    Console.WriteLine("----------------------------");

//////    Console.WriteLine($"Patient Name    : {patient.FullName}");
//////    Console.WriteLine($"Encounter Count : {encounters.Count}");
//////}


//////using System.Diagnostics;
//////using CareBridge.PerformanceLab.Models;
//////while (true)
//////{
//////    Console.Clear();
//////    Console.WriteLine("=================================");
//////    Console.WriteLine(" CAREBRIDGE PERFORMANCE LAB");
//////    Console.WriteLine("=================================");
//////    Console.WriteLine();

//////    Console.WriteLine("1. View Patient");
//////    Console.WriteLine("2. View Patient Encounters");
//////    Console.WriteLine("3. Simulate N+1 Problem");
//////    Console.WriteLine("4. Exit");

//////    Console.WriteLine();

//////    Console.Write("Choose Option: ");

//////    string? choice = Console.ReadLine();

//////    switch (choice)
//////    {
//////        case "1":
//////            ShowPatient();
//////            break;

//////        case "2":
//////            ShowEncounters();
//////            break;

//////        case "3":
//////            SimulateNPlusOne();
//////            break;

//////        case "4":
//////            return;

//////        default:
//////            Console.WriteLine("Invalid Option");
//////            break;
//////    }

//////    Console.WriteLine();
//////    Console.WriteLine("Press any key to continue...");
//////    Console.ReadKey();
//////}
//////static void ShowPatient()
//////{
//////    using var db = new CareBridgeContext();
//////    var patient =
//////        db.Patients
//////          .FirstOrDefault(p => p.Mrn == "MRN999998");

//////    if (patient == null)
//////    {
//////        Console.WriteLine("Patient not found.");
//////        return;
//////    }

//////    Console.WriteLine();
//////    Console.WriteLine("PATIENT DETAILS");
//////    Console.WriteLine("----------------------------");

//////    Console.WriteLine($"Patient Id : {patient.PatientId}");
//////    Console.WriteLine($"MRN        : {patient.Mrn}");
//////    Console.WriteLine($"Name       : {patient.FullName}");
//////    Console.WriteLine($"City       : {patient.City}");
//////    Console.WriteLine($"Active     : {patient.IsActive}");
//////}
//////static void ShowEncounters()
//////{
//////    using var db = new CareBridgeContext();
//////    var patient =
//////        db.Patients
//////          .FirstOrDefault(p => p.Mrn == "MRN999998");

//////    if (patient == null)
//////    {
//////        Console.WriteLine("Patient not found.");
//////        return;
//////    }

//////    var encounters =
//////        db.Encounters
//////          .Where(e => e.PatientId == patient.PatientId)
//////          .ToList();

//////    Console.WriteLine();
//////    Console.WriteLine("PATIENT ENCOUNTERS");
//////    Console.WriteLine("----------------------------");

//////    Console.WriteLine($"Patient Name    : {patient.FullName}");
//////    Console.WriteLine($"Encounter Count : {encounters.Count}");

//////    Console.WriteLine();
//////    Console.WriteLine("Recent Encounters");
//////    Console.WriteLine("----------------------------");

//////    foreach (var encounter in encounters.Take(10))
//////    {
//////        Console.WriteLine(
//////            $"{encounter.EncounterId} | {encounter.EncounterType} | {encounter.AdmitDate:d}");
//////    }
//////}
//////static void SimulateNPlusOne()
//////{
//////    using var db = new CareBridgeContext();
//////    Console.WriteLine();
//////    Console.WriteLine("SIMULATING N+1 QUERY PROBLEM");
//////    Console.WriteLine("----------------------------");

//////    var patient =
//////        db.Patients
//////          .FirstOrDefault(p => p.Mrn == "MRN999998");

//////    if (patient == null)
//////    {
//////        Console.WriteLine("Patient not found.");
//////        return;
//////    }

//////    Stopwatch stopwatch = Stopwatch.StartNew();

//////    var encounters =
//////        db.Encounters
//////          .Where(e => e.PatientId == patient.PatientId)
//////          .ToList();

//////    Console.WriteLine();
//////    Console.WriteLine($"Patient Name      : {patient.FullName}");
//////    Console.WriteLine($"Encounters Loaded : {encounters.Count}");

//////    int totalClaims = 0;

//////    foreach (var encounter in encounters)
//////    {
//////        var claims =
//////            db.Claims
//////              .Where(c =>
//////                     c.EncounterId ==
//////                     encounter.EncounterId)
//////              .ToList();

//////        totalClaims += claims.Count;
//////    }

//////    stopwatch.Stop();

//////    Console.WriteLine();
//////    Console.WriteLine($"Total Claims Loaded : {totalClaims}");

//////    Console.WriteLine();
//////    Console.WriteLine("PERFORMANCE SUMMARY");
//////    Console.WriteLine("----------------------------");

//////    Console.WriteLine("Patient Queries    : 1");
//////    Console.WriteLine("Encounter Queries  : 1");
//////    Console.WriteLine($"Claim Queries      : {encounters.Count}");

//////    Console.WriteLine();

//////    Console.WriteLine(
//////        $"Approx Total Queries : {encounters.Count + 2}");

//////    Console.WriteLine(
//////        $"Elapsed Time         : {stopwatch.ElapsedMilliseconds} ms");

//////    Console.WriteLine();
//////    Console.WriteLine("Check SQL Profiler Now");
//////    Console.WriteLine("----------------------------");
//////    Console.WriteLine("Notice how the Claim query executes repeatedly.");
//////    Console.WriteLine("One ququery is executed for every encounter.");
//////}

////using System.Diagnostics;
////using Microsoft.EntityFrameworkCore;
////using CareBridge.PerformanceLab.Models;
////while (true)
////{
////    Console.Clear();
////    Console.WriteLine("=================================");
////    Console.WriteLine(" CAREBRIDGE PERFORMANCE LAB");
////    Console.WriteLine("=================================");
////    Console.WriteLine();

////    Console.WriteLine("1. View Patient");
////    Console.WriteLine("2. View Patient Encounters");
////    Console.WriteLine("3. Simulate N+1 Problem");
////    Console.WriteLine("4. Eager Loading Demo");
////    Console.WriteLine("5. Exit");

////    Console.WriteLine();
////    Console.Write("Choose Option: ");

////    string? choice = Console.ReadLine();

////    switch (choice)
////    {
////        case "1":
////            ShowPatient();
////            break;

////        case "2":
////            ShowEncounters();
////            break;

////        case "3":
////            SimulateNPlusOne();
////            break;

////        case "4":
////            EagerLoadingDemo();
////            break;

////        case "5":
////            return;

////        default:
////            Console.WriteLine("Invalid Option");
////            break;
////    }

////    Console.WriteLine();
////    Console.WriteLine("Press any key to continue...");
////    Console.ReadKey();
////}
////static void ShowPatient()
////{
////    using var db = new CareBridgeContext();
////    var patient =
////        db.Patients
////          .FirstOrDefault(p => p.Mrn == "MRN999999");

////    if (patient == null)
////    {
////        Console.WriteLine("Patient not found.");
////        return;
////    }

////    Console.WriteLine();
////    Console.WriteLine("PATIENT DETAILS");
////    Console.WriteLine("----------------------------");

////    Console.WriteLine($"Patient Id : {patient.PatientId}");
////    Console.WriteLine($"MRN        : {patient.Mrn}");
////    Console.WriteLine($"Name       : {patient.FullName}");
////    Console.WriteLine($"City       : {patient.City}");
////    Console.WriteLine($"Active     : {patient.IsActive}");
////}
////static void ShowEncounters()
////{
////    using var db = new CareBridgeContext();
////    var patient =
////        db.Patients
////          .FirstOrDefault(p => p.Mrn == "MRN999998");

////    if (patient == null)
////    {
////        Console.WriteLine("Patient not found.");
////        return;
////    }

////    var encounters =
////        db.Encounters
////          .Where(e => e.PatientId == patient.PatientId)
////          .ToList();

////    Console.WriteLine();
////    Console.WriteLine("PATIENT ENCOUNTERS");
////    Console.WriteLine("----------------------------");

////    Console.WriteLine($"Patient Name    : {patient.FullName}");
////    Console.WriteLine($"Encounter Count : {encounters.Count}");
////}
////static void SimulateNPlusOne()
////{
////    using var db = new CareBridgeContext();
////    Console.WriteLine();
////    Console.WriteLine("SIMULATING N+1 QUERY PROBLEM");
////    Console.WriteLine("----------------------------");

////    var patient =
////        db.Patients
////          .FirstOrDefault(p => p.Mrn == "MRN999998");

////    if (patient == null)
////    {
////        Console.WriteLine("Patient not found.");
////        return;
////    }

////    Stopwatch stopwatch = Stopwatch.StartNew();

////    var encounters =
////        db.Encounters
////          .Where(e => e.PatientId == patient.PatientId)
////          .ToList();

////    int totalClaims = 0;

////    foreach (var encounter in encounters)
////    {
////        var claims =
////            db.Claims
////              .Where(c =>
////                     c.EncounterId ==
////                     encounter.EncounterId)
////              .ToList();

////        totalClaims += claims.Count;
////    }

////    stopwatch.Stop();

////    Console.WriteLine();
////    Console.WriteLine($"Patient Name      : {patient.FullName}");
////    Console.WriteLine($"Encounters Loaded : {encounters.Count}");
////    Console.WriteLine($"Claims Loaded     : {totalClaims}");

////    Console.WriteLine();
////    Console.WriteLine("PERFORMANCE SUMMARY");
////    Console.WriteLine("----------------------------");

////    Console.WriteLine("Patient Queries    : 1");
////    Console.WriteLine("Encounter Queries  : 1");
////    Console.WriteLine($"Claim Queries      : {encounters.Count}");

////    Console.WriteLine();

////    Console.WriteLine(
////        $"Approx Total Queries : {encounters.Count + 2}");

////    Console.WriteLine(
////        $"Elapsed Time         : {stopwatch.ElapsedMilliseconds} ms");

////    Console.WriteLine();
////    Console.WriteLine("Check SQL Profiler.");
////}
////static void EagerLoadingDemo()
////{
////    using var db = new CareBridgeContext();
////    Console.WriteLine();
////    Console.WriteLine("EAGER LOADING DEMO");
////    Console.WriteLine("----------------------------");

////    Stopwatch stopwatch = Stopwatch.StartNew();

////    var patient =
////        db.Patients

////          .Include(p => p.Encounters)

////          .ThenInclude(e => e.Claims)

////          .FirstOrDefault(p => p.Mrn == "MRN999998");

////    stopwatch.Stop();

////    if (patient == null)
////    {
////        Console.WriteLine("Patient not found.");
////        return;
////    }

////    int encounterCount =
////        patient.Encounters.Count;

////    int claimCount =
////        patient.Encounters
////               .SelectMany(e => e.Claims)
////               .Count();

////    Console.WriteLine();
////    Console.WriteLine($"Patient Name      : {patient.FullName}");
////    Console.WriteLine($"Encounters Loaded : {encounterCount}");
////    Console.WriteLine($"Claims Loaded     : {claimCount}");

////    Console.WriteLine();
////    Console.WriteLine("PERFORMANCE SUMMARY");
////    Console.WriteLine("----------------------------");

////    Console.WriteLine("Patient Queries           : 0");
////    Console.WriteLine("Encounter Queries         : 0");
////    Console.WriteLine("Claim Queries             : 0");

////    Console.WriteLine();
////    Console.WriteLine("Single Include Query      : 1");

////    Console.WriteLine();
////    Console.WriteLine(
////        $"Elapsed Time             : {stopwatch.ElapsedMilliseconds} ms");

////    Console.WriteLine();

////    Console.WriteLine("Expected Improvement");
////    Console.WriteLine("----------------------------");
////    Console.WriteLine("Module 2 : ~502 Queries");
////    Console.WriteLine("Module 3 : 1 Query");

////    Console.WriteLine();
////    Console.WriteLine("Check SQL Profiler Now.");
////}

//using System.Diagnostics;
//using Microsoft.EntityFrameworkCore;
//using CareBridge.PerformanceLab.Models;
//while (true)
//{
//    Console.Clear();
//    Console.WriteLine("=================================");
//    Console.WriteLine(" CAREBRIDGE PERFORMANCE LAB");
//    Console.WriteLine("=================================");
//    Console.WriteLine();

//    Console.WriteLine("1. View Patient");
//    Console.WriteLine("2. View Patient Encounters");
//    Console.WriteLine("3. Simulate N+1 Problem");
//    Console.WriteLine("4. Eager Loading Demo");
//    Console.WriteLine("5. Explicit Loading Demo");
//    Console.WriteLine("6. Exit");

//    Console.WriteLine();
//    Console.Write("Choose Option: ");

//    string? choice = Console.ReadLine();

//    switch (choice)
//    {
//        case "1":
//            ShowPatient();
//            break;

//        case "2":
//            ShowEncounters();
//            break;

//        case "3":
//            SimulateNPlusOne();
//            break;

//        case "4":
//            EagerLoadingDemo();
//            break;

//        case "5":
//            ExplicitLoadingDemo();
//            break;

//        case "6":
//            return;

//        default:
//            Console.WriteLine("Invalid Option");
//            break;
//    }

//    Console.WriteLine();
//    Console.WriteLine("Press any key to continue...");
//    Console.ReadKey();
//}
//static void ShowPatient()
//{
//    using var db = new CareBridgeContext();
//    var patient =
//        db.Patients
//          .FirstOrDefault(p => p.Mrn == "MRN999998");

//    if (patient == null)
//    {
//        Console.WriteLine("Patient not found.");
//        return;
//    }

//    Console.WriteLine();
//    Console.WriteLine("PATIENT DETAILS");
//    Console.WriteLine("----------------------------");

//    Console.WriteLine($"Patient Id : {patient.PatientId}");
//    Console.WriteLine($"MRN        : {patient.Mrn}");
//    Console.WriteLine($"Name       : {patient.FullName}");
//    Console.WriteLine($"City       : {patient.City}");
//    Console.WriteLine($"Active     : {patient.IsActive}");
//}
//static void ShowEncounters()
//{
//    using var db = new CareBridgeContext();
//    var patient =
//        db.Patients
//          .FirstOrDefault(p => p.Mrn == "MRN999999");

//    if (patient == null)
//    {
//        Console.WriteLine("Patient not found.");
//        return;
//    }

//    var encounters =
//        db.Encounters
//          .Where(e => e.PatientId == patient.PatientId)
//          .ToList();

//    Console.WriteLine();
//    Console.WriteLine("PATIENT ENCOUNTERS");
//    Console.WriteLine("----------------------------");

//    Console.WriteLine($"Patient Name    : {patient.FullName}");
//    Console.WriteLine($"Encounter Count : {encounters.Count}");
//}
//static void SimulateNPlusOne()
//{
//    using var db = new CareBridgeContext();
//    Console.WriteLine();
//    Console.WriteLine("SIMULATING N+1 QUERY PROBLEM");
//    Console.WriteLine("----------------------------");

//    var patient =
//        db.Patients
//          .FirstOrDefault(p => p.Mrn == "MRN999998");

//    if (patient == null)
//    {
//        Console.WriteLine("Patient not found.");
//        return;
//    }

//    Stopwatch stopwatch = Stopwatch.StartNew();

//    var encounters =
//        db.Encounters
//          .Where(e => e.PatientId == patient.PatientId)
//          .ToList();

//    int totalClaims = 0;

//    foreach (var encounter in encounters)
//    {
//        var claims =
//            db.Claims
//              .Where(c =>
//                     c.EncounterId ==
//                     encounter.EncounterId)
//              .ToList();

//        totalClaims += claims.Count;
//    }

//    stopwatch.Stop();

//    Console.WriteLine();
//    Console.WriteLine($"Patient Name      : {patient.FullName}");
//    Console.WriteLine($"Encounters Loaded : {encounters.Count}");
//    Console.WriteLine($"Claims Loaded     : {totalClaims}");

//    Console.WriteLine();
//    Console.WriteLine("PERFORMANCE SUMMARY");
//    Console.WriteLine("----------------------------");

//    Console.WriteLine("Patient Queries    : 1");
//    Console.WriteLine("Encounter Queries  : 1");
//    Console.WriteLine($"Claim Queries      : {encounters.Count}");

//    Console.WriteLine();

//    Console.WriteLine(
//        $"Approx Total Queries : {encounters.Count + 2}");

//    Console.WriteLine(
//        $"Elapsed Time         : {stopwatch.ElapsedMilliseconds} ms");
//}
//static void EagerLoadingDemo()
//{
//    using var db = new CareBridgeContext();
//    Console.WriteLine();
//    Console.WriteLine("EAGER LOADING DEMO");
//    Console.WriteLine("----------------------------");

//    Stopwatch stopwatch = Stopwatch.StartNew();

//    var patient =
//        db.Patients
//          .Include(p => p.Encounters)
//          .ThenInclude(e => e.Claims)
//          .FirstOrDefault(p => p.Mrn == "MRN999998");

//    stopwatch.Stop();

//    if (patient == null)
//    {
//        Console.WriteLine("Patient not found.");
//        return;
//    }

//    int encounterCount =
//        patient.Encounters.Count;

//    int claimCount =
//        patient.Encounters
//               .SelectMany(e => e.Claims)
//               .Count();

//    Console.WriteLine();
//    Console.WriteLine($"Patient Name      : {patient.FullName}");
//    Console.WriteLine($"Encounters Loaded : {encounterCount}");
//    Console.WriteLine($"Claims Loaded     : {claimCount}");

//    Console.WriteLine();

//    Console.WriteLine("PERFORMANCE SUMMARY");
//    Console.WriteLine("----------------------------");

//    Console.WriteLine("Patient Queries      : 0");
//    Console.WriteLine("Encounter Queries    : 0");
//    Console.WriteLine("Claim Queries        : 0");

//    Console.WriteLine();
//    Console.WriteLine("Single Include Query : 1");

//    Console.WriteLine();

//    Console.WriteLine(
//        $"Elapsed Time         : {stopwatch.ElapsedMilliseconds} ms");
//}
//static void ExplicitLoadingDemo()
//{
//    using var db = new CareBridgeContext();
//    Console.WriteLine();
//    Console.WriteLine("EXPLICIT LOADING DEMO");
//    Console.WriteLine("----------------------------");

//    Stopwatch stopwatch = Stopwatch.StartNew();

//    // Query 1
//    // Load only Patient
//    var patient =
//        db.Patients
//          .FirstOrDefault(p => p.Mrn == "MRN999998");

//    if (patient == null)
//    {
//        Console.WriteLine("Patient not found.");
//        return;
//    }

//    Console.WriteLine();
//    Console.WriteLine("Step 1");
//    Console.WriteLine("Patient Loaded");

//    Console.WriteLine($"Patient Name : {patient.FullName}");

//    Console.WriteLine();

//    Console.WriteLine("Step 2");
//    Console.WriteLine("Loading Encounters Explicitly");

//    // Query 2
//    // Load Encounters only when required
//    db.Entry(patient)
//      .Collection(p => p.Encounters)
//      .Load();

//    stopwatch.Stop();

//    Console.WriteLine();

//    Console.WriteLine(
//        $"Encounters Loaded : {patient.Encounters.Count}");

//    Console.WriteLine();

//    Console.WriteLine("PERFORMANCE SUMMARY");
//    Console.WriteLine("----------------------------");

//    Console.WriteLine("Patient Query     : 1");
//    Console.WriteLine("Encounter Query   : 1");
//    Console.WriteLine("Claim Queries     : 0");

//    Console.WriteLine();

//    Console.WriteLine("Total Queries     : 2");

//    Console.WriteLine();

//    Console.WriteLine(
//        $"Elapsed Time      : {stopwatch.ElapsedMilliseconds} ms");

//    Console.WriteLine();

//    Console.WriteLine("Check SQL Profiler");

//    Console.WriteLine(
//        "You should see only 2 SQL queries.");

//    Console.WriteLine(
//        "Patient Query + Encounter Query");
//}


//using CareBridge.PerformanceLab.Models;
//using Microsoft.EntityFrameworkCore;
//using System.Diagnostics;
//while (true)
//{
//    Console.WriteLine("enter 0 if you want to continue..");
//    int f = Convert.ToInt32(Console.ReadLine());
//    if (f == 0)
//        ShowData();
//    else
//       break;
//}

//static void ShowData()
//{
//    Stopwatch stopwatch = Stopwatch.StartNew();
//    int queryCount = 0;

//    using var db = new CareBridgeContext();


//    var summary = db.Claims
//        .AsNoTracking()
//        .GroupBy(c => c.Status)
//        .Select(g => new
//        {
//            Status = g.Key,
//            ClaimCount = g.Count(),
//            TotalBilled = g.Sum(x => x.BilledAmount),
//            TotalReimbursed = g.Sum(x => x.ReimbursedAmt),
//            Gap = g.Sum(x => x.BilledAmount - x.ReimbursedAmt)
//        })
//        .OrderByDescending(x => x.TotalBilled)
//        .ToList();

//    queryCount++;

//    var revenueAtRisk = db.Claims
//        .AsNoTracking()
//        .Where(c => c.Status != "Paid")
//        .Sum(c => c.BilledAmount);


//    queryCount++;
//    Console.WriteLine("---- Claim Summary ----");
//    foreach (var item in summary)
//    {
//        Console.WriteLine($"Status: {item.Status}");
//        Console.WriteLine($"  Count: {item.ClaimCount}");
//        Console.WriteLine($"  Total Billed: {item.TotalBilled}");
//        Console.WriteLine($"  Total Reimbursed: {item.TotalReimbursed}");
//        Console.WriteLine($"  Gap: {item.Gap}");
//        Console.WriteLine();
//    }


//    Console.WriteLine($"Total Revenue At Risk: {revenueAtRisk}");
//    Console.WriteLine();

//    stopwatch.Stop();

//    Console.WriteLine($"Queries Executed: {queryCount}");
//    Console.WriteLine($"Elapsed Time: {stopwatch.ElapsedMilliseconds} ms");
//}

using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CareBridge.PerformanceLab.Models;

class Program
{
    static void Main()
    {
        ShowData();
    }

    static void ShowData()
    {
        Stopwatch st = new Stopwatch();
        int count = 0;

        using var db = new CareBridgeContext();

        // -------------------------------
        // SINGLE QUERY (default include)
        // -------------------------------
        st.Start();

        var res1 = db.Patients
            .Where(p => p.Mrn == "MRN888889")
            .Include(p => p.Encounters)
                .ThenInclude(e => e.Diagnoses)
            .Include(p => p.Encounters)
                .ThenInclude(e => e.Claims)
            .ToList();

        st.Stop();

        int encounters1 = res1.SelectMany(p => p.Encounters).Count();
        int diagnoses1 = res1.SelectMany(p => p.Encounters)
                              .SelectMany(e => e.Diagnoses)
                              .Count();
        int claims1 = res1.SelectMany(p => p.Encounters)
                           .SelectMany(e => e.Claims)
                           .Count();

        Console.WriteLine("SINGLE QUERY (default Include)");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"Encounters : {encounters1}   Diagnoses : {diagnoses1}   Claims : {claims1}");
        Console.WriteLine($"SQL Statements (Profiler)   : 1");
        Console.WriteLine($"Rows returned by SQL        : ~900 (cross-product)");
        Console.WriteLine($"Elapsed Time                : {st.ElapsedMilliseconds} ms");

        // reset
        st.Reset();
        count = 0;

        // -------------------------------
        // SPLIT QUERY (AsSplitQuery)
        // -------------------------------
        st.Start();

        var res2 = db.Patients
            .Where(p => p.Mrn == "MRN888889")
            .Include(p => p.Encounters)
                .ThenInclude(e => e.Diagnoses)
            .Include(p => p.Encounters)
                .ThenInclude(e => e.Claims)
            .AsSplitQuery()
            .ToList();

        st.Stop();

        int encounters2 = res2.SelectMany(p => p.Encounters).Count();
        int diagnoses2 = res2.SelectMany(p => p.Encounters)
                              .SelectMany(e => e.Diagnoses)
                              .Count();
        int claims2 = res2.SelectMany(p => p.Encounters)
                           .SelectMany(e => e.Claims)
                           .Count();

        Console.WriteLine("\nSPLIT QUERY (AsSplitQuery)");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"Encounters : {encounters2}   Diagnoses : {diagnoses2}   Claims : {claims2}");
        Console.WriteLine($"SQL Statements (Profiler)   : 3");
        Console.WriteLine($"Max rows in any statement   : ~300 (no explosion)");
        Console.WriteLine($"Elapsed Time                : {st.ElapsedMilliseconds} ms");

        // -------------------------------
        // VALIDATION (TRUE COUNTS via LINQ)
        // -------------------------------
        var pid = db.Patients
            .Where(p => p.Mrn == "MRN888889")
            .Select(p => p.PatientId)
            .FirstOrDefault();

        var trueDiagnoses = db.Diagnoses
            .Count(d => d.Encounter.PatientId == pid);

        var trueClaims = db.Claims
            .Count(c => c.Encounter.PatientId == pid);

        bool isMatch =
            (diagnoses1 == trueDiagnoses && claims1 == trueClaims) &&
            (diagnoses2 == trueDiagnoses && claims2 == trueClaims);

        Console.WriteLine("\nObject counts identical : " + (isMatch ? "TRUE" : "FALSE"));
    }
}
