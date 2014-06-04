using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace individuele_opdracht
{
    public class gebruiker
    {
        private string gebruikersnaam;
        private string wachtwoord;
        private int gebruikersnummer;
        
        //Properties
        public string Gebruikersnaam { get { return gebruikersnaam; } }
        public string Wachtwoord { get { return wachtwoord; } }
        public int Gebruikersnummer { get { return gebruikersnummer; } }
        
        //constructor
        /// <summary>
        /// Gebruiker object instantiëren
        /// </summary>
        /// <param name="naam">De naam van ingelogde gebruiker</param>
        /// <param name="wachtwoord">Het wachtwoord van ingelogde gebruiker</param>
        /// <param name="gebruikersnummer">Het gebruikersnummer van ingelogde gebruiker</param>
        public gebruiker(string naam, string wachtwoord, int gebruikersnummer)
        {
            this.gebruikersnaam = naam;
            this.wachtwoord = wachtwoord;
            this.gebruikersnummer = gebruikersnummer;
        }
        ////methodes

        public override string ToString()
        {
            return gebruikersnaam + ", " + wachtwoord + ", " + gebruikersnummer;
        }
    }
}