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
            for(int i = 0; i < vehicles.Count; i++)
            {
                if (vehicles[i].ParkedInNr.Count == 0) 
                {
                    double remaining = vehicles[i].ParkingSpotsUsed;//ex remaining = 2 för en buss
                    double available = 0; //Kollar hur många lediga platser det finns kvar

                    foreach(double parkingSpace in ParkingSpaces.Values) //Kontroll om plats finns, INNAN ändring sker. För varje värde i dict, lägg till ledig plats(1) i available 
                    {
                        available += parkingSpace; 
                    }
                    if (available < remaining) 
                    {
                        Console.WriteLine("Det fanns tyvärr inte tillräckligt med plats för ditt fordon i phuset. Välkommen åter!");
                        continue; 
                    } //Om antalet lediga platser inte rymmer fordonet, fortsätt for-loopen.

                    for (int j = 1; j <= ParkingSpaces.Count && remaining > 0; j++)
                    {
                        if (ParkingSpaces[j] == 0) continue;//Om patsen är upptagen(0).
                        if (vehicles[i] is Vehicle.Bus && ParkingSpaces[j] < 1) continue;

                        if (ParkingSpaces[j] >= remaining)//Om fordonet får plats i den rutan
                        {
                            ParkingSpaces[j] -= remaining;
                            vehicles[i].ParkedInNr.Add(j);//Sparar vilket nummer vehicle parkerat på.
                            remaining = 0;
                        }
                        else //Om fordonet behöver mer plats än den som finns tillgänlig på[j]?
                        {
                            remaining -= ParkingSpaces[j];
                            ParkingSpaces[j] = 0; //pplatsen blir upptagen och i nästa loop fylls nästa plats med remaining om det kvarstår.
                            vehicles[i].ParkedInNr.Add(j);
                        }
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
