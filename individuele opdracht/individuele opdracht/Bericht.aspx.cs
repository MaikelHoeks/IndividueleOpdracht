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
    public partial class Bericht : System.Web.UI.Page
    {
        string berichtnaam;
        berichtmanager bm = new berichtmanager();
        database d = new database();
        protected void Page_Load(object sender, EventArgs e)
        {
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
            }
            berichtnaam = Session["Bericht"].ToString();
            bm.BestandenVullen();
            foreach (bericht b in bm.Berichten)
            {
                if (berichtnaam == b.ToString())
                {
                    Labelgegevens.Text = b.ToString();
                    b.ReactieVullen(b.Titel);
                    foreach (reactie r in b.reactie)
                    {
                        Labelgegevens.Text += "<br />" + (r.ToString());
                    }
                }
            }


        }
        protected void btnLike_Click(object sender, EventArgs e)
        {

            foreach (bericht b in bm.Berichten)
            {
                if (berichtnaam == b.ToString())
                {
                    b.Like();
                    bm.UpdateLikes(b.Likes, b.Titel);
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Like is toegevoegd!');", true);
                    Response.Redirect("Bericht.aspx");
                    
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

            foreach (bericht b in bm.Berichten)
            {
                if (berichtnaam == b.ToString())
                {
                    b.Dislike();
                    bm.UpdateLikes(b.Dislikes, b.Titel);
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Dislike is toegevoegd!');", true);
                    break;
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

            foreach (bericht b in bm.Berichten)
            {
                if (berichtnaam == b.ToString())
                {
                    b.Report();
                    bm.UpdateLikes(b.Reports, b.Titel);
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('report is toegevoegd!');", true);
                    break;
                }
            }
        }

        protected void btnComment_Click(object sender, EventArgs e)
        {
            bm.BestandenVullen();
            foreach (bericht b in bm.Berichten)
            {
                if (berichtnaam == b.ToString())
                {
                    bool test = b.VoegReactieToe(new reactie(Session["gebruiker"].ToString(), 22009 , tbComment.Text), false);
                        if (test)
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Commentaar toegevoegd');", true);
                            Response.Redirect("Bericht.aspx");
                        }
                        if (!test)
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Commentaar niet toegevoegd');", true);
                        }
                }
            }
        }
    }
}