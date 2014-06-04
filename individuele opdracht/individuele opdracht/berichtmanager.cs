using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace individuele_opdracht
{
    public class berichtmanager
    {
        private List<categorie> categorieen;
        private List<bericht> berichten;
        private gebruiker gebruiker;
        private database db;


        //Properties
        public List<categorie> Categorieen { get { return categorieen; } }
        public List<bericht> Berichten { get { return berichten; } }
        public gebruiker Gebruiker { get { return gebruiker; } }



        //Constructor
        /// <summary>
        /// Een BestandManager object instantiëren en een ingelogde gebruiker meegeven
        /// </summary>
        /// <param name="IngelogdeGebruiker">Een gebruikerobject</param>
        public berichtmanager(gebruiker IngelogdeGebruiker)
        {
            this.gebruiker = IngelogdeGebruiker;
            categorieen = new List<categorie>();
            berichten = new List<bericht>();
            db = new database();

        }
        public berichtmanager()
        {
            categorieen = new List<categorie>();
            berichten = new List<bericht>();
            db = new database();
        }

        //Methodes
        /// <summary>
        /// Voeg een Categorie toe aan de lijst met Categorieen, als het commentaar niet al eerder voorkomt in de lijst
        /// </summary>
        /// <param name="categorie">De categorie die toegevoegd moet worden aan de lijst met categorieen</param>
        /// <param name="database">Een boolean of de gegevens uit de applicatie of de database komen</param>
        /// <returns>Een boolean of het toevoegen gelukt is</returns>
        public bool VoegCategorieToe(categorie categorie, bool database)
        {
            db.Connect();
            if (!database)
            {
                foreach (categorie c in categorieen)
                {
                    if (categorie.CategorieNaam == c.CategorieNaam)
                    {
                        return false;
                    }
                }
                if (categorie.OuderCategorie == null)
                {
                    db.CategorieToevoegen(categorie.CategorieNaam, "");
                }
                else
                {
                    db.CategorieToevoegen(categorie.CategorieNaam, categorie.OuderCategorie.CategorieNaam);
                }

                categorieen.Add(categorie);
                return true;
            }
            if (database)
            {
                foreach (categorie c in categorieen)
                {
                    if (categorie.CategorieNaam == c.CategorieNaam)
                    {
                        return false;
                    }
                }
                categorieen.Add(categorie);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Een categorie uit de lijst met categorieen verwijderen
        /// </summary>
        /// <param name="categorie">De categorie die verwijdert moet worden</param>
        /// <returns>Een boolean of het verwijderen gelukt is</returns>
        public bool VerwijderCategorie(categorie categorie)
        {
            foreach (categorie c in categorieen)
            {
                if (categorie.CategorieNaam == c.CategorieNaam)
                {
                    categorieen.Remove(categorie);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Een aantal categorie objecten vullen vanuit de database
        /// </summary>
        public void CategorieVullen()
        {
            db.Connect();
            foreach (string s in db.CatergorieOpvragen())
            {
                string oudercategorie = db.OuderCatergorieOpvragen(s);
                if (oudercategorie == "")
                {
                    VoegCategorieToe(new categorie(s), true);
                }
                else
                {
                    VoegCategorieToe(new categorie(s, new categorie(oudercategorie)), true);
                }
            }
        }
        
        /// <summary>
        /// Een bestand toevoegen aan de lijst met bestanden
        /// </summary>
        /// <param name="bestand">Het bestand dat toegevoegd moet worden aan de lijst</param>
        /// <param name="database">Een boolean of de gegevens uit de applicatie of de database komen</param>
        /// <returns>Een boolean of het vullen gelukt is</returns>
        public bool VoegBestandToe(bericht bericht, bool database)
        {
            if (!database)
            {
                foreach (bericht b in berichten)
                {
                    if (b.Titel == bericht.Titel)
                    {
                        return false;
                    }
                }
                if (bericht.BerichtCategorie == null)
                {
                    db.BerichtToevoegen(bericht.Titel, bericht.Likes, bericht.Dislikes, bericht.Reports, bericht.Berichtlocatie, bericht.Gebruikersnummer, null);
                }
                else
                {
                    db.BerichtToevoegen(bericht.Titel, bericht.Likes, bericht.Dislikes, bericht.Reports, bericht.Berichtlocatie, bericht.Gebruikersnummer, bericht.BerichtCategorie.CategorieNaam);
                }

                berichten.Add(bericht);
                return true;
            }
            if (database)
            {
                foreach (bericht b in berichten)
                {
                    if (b.Titel == bericht.Titel)
                    {
                        return false;
                    }
                }
                berichten.Add(bericht);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Een bestand uit de lijst met bestanden verwijderen
        /// </summary>
        /// <param name="bestand">Het bestand dat verwijdert moet worden</param>
        /// <returns>Een boolean of het verwijderen gelukt is</returns>
        public bool VerwijderBestand(bericht bericht)
        {
            db.Connect();
            db.DeleteBericht(bericht.Titel);
            foreach (bericht b in berichten)
            {
                if (b.Titel == bericht.Titel)
                {
                    berichten.Remove(b);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Een aantal bestand objecten vullen vanuit de database
        /// </summary>
        public void BestandenVullen()
        {
            db.Connect();
            foreach (string s in db.BerichtOpvragen())
            {
                int Sublikes = s.IndexOf(".NUMBER1."); 
                int Subdislikes = s.IndexOf(".NUMBER2."); 
                int Subreports = s.IndexOf(".NUMBER3."); 
                int Subbestandpad = s.IndexOf(".NUMBER4.");
                int Subgebruikersnummer = s.IndexOf(".NUMBER5.");
                int Subbestandtype = s.IndexOf(".NUMBER6.");


                string naam = s.Substring(0, Sublikes);
                int likes = Convert.ToInt32(s.Substring(Sublikes + 9, Subdislikes - Sublikes - 9));
                int dislikes = Convert.ToInt32(s.Substring(Subdislikes + 9, Subreports - Subdislikes - 9));
                int reports = Convert.ToInt32(s.Substring(Subreports + 9, Subbestandpad - Subreports - 9));
                string bestandpad = s.Substring(Subbestandpad + 9, Subgebruikersnummer - Subbestandpad - 9);
                int gebruikersnummer = Convert.ToInt32(s.Substring(Subgebruikersnummer + 9, Subbestandtype - Subgebruikersnummer - 9));
                string bestandtype = s.Substring(Subbestandtype + 9);
                string oudercategorie = db.OuderCatergorieBerichtOpvragen(naam);

                if (oudercategorie == "")
                {
                    VoegBestandToe(new bericht(naam, bestandpad, db.GebruikersNaamOphalen(gebruikersnummer), gebruikersnummer, likes, dislikes, reports), true);
                }
                else
                {
                    VoegBestandToe(new bericht(naam, bestandpad, db.GebruikersNaamOphalen(gebruikersnummer), gebruikersnummer, likes, dislikes, reports, new categorie(oudercategorie)), true);
                }
            }
        }

        /// <summary>
        /// Een connectie met de database openen
        /// </summary>
        public void ConnectDB()
        {
            db.Connect();
        }

        /// <summary>
        /// Een connectie met de database sluiten
        /// </summary>
        public void CloseDB()
        {
            db.Close();
        }

        /// <summary>
        /// Het aantal likes in de database van een bestand updaten
        /// </summary>
        /// <param name="likes">Het aantal likes</param>
        /// <param name="naam">De naam van het bestand waaraan het toegevoegd moet worden</param>
        public void UpdateLikes(int likes, string naam)
        {
            db.UpdateLikes(likes, naam);
        }

        /// <summary>
        /// Het aantal dislikes in de database van een bestand updaten
        /// </summary>
        /// <param name="dislikes">Het aantal dislikes</param>
        /// <param name="naam">De naam van het bestand waaraan het toegevoegd moet worden</param>
        public void UpdateDislikes(int dislikes, string naam)
        {
            db.Updatedislikes(dislikes, naam);
        }

        /// <summary>
        /// Het aantal reports in de database van een bestand updaten
        /// </summary>
        /// <param name="reports">Het aantal reports</param>
        /// <param name="naam">De naam van het bestand waaraan het toegevoegd moet worden</param>
        public void UpdateReports(int reports, string naam)
        {
            db.UpdateReports(reports, naam);
        }
    }
}