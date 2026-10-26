using System;
using System.Collections.Generic;
using System.Text;

namespace EA_ParkingGarageVGUppgift
{
    internal class ParkingGarage
    {
        public double AmountParkingSpaces { get; } //Endast get;
        public double availableParkingSpaces { get; set; } = 15;
        public List<Vehicle> parkedVehicles = new();

        public ParkingGarage()
        {
            AmountParkingSpaces = 15;

        }

        public void ParkVehicle()
        {
            if (availableParkingSpaces > 0)
            {
                //Metod för parkera bilar, eftersom att det finns plats
            }
            else { Console.WriteLine("Sorry, there are no avaiable parking spots in the garage."); }
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
