using System;
using System.Collections.Generic;
using System.Text;

namespace EA_ParkingGarageVGUppgift
{
    internal class ParkingGarage
    {
        public double AmountParkingSpaces { get; } = 15; //Endast get;
        public double AvailableParkingSpaces { get; set; } = 15;
        public Dictionary<double, double> ParkingSpaces { get; set; } = // 
        new Dictionary<double, double>
        {
            { 1, 1 }, { 2, 1 }, { 3, 1 }, { 4, 1 }, { 5, 1 }, { 6, 1 }, { 7, 1 }, { 8, 1 }, { 9, 1 }, { 10, 1 }, { 11, 1 }, { 12, 1 }, { 13, 1 }, { 14, 1 }, { 15, 1 }

        };

        public void ParkVehicle(List<Vehicle> vehicles)
        {
            for(int i = 0; i < vehicles.Count; i++)
            {
                if (vehicles[i].ParkedInNr == 0 && AvailableParkingSpaces > vehicles[i].ParkingSpotsUsed)//Om fordondet inte är parkerat.
                {
                    double remaining = vehicles[i].ParkingSpotsUsed;//ex remaining = 2 för en buss

                    for(int j = 1; j <= ParkingSpaces.Count && remaining > 0; j++)
                    {
                        if (ParkingSpaces[j] == 0) continue;
                        if (ParkingSpaces[j] >= remaining)//Om fordonet får plats i den rutan
                        {
                            ParkingSpaces[j] -= remaining;
                            vehicles[i].ParkedInNr = j;
                            remaining = 0;
                        }
                        else //Om fordonet behöver mer plats än den som finns tillgänlig på[j]?
                        {
                            remaining -= ParkingSpaces[j];
                            ParkingSpaces[j] = 0; //pplatsen blir upptagen och i nästa loop fylls nästa plats med remaining om det kvarstår.
                            vehicles[i].ParkedInNr = j;
                        }
                    } 
                }
            }
        }
            
        public static void PrintParkedInfo(List<Vehicle> vehicles)
        {
            for (int i = 0; i < vehicles.Count; i++)
            {
                if (vehicles[i].ParkedInNr != 0) //Behöver jag denna? det ska vara om jag väljer att inte ta bort de från vehicles efter att de checkat ut från park.
                {
                    Console.WriteLine($"Plats {vehicles[i].ParkedInNr}  {vehicles[i].GetType().Name}    {vehicles[i].RegPlate} {vehicles[i].VehicleColor}   ");
                    if (vehicles[i] is Vehicle.Car car)
                    {
                        Console.Write($"{car.IsElectricCar}");
                    }
                    if (vehicles[i] is Vehicle.Bus bus)
                    {
                        Console.Write($"{bus.NumberOfPassengers}");
                    }
                    if (vehicles[i] is Vehicle.Motorcycle motorcycle)
                    {
                        Console.Write($"{motorcycle.MotorcycleBrand}");
                    }
                }
            }
        }
    }
}
