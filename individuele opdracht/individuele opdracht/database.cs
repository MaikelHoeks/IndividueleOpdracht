using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace individuele_opdracht
{
    class database
    {
        private OracleConnection con;

        public database()
        {
            con = new OracleConnection();
        }
        /// <summary>
        /// Hier wordt de connectie gemaakt met de database
        /// </summary>
        public void Connect()
        {
            try
            {
                string pcn = "dbi260972";
                string pw = "UfvgV5Mk53";
                con.ConnectionString = "User Id=" + pcn + ";Password=" + pw + ";Data Source=" + " //192.168.15.50:1521/fhictora" + ";";
                con.Open();
                Console.WriteLine("Connected to Oracle" + con.ServerVersion);
            }
            catch { }
        }
        /// <summary>
        /// hier wordt de connectie gesloten met de database
        /// </summary>
        public void Close()
        {
            try
            {
                con.Close();
            }
            catch { }

        }

        public void Open()
        {
            try
            {
                con.Open();
            }
            catch { }
        }
        /// <summary>
        /// hier wordt gekeken of je toegang hebt tot de applicatie
        /// </summary>
        /// <param name="gebruikersnaam"></param>
        /// <param name="wachtwoord"></param>
        public bool Login(string gebruikersnaam, string wachtwoord)
        {
            int gebruikersnummer;
            string query = "SELECT GEBRUIKERSNAAM, WACHTWOORD, GEBRUIKERNR FROM GEBRUIKER WHERE GEBRUIKERSNAAM=:gebruikersnaam and WACHTWOORD=:wachtwoord";
            OracleCommand om = new OracleCommand(query, con);
            om.Parameters.Add(":gebruikersnaam", gebruikersnaam);
            om.Parameters.Add(":wachtwoord", wachtwoord);
            try
            {
                OracleDataReader reader = om.ExecuteReader();
                bool hasRow = reader.Read();
                if (hasRow)
                {
                    gebruikersnummer = reader.GetInt32(2);
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// hier wordt een categorie toegevoegd aan de database
        /// </summary>
        /// <param name="naam"></param>
        /// <param name="ouder"></param>
        public void CategorieToevoegen(string naam, string ouder)
        {
            OracleCommand om = new OracleCommand("INSERT INTO CATEGORIE(NAAM, OUDERCATEGORIE) VALUES(:naam,:ouder)", con);
            om.Parameters.Add(":naam", naam);
            om.Parameters.Add(":ouder", ouder);

            try
            {
                int affectedRows = om.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    //MessageBox.Show("succesvol toegevoegd!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //MessageBox.Show("Insert failed!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { }
        }
        /// <summary>
        /// hier krijg je een lijst met bestanden terug van de database
        /// </summary>
        /// <returns></returns>
        public List<string> BerichtOpvragen()
        {
            List<string> bestand = new List<string>();
            string naam = "";
            int likes = 0;
            int dislikes = 0;
            int reports = 0;
            string bestandpad = "";
            int gebruikersnummer = 0;
            string bestandstype;

            string samen = "";

            string query = "SELECT BESTANDSNAAM, LIKES, DISLIKES, GERAPPORTEERD, BESTANDPAD, GEBRUIKERNR, BESTANDSTYPE FROM BESTAND";
            OracleCommand oc = new OracleCommand(query, con);
            OracleDataReader reader = oc.ExecuteReader();
            reader = oc.ExecuteReader();
            while (reader.Read())
            {
                naam = Convert.ToString(reader["BESTANDSNAAM"]);
                likes = Convert.ToInt32(reader["LIKES"]);
                dislikes = Convert.ToInt32(reader["DISLIKES"]);
                reports = Convert.ToInt32(reader["GERAPPORTEERD"]);
                bestandpad = Convert.ToString(reader["BESTANDPAD"]);
                gebruikersnummer = Convert.ToInt32(reader["GEBRUIKERNR"]);
                bestandstype = Convert.ToString(reader["BESTANDSTYPE"]);
                samen = naam + ".NUMBER1." + likes + ".NUMBER2." + dislikes + ".NUMBER3." + reports + ".NUMBER4." + bestandpad + ".NUMBER5." + gebruikersnummer + ".NUMBER6." + bestandstype;
                bestand.Add(samen);
            }
            return bestand;
        }
        /// <summary>
        /// hier wordt een bestand toegevoegd aan de database
        /// </summary>
        /// <param name="naam"></param>
        /// <param name="likes"></param>
        /// <param name="dislikes"></param>
        /// <param name="geraporteerd"></param>
        /// <param name="bestandpad"></param>
        /// <param name="gebruikernr"></param>
        /// <param name="categorie"></param>
        public void BerichtToevoegen(string naam, int likes, int dislikes, int geraporteerd, string bestandpad, int gebruikernr, string categorie)
        {
            OracleCommand om = new OracleCommand("INSERT INTO BESTAND(BESTANDSNAAM, LIKES, DISLIKES, GERAPPORTEERD, BESTANDPAD, GEBRUIKERNR, CATEGORIE) VALUES(:naam,:likes, : dislikes, :geraporteerd, :bestandpad,  :gebruikernr, :categorie)", con);
            om.Parameters.Add(":naam", naam);
            om.Parameters.Add(":likes", likes);
            om.Parameters.Add(":dislikes", dislikes);
            om.Parameters.Add(":geraporteerd", geraporteerd);
            om.Parameters.Add(":bestandpad", bestandpad);
            om.Parameters.Add(":gebruikernr", gebruikernr);
            om.Parameters.Add(":categorie", categorie);


            try
            {
                int affectedRows = om.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    //MessageBox.Show("succesvol toegevoegd!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //MessageBox.Show("Insert failed!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { }
        }
        /// <summary>
        /// hier wordt een bestand verwijdert uit de database
        /// </summary>
        /// <param name="bestandsnaam"></param>
        public void DeleteBericht(string bestandsnaam)
        {
            OracleCommand om = new OracleCommand("", con);
            om.CommandText = "DELETE FROM BESTAND WHERE BESTANDSNAAM=:bestandsnaam";
            om.Parameters.Add(":bestandsnaam", bestandsnaam);
            try
            {
                om.CommandType = CommandType.Text;
                om.ExecuteReader();
                om.Dispose();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// hier worden de categorieen uit de database gehaald
        /// </summary>
        /// <returns></returns>
        public List<string> CatergorieOpvragen()
        {
            List<string> categorie = new List<string>();
            string query = "SELECT NAAM FROM CATEGORIE";
            OracleCommand oc = new OracleCommand(query, con);
            OracleDataReader reader = oc.ExecuteReader();
            reader = oc.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    categorie.Add(reader.GetString(i));
                }
            }
            return categorie;
        }
        /// <summary>
        /// hier wordt de oudercategorie uit de database gehaald
        /// </summary>
        /// <param name="naam"></param>
        /// <returns></returns>
        public string OuderCatergorieOpvragen(string naam)
        {
            string query = "SELECT OUDERCATEGORIE FROM CATEGORIE WHERE NAAM =:naam";
            OracleCommand oc = new OracleCommand(query, con);
            oc.Parameters.Add(":naam", naam);
            OracleDataReader reader = oc.ExecuteReader();
            reader = oc.ExecuteReader();
            if (reader.HasRows)
            {
                naam = oc.ExecuteScalar().ToString();
            }
            return naam;
        }
        /// <summary>
        /// de bestanden opvragen die in de database staan
        /// </summary>
        /// <param name="bestandnaam"></param>
        /// <returns></returns>
        public string OuderCatergorieBerichtOpvragen(string bestandnaam)
        {
            string query = "SELECT CATEGORIE FROM BESTAND WHERE BESTANDSNAAM =:bestandnaam";
            OracleCommand oc = new OracleCommand(query, con);
            oc.Parameters.Add(":bestandnaam", bestandnaam);
            OracleDataReader reader = oc.ExecuteReader();
            reader = oc.ExecuteReader();
            if (reader.HasRows)
            {
                bestandnaam = oc.ExecuteScalar().ToString();
            }
            return bestandnaam;
        }
        /// <summary>
        /// de naam van de gebruiker ophalen uit de database
        /// </summary>
        /// <param name="gebruikersnummer"></param>
        /// <returns></returns>
        public string GebruikersNaamOphalen(int gebruikersnummer)
        {
            string naam = "";
            string query = "SELECT GEBRUIKERSNAAM FROM GEBRUIKER WHERE GEBRUIKERNR =:gebruikersnummer";
            OracleCommand oc = new OracleCommand(query, con);
            oc.Parameters.Add(":gebruikersnummer", gebruikersnummer);
            OracleDataReader reader = oc.ExecuteReader();
            reader = oc.ExecuteReader();
            if (reader.HasRows)
            {
                naam = Convert.ToString(oc.ExecuteScalar().ToString());
            }
            return naam;
        }
        /// <summary>
        /// verandert de likes in het bestand met +1
        /// </summary>
        /// <param name="likes"></param>
        /// <param name="naam"></param>
        public void UpdateLikes(int likes, string naam)
        {
            OracleCommand om = new OracleCommand("", con);
            om.CommandText = "UPDATE BESTAND SET LIKES=:likes  WHERE BESTANDSNAAM= :naam ";
            om.Parameters.Add(":likes", likes);
            om.Parameters.Add(":naam", naam);
            try
            {
                om.CommandType = CommandType.Text;
                om.ExecuteReader();
                om.Dispose();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// verandert de dislikes in het bestand met +1
        /// </summary>
        /// <param name="dislikes"></param>
        /// <param name="naam"></param>
        public void Updatedislikes(int dislikes, string naam)
        {
            OracleCommand om = new OracleCommand("", con);
            om.CommandText = "UPDATE BESTAND SET DISLIKES=:dislikes WHERE BESTANDSNAAM= :naam ";
            om.Parameters.Add(":dislikes", dislikes);
            om.Parameters.Add(":naam", naam);
            try
            {
                om.CommandType = CommandType.Text;
                om.ExecuteReader();
                om.Dispose();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// verandert de reports in het bestand met +1
        /// </summary>
        /// <param name="reports"></param>
        /// <param name="naam"></param>
        public void UpdateReports(int reports, string naam)
        {
            OracleCommand om = new OracleCommand("", con);
            om.CommandText = "UPDATE BESTAND SET GERAPPORTEERD=:reports WHERE BESTANDSNAAM=:naam ";
            om.Parameters.Add(":naam", naam);
            om.Parameters.Add(":reports", reports);
            try
            {
                om.CommandType = CommandType.Text;
                om.ExecuteReader();
                om.Dispose();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Haalt alle commentaar uit de database
        /// </summary>
        /// <returns></returns>
        public List<string> ReactieOpvragen()
        {
            List<string> comment = new List<string>();
            string tekst = "";
            int likes = 0;
            int dislikes = 0;
            int reports = 0;
            int gebruikersnummer = 0;
            string bestandsnaam = "";

            string samen = "";

            string query = "SELECT TEKST, COMMENTLIKE, COMMENTDISLIKE, COMMENTGERAPPORTEERD, GEBRUIKERNR, BESTANDSNAAM FROM  COMMENTAAR";
            OracleCommand oc = new OracleCommand(query, con);
            OracleDataReader reader = oc.ExecuteReader();
            reader = oc.ExecuteReader();
            while (reader.Read())
            {
                tekst = Convert.ToString(reader["TEKST"]);
                likes = Convert.ToInt32(reader["COMMENTLIKE"]);
                dislikes = Convert.ToInt32(reader["COMMENTDISLIKE"]);
                reports = Convert.ToInt32(reader["COMMENTGERAPPORTEERD"]);
                gebruikersnummer = Convert.ToInt32(reader["GEBRUIKERNR"]);
                bestandsnaam = Convert.ToString(reader["BESTANDSNAAM"]);
                samen = tekst + ".NUMBER1." + likes + ".NUMBER2." + dislikes + ".NUMBER3." + reports + ".NUMBER4. " + gebruikersnummer + ".NUMBER5." + bestandsnaam;
                comment.Add(samen);
            }
            return comment;
        }
        /// <summary>
        /// commentaar wordt toegevoegd aan de database
        /// </summary>
        /// <param name="tekst"></param>
        /// <param name="likes"></param>
        /// <param name="dislikes"></param>
        /// <param name="geraporteerd"></param>
        /// <param name="gebruikernr"></param>
        /// <param name="bestand"></param>
        public void ReactieToevoegen(string tekst, int likes, int dislikes, int geraporteerd, int gebruikernr, string bestand)
        {
            OracleCommand om = new OracleCommand("INSERT INTO COMMENTAAR (TEKST, COMMENTLIKE, COMMENTDISLIKE, COMMENTGERAPPORTEERD, GEBRUIKERNR, BESTANDSNAAM) VALUES(:tekst,:likes, :dislikes, :geraporteerd,:gebruikernr, :bestand)", con);
            om.Parameters.Add(":tekst", tekst);
            om.Parameters.Add(":likes", likes);
            om.Parameters.Add(":dislikes", dislikes);
            om.Parameters.Add(":geraporteerd", geraporteerd);
            om.Parameters.Add(":gebruikernr", gebruikernr);
            om.Parameters.Add(":bestand ", bestand);


            try
            {
                int affectedRows = om.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    // MessageBox.Show("succesvol toegevoegd!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // MessageBox.Show("Insert failed!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// verandert de likes in het commentaar met +1
        /// </summary>
        /// <param name="likes"></param>
        /// <param name="naam"></param>
        public void UpdateLikesReactie(int likes, string naam)
        {
            OracleCommand om = new OracleCommand("", con);
            om.CommandText = "UPDATE COMMENTAAR SET COMMENTLIKE=:likes WHERE TEKST= :naam ";
            om.Parameters.Add(":naam", naam);
            om.Parameters.Add(":likes", likes);
            try
            {
                om.CommandType = CommandType.Text;
                om.ExecuteReader();
                om.Dispose();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// verandert de dislikes in het commentaar met +1
        /// </summary>
        /// <param name="dislikes"></param>
        /// <param name="naam"></param>
        public void UpdatedislikesReactie(int dislikes, string naam)
        {
            OracleCommand om = new OracleCommand("", con);
            om.CommandText = "UPDATE COMMENTAAR SET COMMENTDISLIKE =:dislikes WHERE TEKST = :naam";
            om.Parameters.Add(":dislikes", dislikes);
            om.Parameters.Add(":naam", naam);
            try
            {
                om.CommandType = CommandType.Text;
                om.ExecuteReader();
                om.Dispose();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// verandert de reports in het commentaar met +1
        /// </summary>
        /// <param name="reports"></param>
        /// <param name="naam"></param>
        public void UpdateReportsReactie(int reports, string naam)
        {
            OracleCommand om = new OracleCommand("", con);
            om.CommandText = "UPDATE COMMENTAAR SET COMMENTGERAPPORTEERD=:reports WHERE TEKST = :naam";
            om.Parameters.Add(":reports", reports);
            om.Parameters.Add(":naam", naam);
            try
            {
                om.CommandType = CommandType.Text;
                om.ExecuteReader();
                om.Dispose();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// verwijdert commentaar uit de database
        /// </summary>
        /// <param name="tekst"></param>
        public void DeleteReactie(string tekst)
        {
            OracleCommand om = new OracleCommand("", con);
            om.CommandText = "DELETE  FROM COMMENTAAR WHERE TEKST= :tekst";
            om.Parameters.Add(":tekst", tekst);
            try
            {
                om.CommandType = CommandType.Text;
                om.ExecuteReader();
                om.Dispose();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DataTable Getlikes()
        {
            DataTable dt = new DataTable();
            try
            {
                Open();
                OracleCommand om = new OracleCommand("", con);
                om.CommandText = "SELECT COUNT(LIKES) AS LIKES, COUNT(DISLIKES) AS DISLIKES FROM BESTAND";
                om.CommandType = CommandType.Text;
                dt.Load(om.ExecuteReader());
            }
            catch { }
            return dt;
        }
    }
}