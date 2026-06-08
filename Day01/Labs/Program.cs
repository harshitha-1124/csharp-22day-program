using System;
using System.Collections.Generic;
// Measure current managed memory used by the application.
// &#39;true&#39; asks the CLR to perform a collection before measuring.
long before = GC.GetTotalMemory(true);
Console.WriteLine($&quot;Memory Before Allocation: {before / 1024} KB&quot;);
// Create an empty list that will hold patient names.
var patients = new List&lt;string&gt;();
// Create 100,000 patient records.
// The underscore (_) is a digit separator for readability.
for (int i = 0; i &lt; 100_000; i++)

.NET 9 · Visual Studio 2022 · Page 5

{
patients.Add($&quot;Patient-{i}&quot;);
}
// Measure memory again after creating the objects.
long after = GC.GetTotalMemory(true);
Console.WriteLine($&quot;Memory After Allocation: {after / 1024} KB&quot;);
// Calculate approximately how much additional memory was allocated.
Console.WriteLine($&quot;Allocated Approx: {(after - before) / 1024} KB&quot;);
// Remove the reference to the list.
// The objects are NOT deleted here.
// They simply become eligible for garbage collection.
patients = null;
// Request the Garbage Collector to run.
// In real production applications, developers rarely call this directly.
GC.Collect();
// Wait for any pending finalizers to complete.
GC.WaitForPendingFinalizers();
// Run GC again to ensure cleanup has completed.
GC.Collect();
// Measure memory after garbage collection.
long cleaned = GC.GetTotalMemory(true);
Console.WriteLine($&quot;Memory After Cleanup: {cleaned / 1024} KB&quot;);
// Compare current memory usage with the starting point.
Console.WriteLine($&quot;Difference From Start: {(cleaned - before) / 1024} KB&quot;);