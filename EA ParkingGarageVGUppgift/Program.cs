namespace EA_ParkingGarageVGUppgift
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            ParkingGarage parkingGarage = new ParkingGarage();                                                                        
            Dictionary<string, Vehicle> vehicles = new Dictionary<string, Vehicle>();                                                   
            Vehicle vehicle = new Vehicle();                                                                                          

            while (true)
            {
                Helpers.DrawScreen();                                                                                                  

                char input = char.ToLower(Console.ReadKey(true).KeyChar);                                                               

                switch (input)
                {
                    case 'c':
                    case 'm':
                    case 'b':
                        Vehicle newVehicle = vehicle.ChooseVehicle(input);                                                              
                        if (newVehicle != null && parkingGarage.ParkVehicle(newVehicle))                                                //Null-koll, försök att parkera fordonsojbekt.
                        {
                            vehicles.Add(newVehicle.RegPlate, newVehicle);                                                              
                            Console.WriteLine();
                            Helpers.Pause();                                                                                          
                        }
                        break;
                    case 'e':
                        parkingGarage.PrintParkedInfo(vehicles);
                        parkingGarage.LeaveGarage(vehicles);
                        Helpers.Pause();
                        break;

                    case 's':
                        Console.Clear();                                                                  
                        parkingGarage.PrintParkedInfo(vehicles);                                                                       
                        Helpers.Pause();                                                                                                      
                        break;

                    default:
                        Console.WriteLine("Please try again(invalid input)");
                        Helpers.Pause();      
                        break;
                }
            }
        }
    }
}
