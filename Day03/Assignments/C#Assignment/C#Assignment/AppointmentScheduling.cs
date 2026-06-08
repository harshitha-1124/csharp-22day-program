using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace C_Assignment
{
    internal class AppointmentScheduling
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("       APPOINTMENT BOOKING SYSTEM");
            Console.WriteLine("--------------------------------------------------");

            Console.Write("Enter Patient Name: ");
           // string name = Console.ReadLine();

            // ✅ Separate Department Dictionaries

            Dictionary<string, List<string>> generalDoctors =
                new Dictionary<string, List<string>>()
            {
                { "Dr. A. Kumar", new List<string> { "10:00 AM", "11:00 AM", "12:00 PM" } },
                { "Dr. B. Singh", new List<string> { "10:00 AM", "11:00 AM", "12:00 PM" } }
            };

            Dictionary<string, List<string>> dentalDoctors =
                new Dictionary<string, List<string>>()
            {
                { "Dr. C. Roy", new List<string> { "10:00 AM", "11:00 AM" } },
                { "Dr. D. Gupta", new List<string> { "11:00 AM", "12:00 PM" } }
            };

            Dictionary<string, List<string>> orthoDoctors =
                new Dictionary<string, List<string>>()
            {
                { "Dr. E. Mehta", new List<string> { "10:00 AM", "12:00 PM" } },
                { "Dr. F. Reddy", new List<string> { "11:00 AM", "01:00 PM" } }
            };
            int f = 0;

            while (true)
            {
                string name = Console.ReadLine();
                Console.WriteLine("\nSelect Department:");
                Console.WriteLine("1. General Medicine");
                Console.WriteLine("2. Dental");
                Console.WriteLine("3. Orthopedics");

                int choice;

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }

                switch (choice)
                {
                    // ✅ CASE 1: GENERAL
                    case 1:
                        {
                            string deptName = "General Medicine";
                            var selectedDept = generalDoctors;

                            Console.WriteLine("\nSelect Doctor:");
                            List<string> doctorList = new List<string>(selectedDept.Keys);

                            for (int i = 0; i < doctorList.Count; i++)
                            {
                                Console.WriteLine((i + 1) + ". " + doctorList[i]);
                            }

                            int docChoice;
                            if (!int.TryParse(Console.ReadLine(), out docChoice) ||
                                docChoice < 1 || docChoice > doctorList.Count)
                            {
                                Console.WriteLine("Invalid doctor");
                                break;
                            }

                            string selectedDoctor = doctorList[docChoice - 1];

                            List<string> slots = selectedDept[selectedDoctor];

                            Console.WriteLine("\nSelect Time Slot:");
                            for (int i = 0; i < slots.Count; i++)
                            {
                                Console.WriteLine((i + 1) + ". " + slots[i]);
                            }

                            int timeChoice;
                            if (!int.TryParse(Console.ReadLine(), out timeChoice) ||
                                timeChoice < 1 || timeChoice > slots.Count)
                            {
                                Console.WriteLine("Invalid time");
                                break;
                            }

                            string selectedTime = slots[timeChoice - 1];

                            slots.Remove(selectedTime);

                            display(name, deptName, selectedDoctor, selectedTime);


                            break;
                        }

                    // ✅ CASE 2: DENTAL
                    case 2:
                        {
                            string deptName = "Dental";
                            var selectedDept = dentalDoctors;

                            Console.WriteLine("\nSelect Doctor:");
                            List<string> doctorList = new List<string>(selectedDept.Keys);

                            for (int i = 0; i < doctorList.Count; i++)
                            {
                                Console.WriteLine((i + 1) + ". " + doctorList[i]);
                            }

                            int docChoice;
                            if (!int.TryParse(Console.ReadLine(), out docChoice) ||
                                docChoice < 1 || docChoice > doctorList.Count)
                            {
                                Console.WriteLine("Invalid doctor");
                                break;
                            }

                            string selectedDoctor = doctorList[docChoice - 1];

                            List<string> slots = selectedDept[selectedDoctor];

                            Console.WriteLine("\nSelect Time Slot:");
                            for (int i = 0; i < slots.Count; i++)
                            {
                                Console.WriteLine((i + 1) + ". " + slots[i]);
                            }

                            int timeChoice;
                            if (!int.TryParse(Console.ReadLine(), out timeChoice) ||
                                timeChoice < 1 || timeChoice > slots.Count)
                            {
                                Console.WriteLine("Invalid time");
                                break;
                            }

                            string selectedTime = slots[timeChoice - 1];

                            slots.Remove(selectedTime);

                            display(name, deptName, selectedDoctor, selectedTime);
                            break;
                        }

                    // ✅ CASE 3: ORTHOPEDICS
                    case 3:
                        {
                            string deptName = "Orthopedics";
                            var selectedDept = orthoDoctors;

                            Console.WriteLine("\nSelect Doctor:");
                            List<string> doctorList = new List<string>(selectedDept.Keys);

                            for (int i = 0; i < doctorList.Count; i++)
                            {
                                Console.WriteLine((i + 1) + ". " + doctorList[i]);
                            }

                            int docChoice;
                            if (!int.TryParse(Console.ReadLine(), out docChoice) ||
                                docChoice < 1 || docChoice > doctorList.Count)
                            {
                                Console.WriteLine("Invalid doctor");
                                break;
                            }

                            string selectedDoctor = doctorList[docChoice - 1];

                            List<string> slots = selectedDept[selectedDoctor];

                            Console.WriteLine("\nSelect Time Slot:");
                            for (int i = 0; i < slots.Count; i++)
                            {
                                Console.WriteLine((i + 1) + ". " + slots[i]);
                            }

                            int timeChoice;
                            if (!int.TryParse(Console.ReadLine(), out timeChoice) ||
                                timeChoice < 1 || timeChoice > slots.Count)
                            {
                                Console.WriteLine("Invalid time");
                                break;
                            }

                            string selectedTime = slots[timeChoice - 1];

                            slots.Remove(selectedTime);

                            display(name, deptName, selectedDoctor, selectedTime);
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("Invalid format");
                            f = 1;
                        }
                        break;
                }

                if (f == 1)
                    break; // exit after one booking
            }
        }

        public  static void display(string name, string deptName, string selectedDoctor, string selectedTime)
        {

        Console.WriteLine("\n[Booking Confirmed]\n");

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("            APPOINTMENT TICKET");
Console.WriteLine("--------------------------------------------------");

Console.WriteLine("Patient:    " + name);
Console.WriteLine("Department: " + deptName);
Console.WriteLine("Doctor:     " + selectedDoctor);
Console.WriteLine("Time:       " + selectedTime);
Console.WriteLine("Status:     Confirmed");

Console.WriteLine("\nPlease arrive 15 mins before your slot.");
Console.WriteLine("--------------------------------------------------");
        
    }

    }
}