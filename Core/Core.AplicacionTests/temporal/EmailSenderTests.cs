using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Aplicacion.Helpers.Tests
{
    [TestClass()]
    public class EmailSenderTests
    {
        [TestMethod()]
        public void SenderTest()
        {
            Assert.IsTrue(EmailSender.Sender("jrosas1982@gmail.com", "Hola"));
        }
    }
}