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
        public List<int> ParkedInNr { get; set; }   //List pga buss
        public DateTime StartParking { get; set; }

        public Vehicle()
        {
            RegPlate = Helpers.GetRegPlate();
            VehicleColor = Helpers.GetVehicleColor();
            ParkedInNr = new List<int>();
        }

        public /*virtual*/ Vehicle ChooseVehicle(char input) //gör virtual?
        {
            bool electricCar = false;
            //ParkingGarage parkingGarage = new ParkingGarage();//skapa instans för att kunna anropa senare i metoden
            Vehicle vehicle; //Låter varje case tilldela rätt subclass - Vehicle vehicle kan inte användas.

            switch (input)
            {
                case 'c':
                    Console.Write("Is your car an electric car? Press \"Y\" if it is. Press \"N\" if it is not.");
                    char isElectricCar = Console.ReadKey(true).KeyChar; //char.ToLower(Console.ReadKey(true).KeyCar); för minska risk för felinmatning

                    while (isElectricCar != 'y' && isElectricCar != 'n')//Kör while tills inmatningen blir Y eller N
                    { 
                            Console.Write("Please enter \"N\" for no, or \"Y\" yes.");
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
                    Console.Write("What kind of brand is your motorcycle? ");
                    string motorcycleBrand = Console.ReadLine(); //randomisera ett märke?
                    vehicle = new MC(motorcycleBrand);
                    break;

                case 'b':
                    Console.Write("How many seats for passengers does the bus have? ");
                    int numberOfPassengers;
                    while(!int.TryParse(Console.ReadLine(), out numberOfPassengers) || numberOfPassengers < 1) //Körs så länge inmatningen inte är godkänt positivt tal.
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

        public class Car : Vehicle //Är private så kan inte skapas utanför klassen
        {
            public override double ParkingSpotsUsed { get; }    //Jag vill inte att antalet platser ska gå att ändra -> get;
            public bool IsElectricCar { get; set; }

            internal Car(bool electricCar)
            {
                ParkingSpotsUsed = 1;
                IsElectricCar = electricCar;
            }
            //public override Vehicle ChooseVehicle(/*char input*/)
            //{
            
             
            //}
        }

        public class MC : Vehicle //Är private så kan inte skapas utanför klassen
        {
            public override double ParkingSpotsUsed { get; }
            public string MotorcycleBrand { get; set; }

            public MC(string motorcycleBrand)
            {
                ParkingSpotsUsed = 0.5;
                MotorcycleBrand = motorcycleBrand;
            }
            //metod parkvehicle
        }

        public class Bus : Vehicle //Är private så kan inte skapas utanför klassen
        {
            public override double ParkingSpotsUsed { get; }
            public int NumberOfPassengers { get; set; }

            public Bus(int numberOfPassengers)
            {
                ParkingSpotsUsed = 2;
                NumberOfPassengers = numberOfPassengers;
            }
            //metod parkvehicle
        }
    }
}
