using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace individuele_opdracht
{
    public partial class Inlogsysteem : System.Web.UI.Page
    {
        berichtmanager bm;
        database d = new database();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                d.Connect();

            }
            catch (OracleNullValueException ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Kan geen verbinding maken met de database');", true);
            }
            finally
            {
                d.Close();
            }
        }
        protected void Button1_click(object sender, EventArgs e)
        {
            d.Open();
            if (d.Login(this.inlognaam.Text, this.wachtwoord.Text))
            {
                Session["gebruiker"] = inlognaam.Text;
                FormsAuthentication.RedirectFromLoginPage(inlognaam.Text, Persist.Checked);
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Gegevens komen niet overeen.');", true);
            }
        }

    }
}