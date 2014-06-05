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
                    foreach (bericht b in bm.Berichten)
                    {
                        lbFileSharing.Items.Clear();
                        b.ReactieVullen(b.Titel);
                        foreach (reactie r in b.reactie)
                        {
                            lbFileSharing.Items.Add(r.ToString());
                        }

                    }
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
        protected void btnLike_Click(object sender, EventArgs e)
        {
            string comment = "";
            int likes = 0;
            selectedCategorie = lbFileSharing.SelectedItem.ToString();
            if (lbFileSharing.SelectedItem.ToString().Length > 8)
            {
                if (lbFileSharing.SelectedItem.ToString().Substring(0, 8) == "Bestand:")
                {
                    bm.BestandenVullen();
                    foreach (bericht b in bm.Berichten)
                    {
                        if (lbFileSharing.SelectedItem.ToString() == b.ToString())
                        {
                            b.Like();
                            bm.UpdateLikes(b.Likes, b.Titel);
                            if (selectedCategorie == null)
                            {
                                RefreshList();
                                break;
                            }
                            else
                            {
                                RefreshList(selectedCategorie);
                                break;
                            }
                        }
                    }
                }
            }
            foreach (bericht b in bm.Berichten)
            {
                if (selectedBestand == b.ToString())
                {
                    b.ReactieVullen(b.Titel);
                    foreach (reactie c in b.reactie)
                    {
                        if (lbFileSharing.SelectedItem.Text == c.ToString())
                        {
                            c.Like();
                            comment = c.Inhoud;
                            likes = c.Likes;
                        }
                    }
                    lbFileSharing.Items.Clear();
                    foreach (reactie c in b.reactie)
                    {
                        lbFileSharing.Items.Add(c.ToString());
                    }
                    b.UpdateReactieLikes(likes, comment);
                }
            }
        }

        /// <summary>
        /// Code waarmee dislikes aan bestanden en commentaren kan worden toegevoegd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDislike_Click(object sender, EventArgs e)
        {
            string comment = "";
            int dislikes = 0;
            if (lbFileSharing.SelectedItem.ToString().Length > 8)
            {
                if (lbFileSharing.SelectedItem.ToString().Substring(0, 8) == "Bestand:")
                {
                    bm.BestandenVullen();
                    foreach (bericht b in bm.Berichten)
                    {
                        if (lbFileSharing.SelectedItem.ToString() == b.ToString())
                        {
                            b.Dislike();
                            bm.UpdateDislikes(b.Dislikes, b.Titel);
                            if (selectedCategorie == null)
                            {
                                RefreshList();
                                break;
                            }
                            else
                            {
                                RefreshList(selectedCategorie);
                                break;
                            }
                        }
                    }
                }
            }
            foreach (bericht b in bm.Berichten)
            {
                b.ReactieVullen(b.Titel);
                if (selectedBestand == b.ToString())
                {
                    foreach (reactie c in b.reactie)
                    {
                        if (lbFileSharing.SelectedItem.Text == c.ToString())
                        {
                            c.Dislike();
                            comment = c.Inhoud;
                            dislikes = c.Dislikes;
                        }
                    }
                    lbFileSharing.Items.Clear();
                    foreach (reactie c in b.reactie)
                    {
                        lbFileSharing.Items.Add(c.ToString());
                    }
                    b.UpdateReactieDislikes(dislikes, comment);
                }
            }
        }

        /// <summary>
        /// /// Code waarmee reports aan bestanden en commentaren kan worden toegevoegd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReport_Click(object sender, EventArgs e)
        {
            string comment = "";
            int reports = 0;
            try
            {
                if (lbFileSharing.SelectedItem.ToString().Length > 8)
                {
                    if (lbFileSharing.SelectedItem.ToString().Substring(0, 8) == "Bestand:")
                    {
                        bm.BestandenVullen();
                        foreach (bericht b in bm.Berichten)
                        {
                            if (lbFileSharing.SelectedItem.Text == b.ToString())
                            {
                                if (b.Reports < 4)
                                {
                                    b.Report();

                                    bm.UpdateReports(b.Reports, b.Titel);
                                    break;
                                }
                                else
                                {
                                    foreach (reactie c in b.reactie)
                                    { b.DeleteComment(c.Inhoud); }
                                    bm.VerwijderBestand(b);
                                    b.Report();
                                    return;
                                }
                            }
                        }
                    }
                }
                foreach (bericht b in bm.Berichten)
                {
                    if (selectedBestand == b.ToString())
                    {
                        b.ReactieVullen(b.Titel);
                        foreach (reactie c in b.reactie)
                        {
                            if ((string)lbFileSharing.SelectedItem.Text == c.ToString())
                            {
                                if (c.Reports < 4)
                                {
                                    c.Report();
                                    comment = c.Inhoud;
                                    reports = c.Reports;

                                }
                                else
                                { b.Verwijderreactie(c); b.DeleteComment(c.Inhoud); c.Report(); lbFileSharing.Items.Clear(); foreach (reactie c2 in b.reactie) { lbFileSharing.Items.Add(c2.ToString()); } return; }
                            }
                        }
                        foreach (reactie c in b.reactie)
                        {
                            lbFileSharing.Items.Add(c.ToString());
                        }
                        b.UpdateCommentReports(reports, comment);
                    }
                }
            }
            finally
            {

            }
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
        protected void btnComment_Click(object sender, EventArgs e)
        {
            selectedBestand = lbFileSharing.SelectedItem.ToString();
            bm.BestandenVullen();
            foreach (bericht b in bm.Berichten)
            {
                if (b.ToString() == selectedBestand)
                {
                    bool test = b.VoegReactieToe(new reactie("admin", 11111, tbComment.Text), false);
                    if (test)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Commentaar toegevoegd');", true);
                    }
                    if (!test)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Commentaar niet toegevoegd');", true);
                    }
                    lbFileSharing.Items.Clear();
                    foreach (reactie r in b.reactie)
                    {
                        lbFileSharing.Items.Add(r.ToString());
                    }
                }
            }
            tbComment.Text = "";
        }
    }
}