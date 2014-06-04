using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace individuele_opdracht
{
    public class categorie
    {
        private string categorienaam;
        private categorie oudercategorie;

        //Properties
        public string CategorieNaam { get { return categorienaam; } }
        public categorie OuderCategorie { get { return oudercategorie; } }

        //Constructor
        /// <summary>
        /// Een categorie object instantiëren waarbij er geen bovenliggende categorie is
        /// </summary>
        /// <param name="categorienaam">Naam van de categorie</param>
        public categorie(string categorienaam)
        {
            this.categorienaam = categorienaam;
            this.oudercategorie = null;
        }

        /// <summary>
        /// Een categorie object instantiëren waarbij er wel een bovenliggende categorie is
        /// </summary>
        /// <param name="categorienaam">Naam van de categorie</param>
        /// <param name="categorie">De oudercategorie</param>
        public categorie(string categorienaam, categorie categorie)
        {
            this.categorienaam = categorienaam;
            this.oudercategorie = categorie;
        }

        //Methodes
        public override string ToString()
        {
            return categorienaam;
        }

    }
}