using System;
using System.Collections.Generic;
using System.Text;

namespace EA_ParkingGarageVGUppgift
{
    internal class Helpers
    {
        private static List<string> regPlates = new List<string>                                        //Begränsa åtkomst så att andra klasser inte kan ändra listan. 
        {
            "BMX 939", "GWH 959", "KLS 331", "MEF 031", "HAM 010", "KOD 666", "FBW 112", "BOB 910",     //Feedback Micke: Skulle kunna ha randomiserat listan helt.
            "KOP 446", "TAL 764", "BUB 990", "HEJ 123", "PAL 458", "GIF 003", "JJB 007", "BAP 559",
            "BSX 939", "GWH 969", "KLT 331", "PEF 031", "HAL 010", "GOD 666", "FCW 112", "BOB 990",
            "SOP 446", "RAL 764", "BEB 990", "HAJ 123", "PAL 558", "GIT 003", "JBL 007", "BRA 559"
        };
        internal static string GetRegPlate()                                                           
        {
            if (regPlates.Count == 0)
            {
                return "Slut på regnummer(Helpers).";                                                   //TryGetRegPlate(out string regPlate med en bool-metod skulle vara en säker felhantering för slut på regnr. Metoden returnerar false om ingen finns.
            }

            int rnd = Random.Shared.Next(regPlates.Count);                                              
            string regPlate = regPlates[rnd];
            regPlates.RemoveAt(rnd);                                                                   
            return regPlate;
        }

        internal static string GetVehicleColor()                                                       
        {
            List<string> vehicleColor = new List<string>
            {   "red", "blue", "black", "magenta", "purple", "gold with unicorns", "white", "pink", "silver",
                "beige", "rainbow coloured", "grey", "dark green", "baby blue", "yellow", "wine red", "brown"};

            return vehicleColor[Random.Shared.Next(0, vehicleColor.Count)];
        }

        internal static void DrawScreen()
        {
            double price = 1;

            Console.Clear();
            Console.WriteLine($"------------------------------------------------------------\n" +
                              $"Welcome to the Parking Garage!\t" +
                              $"Price: {price} SEK/parking space/second.\n" +
                              $"\nEnter \"C\"(car), \"M\"(MC) or \"B\"(bus) for parking.\n" +
                              $"Enter \"E\" to exit or \"S\" to show all of the parking spaces.\n" +
                              "------------------------------------------------------------");
        }

        internal static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);  
        }
    }
}
