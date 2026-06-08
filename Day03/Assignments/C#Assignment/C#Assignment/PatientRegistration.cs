using System;
using System.Text.RegularExpressions;

namespace C_Assignment
{
   

    internal class PatientRegistration
    {
        public static void Main(string[] args)
        {
            Patient patient = new Patient();

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("       HOSPITAL PATIENT REGISTRATION SYSTEM");
            Console.WriteLine("--------------------------------------------------\n");

         
            while (true)
            {
                Console.Write("Enter Patient Name: ");
                string name = Console.ReadLine();

                try
                {
                    if (!string.IsNullOrWhiteSpace(name) &&
                        Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
                    {
                        patient.Name = name;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid name.");
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid format");
                }
            }

            // ✅ AGE (Try-Catch)
            while (true)
            {
                try
                {
                    Console.Write("Enter Age: ");
                    short age = Convert.ToInt16(Console.ReadLine());

                    if (age > 0 && age < 120)
                    {
                        patient.Age = age;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Age must be between 1 and 119.");
                    }
                }
                catch
                {
                    Console.WriteLine("Error: Please enter a valid numeric age.");
                }
            }



            while (true)
            {
                Console.Write("Enter Gender (M/F/O): ");
                string input = Console.ReadLine().ToUpper();

                if (input.Length == 1 &&
                   (input[0] == 'M' || input[0] == 'F' || input[0] == 'O'))
                {
                    patient.Gender = input[0]; 
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Enter M, F, or O only.");
                }
            }

                while (true)
            {
                Console.Write("Enter Phone Number: ");
                string phone = Console.ReadLine();

                try
                {
                    if (Regex.IsMatch(phone, @"^\d{10}$"))
                    {
                        patient.PhoneNumber = phone;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Phone number must be exactly 10 digits.");
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid format");
                }
            }

            
            Console.Write("Enter City: ");
            patient.City = Console.ReadLine();

           
            patient.PatientID = "PAT-" + DateTime.Now.Year + "-001";

            Console.WriteLine("\n[Registration Complete]\n");

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("            PATIENT REGISTRATION SLIP");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("Date: " + DateTime.Now.ToShortDateString() + "\n");

            Console.WriteLine("Patient ID: " + patient.PatientID);
            Console.WriteLine("Name:       " + patient.Name);
            Console.WriteLine("Age:        " + patient.Age + " years");
            Console.WriteLine("Gender:     " + patient.Gender);
            Console.WriteLine("Contact:    " + patient.PhoneNumber);
            Console.WriteLine("Location:   " + patient.City);

            Console.WriteLine("\nInstructions:");
            Console.WriteLine("Please proceed to the waiting area.");
            Console.WriteLine("--------------------------------------------------");
        }
    }
}