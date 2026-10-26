namespace EA_ParkingGarageVGUppgift
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //inparametrar till Vehicle ellbil bool, motorcycle(märke string), buss(antal passagerare int)
            //Villkor för att Vehicle ska vara av någon subclass - inget Vehicle vehicle får parkeras!
            //Använd Någon annan collection för parkeringen?
            //Grafisk representation hade varit kul?

            List<Vehicle> vehicles = new List<Vehicle>();
            //SortedList<double,string> ParkingSpaces = new SortedList<double, string>();//Kommer den sorter i nummerodning? dvs att 0.5 kommer upp först?

            while (true)
            {
                Console.WriteLine("Welcome to the Parking Garage!\n------------------------------");
                Console.WriteLine("What kind of vehicle do you want to park?\nPress: \"C\" for car, \"M\" for motorcycle or \"B\" for bus.");
                char chooseType = Console.ReadKey().KeyChar;
                Vehicle.ChooseVehicle(chooseType, vehicles);

                //ParkingGarage.PrintParkedInfo(vehicles);

            }

        }
    }
}
