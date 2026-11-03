using System;
using System.Collections.Generic;
using System.Text;

namespace EA_ParkingGarageVGUppgift
{
    internal class Helpers
    {
        private static List<string> regPlates = new List<string>                                        //Begränsa åtkomst så att andra klasser inte kan ändra listan. 
        {
            "BMX 939", "GWH 959", "KLS 331", "MEF 031", "HAM 010", "KOD 666", "FBW 112", "BOB 910",
            "KOP 446", "TAL 764", "BUB 990", "HEJ 123", "PAL 458", "GIF 003", "JJB 007", "BAP 559",
            "BSX 939", "GWH 969", "KLT 331", "PEF 031", "HAL 010", "GOD 666", "FCW 112", "BOB 990",
            "SOP 446", "RAL 764", "BEB 990", "HAJ 123", "PAL 558", "GIT 003", "JBL 007", "BRA 559"
        };

        internal static string GetRegPlate()                                                            //static = metoden tillhör klassen(Heleprs) inte ett objekt. Anrop jobbar med samma lista.
        {
            if (regPlates.Count == 0)
            {
                return "Slut på regnummer(Helpers).";                                                   //Om listan är tom. Ingen felhantering i sig.
            }

            int rnd = Random.Shared.Next(regPlates.Count);                                              //Annars ta ett random regnr -> regPlateTxt och ta sedan bort det från Listan regPlate.
            string regPlate = regPlates[rnd];
            regPlates.RemoveAt(rnd);                                                                    //Dict PG kan inte ha samma nyckel.
            return regPlate;
        }

        internal static string GetVehicleColor()                                                        //Anropa görs direkt via klassen(Helpers), utan objektskapande
        {
            List<string> vehicleColor = new List<string>
            {   "red", "blue", "black", "magenta", "purple", "gold with unicorns", "white", "pink", "silver",
                "beige", "rainbow coloured", "grey", "dark green", "baby blue", "yellow", "wine red", "brown"};

            return vehicleColor[Random.Shared.Next(0, vehicleColor.Count)];
        }
    }
}
