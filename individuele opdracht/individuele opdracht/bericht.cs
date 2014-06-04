using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace individuele_opdracht
{
    public class bericht
    {
        private database db;
        private string titel;
        private string berichtlocatie;
        private string gebruikersnaam;
        private categorie berichtcategorie;
        private List<reactie> reacties;
        private int likes;
        private int dislikes;
        private int reports;
        private int gebruikersnummer;

        public string Titel { get { return titel; } }
        public string Berichtlocatie { get { return berichtlocatie; } }
        public string Gebruikersnaam { get { return gebruikersnaam; } }
        public categorie BerichtCategorie { get { return berichtcategorie; } }
        public List<reactie> reactie { get { return reacties; } }
        public int Likes { get { return likes; } }
        public int Dislikes { get { return dislikes; } }
        public int Reports { get { return reports; } }
        public int Gebruikersnummer { get { return gebruikersnummer; } }

        //Constructor
        /// <summary>
        /// Een bestand object instantiëren zonder bovenliggende categorie waarbij de likes, dislikes en reports nog niet ingevuld zijn
        /// </summary>
        /// <param name="titel">De titel van het bestand</param>
        /// <param name="bestandtype">Het type van het bestand</param>
        /// <param name="bestandlocatie">De locatie van het bestand</param>
        /// <param name="gebruikersnaam">De gebruikersnaam van de persoon die het bestand upload</param>
        /// <param name="gebruikersnummer">Het gebruikersnummer van de persoon die het bestand upload</param>
        public bericht(string titel, string berichtlocatie, string gebruikersnaam, int gebruikersnummer)
        {
            this.titel = titel;
            this.berichtlocatie = berichtlocatie;
            this.gebruikersnaam = gebruikersnaam;
            this.berichtcategorie = null;
            reacties = new List<reactie>();
            this.likes = 0;
            this.dislikes = 0;
            this.reports = 0;
            this.gebruikersnummer = gebruikersnummer;
            db = new database();
        }

        /// <summary>
        /// Een bestand object instantiëren met een bovenliggende categorie waarbij de likes, dislikes en reports nog niet ingevuld zijn
        /// </summary>
        /// <param name="titel">De titel van het bestand</param>
        /// <param name="bestandtype">Het type van het bestand</param>
        /// <param name="bestandlocatie">De locatie van het bestand</param>
        /// <param name="gebruikersnaam">De gebruikersnaam van de persoon die het bestand upload</param>
        /// <param name="gebruikersnummer">Het gebruikersnummer van de persoon die het bestand upload</param>
        /// <param name="bestandcategorie">De bovenliggende categorie</param>
        public bericht(string titel, string berichtlocatie, string gebruikersnaam, int gebruikersnummer, categorie berichtcategorie)
        {
            this.titel = titel;
            this.berichtlocatie = berichtlocatie;
            this.gebruikersnaam = gebruikersnaam;
            this.berichtcategorie = berichtcategorie;
            reacties = new List<reactie>();
            this.likes = 0;
            this.dislikes = 0;
            this.reports = 0;
            this.gebruikersnummer = gebruikersnummer;
            db = new database();
        }

        /// <summary>
        /// Een bestand object instantiëren zonder bovenliggende categorie waarbij de likes, dislikes en reports al ingevuld zijn (Vanuit de database)
        /// </summary>
        /// <param name="titel">De titel van het bestand</param>
        /// <param name="bestandtype">Het type van het bestand</param>
        /// <param name="bestandlocatie">De locatie van het bestand</param>
        /// <param name="gebruikersnaam">De gebruikersnaam van de persoon die het bestand upload</param>
        /// <param name="gebruikersnummer">Het gebruikersnummer van de persoon die het bestand upload</param>
        /// <param name="likes">Het aantal likes van het bestand</param>
        /// <param name="dislikes">Het aantal dislikes van het bestand</param>
        /// <param name="reports">Het aantal reports van het bestand</param>
        public bericht(string titel, string berichtlocatie, string gebruikersnaam, int gebruikersnummer, int likes, int dislikes, int reports)
        {
            this.titel = titel;
            this.berichtlocatie = berichtlocatie;
            this.gebruikersnaam = gebruikersnaam;
            this.berichtcategorie = null;
            reacties = new List<reactie>();
            this.likes = likes;
            this.dislikes = dislikes;
            this.reports = reports;
            this.gebruikersnummer = gebruikersnummer;
            db = new database();
        }

        /// <summary>
        /// Een bestand object instantiëren met bovenliggende categorie waarbij de likes, dislikes en reports al ingevuld zijn (Vanuit de database)
        /// </summary>
        /// <param name="titel">De titel van het bestand</param>
        /// <param name="bestandtype">Het type van het bestand</param>
        /// <param name="bestandlocatie">De locatie van het bestand</param>
        /// <param name="gebruikersnaam">De gebruikersnaam van de persoon die het bestand upload</param>
        /// <param name="gebruikersnummer">Het gebruikersnummer van de persoon die het bestand upload</param>
        /// <param name="likes">Het aantal likes van het bestand</param>
        /// <param name="dislikes">Het aantal dislikes van het bestand</param>
        /// <param name="reports">Het aantal reports van het bestand</param>
        /// <param name="bestandcategorie">De bovenliggende categorie</param>
        public bericht(string titel, string berichtlocatie, string gebruikersnaam, int gebruikersnummer, int likes, int dislikes, int reports, categorie berichtcategorie)
        {
            this.titel = titel;
            this.berichtlocatie = berichtlocatie;
            this.gebruikersnaam = gebruikersnaam;
            this.berichtcategorie = berichtcategorie;
            reacties = new List<reactie>();
            this.likes = likes;
            this.dislikes = dislikes;
            this.reports = reports;
            this.gebruikersnummer = gebruikersnummer;
            db = new database();
        }


        //Methodes
        /// <summary>
        /// Voeg een commentaar toe aan de lijst met commentaren, als het commentaar niet al eerder voorkomt in de lijst
        /// </summary>
        /// <param name="commentaar">Het commentaar dat toegevoegd word aan de lijst</param>
        /// <param name="database">Een boolean die onderscheid maakt tussen toegevoegd worden vanuit de database, of de applicatie zelf. True = Vanuit DB, False = Vanuit Applicatie</param>
        /// <returns>Een boolean die aangeeft of het toevoegen gelukt is of niet</returns>
        public bool VoegReactieToe(reactie reactie, bool database)
        {
            if (!database)
            {
                db.Connect();
                foreach (reactie r in reacties)
                {
                    if (r.Inhoud == reactie.Inhoud)
                    {
                        return false;
                    }
                }
                db.ReactieToevoegen(reactie.Inhoud, reactie.Likes, reactie.Dislikes, reactie.Reports, reactie.GebruikerNR, Titel);
                reacties.Add(reactie);
                db.Close();
                return true;
            }
            if (database)
            {
                foreach (reactie r in reacties)
                {
                    if (r.Inhoud == reactie.Inhoud)
                    {
                        return false;
                    }
                }
                reacties.Add(reactie);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Alle commentaren uit de database toevoegen aan de lijst met commentaren
        /// </summary>
        /// <param name="titel">De titel van het bestandnaam aan welk het commentaar moet worden toegevoegd</param>
        public void ReactieVullen(string titel)
        {
            db.Connect();
            foreach (string s in db.ReactieOpvragen())
            {
                int sublikes = s.IndexOf(".NUMBER1."); 
                int subdislikes = s.IndexOf(".NUMBER2."); 
                int subreports = s.IndexOf(".NUMBER3."); 
                int subgebruikersnummer = s.IndexOf(".NUMBER4.");
                int subberichtnaam = s.IndexOf(".NUMBER5.");

                string tekst = s.Substring(0, sublikes);
                int likes = Convert.ToInt32(s.Substring(sublikes + 9, subdislikes - sublikes - 9));
                int dislikes = Convert.ToInt32(s.Substring(subdislikes + 9, subreports - subdislikes - 9));
                int reports = Convert.ToInt32(s.Substring(subreports + 9, subgebruikersnummer - subreports - 9));
                int gebruikersnummer = Convert.ToInt32(s.Substring(subgebruikersnummer + 9, subberichtnaam - subgebruikersnummer - 9));
                string berichtnaam = s.Substring(subberichtnaam + 9);
                string gebruikersnaam = db.GebruikersNaamOphalen(gebruikersnummer);
                if (berichtnaam == titel)
                {
                    VoegReactieToe(new reactie(gebruikersnaam, gebruikersnummer, tekst, likes, dislikes, reports), true);
                }
            }
            db.Close();
        }

        /// <summary>
        /// Verwijder een commentaar van de lijst met commentaren
        /// </summary>
        /// <param name="commentaar"></param>
        /// <returns>Boolean returnt true als verwijderen lukt, false als het niet lukt</returns>
        public bool Verwijderreactie(reactie reactie)
        {
            foreach (reactie r in reacties)
            {
                if (r.Inhoud == reactie.Inhoud)
                {
                    reacties.Remove(r);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Een comment verwijderen uit de database
        /// </summary>
        /// <param name="comment">De inhoud van het commentaar</param>
        public void DeleteComment(string reactie)
        {
            db.Connect();
            db.DeleteReactie(reactie);
            db.Close();
        }

        /// <summary>
        /// 3 Methodes om de likes, dislikes en reports met 1 op te tellen
        /// </summary>
        public void Like()
        {
            this.likes += 1;
        }
        public void Dislike()
        {
            this.dislikes += 1;
        }
        public void Report()
        {
            this.reports += 1;
        }

        /// <summary>
        /// Klasse als string weergeven
        /// </summary>
        /// <returns>Klasse als string</returns>
        public override string ToString()
        {
            return "Bestand: " + titel  + " - Gedeeld door: " + gebruikersnaam + " L: " + likes + " D: " + dislikes + " R: " + reports;
        }

        /// <summary>
        /// Verbinden met de database en het aantal likes updaten van een commentaar
        /// </summary>
        /// <param name="likes">Het aantal likes van het commentaar</param>
        /// <param name="naam">De inhoud van het commentaar</param>
        public void UpdateReactieLikes(int likes, string naam)
        {
            db.Connect();
            foreach (reactie r in reacties)
            {
                if (r.Inhoud == naam)
                {
                    db.UpdateLikesReactie(r.Likes, r.Inhoud);
                }
            }
            db.Close();
        }

        /// <summary>
        /// Verbinden met de database en het aantal dislikes updaten van een commentaar
        /// </summary>
        /// <param name="dislikes">Het aantal dislikes van het commentaar</param>
        /// <param name="naam">De inhoud van het commentaar</param>
        public void UpdateReactieDislikes(int dislikes, string naam)
        {
            db.Connect();
            foreach (reactie r in reacties)
            {
                if (r.Inhoud == naam)
                {
                    db.UpdatedislikesReactie(dislikes, naam);
                }
            }
            db.Close();
        }

        /// <summary>
        /// Verbinden met de database en het aantal reports updaten van een commentaar
        /// </summary>
        /// <param name="reports">Het aantal reports van het commentaar</param>
        /// <param name="naam">De inhoud van het commentaar</param>
        public void UpdateCommentReports(int reports, string naam)
        {
            db.Connect();
            foreach (reactie r in reacties)
            {
                if (r.Inhoud == naam)
                {
                    db.UpdateReportsReactie(reports, naam);
                }
            }
            db.Close();
        }
    }
}