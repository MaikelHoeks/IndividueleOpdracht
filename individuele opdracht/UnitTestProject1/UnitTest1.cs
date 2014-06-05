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
            actual = bm.VoegCategorieToe(new categorie("Fiets"), true);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("werkt niet");
        }
    }
}
