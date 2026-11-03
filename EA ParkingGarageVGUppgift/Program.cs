namespace EA_ParkingGarageVGUppgift
{
    internal class Program
    {
       internal static void Main(string[] args)
       {
            ParkingGarage parkingGarage = new ParkingGarage();
            Dictionary<string, Vehicle> vehicles = new Dictionary<string, Vehicle>();//REGNR + Vehicle med resten av attributen
            Vehicle vehicle = new Vehicle();

            Console.WriteLine($"------------------------------------------------------------\nWelcome to the Parking Garage!\t" +
                              $"Price: 1 SeK/parking space/second.\n------------------------------------------------------------");
            while (true)
            {
                Console.WriteLine("Please enter \"C\"(car), \"M\"(MC) or \"B\"(bus) for parking." +
                                  "\nEnter \"E\" to exit or \"S\" to show all of the parking spaces\n");

                char input = char.ToLower(Console.ReadKey(true).KeyChar);

                switch (input)
                {
                    case 'c':
                    case 'm':
                    case 'b':
                        Vehicle newVehicle = vehicle.ChooseVehicle(input);
                        if (newVehicle != null && parkingGarage.ParkVehicle(newVehicle))
                        {
                            vehicles.Add(newVehicle.RegPlate, newVehicle);//anropa med listan vehicles via parkingGarage inte via klassnamnet ParkingGarage för att se alla parkerade bilar. Ändringar i dicitonary görs i den instansen.
                            Console.WriteLine();
                        }
                        break;
                    case 'e'://vad händer om ingen bil är parkerad? eller om fel reg matas in?
                        parkingGarage.LeaveGarage(vehicles);
                        break;

                    case 's':
                        parkingGarage.PrintParkedInfo(vehicles);//tídigare ParkingGarage.PrintParkedInfo -> gjorde till virtual/override i vehicle istl
                        break; //metod för att visa parkerade bilar.

                    default:
                        Console.WriteLine($"Please try again(invalid input)");
                        Console.ReadKey(true);
                        break;
                }
            }
        }
    }
}
