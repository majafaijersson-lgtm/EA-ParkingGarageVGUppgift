using System;
using System.Collections.Generic;
using System.Text;

namespace EA_ParkingGarageVGUppgift
{
    internal class Helpers
    {
        public static string GetRegPlate()
        {
            string regPlateTxt;

            List<string> regPlate = new List<string>
            {
                "BMX 939", "GWH 959", "KLS 331", "MEF 031", "HAM 010", "KOD 666", "FBW 112", "BOB 910",
                "KOP 446", "TAL 764", "BUB 990", "HEJ 123", "PAL 458", "GIF 003", "JJB 007", "BAP 559" };

            if (regPlate.Count == 0)                            //Om listan är tom
            {
                regPlateTxt = "Slut på regnummer(Helpers).";
            }
            else                                                //Annars ta ett random regnr -> regPlateTxt och ta sedan bort det från Listan regPlate.
            {
                int rnd = Random.Shared.Next(regPlate.Count);
                regPlateTxt = regPlate[rnd];
                regPlate.RemoveAt(rnd);
            }

            return regPlateTxt;
        }

        public static string GetVehicleColor()
        {
            List<string> vehicleColor = new List<string>
            {   "red", "blue", "black", "magenta", "purple", "gold with unicorns", "white", "pink", "silver",
                "beige", "rainbow coloured", "grey", "dark green", "baby blue", "yellow", "wine red", "brown"};

            return vehicleColor[Random.Shared.Next(0, vehicleColor.Count)];
        }

    }
}
