//using CareBridge.EFCoreDemo.Models.Generated;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//// Register EF Core DbContext.
//// ASP.NET Core will automatically create and inject it when needed.
//builder.Services.AddDbContext<CareBridgeScaffoldContext>();

//// Add Swagger support.
//// Swagger gives us a testing screen for APIs.
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// Allow Vue.js running on another port
//// to call this API from the browser.
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyHeader()
//              .AllowAnyMethod();
//    });
//});

//var app = builder.Build();

//// Enable Swagger.
//app.UseSwagger();
//app.UseSwaggerUI();

//// Enable CORS.
//app.UseCors();

//// Simple health-check endpoint.
//app.MapGet("/", () =>
//{
//    return "CareBridge API is running";
//});

//// Return first 20 patients.
//// EF Core converts this LINQ query into SQL.
//app.MapGet("/api/patients",
//    (CareBridgeScaffoldContext db) =>
//{
//    return db.Patients

//             // Select only columns we need.
//             .Select(p => new
//             {
//                 p.PatientId,
//                 p.FullName,
//                 p.City
//             })

//             // Return only first 20 rows.
//             .Take(20)

//             // Execute query.
//             .ToList();
//});



//app.MapGet("/api/activePatients",
//    (CareBridgeScaffoldContext db) =>
//{
//    return db.Patients
//                 .Where(p => p.IsActive = true)

//             // Select only columns we need.
//             .Select(p => new
//             {
//                 p.PatientId,
//                 p.FullName,
//                 p.City
//             })



//             // Execute query.
//             .ToList();
//});


//app.MapGet("/api/city/{"cityname"},
//    (CareBridgeScaffoldContext db) =>
//{
//    return db.Patients
//                 .Where(p => p.City == cityname)

//             // Select only columns we need.
//             .Select(p => new
//             {
//                 p.PatientId,
//                 p.FullName,
//                 p.City
//             })



//             // Execute query.
//             .ToList();
//});


//app.MapGet("/api/cityAndActive/{"cityname"}{"active"},
//    (CareBridgeScaffoldContext db) =>
//{
//    return db.Patients
//                 .Where(p => p.City == cityname && p.IsActive == active)

//             // Select only columns we need.
//             .Select(p => new
//             {
//                 p.PatientId,
//                 p.FullName,
//                 p.City
//             })



//             // Execute query.
//             .ToList();
//});

//app.Run();

using CareBridge.EFCoreDemo.Models.Generated;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register EF Core DbContext
builder.Services.AddDbContext<CareBridgeScaffoldContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();

app.MapGet("/", () => "CareBridge API is running");


//app.MapGet("/api/patients", (CareBridgeScaffoldContext db) =>
//{
//    return db.Patients
//             .Select(p => new
//             {
//                 p.PatientId,
//                 p.FullName,
//                 p.City
//             })
//             .Take(20)
//             .ToList();
//});

//app.MapGet("/api/activePatients", (CareBridgeScaffoldContext db) =>
//{
//    return db.Patients
//             .Where(p => p.IsActive == true)
//             .Select(p => new
//             {
//                 p.PatientId,
//                 p.FullName,
//                 p.City
//             })
//             .ToList();
//});


//app.MapGet("/api/city",
//    (CareBridgeScaffoldContext db) =>
//    {
//        return db.Patients
//                 .Where(p => p.City == "Pune")
//                 .Select(p => new
//                 {
//                     p.PatientId,
//                     p.FullName,
//                     p.City
//                 })
//                 .ToList();
//    });


//app.MapGet("/api/cityAndActive/{cityname}/{active}",
//    (string cityname, bool active, CareBridgeScaffoldContext db) =>
//    {
//        return db.Patients
//                 .Where(p => p.City == cityname && p.IsActive == active)
//                 .Select(p => new
//                 {
//                     p.PatientId,
//                     p.FullName,
//                     p.City
//                 })
//                 .ToList();
//    });


//app.MapGet("/api/searchName/{name}",
//    (string name, CareBridgeScaffoldContext db) =>
//    {
//        return db.Patients
//                 .Where(p => p.FullName.Contains(name))
//                 .Select(p => new
//                 {
//                     p.PatientId,
//                     p.FullName,
//                     p.City
//                 })
//                 .ToList();
//    });




app.MapGet("/api/getData", (CareBridgeScaffoldContext db) =>
{
    var data = db.Departments
        .Select(d => new
        {
            DepartmentName = d.Name,
            Inpatient = d.Encounters.Count(e => e.EncounterType == "Inpatient"),
            Outpatient = d.Encounters.Count(e => e.EncounterType == "Outpatient"),
            ED = d.Encounters.Count(e => e.EncounterType == "ED"),
            Total = d.Encounters.Count()
        })
        .Where(x => x.Total > 0)
        .ToList();

    var grandTotal = data.Sum(x => x.Total);   

    return new
    {
        Data = data,
        GrandTotal = grandTotal
    };
});

app.MapGet("/api/getDataByDate/{days}", (CareBridgeScaffoldContext db) =>
{
    var data = db.Departments
        .Select(d => new
        {
            DepartmentName = d.Name,
            Inpatient = d.Encounters.Count(e => e.EncounterType == "Inpatient"),
            Outpatient = d.Encounters.Count(e => e.EncounterType == "Outpatient"),
            ED = d.Encounters.Count(e => e.EncounterType == "ED"),
            Total = d.Encounters.Count()
        })
        .Where(x => x.Total > 0 && currentDate-days<x.AdmitDate)
        
        .ToList();

    var grandTotal = data.Sum(x => x.Total);

    return new
    {
        Data = data,
        GrandTotal = grandTotal
    };
});




app.Run();