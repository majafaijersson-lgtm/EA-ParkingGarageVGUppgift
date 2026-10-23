using System;
using System.Collections.Generic;
using System.Text;

namespace EA_ParkingGarageVGUppgift
{
    internal class Vehicle
    {
        public string RegPlate { get; }                     //Endast get;
        public string VehicleColor { get; }
        public virtual double ParkingSpotsUsed { get; }     //Override i subclasser. För att inte behöva kontrollera vilken typ subclassen är innan man komma åt pSpots

        public Vehicle()
        {
            RegPlate = Helpers.GetRegPlate();
            VehicleColor = Helpers.GetVehicleColor();
        }

        public static void ChooseVehicle(char vehicleType, List<Vehicle> vehicles) //gör virtual?
        {
            bool electricCar = false;

            switch (vehicleType)
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

                    break;
                case 'm':
                    Console.WriteLine("What kind of brand is your motorcycle?");
                    string motorcycleBrand = Console.ReadLine();

                    Motorcycle motorcycle = new Motorcycle(motorcycleBrand);
                    vehicles.Add(motorcycle);

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

                    break;
                default:
                    Console.WriteLine("Inmatningen blev fel. Försök igen.");
                    break;
            }
        }

        class Car : Vehicle //Är private så kan inte skapas utanför klassen
        {
            public override double ParkingSpotsUsed { get; }    //Jag vill inte att antalet platser ska gå att ändra -> get;
            public bool IsElectricCar { get; set; }

            public Car(bool electricCar)
            {
                ParkingSpotsUsed = 1;
                IsElectricCar = electricCar;
            }
        }

        class Motorcycle : Vehicle //Är private så kan inte skapas utanför klassen
        {
            public override double ParkingSpotsUsed { get; }
            public string MotorcycleBrand { get; set; }

            public Motorcycle(string motorcycleBrand)
            {
                ParkingSpotsUsed = 0.5;
                MotorcycleBrand = motorcycleBrand;
            }
        }

        class Bus : Vehicle //Är private så kan inte skapas utanför klassen
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
