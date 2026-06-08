using System;
using System.Collections.Generic;

namespace C_Assignment
{
    internal class BillingModule
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("       HOSPITAL BILLING CALCULATOR");
            Console.WriteLine("--------------------------------------------------");

            Bill bill = new Bill();

          
            Console.Write("Patient Name: ");
            bill.PatientName = Console.ReadLine();

            
            while (true)
            {
                Console.Write("Patient Age: ");
                if (int.TryParse(Console.ReadLine(), out int age))
                {
                    bill.Age = age;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid age.");
                }
            }

            List<string> services = new List<string>
            {
                "Consultation",
                "Blood Test",
                "X-Ray",
                "Admission"
            };

            bool consultationAdded = false;
            while (true)
            {
                Console.WriteLine("\nAvailable Services:");

                for (int i = 0; i < services.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + services[i]);
                }

                Console.WriteLine((services.Count + 1) + ". Done");

                Console.Write("Choice: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }

                if (choice == services.Count + 1)
                {
                    break;
                }

                if (choice < 1 || choice > services.Count)
                {
                    Console.WriteLine("Invalid choice");
                    continue;
                }

                string selectedService = services[choice - 1];

                
                switch (selectedService)
                {
                    case "Consultation":
                        bill.TotalAmount += Bill.CONSULTATION;
                        consultationAdded = true;
                        break;

                    case "Blood Test":
                        bill.TotalAmount += Bill.BLOOD_TEST;
                        break;

                    case "X-Ray":
                        bill.TotalAmount += Bill.XRAY;
                        break;

                    case "Admission":
                        bill.TotalAmount += Bill.ADMISSION;
                        break;
                }

                Console.WriteLine("[Added " + selectedService + "]");

                
                services.Remove(selectedService);
            }

            Console.WriteLine("\n[Calculating Bill...]\n");

            
            if (bill.Age > 60)
            {
                bill.DiscountAmount = bill.TotalAmount * 0.20m;
            }
            else if (bill.Age < 10 && consultationAdded)
            {
                bill.DiscountAmount = Bill.CONSULTATION * 0.50m;
            }
            else
            {
                bill.DiscountAmount = 0;
            }

            decimal amountAfterDiscount = bill.TotalAmount - bill.DiscountAmount;
            bill.TaxAmount = amountAfterDiscount * 0.05m;

            decimal finalAmount = amountAfterDiscount + bill.TaxAmount;

          
            string category = (bill.Age > 60) ? "Senior Citizen"
                               : (bill.Age < 10) ? "Child"
                               : "Regular";

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("            FINAL BILL INVOICE");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine($"Patient: {bill.PatientName} ({category})\n");

            Console.WriteLine($"Base Amount:      {bill.TotalAmount:F2}");
            Console.WriteLine($"Discount:        -{bill.DiscountAmount:F2}");
            Console.WriteLine($"Tax (5%):        +{bill.TaxAmount:F2}");

            Console.WriteLine("\n--------------------------------------------------");
            Console.WriteLine($"TOTAL PAYABLE:    {finalAmount:F2}");
            Console.WriteLine("--------------------------------------------------");
        }
    }
}