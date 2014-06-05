using Microsoft.VisualStudio.TestTools.UnitTesting;
using individuele_opdracht;

namespace UnitTestIndividueleOpdracht
{
    [TestClass()]
    public class Database
    {
        [TestMethod()]
        public void TestLogin()
        {
            bool expected = true;
            bool actual;
            actual = Database.Login("admin", "admin");
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("werkt niet");
        }
    }
}
