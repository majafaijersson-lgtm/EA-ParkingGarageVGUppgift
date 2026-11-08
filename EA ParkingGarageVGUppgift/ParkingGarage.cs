using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace EA_ParkingGarageVGUppgift
{
    internal class ParkingGarage
    {
        private Dictionary<int, double> ParkingSpaces { get; } = new Dictionary<int, double>();

        internal ParkingGarage()                                                                            //Ändring från feedback(Micke) - forloop för att fylla alla platser.
        {
            for(int i = 1; i <= 15; i++)
            {
                ParkingSpaces.Add(i, 1);
            }
        }

        internal bool ParkVehicle(Vehicle vehicle)                                                          
        {
            DateTime startParking = DateTime.Now;                                                         
            double spacesNeededForVehicle = vehicle.ParkingSpotsUsed;                                       //Hämtar och ger variabeln värdet för hur många pplatser fordonet behöver.
            double availableCapacity = 0;                                                                           //anger hur stor del av platsen som är ledig(1, 0.5, 0)

            foreach (double parkingSpace in ParkingSpaces.Values)                                           
            {
                if (vehicle is Vehicle.MC)  //Kolla upp
                {
                    availableCapacity += parkingSpace;                                                               
                }
                else if (parkingSpace == 1)                                                                 
                {
                    availableCapacity += parkingSpace;                                                              
                }
            }

            if (availableCapacity < spacesNeededForVehicle)
            {
                Console.WriteLine("Det fanns tyvärr inte tillräckligt med plats i p-huset. Välkommen åter!");
                return false;
            }

            if (vehicle is Vehicle.Bus)
            {
                int availableSpaces = 0;                                                                    //Variabel för antalet lediga parkeringsplatser                                                           

                foreach (double parkingSpace in ParkingSpaces.Values)                                       //Går igenom alla värden i dict.
                {
                    if (parkingSpace == 1)                                                                  //Om en ledig plats finns så läggs +1 till i availableSpaces
                        availableSpaces++;                                                                 
                }

                if (availableSpaces < 2)                                                                    
                {
                    Console.WriteLine("Det fanns inte två hela parkeringsrutor för bussen.");
                    return false;
                }
            }

            for (int j = 1; j <= ParkingSpaces.Count && spacesNeededForVehicle > 0; j++)                    //Kör igenom alla platser, så länge spacesNeededForVehicle > 0 
            {
                if (ParkingSpaces[j] == 0)                                                                  //stoppar iteration(går vidare till nästa plats).
                    continue;

                if (!(vehicle is Vehicle.MC) && ParkingSpaces[j] < 1)                                       //Om int är MC och pplatsen(0.5). Gå vidare till nästa plats.
                    continue;

                if (ParkingSpaces[j] >= spacesNeededForVehicle)                                             //Om platsen har ett större värde än behovet av plats
                {
                    ParkingSpaces[j] -= spacesNeededForVehicle;                                             //parkeringens värde - spacesNeededFV(0 = parkerad)
                    spacesNeededForVehicle = 0;
                }
                else
                {
                    spacesNeededForVehicle -= ParkingSpaces[j];                                             //Värdet spacesNFV - platsen. Få fram hur mycket mer plats fordonet behöver -> vidare till nästa varv i for-loopen.
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
                    Console.Write(" and ");                                                                 //, mellan numren, men inte efter det sista. Hur kan man göra kode mer DRY(string.Join)?
                }
            }
            Console.WriteLine(".\n");

            return true;                                                                                    // = parkerad
        }

        internal void LeaveGarage(Dictionary<string, Vehicle> vehicles)
        {
            Console.Write("\n\nEnter the registration number for the vehicle you wish to sign out from the Parking garage(ex. GWH 959): ");

            bool correctInput = false;                                                                      

            while (!correctInput)
            {
                string inputRegPlate = Console.ReadLine();

                if (vehicles.TryGetValue(inputRegPlate, out Vehicle vehicle))                               
                {
                    TimeSpan parkedTime = DateTime.Now - vehicle.StartParking;                              
                    double parkedSeconds = parkedTime.TotalSeconds;                                       

                    Console.WriteLine($"\nYour vehicle with the registration number {inputRegPlate} is being signed out of the garage. It has been parked since {vehicle.StartParking} " +
                                      $"and the price is {Math.Round((parkedSeconds * 1) * (vehicle.ParkingSpotsUsed))}(for the {Math.Round(parkedSeconds)} seconds it was parked).\n"); //Math.Round avrundar 2.5 blir 2, 2.51 blir 3(.Floor för att avrunda nedåt).

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

            foreach (var item in vehicles)                                                             
            {
                Vehicle vehicle = item.Value;                                                         

                Console.Write("Plats ");

                for (int i = 0; i < vehicle.ParkedInNr.Count; i++)                                      //går igenom pplatser som tilldelats fordonet
                {
                    Console.Write(vehicle.ParkedInNr[i]);

                    if (i < vehicle.ParkedInNr.Count - 1)                                               // Kontrollerar om det finns fler parkeringsplatsnummer att skriva ut.
                    {
                        Console.Write(", ");                                                            // komma mellan numren, men inte efter det sista
                    }
                }

                Console.Write($": {vehicle.GetType().Name} - {vehicle.RegPlate} - parking started: {vehicle.StartParking} - {vehicle.VehicleColor} - ");

                vehicle.PrintVehicleInfo();                                                             //anrop av metod i det specifika objektet. Polymorfism kan nyttjas utifrån fordonstypen

            }
        }
    }
}


       
