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

            List<Vehicle> vehicles = new List<Vehicle>();
            ParkingGarage parkingGarage = new ParkingGarage();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Parking Garage!\n------------------------------");
                Console.WriteLine("What kind of vehicle do you want to park?\nPress: \"C\" for car, \"M\" for motorcycle or \"B\" for bus.");
                char chooseType = Console.ReadKey(true).KeyChar;//true = dölj inmatning?
                vehicles = Vehicle.ChooseVehicle(chooseType, vehicles); //Lägger till nytt fordon i listan vehicles
                parkingGarage.ParkVehicle(vehicles);//anropa med listan vehicles via parkingGarage inte via klassnamnet ParkingGarage för att se alla parkerade bilar. Ändringar i dicitonary görs i den instansen.
                ParkingGarage.PrintParkedInfo(vehicles);
                Console.WriteLine("------------------------------");
                Console.WriteLine("\nPlease press any key to park another car.");
                Console.ReadLine();

            }

        }
    }
}
