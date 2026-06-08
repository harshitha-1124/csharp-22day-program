using System;

namespace C_Assignment
{
    internal class VitalSignsMonitor
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("       VITAL SIGNS MONITOR");
            Console.WriteLine("--------------------------------------------------");

            // ✅ Patient Name
            Console.Write("Enter Patient Name: ");
            string name = Console.ReadLine();

            double temp = 0;
            int oxygen = 0;
            int pulse = 0;

            // ✅ Temperature input
            while (true)
            {
                try
                {
                    Console.Write("Enter Temperature (C): ");
                    temp = Convert.ToDouble(Console.ReadLine());

                    if (temp > 0 && temp < 50)
                        break;
                    else
                        Console.WriteLine("Invalid temperature range.");
                }
                catch
                {
                    Console.WriteLine("Invalid input. Enter numeric value.");
                }
            }

            // ✅ Oxygen input
            while (true)
            {
                try
                {
                    Console.Write("Enter Oxygen Level (%): ");
                    oxygen = Convert.ToInt32(Console.ReadLine());

                    if (oxygen >= 0 && oxygen <= 100)
                        break;
                    else
                        Console.WriteLine("Oxygen must be between 0–100.");
                }
                catch
                {
                    Console.WriteLine("Invalid input.");
                }
            }

            // ✅ Pulse input
            while (true)
            {
                try
                {
                    Console.Write("Enter Pulse Rate (BPM): ");
                    pulse = Convert.ToInt32(Console.ReadLine());

                    if (pulse > 0 && pulse < 200)
                        break;
                    else
                        Console.WriteLine("Invalid pulse range.");
                }
                catch
                {
                    Console.WriteLine("Invalid input.");
                }
            }

            Console.WriteLine("\n[Analyzing Data...]\n");

            
            string status = CheckStatus(temp, oxygen, pulse);

       
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("       MEDICAL ASSESSMENT REPORT");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("Patient: " + name + "\n");

            Console.WriteLine("Vitals Recorded:");
            Console.WriteLine("- Temp:   " + temp + " C");
            Console.WriteLine("- Oxygen: " + oxygen + " %");
            Console.WriteLine("- Pulse:  " + pulse + " BPM\n");

            Console.WriteLine("Status Assessment: " + status);

           
            Console.WriteLine("--------------------------------------------------");
        }

    
        public static string CheckStatus(double temp, int oxygen, int pulse)
        {
         
            if (temp > 39.0 || oxygen < 90 || pulse < 50 || pulse > 120)
            {
                return "CRITICAL";
            }
            
            else if (temp > 37.5 || oxygen < 95 || pulse > 100)
            {
                return "OBSERVATION NEEDED";
            }
            
            else
            {
                return "NORMAL";
            }
        }
    }
}