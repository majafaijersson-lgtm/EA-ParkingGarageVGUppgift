using System;
using System.Collections.Generic;
using System.Text;
using static EA_ParkingGarageVGUppgift.Vehicle;

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



        public Vehicle ChooseVehicle(char input)
        {
            bool electricCar = false;

            Vehicle vehicle;

            switch (input)
            {
                case 'c':
                    Console.Write("Is your car an electric car? Press \"Y\" if it is. Press \"N\" if it is not.");
                    char isElectricCar = Console.ReadKey(true).KeyChar;

                    while (isElectricCar != 'y' && isElectricCar != 'n')
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
                    string motorcycleBrand = Console.ReadLine();

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

        public virtual void PrintVehicleInfo() //gör virtual?
        {
            Console.WriteLine("Utskrift i virtualmetod");
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

            public override void PrintVehicleInfo()
            {
                Console.WriteLine("Utskrift i overridemetod");
                Console.Write(IsElectricCar ? "electric car." : "not an electric vehicle.");
            }
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
            public override void PrintVehicleInfo()
            {
                Console.WriteLine("Utskrift i overridemetod");
                Console.Write($"{MotorcycleBrand}.");
            }
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
            public override void PrintVehicleInfo()
            {
                Console.WriteLine("Utskrift i overridemetod");
                Console.Write($"{NumberOfPassengers}.");
            }
        }
    }
}
