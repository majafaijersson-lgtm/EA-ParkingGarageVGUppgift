using System;
using System.Collections.Generic;
using System.Text;

namespace EA_ParkingGarageVGUppgift
{
    internal class ParkingGarage
    {
        public int AmountParkingSpaces { get; } //Endast get;

        public ParkingGarage()
        {
            AmountParkingSpaces = 15; 
            
        }
    }
}
