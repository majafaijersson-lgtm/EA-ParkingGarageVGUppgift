using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace EA_ParkingGarageVGUppgift
{
    internal class ParkingGarage
    {
        private Dictionary<int, double> ParkingSpaces { get; } = new Dictionary<int, double>();

        internal ParkingGarage()                                                                                                                                            //Ändring från feedback(Micke) - for-loop för att ge alla nycklar + värden.
        {
            for(int i = 1; i <= 15; i++)
            {
                ParkingSpaces.Add(i, 1);
            }
        }

        internal bool ParkVehicle(Vehicle vehicle)                                                                                                                          //Ändring. Tidigare fel - bussen kunde stå på två platser(ej intill varandra).
        {
            //Del som klargör hur stor plats fordonet tar och om det behöver fler platser bredvid varandra(buss).
            double spotsUsed = vehicle.ParkingSpotsUsed;                                                                                                                    
            double spotsInARow;                                                                                                                                             
            double amountPerSpace;

            if (spotsUsed < 1)                                                                               
            {
                spotsInARow = 1;
                amountPerSpace = spotsUsed;                                                                 
            }
            else                                                                                                                                                            
            {
                spotsInARow = spotsUsed;
                amountPerSpace = 1;                                                                                                                                        
            }

            //Del som räknar lediga platser(+ lediga platser i rad). Om kedja med platser hittas som täcker behovet -> sparas i variabel firstSpace
            int freeInARow = 0;                                                                             
            int firstSpace = -1;

            for (int i = 1; i <= ParkingSpaces.Count; i++)
            {
                if (ParkingSpaces[i] >= amountPerSpace)
                {
                    freeInARow++;
                }
                else
                {
                    freeInARow = 0;
                }

                if (freeInARow == spotsInARow)                                                                                                                              //När tillräckligt antal platser i rad hittats - sparar variabel för den första. ex lediga: 1 och 2 - behovet(2) + 1.
                {
                    firstSpace = (int)(i - spotsInARow + 1);
                    break;
                }
            }

            //Parkerar om möjligt och returnerar bool.
            if (firstSpace == -1)
            {
                Console.WriteLine("There is not enough room for the vehicle in the parking garage.");
                return false;
            }
            else
            {
                for (int i = firstSpace; i < firstSpace + spotsUsed; i++)
                {
                    ParkingSpaces[i] -= amountPerSpace;
                    vehicle.ParkedInNr.Add(i);
                }
            }

            vehicle.StartParking = DateTime.Now;
            return true;
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
                                      $"and the price is {Math.Round(parkedSeconds * vehicle.ParkingSpotsUsed)}(for the {Math.Round(parkedSeconds)} seconds it was parked).\n"); //Math.Round avrundar 2.5 blir 2, 2.51 blir 3(.Floor för att avrunda nedåt).

                    double amountPerSpace = vehicle.ParkingSpotsUsed / vehicle.ParkedInNr.Count;                                                                                //Värdeändring i parkingspaces-queue
                    foreach(int spotNr in vehicle.ParkedInNr)
                    {
                        ParkingSpaces[spotNr] += amountPerSpace;
                    }

                    vehicles.Remove(inputRegPlate);

                    correctInput = true;
                }
                else
                {
                    Console.WriteLine("Incorrect input. Please try again.");
                }
            }
        }

        internal void PrintParkedInfo(Dictionary<string, Vehicle> vehicles)                                                                                                 
        {
            Console.WriteLine($"Currently parked vehicles: ");                                                                                                                  //Feedback Micke - hade gärna sett sortering i sifferordning(pplatsnr).

            foreach (var item in vehicles)                                                             
            {
                Vehicle vehicle = item.Value;                                                         

                Console.Write("Space ");

                for (int i = 0; i < vehicle.ParkedInNr.Count; i++)                                      
                {
                    Console.Write(vehicle.ParkedInNr[i]);

                    if (i < vehicle.ParkedInNr.Count - 1)                                               
                    {
                        Console.Write(", ");                                                            
                    }
                }

                vehicle.PrintVehicleInfo();                                                             
            }
        }
    }
}


       
