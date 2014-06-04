using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace individuele_opdracht
{
    public class reactie
    {
        //Datavelden
        private string gebruiker;
        private int gebruikernr;
        private string inhoud;
        private int likes;
        private int dislikes;
        private int reports;

        //Properties
        public string Gebruiker { get { return gebruiker; } }
        public int GebruikerNR { get { return gebruikernr; } }
        public string Inhoud { get { return inhoud; } }
        public int Likes { get { return likes; } }
        public int Dislikes { get { return dislikes; } }
        public int Reports { get { return reports; } }

        //Constructor
        /// <summary>
        /// Een Commentaar object instantiëren waarbij de likes, dislikes & reports null zijn
        /// </summary>
        /// <param name="gebruiker">De gebruikersnaam van de persoon die het commentaar plaatst</param>
        /// <param name="gebruikernr">Het gebruikersnummer van de persoon die het commentaar plaatst</param>
        /// <param name="inhoud">Het commentaar zelf</param>
        public reactie(string gebruiker, int gebruikernr, string inhoud)
        {
            this.gebruiker = gebruiker;
            this.gebruikernr = gebruikernr;
            this.inhoud = inhoud;
        }

        /// <summary>
        /// Een Commentaar object instantiëren waarbij de likes, dislikes & reports al ingevuld zijn (Vanuit de Database)
        /// </summary>
        /// <param name="gebruiker">De gebruikersnaam van de persoon die het commentaar plaatst</param>
        /// <param name="gebruikernr">Het gebruikersnummer van de persoon die het commentaar plaatst</param>
        /// <param name="inhoud">Het commentaar zelf</param>
        /// <param name="likes">Het aantal likes van het commentaar</param>
        /// <param name="dislikes">Het aantal dislikes van het commentaar</param>
        /// <param name="reports">Het aantal reports van het commentaar</param>
        public reactie(string gebruiker, int gebruikernr, string inhoud, int likes, int dislikes, int reports)
        {
            this.gebruiker = gebruiker;
            this.gebruikernr = gebruikernr;
            this.inhoud = inhoud;
            this.likes = likes;
            this.dislikes = dislikes;
            this.reports = reports;
            
        }
        ////Methodes
        public override string ToString()
        {
            return gebruiker + ": " + inhoud + " - " + " L: " + likes + " D: " + dislikes + " R: " + reports;
        }

        //Likes met 1 verhogen
        public void Like()
        {
            this.likes += 1;
        }

        //Dislikes met 1 verhogen
        public void Dislike()
        {
            this.dislikes += 1;
        }

        //Report met 1 verhogen
        public void Report()
        {
            this.reports += 1;
        }
    }
}
