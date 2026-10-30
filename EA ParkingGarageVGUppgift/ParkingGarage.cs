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

        public Dictionary<string, Vehicle> ParkVehicle(Dictionary<string, Vehicle> vehicles)
        {
            foreach(var item in vehicles)
            {
                Vehicle vehicle = item.Value;
                double remaining = vehicle.ParkingSpotsUsed;
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
                        continue;
                    }
                }

                for (int j = 1; j <= ParkingSpaces.Count && remaining > 0; j++)
                {
                    if (ParkingSpaces[j] == 0)
                        continue;

                    if (vehicle is Vehicle.Bus && ParkingSpaces[j] < 1)
                        continue;

                    if (ParkingSpaces[j] >= remaining)
                    {
                        ParkingSpaces[j] -= remaining;
                        vehicle.ParkedInNr.Add(j);
                        remaining = 0;
                    }
                    else
                    {
                        remaining -= ParkingSpaces[j];
                        ParkingSpaces[j] = 0;
                        vehicle.ParkedInNr.Add(j);

                    }

                    vehicles.Add(item.Key, item.Value);
                }
            }
            return vehicles;
        }


        public static void PrintParkedInfo(Dictionary<string, Vehicle> vehicles)
        {
            foreach (var item in vehicles)
            {

                Vehicle vehicle = item.Value; //Ger tillgng till hela vehicle(list)
                String regNr = item.Key;
                List<int> parkingNumbers = vehicle.ParkedInNr;
                String Color = vehicle.VehicleColor;
                int count = vehicle.ParkedInNr.Count;

                Console.Write("Plats. ");
                foreach(int pNr in vehicle.ParkedInNr)
                {
                    if (count >= vehicle.ParkedInNr.Count)
                    {
                        Console.Write($"{vehicle.ParkedInNr}");
                        if (count > 1)
                        {
                            Console.Write(", ");
                        }
                        else
                        {
                            Console.Write(": ");
                        }
                        count--;
                    }
                }
   
                Console.Write($"{vehicle.GetType().Name} - {vehicle.RegPlate} - {vehicle.VehicleColor} - ");

                if (vehicle is Vehicle.Car car)
                {
                    Console.Write(car.IsElectricCar ? "electric car." : "not an electric vehicle.");
                }
                if (vehicle is Vehicle.Bus bus)
                {
                    Console.Write($"{bus.NumberOfPassengers}.");
                }
                if (vehicle is Vehicle.Motorcycle motorcycle)
                {
                    Console.Write($"{motorcycle.MotorcycleBrand}.");
                }
                Console.WriteLine();

            }  
        }
    }
}
