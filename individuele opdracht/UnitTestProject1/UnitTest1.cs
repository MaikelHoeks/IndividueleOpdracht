using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using individuele_opdracht;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodLogin()
        {
            database db = new database();
            bool expected = true;
            bool actual;
            actual = db.Login("admin", "admin");
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("werkt niet");
        }

        [TestMethod]
        public void TestVoegCategorieToe()
        {
            berichtmanager bm = new berichtmanager();
            bool expected = true;
            bool actual;
            actual = bm.VoegCategorieToe(new categorie("Test"), true);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Controleer de test methode");
        }

        [TestMethod]
        public void TestVoegReactieToe()
        {
            bericht b = new bericht("test", "test", "admin", 22009);
            bool expected = true;
            bool actual;
            actual = b.VoegReactieToe(new reactie("admin", 22009, "test is gelukt"), true);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Controleer de test methode");
        }

        [TestMethod]
        public void TestVerwijderReactie()
        {
            bericht b = new bericht("test", "test", "admin", 22009);
            bool expected = true;
            bool actual;
            actual = b.Verwijderreactie(new reactie("admin",22009,"test"));
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Controleer de test methode");
        }

        [TestMethod]
        public void TestVerwijderCategorie()
        {
            berichtmanager bm = new berichtmanager();
            bool expected = true;
            bool actual;
            actual = bm.VerwijderCategorie(new categorie("Test"));
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Controleer de test methode");
        }

        [TestMethod]
        public void TestVoegBestandToe()
        {
            berichtmanager bm = new berichtmanager();
            bool expected = true;
            bool actual;
            actual = bm.VoegBestandToe(new bericht("test", "test", "admin", 22009),true);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Controleer de test methode");
        }

        [TestMethod]
        public void TestVerwijderBestand()
        {
            berichtmanager bm = new berichtmanager();
            bool expected = true;
            bool actual;
            actual = bm.VerwijderBestand(new bericht("test", "test", "admin", 22009));
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Controleer de test methode");
        }
    }
}
