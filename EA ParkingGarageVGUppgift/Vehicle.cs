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
        internal List<int> ParkedInNr { get; set; }                                                                     
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
            Vehicle vehicle;                                                                                            //Deklarerar ny variabel av typen V

            switch (input)
            {
                case 'c':
                    Console.Write("Is your car an electric car? Press \"Y\" if it is. Press \"N\" if it is not.");
                    char isElectricCar = Console.ReadKey(true).KeyChar;

                    while (isElectricCar != 'y' && isElectricCar != 'n')                                                //hanterar felinmatning
                    {
                        Console.Write("Please enter \"N\" for no, or \"Y\" yes.");
                        isElectricCar = Console.ReadKey(true).KeyChar;
                    }

                    if (isElectricCar == 'y')
                    {
                        electricCar = true;
                    }

                    if (isElectricCar == 'n')                                                                           //Räcker med else - men för tydlighet
                    {
                        electricCar = false;
                    }

                    vehicle = new Car(electricCar);                                                                     //Skapar nytt objekt av redan deklarerad variabel. Subklass ärver från basklass
                    break;

                case 'm':
                    Console.Write("What kind of brand is your motorcycle? ");                                           
                    string motorcycleBrand = Console.ReadLine();

                    while (motorcycleBrand == "")                                                                       //Felhantering tom sträng string.IsNullOrWhiteSPace() -> tom, utan värde eller blanksteg
                    {
                        Console.Write("Please enter a motorcycle brand: ");
                        motorcycleBrand = Console.ReadLine();
                    }

                    vehicle = new MC(motorcycleBrand);
                    break;

                case 'b':
                    Console.Write("How many seats for passengers does the bus have? ");

                    int numberOfPassengers;

                    while (!int.TryParse(Console.ReadLine(), out numberOfPassengers) || numberOfPassengers < 1)         //inmatning är int & nOP inte är < 1
                    {
                        Console.Write("Please enter av valid number for the amount of passenger seats in the bus.");
                    }

                    vehicle = new Bus(numberOfPassengers);
                    break;

                default:
                    return null;                                                                                        //Om ingen annat case uppfylls 
            }

            return vehicle;
        }

        internal virtual void PrintVehicleInfo()                                                                        //P-metod
        {

        }

        private class Car : Vehicle
        {
            internal override double ParkingSpotsUsed { get; }                                                      //Jag vill inte att antalet platser ska gå att ändra -> get; Åsidosätter BC.
            internal bool IsElectricCar { get; set; }

            internal Car(bool electricCar)
            {
                ParkingSpotsUsed = 1;
                IsElectricCar = electricCar;
            }

            internal override void PrintVehicleInfo()
            {
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
                Console.Write($"{NumberOfPassengers}.\n");
            }
        }
    }
}
