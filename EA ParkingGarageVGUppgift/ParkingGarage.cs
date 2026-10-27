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

        public void ParkVehicle(List<Vehicle> vehicles)
        {
            for (int i = 0; i < vehicles.Count; i++)
            {
                if (vehicles[i].ParkedInNr.Count > 0) continue;

                double remaining = vehicles[i].ParkingSpotsUsed;
                double available = 0;

                foreach (double parkingSpace in ParkingSpaces.Values)
                {
                    available += parkingSpace;
                }

                if (available < remaining)
                {
                    Console.WriteLine("Det fanns tyvärr inte tillräckligt med plats i p-huset. Välkommen åter!");
                    continue;
                }

                if (vehicles[i] is Vehicle.Bus)
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
                        continue;
                    }
                }

                for (int j = 1; j <= ParkingSpaces.Count && remaining > 0; j++)
                {
                    if (ParkingSpaces[j] == 0)
                        continue;

                    if (vehicles[i] is Vehicle.Bus && ParkingSpaces[j] < 1)
                        continue;

                    if (ParkingSpaces[j] >= remaining)
                    {
                        ParkingSpaces[j] -= remaining;
                        vehicles[i].ParkedInNr.Add(j);
                        remaining = 0;
                    }
                    else
                    {
                        remaining -= ParkingSpaces[j];
                        ParkingSpaces[j] = 0;
                        vehicles[i].ParkedInNr.Add(j);
                    }
                }
            }
        }

            
        public static void PrintParkedInfo(List<Vehicle> vehicles)
        {
            for (int i = 0; i < vehicles.Count; i++)
            {
                if (vehicles[i].ParkedInNr.Count > 0) //Om fordonet inte är parkerat, men ej borttagen från List 
                {
                    Console.Write("Plats. ");
                    foreach (int pNr in vehicles[i].ParkedInNr)
                    {
                        Console.Write($"{pNr}");
                    }
                    Console.Write($": {vehicles[i].GetType().Name} - {vehicles[i].RegPlate} - {vehicles[i].VehicleColor} - ");

                    if (vehicles[i] is Vehicle.Car car)
                    {
                        Console.Write(car.IsElectricCar ? "electric car." : "not an electric car.");
                    }
                    if (vehicles[i] is Vehicle.Bus bus)
                    {
                        Console.Write($"{bus.NumberOfPassengers}.");
                    }
                    if (vehicles[i] is Vehicle.Motorcycle motorcycle) 
                    {
                        Console.Write($"{motorcycle.MotorcycleBrand}.");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
