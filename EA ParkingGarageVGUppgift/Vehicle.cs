using System;
using System.Collections.Generic;
using System.Text;

namespace EA_ParkingGarageVGUppgift //KOLLA ÅTKOMSTMODIFIERARE
{
    public class Vehicle
    {
        public string RegPlate { get; }                     //Endast get;
        public string VehicleColor { get; }
        public virtual double ParkingSpotsUsed { get; }     //Override i subclasser. För att inte behöva kontrollera vilken typ subclassen är innan man komma åt pSpots
        public virtual List<int> ParkedInNr { get; set; }   //override från bus

        public Vehicle()
        {
            RegPlate = Helpers.GetRegPlate();
            VehicleColor = Helpers.GetVehicleColor();
            ParkedInNr = new List<int>();
        }

        public static List<Vehicle> ChooseVehicle(char chooseType, List<Vehicle> vehicles) //gör virtual?
        {
            bool electricCar = false;
            //ParkingGarage parkingGarage = new ParkingGarage();//skapa instans för att kunna anropa senare i metoden
            Vehicle vehicle = new Vehicle();

            switch (chooseType)
            {
                case 'c':
                    Console.WriteLine("Is your car an electric car? Press \"Y\" if it is. Press \"N\" if it is not.");
                    char isElectricCar = Console.ReadKey(true).KeyChar;

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

                    vehicle = new Car(electricCar);
                    break;

                case 'm':
                    Console.WriteLine("What kind of brand is your motorcycle?");
                    string motorcycleBrand = Console.ReadLine(); //randomisera ett märke?
                    vehicle = new Motorcycle(motorcycleBrand);
                    break;

                case 'b':
                    Console.WriteLine("How many seats for passengers does the bus have?");
                    int numberOfPassengers;
                    while(!int.TryParse(Console.ReadLine(), out numberOfPassengers) || numberOfPassengers < 1) //Körs så länge inmatningen inte är godkänt positivt tal.
                    {
                        Console.WriteLine("Please enter av valid number for the amount of passenger seats in the bus.");
                    }

                    vehicle = new Bus(numberOfPassengers);
                    break;

                default:
                    Console.WriteLine("Inmatningen blev fel. Försök igen.");
                    break;
            }
            vehicles.Add(vehicle);

            return vehicles;
        }

        public class Car : Vehicle //Är private så kan inte skapas utanför klassen
        {
            public override double ParkingSpotsUsed { get; }    //Jag vill inte att antalet platser ska gå att ändra -> get;
            public bool IsElectricCar { get; set; }

            internal Car(bool electricCar)
            {
                ParkingSpotsUsed = 1;
                IsElectricCar = electricCar;
            }
        }

        public class Motorcycle : Vehicle //Är private så kan inte skapas utanför klassen
        {
            public override double ParkingSpotsUsed { get; }
            public string MotorcycleBrand { get; set; }

            public Motorcycle(string motorcycleBrand)
            {
                ParkingSpotsUsed = 0.5;
                MotorcycleBrand = motorcycleBrand;
            }
        }

        public class Bus : Vehicle //Är private så kan inte skapas utanför klassen
        {
            public override double ParkingSpotsUsed { get; }
            public int NumberOfPassengers { get; set; }
            public override List<int> ParkedInNr { get; set; }

            public Bus(int numberOfPassengers)
            {
                ParkingSpotsUsed = 2;
                NumberOfPassengers = numberOfPassengers;
                ParkedInNr = new List<int>();//skapar en lista
            }
        }
    }
}
