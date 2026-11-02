using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace EA_ParkingGarageVGUppgift
{
    internal class ParkingGarage
    {
        public double AmountParkingSpaces { get; } = 15; //Endast get;
        public double TotalParkingSpaces { get; set; } = 15;
        public Dictionary<double, double> ParkingSpaces { get; set; } = //hör endast ihop med ParkVehicle för att hålla koll på räkning?
        new Dictionary<double, double>
        {
            { 1, 1 }, { 2, 1 }, { 3, 1 }, { 4, 1 }, { 5, 1 }, { 6, 1 }, { 7, 1 }, { 8, 1 }, { 9, 1 }, { 10, 1 }, { 11, 1 }, { 12, 1 }, { 13, 1 }, { 14, 1 }, { 15, 1 }

        };

        public bool ParkVehicle(Vehicle vehicle)
        {
            DateTime startParking = DateTime.Now;
            double parkingSpotsNeededForVehicle = vehicle.ParkingSpotsUsed;//Ändra namn på dessa variabler sedan
            double available = 0;

            foreach (double parkingSpace in ParkingSpaces.Values)
            {
                if (vehicle is Vehicle.MC)
                {
                    available += parkingSpace;      // MC kan använda halva rutor
                }
                else if (parkingSpace == 1)
                {
                    available += parkingSpace;      // övriga får bara använda hela rutor
                }
            }

            if (available < parkingSpotsNeededForVehicle)
            {
                Console.WriteLine("Det fanns tyvärr inte tillräckligt med plats i p-huset. Välkommen åter!");
                return false;
            }

            if (vehicle is Vehicle.Bus)
            {
                int fullSpaces = 0;

                foreach (double parkingSpace in ParkingSpaces.Values)
                {
                    if (parkingSpace == 1)
                        fullSpaces++;
                }

                if (fullSpaces < 2)
                {
                    Console.WriteLine("Det fanns inte två hela parkeringsrutor för bussen.");
                    return false;
                }
            }

            for (int j = 1; j <= ParkingSpaces.Count && parkingSpotsNeededForVehicle > 0; j++)
            {
                if (ParkingSpaces[j] == 0)
                    continue;

                if (!(vehicle is Vehicle.MC) && ParkingSpaces[j] < 1)   // bara MC får dela ruta
                    continue;

                if (ParkingSpaces[j] >= parkingSpotsNeededForVehicle)
                {
                    ParkingSpaces[j] -= parkingSpotsNeededForVehicle;
                    parkingSpotsNeededForVehicle = 0;
                }
                else
                {
                    parkingSpotsNeededForVehicle -= ParkingSpaces[j];
                    ParkingSpaces[j] = 0;
                }

                vehicle.ParkedInNr.Add(j);
            }

            vehicle.StartParking = startParking;
            Console.Write($"\nYour {vehicle.GetType().Name} {vehicle.RegPlate} was parked in parking spot number ");
            for (int i = 0; i < vehicle.ParkedInNr.Count; i++)
            {
                Console.Write($"{vehicle.ParkedInNr[i]}");

                if (i < vehicle.ParkedInNr.Count - 1)
                {
                    Console.Write(" and ");   // komma mellan numren, men inte efter det sista
                }
            }
            Console.WriteLine(".");
            return true;
        }

        public void LeaveGarage(Dictionary<string, Vehicle> vehicles)
        {
            Console.Write("Enter the registration number for the vehicle you wish to sign out from the Parking garage(ex. GWH 959): ");//risk för felinamtning

            bool correctInput = false;//felhantering: felinmatning och regnr som inte finns regisrerat

            while (!correctInput)
            {
                string inputRegPlate = Console.ReadLine();


                if (vehicles.TryGetValue(inputRegPlate, out Vehicle vehicle))//Om inmatningen matchar en nyckeln
                {
                    TimeSpan parkedTime = DateTime.Now - vehicle.StartParking; //Räknar ut TimeSpan 
                    double parkedSeconds = parkedTime.TotalSeconds;//.TS egenskap som räknar sek.

                    Console.WriteLine($"\nYour vehicle with the registration number {inputRegPlate} is being signed out of the garage. It has been parked since {vehicle.StartParking} " +
                                      $"and the price is {Math.Round((parkedSeconds * 1) * (vehicle.ParkingSpotsUsed))}(for the {Math.Round(parkedSeconds)} seconds it was parked).");//Math.Round avrundar 2.5 blir 2, 2.51 blir 3(.Floor för att avrunda nedåt).

                    vehicles.Remove(inputRegPlate);

                    correctInput = true;
                }
                else
                {
                    Console.WriteLine("Felaktig inmatning. Försök igen.");
                }
            }
        }

        public void PrintParkedInfo(Dictionary<string, Vehicle> vehicles)
        {
            Console.WriteLine($"Currently parked vehicles: ");
            foreach (var item in vehicles)
            {
                Vehicle vehicle = item.Value;

                Console.Write("Plats ");

                for (int i = 0; i < vehicle.ParkedInNr.Count; i++)
                {
                    Console.Write(vehicle.ParkedInNr[i]);

                    if (i < vehicle.ParkedInNr.Count - 1)
                    {
                        Console.Write(", ");   // komma mellan numren, men inte efter det sista
                    }
                }

                Console.Write(": ");

                Console.Write($"{vehicle.GetType().Name} - {vehicle.RegPlate} - parking started: {vehicle.StartParking} - {vehicle.VehicleColor} - ");

                vehicle.PrintVehicleInfo();

            }
        }
    }
}


       
