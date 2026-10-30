namespace EA_ParkingGarageVGUppgift
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Villkor för att Vehicle ska vara av någon subclass - inget Vehicle vehicle får parkeras!
            //Använd Någon annan collection för parkeringen?
            //Grafisk representation hade varit kul?

            //Funktion för att parkera motorcykel på halvledig plats!
            //Visa båda rutorna som buss parkerar på i PrintParkedInfo

         
            ParkingGarage parkingGarage = new ParkingGarage();
            Dictionary<string, Vehicle> vehicles = new Dictionary<string, Vehicle>();//REGNR + Vehicle med resten av attributen
            //ParkingGarage parkingGarage1 = new ParkingGarage();
            Vehicle vehicle = new Vehicle();
            while (true)
            {


                Console.Clear();
                Console.WriteLine("Welcome to the Parking Garage!\n------------------------------");
                Console.WriteLine("What kind of vehicle do you want to park?\nPress: \"C\" for car, \"M\" for motorcycle or \"B\" for bus.");
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    char input = (char)key.KeyChar;
                    switch (input)
                    {
                        case 'l':
                            //metod för attb ta bort från dict
                            break;
                        case 'c':
                            vehicles = vehicle.ChooseVehicle(input, vehicles);
                            break;
                        case 'm':
                            vehicles = vehicle.ChooseVehicle(input, vehicles);
                            break;
                        case 'b':
                            vehicles = vehicle.ChooseVehicle(input, vehicles);
                            break;
                        case 's':
                            break; //metod för att visa parkerade bilar.
                    }

                    vehicles = parkingGarage.ParkVehicle(vehicles);//anropa med listan vehicles via parkingGarage inte via klassnamnet ParkingGarage för att se alla parkerade bilar. Ändringar i dicitonary görs i den instansen.
                    ParkingGarage.PrintParkedInfo(vehicles);
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("\nPlease press any key to park another car.");
                    Console.ReadLine();

                }

            }
        }
    }
}
