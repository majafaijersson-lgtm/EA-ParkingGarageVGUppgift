namespace EA_ParkingGarageVGUppgift
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            ParkingGarage parkingGarage = new ParkingGarage();                                                                          //Platser
            Dictionary<string, Vehicle> vehicles = new Dictionary<string, Vehicle>();                                                   //REGNR + Vehicle m. attr.
            Vehicle vehicle = new Vehicle();                                                                                            //Skapar en ny instans av klassen används i chooseVehicle

            while (true)
            {
                Helpers.DrawScreen();                                                                                                           //Rensar och ritar

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
                        Console.WriteLine($"Please try again(invalid input)");
                        Helpers.Pause();      
                        break;
                }
            }
        }
    }
}
