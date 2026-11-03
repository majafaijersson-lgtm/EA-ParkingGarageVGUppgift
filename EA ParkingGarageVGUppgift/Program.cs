namespace EA_ParkingGarageVGUppgift
{
    internal class Program
    {
       internal static void Main(string[] args)
       {
            ParkingGarage parkingGarage = new ParkingGarage();                                                                          //Platser. In- & utcheckn.
            Dictionary<string, Vehicle> vehicles = new Dictionary<string, Vehicle>();                                                   //REGNR + Vehicle m. attr.
            Vehicle vehicle = new Vehicle();                                                                                            //Skapar en ny instans av klassen till chooseVehicle

            Console.WriteLine($"------------------------------------------------------------\nWelcome to the Parking Garage!\t" +
                              $"Price: 1 SeK/parking space/second.\n------------------------------------------------------------");
            while (true)
            {
                Console.WriteLine("Please enter \"C\"(car), \"M\"(MC) or \"B\"(bus) for parking." +
                                  "\nEnter \"E\" to exit or \"S\" to show all of the parking spaces\n");

                char input = char.ToLower(Console.ReadKey(true).KeyChar);                                                               //tecken-input som sparas i variabel.

                switch (input)
                {
                    case 'c':
                    case 'm':
                    case 'b':
                        Vehicle newVehicle = vehicle.ChooseVehicle(input);                                                              //anrop PC i klassen Vehicle genom objektet vehicle utifrån inputval.
                        if (newVehicle != null && parkingGarage.ParkVehicle(newVehicle))                                                //Null-koll, försök att parkera fordonsojbekt. Anrop görs via objekte pG och inte direkt i klassen PG
                        {
                            vehicles.Add(newVehicle.RegPlate, newVehicle);                                                              //add vehicles med nyckel och vehiclevärde. 
                            Console.WriteLine();
                        }
                        break;
                    case 'e':
                        parkingGarage.LeaveGarage(vehicles);
                        break;

                    case 's':
                        parkingGarage.PrintParkedInfo(vehicles);                                                                        //Polymorfism med anrop i PPI
                        break;

                    default:
                        Console.WriteLine($"Please try again(invalid input)"); 
                        Console.ReadKey(true);
                        break;
                }
            }
        }
    }
}
