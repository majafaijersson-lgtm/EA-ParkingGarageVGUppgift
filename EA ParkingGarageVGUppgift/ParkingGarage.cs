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
            { 1, 1 }, { 2, 1 }, { 3, 1 }, { 4, 1 }, { 5, 1 }, { 6, 1 }, { 7, 1 }, { 8, 1 }, { 9, 1 }, { 10, 1 }, { 11, 1 }, { 12, 1 }, { 13, 1 }, { 14, 1 }, { 15, 1 }/*, { 16, 1 },*/
            //{ 17, 1 }, { 18, 1 }, { 19, 1 }, { 20, 1 }, { 21, 1 }, { 22, 1 }, { 23, 1 }, { 24, 1 }, { 25, 1 }, { 26, 1 }, { 27, 1 }, { 28, 1 }, { 29, 1 }, { 30, 1 }

        };

        public void ParkVehicle(List<Vehicle> vehicles)
        {
            bool parked = false;

            for(int i = 0; i < vehicles.Count; i++)
            {

                if (AvailableParkingSpaces > vehicle.ParkingSpotsUsed)
                {
                    while (!parked)
                    {
                        for (int i = 1; i < ParkingSpaces.Count; i++)
                        {
                            //if vehicle is motorcycle
                            if (ParkingSpaces[i] == (1))
                            {

                                vehicle.ParkingSpotsUsed
                                ParkingSpaces[i] = 0;//Ändrar från ledigt(1) till upptaget i ParkingSpaces.

                                break;
                            }
                        }
                    }
                }

                //if (ParkingSpaces[i] = (1)
                //{
                //    ParkingSpaces.Remove();
                //    ParkingSpaces.Add(vehicle.RegPlate);

                //}

            }
            Console.WriteLine($"Your {vehicle} will be parked in parking spot number  ");//lägg till nummer


            //else
            //{ Console.WriteLine("Sorry, there are no avaiable parking spots in the garage.");
        }
            
        public static void PrintParkedInfo(List<Vehicle> vehicles)
        {
            for (int i = 0; i < vehicles.Count; i++)//Utskrift över alla parkerade fordon.
            {
                Console.WriteLine($"The {vehicles[i].GetType().Name} with the registrationplate {vehicles[i].RegPlate} is parked at parkingspot number ");//Lägg till info för angedd arkeringsplats.
            }
        }
    }
}
