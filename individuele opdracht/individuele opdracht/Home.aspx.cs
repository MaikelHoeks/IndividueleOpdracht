using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace individuele_opdracht
{
    public partial class Home : System.Web.UI.Page
    {
        string selectedCategorie;
        string selectedBestand;
        string naam;
        berichtmanager bm = new berichtmanager();
        database d = new database();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            foreach (bericht b in bm.Berichten)
            {
                b.ReactieVullen(b.Titel);
            }
            if (!IsPostBack)
            {
                naam = Session["gebruiker"].ToString();
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('welkom  " + naam + "');", true);
                try
                {
                    d.Connect();
                }
                catch (OracleNullValueException ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Kan geen verbinding maken met de database');", true);
                }
                bm.CategorieVullen();
                bm.BestandenVullen();
                foreach (bericht b in bm.Berichten)
                {
                    b.ReactieVullen(b.Titel);
                }
                RefreshList();
            }
        }
        private void RefreshList()
        {
            lbFileSharing.Items.Clear();
            lbFileSharing.Items.Add("Categorieën:");
            lbFileSharing.Items.Add("");
            foreach (categorie c in bm.Categorieen)
            {
                if (c.OuderCategorie == null)
                {
                    lbFileSharing.Items.Add(c.ToString());
                }
            }
        }

        private void RefreshList(string categorie)
        {
            lbFileSharing.Items.Clear();
            lbFileSharing.Items.Add("Categorieën:");
            lbFileSharing.Items.Add("");
            bm.CategorieVullen();
            bm.BestandenVullen();
            foreach (categorie c in bm.Categorieen)
            {
                try
                {
                    if (c.OuderCategorie.ToString() == categorie)
                    {
                        lbFileSharing.Items.Add(c.ToString());
                    }
                }
                catch { }
            }
            lbFileSharing.Items.Add("");
            lbFileSharing.Items.Add("");
            lbFileSharing.Items.Add("Berichten:");
            lbFileSharing.Items.Add("");
            foreach (bericht b in bm.Berichten)
            {
                try
                {
                    if (b.BerichtCategorie.ToString() == categorie)
                    {
                        lbFileSharing.Items.Add(b.ToString());
                    }
                }
                catch { }
            }
        }

        protected void lbFileSharing_DoubleClick(object sender, EventArgs e)
        {
            d.Open();
            if (lbFileSharing.SelectedItem != null)
            {
                selectedCategorie = lbFileSharing.SelectedItem.ToString();
                if (selectedBestand != null)
                {
                    return;
                }
                if (lbFileSharing.SelectedItem.ToString().Length > 8)
                {

                    bm.BestandenVullen();
                    selectedBestand = lbFileSharing.SelectedItem.ToString();
                    Session["Bericht"] = selectedBestand;
                    Response.Redirect("Bericht.aspx");
                }
                foreach (categorie c in bm.Categorieen)
                {
                    try
                    {
                        if (c.OuderCategorie.ToString() == selectedCategorie)
                        {
                            lbFileSharing.Items.Add(c.ToString());
                        }
                    }
                    catch
                    { }
                }
                RefreshList(selectedCategorie);
            }
            d.Close();
        }

        protected void btnUploadfile_Click(object sender, EventArgs e)
        {
            if (selectedCategorie != "")
            {
                if (tbBericht.Text != "")
                {
                    selectedCategorie = lbFileSharing.SelectedItem.ToString();
                    if (selectedCategorie == null)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Bestanden mogen hier niet toegevoegd worden, Selecteer een categorie');", true);
                    }
                    else
                    {
                        bm.CategorieVullen();
                        foreach (categorie c in bm.Categorieen)
                        {
                            if (c.ToString() == selectedCategorie)
                            {
                                bool test = bm.VoegBestandToe(new bericht(tbTitel.Text, tbBericht.Text, "admin", 11111), false);
                                if (test)
                                {
                                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Bericht toegevoegd');", true);
                                }
                                if (!test)
                                {
                                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Bericht niet toegevoegd');", true);
                                }
                                RefreshList(selectedCategorie);
                                return;
                            }
                        }

                    }
                }
                else
                {
                    //MessageBox.Show("Selecteer eerst een bestand");
                }
            }
        }
        
    }
}