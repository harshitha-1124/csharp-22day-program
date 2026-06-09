//// Program.cs
//using CareBridge.EFCoreDemo.Data;

//// Create a DbContext object.
//// DbContext represents a session with the database.
//// It manages connections, queries, tracking, and saving changes.
//using var db = new CareBridgeContext();

//// Count() is a LINQ aggregate method.
//// EF Core translates this C# code into SQL and executes it on SQL Server.
//// Roughly equivalent SQL:
//// SELECT COUNT(*) FROM dbo.Patient
//int patientCount = db.Patients.Count();

//// Display the result on the console.
//Console.WriteLine($"CareBridgeDB has {patientCount} patients.");


//using CareBridge.EFCoreDemo.Models.Generated;

//using var db = new CareBridgeScaffoldContext();

//var newPatient = new Patient
//{
//    Mrn = "MRN900001",
//    FullName = "Ananya Sharma",
//    DateOfBirth = new DateOnly(1990, 5, 14),
//    Gender = "F",
//    City = "Hyderabad",
//    IsActive = true
//};

//db.Patients.Add(newPatient); // here patients means,
//                             // the reference which is used in dbcontext code

////DbSet<Patient> Patients, here Patient is actual table name, but Patients is used
//// in dbContext code
//db.SaveChanges();

//Console.WriteLine($"Inserted patient with new Id = {newPatient.PatientId}");

//using CareBridge.EFCoreDemo.Models.Generated;   // Generated Patient, Provider, Encounter classes

//// Create a DbContext object.
//// DbContext represents a session with the database.
//// Through this object we can query and modify database tables.
//using var db = new CareBridgeScaffoldContext();

//// ---------------------------
//// READ ONE PATIENT
//// ---------------------------

//// Find() searches using the primary key (PatientId).
//// EF Core generates SQL similar to:
//// SELECT * FROM Patient WHERE PatientId = 1

//var p = db.Patients.Find(1);

//if (p != null)
//{
//    Console.WriteLine($"{p.PatientId}: {p.FullName} ({p.City})");
//}
//else
//{
//    Console.WriteLine("Patient not found.");
//}

//// ---------------------------
//// READ MULTIPLE PATIENTS
//// ---------------------------

//// Take(5) limits the result to the first 5 rows.
//// Similar SQL:
//// SELECT TOP 5 * FROM Patient

//foreach (var patient in db.Patients.Take(5))
//{
//    Console.WriteLine($"{patient.PatientId} - {patient.FullName}");
//}


//using CareBridge.EFCoreDemo.Models.Generated;   // Generated Patient, Provider, Encounter classes

//// Create a DbContext object.
//// DbContext represents a session with the database.
//// EF Core uses this object to query and update data.
//using var db = new CareBridgeScaffoldContext();

//// ---------------------------
//// UPDATE A PATIENT
//// ---------------------------

//// Step 1: Load a patient from the database.
//// Find() searches using the primary key (PatientId).
////
//// EF Core generates SQL similar to:
//// SELECT * FROM Patient WHERE PatientId = 1
//var patient = db.Patients.Find(1);

//// Always check whether a record was found.
//if (patient != null)
//{
//    // Step 2: Modify a property in memory.
//    // This is just a normal C# property assignment.
//    // EF Core is tracking this object and notices the change.
//    patient.City = "Bengaluru";

//    // Step 3: Save the changes.
//    // EF Core compares the current object with the original values
//    // it loaded from the database.
//    //
//    // EF Core generates SQL similar to:
//    // UPDATE Patient
//    // SET City = 'Bengaluru'
//    // WHERE PatientId = 1
//    db.SaveChanges();

//    Console.WriteLine($"Patient 1 city is now {patient.City}");
//}
//else
//{
//    Console.WriteLine("Patient not found.");
//}

using CareBridge.EFCoreDemo.Models.Generated;

// Create a DbContext object.
// DbContext represents a session/connection with the database.
// Through this object we can query and modify data in CareBridgeDB.
using var db = new CareBridgeScaffoldContext();

while (true)
{
    Console.Clear();

    Console.WriteLine("=================================");
    Console.WriteLine("       LINQ QUERY DEMO");
    Console.WriteLine("=================================");
    Console.WriteLine("1. Get All Patients");
    Console.WriteLine("2. Active Patients");
    Console.WriteLine("3. Patients From Pune");
    Console.WriteLine("4. Recent Encounters");
    Console.WriteLine("5. Denied Claims");
    Console.WriteLine("6. Join Patient + Encounter");
    Console.WriteLine("7. Encounters By Department");
    Console.WriteLine("8. Exit");
    Console.WriteLine();

    Console.Write("Choose an option (1-8): ");
    string? choice = Console.ReadLine();

    Console.WriteLine();

    switch (choice)
    {
        case "1":

            Console.WriteLine("===== QUERY 1 - ALL PATIENTS =====");

            // db.Patients represents the Patient table.
            // ToList() executes the query and fetches all rows.
            //
            // SQL Equivalent:
            // SELECT * FROM Patient

            var all = db.Patients.ToList();

            Console.WriteLine($"Total patients: {all.Count}");

            break;

        case "2":

            Console.WriteLine("===== QUERY 2 - ACTIVE PATIENTS =====");

            // Where() is the LINQ equivalent of SQL WHERE.
            //
            // p => p.IsActive means:
            // "For each patient p, keep only rows
            // where IsActive is true."
            //
            // SQL Equivalent:
            // SELECT * FROM Patient
            // WHERE IsActive = 1

            var active = db.Patients
                           .Where(p => p.IsActive)
                           .ToList();

            Console.WriteLine($"Active patients: {active.Count}");

            break;

        case "3":

            Console.WriteLine("===== QUERY 3 - PATIENTS FROM PUNE =====");

            // Step 1: Filter only Pune patients.
            // Step 2: Sort by FullName (A-Z).
            // Step 3: Execute the query.
            //
            // SQL Equivalent:
            // SELECT *
            // FROM Patient
            // WHERE City = 'Pune'
            // ORDER BY FullName

            var punePatients = db.Patients
                                 .Where(p => p.City == "Pune")
                                 .OrderByDescending(p => p.FullName)
                                 .ToList();

            foreach (var patient in punePatients)
            {
                Console.WriteLine(patient.FullName);
            }

            Console.WriteLine($"\nTotal patients from Pune: {punePatients.Count}");

            break;

        case "4":

            Console.WriteLine("===== QUERY 4 - RECENT ENCOUNTERS =====");

            // Create a date representing 30 days ago.
            var cutoff = DateTime.Now.AddDays(-30);

            // Keep only encounters whose AdmitDate
            // is within the last 30 days.
            //
            // OrderByDescending() sorts newest first.
            //
            // SQL Equivalent:
            // SELECT *
            // FROM Encounter
            // WHERE AdmitDate >= cutoff
            // ORDER BY AdmitDate DESC

            var recent = db.Encounters
                           .Where(e => e.AdmitDate >= cutoff)
                           .OrderByDescending(e => e.AdmitDate)
                           .ToList();

            Console.WriteLine($"Encounters in last 30 days: {recent.Count}");

            break;

        case "5":

            Console.WriteLine("===== QUERY 5 - DENIED CLAIMS =====");

            // Filter only claims whose status is Denied.
            //
            // SQL Equivalent:
            // SELECT *
            // FROM Claim
            // WHERE Status = 'Denied'

            var denied = db.Claims
                           .Where(c => c.Status == "Denied")
                           .ToList();

            // Sum() adds all BilledAmount values together.
            //
            // SQL Equivalent:
            // SELECT SUM(BilledAmount)
            // FROM Claim
            // WHERE Status = 'Denied'

            decimal lost = denied.Sum(c => c.BilledAmount);

            Console.WriteLine(
                $"Denied claims: {denied.Count}, billed total: {lost:C}");

            break;

        case "6":

            Console.WriteLine("===== QUERY 6 - JOIN PATIENT + ENCOUNTER =====");

            // JOIN combines rows from multiple tables.
            //
            // Here we join:
            // Encounter table
            // +
            // Patient table
            //
            // Matching condition:
            // Encounter.PatientId = Patient.PatientId
            //
            // SQL Equivalent:
            //
            // SELECT p.FullName,
            //        e.EncounterType,
            //        e.AdmitDate
            // FROM Encounter e
            // JOIN Patient p
            //      ON e.PatientId = p.PatientId

            var joined =
            (
                from e in db.Encounters

                join pat in db.Patients
                    on e.PatientId equals pat.PatientId

                // Create a smaller result object.
                // Similar to choosing specific columns in SQL.
                select new
                {
                    pat.FullName,
                    e.EncounterType,
                    e.AdmitDate
                }
            )
            .Take(10)
            .OrderByDescending(e => e.AdmitDate) //imilar to SQL TOP 10
            .ToList();

            foreach (var row in joined)
            {
                Console.WriteLine(
                    $"{row.FullName} - {row.EncounterType} on {row.AdmitDate:d}");
            }

            break;

        case "7":

            Console.WriteLine("===== QUERY 7 - ENCOUNTERS BY DEPARTMENT =====");

            // GroupBy() is the LINQ equivalent of SQL GROUP BY.
            //
            // SQL Equivalent:
            //
            // SELECT DepartmentId,
            //        COUNT(*)
            // FROM Encounter
            // GROUP BY DepartmentId

            var byDept = db.Encounters

                           // Create one group per department
                           .GroupBy(e => e.DepartmentId)

                           // Create a result object for each group
                           .Select(g => new
                           {
                               DepartmentId = g.Key,

                               // Count rows in this group
                               Count = g.Count()
                           })

                           // Highest count first
                           .OrderByDescending(x => x.Count)

                           .ToList();

            foreach (var row in byDept)
            {
                Console.WriteLine(
                    $"Department {row.DepartmentId}: {row.Count} encounters");
            }

            break;

        case "8":

            Console.WriteLine("Goodbye!");
            return;

        default:

            Console.WriteLine("Please enter a number from 1 to 8.");
            break;
    }

    Console.WriteLine();
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}


//e = one Encounter
//g = one Group of Encounters
//x = one Result Row


