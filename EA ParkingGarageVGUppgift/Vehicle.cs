using System;
using System.Collections.Generic;
using System.Text;
using static EA_ParkingGarageVGUppgift.Vehicle;

namespace EA_ParkingGarageVGUppgift
{
    internal class Vehicle
    {
        internal string RegPlate { get; }                                                                           
        internal string VehicleColor { get; }
        internal virtual double ParkingSpotsUsed { get; }                                                               //Override i subclasser. double(dec)
        internal List<int> ParkedInNr { get; }                                                                     
        internal DateTime StartParking { get; set; }

        internal Vehicle()
        {
            RegPlate = Helpers.GetRegPlate();
            VehicleColor = Helpers.GetVehicleColor();
            ParkedInNr = new List<int>();
        }

        internal Vehicle ChooseVehicle(char input)
        {
            bool electricCar = false;
            Vehicle vehicle;                                                                                            

            switch (input)
            {
                case 'c':
                    Console.Write("Is your car an electric car? Press \"Y\" if it is. Press \"N\" if it is not.");
                    char isElectricCar = char.ToLower(Console.ReadKey(true).KeyChar);

                    while (isElectricCar != 'y' && isElectricCar != 'n')                                                //om felinmatning sker, fortsätter loopen.
                    {
                        Console.Write("Please enter \"N\" for no, or \"Y\" yes.");
                        isElectricCar = char.ToLower(Console.ReadKey(true).KeyChar);
                    }

                    if (isElectricCar == 'y')
                    {
                        electricCar = true;
                    }
                    else
                    {
                        electricCar = false;
                    }
                    
                    vehicle = new Car(electricCar);                                                                     
                    break;

                case 'm':
                    Console.Write("What kind of brand is your motorcycle? ");                                           
                    string motorcycleBrand = Console.ReadLine();

                    while (motorcycleBrand == "")                                                                       //Felhantering kan även ske med string.IsNullOrWhiteSPace() -> tom, utan värde eller blanksteg
                    {
                        Console.Write("Please enter a motorcycle brand: ");
                        motorcycleBrand = Console.ReadLine();
                    }

                    vehicle = new MC(motorcycleBrand);
                    break;

                case 'b':
                    Console.Write("How many seats for passengers does the bus have? ");

                    int numberOfPassengers;

                    while (!int.TryParse(Console.ReadLine(), out numberOfPassengers) || numberOfPassengers < 1)         
                    {
                        Console.Write("Please enter av valid number for the amount of passenger seats in the bus.");
                    }

                    vehicle = new Bus(numberOfPassengers);
                    break;

                default:
                    return null;                                                                                        
            }
            return vehicle;
        }

        internal virtual void PrintVehicleInfo()                                                                   
        {
            Console.Write($": {GetType().Name} - {RegPlate} - parking started: {StartParking} - {VehicleColor} - ");
        }

        internal class Car : Vehicle
        {
            internal override double ParkingSpotsUsed { get; }                                                      
            internal bool IsElectricCar { get; set; }

            internal Car(bool electricCar)
            {
                ParkingSpotsUsed = 1;
                IsElectricCar = electricCar;
            }

            internal override void PrintVehicleInfo()
            {
                base.PrintVehicleInfo();
                Console.WriteLine(IsElectricCar ? "electric car." : "not an electric vehicle.");
            }
        }    

        internal class MC : Vehicle
        {
            internal override double ParkingSpotsUsed { get; }
            internal string MotorcycleBrand { get; set; }

            internal MC(string motorcycleBrand)
            {
                ParkingSpotsUsed = 0.5;
                MotorcycleBrand = motorcycleBrand;
            }
            internal override void PrintVehicleInfo()
            {
                base.PrintVehicleInfo();
                Console.Write($"{MotorcycleBrand}.\n");
            }
        }

        internal class Bus : Vehicle
        {
            internal override double ParkingSpotsUsed { get; }
            internal int NumberOfPassengers { get; set; }

            internal Bus(int numberOfPassengers)
            {
                ParkingSpotsUsed = 2;
                NumberOfPassengers = numberOfPassengers;
            }
            internal override void PrintVehicleInfo()
            {
                base.PrintVehicleInfo();
                Console.Write($"{NumberOfPassengers}.\n");
            }
        }
    }
}
