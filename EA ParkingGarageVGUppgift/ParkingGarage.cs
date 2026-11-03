using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace EA_ParkingGarageVGUppgift
{
    internal class ParkingGarage
    {
        private Dictionary<double, double> ParkingSpaces { get; set; } =                                    //Används i ParkVehicle. Håller koll på parkeringsplatsem(key) och om den är upptagen(0) eller inte.
        new Dictionary<double, double>
        {
            { 1, 1 }, { 2, 1 }, { 3, 1 }, { 4, 1 }, { 5, 1 }, { 6, 1 }, { 7, 1 }, 
            { 8, 1 }, { 9, 1 }, { 10, 1 }, { 11, 1 }, { 12, 1 }, { 13, 1 }, { 14, 1 }, { 15, 1 }

        };

        internal bool ParkVehicle(Vehicle vehicle)                                                          //Return om park kunde ske
        {
            DateTime startParking = DateTime.Now;                                                           //Sparar tiden
            double spacesNeededForVehicle = vehicle.ParkingSpotsUsed;                                       //Hämtar och ger variabeln värdet för hur många pplatser fordonet behöver.
            double available = 0;                                                                           //anger hur stor del av platsen som är ledig(1, 0.5, 0)

            foreach (double parkingSpace in ParkingSpaces.Values)                                           //Går igenom alla pplatsvärden
            {
                if (vehicle is Vehicle.MC)
                {
                    available += parkingSpace;                                                              //Man ger available värdet för just den rutan. MC kan använda 0.5 platser. Om värdet = 0 så sker inget.  
                }
                else if (parkingSpace == 1)                                                                 //Övriga fordon behöver 1 eller 2 hela rutor.
                {
                    available += parkingSpace;                                                              //Adderar tillgängligvariabel med värdet av pplatsen(1, 0.5, 0). Om 0 -> händer inget -> nästa varv i loopen.    
                }
            }

            if (available < spacesNeededForVehicle)
            {
                Console.WriteLine("Det fanns tyvärr inte tillräckligt med plats i p-huset. Välkommen åter!");
                return false;
            }

            if (vehicle is Vehicle.Bus)
            {
                int spacesNeededBus = 0;                                                                    //Variabel som håller koll på om buss är fullständigt parkerad(2) eller delvis parkerad(1)                                                                

                foreach (double parkingSpace in ParkingSpaces.Values)                                       //Går igenom alla värden.
                {
                    if (parkingSpace == 1)                                                                  //Om en ledig plats finns så läggs +1 till i spacesNeededBus
                        spacesNeededBus++;
                }

                if (spacesNeededBus < 2)                                                                    //Om det inte går att hitta två platser i tidigare foreach
                {
                    Console.WriteLine("Det fanns inte två hela parkeringsrutor för bussen.");
                    return false;
                }
            }

            for (int j = 1; j <= ParkingSpaces.Count && spacesNeededForVehicle > 0; j++)                    //Kör loop som är lika lång som pplatser(15), så länge spacesNeededForVehicle > 0 (dvs att den ej är parkerad)
            {
                if (ParkingSpaces[j] == 0)                                                                  //Om plats == 0
                    continue;

                if (!(vehicle is Vehicle.MC) && ParkingSpaces[j] < 1)                                       //Om fordonet int är MC och pplatsen är tom(0) eller innehåller en MC(0.5).
                    continue;

                if (ParkingSpaces[j] >= spacesNeededForVehicle)                                             //Om pplatsen har ett större värde än behover av pplats
                {
                    ParkingSpaces[j] -= spacesNeededForVehicle;                                             //Ändra värde pplats och nolla spacesNeededFV(parkerad)
                    spacesNeededForVehicle = 0;
                }
                else
                {
                    spacesNeededForVehicle -= ParkingSpaces[j];                                             //Ändra värdet på spacesNFV med värdet på pplatsen. Få fram hur mycket mer plats fordonet behöver -> vidare till nästa varv i for-loopen.
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
                    Console.Write(" and ");                                                                 //, mellan numren, men inte efter det sista. Hur kan man göra utskriften med DRY?
                }
            }
            Console.WriteLine(".");

            return true;                                                                                    // = parkerad
        }

        internal void LeaveGarage(Dictionary<string, Vehicle> vehicles)
        {
            Console.Write("Enter the registration number for the vehicle you wish to sign out from the Parking garage(ex. GWH 959): ");

            bool correctInput = false;                                                                      //variabel för om rätt inmatning har skett.

            while (!correctInput)
            {
                string inputRegPlate = Console.ReadLine();

                if (vehicles.TryGetValue(inputRegPlate, out Vehicle vehicle))                               //Om input finns som en nyckel i listan vehicles - returnera det fordonet,
                {
                    TimeSpan parkedTime = DateTime.Now - vehicle.StartParking;                              //Räknar ut TimeSpan 
                    double parkedSeconds = parkedTime.TotalSeconds;                                         //.TS egenskap som räknar sek.

                    Console.WriteLine($"\nYour vehicle with the registration number {inputRegPlate} is being signed out of the garage. It has been parked since {vehicle.StartParking} " +
                                      $"and the price is {Math.Round((parkedSeconds * 1) * (vehicle.ParkingSpotsUsed))}(for the {Math.Round(parkedSeconds)} seconds it was parked).\n");//Math.Round avrundar 2.5 blir 2, 2.51 blir 3(.Floor för att avrunda nedåt).

                    vehicles.Remove(inputRegPlate);

                    correctInput = true;
                }
                else
                {
                    Console.WriteLine("Felaktig inmatning. Försök igen.");
                }
            }
        }

        internal void PrintParkedInfo(Dictionary<string, Vehicle> vehicles)
        {
            Console.WriteLine($"Currently parked vehicles: ");
            foreach (var item in vehicles)                                                              //går igenom Dict
            {
                Vehicle vehicle = item.Value;                                                           //sparar värdet på det aktuella fordonet i vehicle.

                Console.Write("Plats ");

                for (int i = 0; i < vehicle.ParkedInNr.Count; i++)                                      //Nödvändigt för bus
                {
                    Console.Write(vehicle.ParkedInNr[i]);

                    if (i < vehicle.ParkedInNr.Count - 1)                                               //Om fordonet behöver två platser och ett varv i loopen(en utskrift av plats) redan har skett.
                    {
                        Console.Write(", ");                                                            // komma mellan numren, men inte efter det sista
                    }
                }

                Console.Write($": {vehicle.GetType().Name} - {vehicle.RegPlate} - parking started: {vehicle.StartParking} - {vehicle.VehicleColor} - ");

                vehicle.PrintVehicleInfo();                                                             //anrop av metod i det specifika objektet. P

            }
        }
    }
}


       
