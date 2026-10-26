using System;
using System.Collections.Generic;
using System.Text;

namespace EA_ParkingGarageVGUppgift //KOLLA ÅTKOMSTMODIFIERARE
{
    internal class Vehicle
    {
        public string RegPlate { get; }                     //Endast get;
        public string VehicleColor { get; }
        public virtual double ParkingSpotsUsed { get; }     //Override i subclasser. För att inte behöva kontrollera vilken typ subclassen är innan man komma åt pSpots
        public int parkedInNr { get; set; }

        public Vehicle()
        {
            RegPlate = Helpers.GetRegPlate();
            VehicleColor = Helpers.GetVehicleColor();
            parkedInNr = 0;
        }

        public static void ChooseVehicle(char chooseType, List<Vehicle> vehicles, SortedList<double, string> parkingSpaces) //gör virtual?
        {
            bool electricCar = false;

            switch (chooseType)
            {
                case 'c':
                    Console.WriteLine("Is your car an electric car? Press \"Y\" if it is. Press \"N\" if it is not.");
                    char isElectricCar = Console.ReadKey().KeyChar;

                    while (isElectricCar != 'y' && isElectricCar != 'n')//Kör while tills inmatningen blir Y eller N
                    { 
                            Console.WriteLine("Please enter \"N\" for no, or \"Y\" yes.");
                            isElectricCar = Console.ReadKey().KeyChar;
                    }

                    if (isElectricCar == 'y')
                    {
                        electricCar = true;
                    }
                    if (isElectricCar == 'n')
                    {
                        electricCar = false;
                    }

                    Car car = new Car(electricCar);
                    vehicles.Add(car);
                    parkingSpaces.Add(car.ParkingSpotsUsed, car.RegPlate);
                    break;

                case 'm':
                    Console.WriteLine("What kind of brand is your motorcycle?");
                    string motorcycleBrand = Console.ReadLine();

                    Motorcycle motorcycle = new Motorcycle(motorcycleBrand);
                    vehicles.Add(motorcycle);
                    parkingSpaces.Add(motorcycle.ParkingSpotsUsed, motorcycle.RegPlate);
                    break;

                case 'b':
                    Console.WriteLine("How many seats for passengers does the bus have?");
                    int numberOfPassengers;
                    while(!int.TryParse(Console.ReadLine(), out numberOfPassengers) || numberOfPassengers < 1) //Körs så länge inmatningen inte är godkänt positivt tal.
                    {
                        Console.WriteLine("Please enter av valid number for the amount of passenger seats in the bus.");
                    }

                    Bus bus = new Bus(numberOfPassengers);
                    vehicles.Add(bus);
                    parkingSpaces.Add(bus.ParkingSpotsUsed, bus.RegPlate);
                    break;

                default:
                    Console.WriteLine("Inmatningen blev fel. Försök igen.");
                    break;
            }
        }

        private class Car : Vehicle //Är private så kan inte skapas utanför klassen
        {
            public override double ParkingSpotsUsed { get; }    //Jag vill inte att antalet platser ska gå att ändra -> get;
            internal bool IsElectricCar { get; set; }

            internal Car(bool electricCar)
            {
                ParkingSpotsUsed = 1;
                IsElectricCar = electricCar;
            }
        }

       private class Motorcycle : Vehicle //Är private så kan inte skapas utanför klassen
        {
            public override double ParkingSpotsUsed { get; }
            public string MotorcycleBrand { get; set; }

            public Motorcycle(string motorcycleBrand)
            {
                ParkingSpotsUsed = 0.5;
                MotorcycleBrand = motorcycleBrand;
            }
        }

        private class Bus : Vehicle //Är private så kan inte skapas utanför klassen
        {
            public override double ParkingSpotsUsed { get; }
            public int NumberOfPassengers { get; set; }

            public Bus(int numberOfPassengers)
            {
                ParkingSpotsUsed = 2;
                NumberOfPassengers = numberOfPassengers;
            }
        }
    }
}
