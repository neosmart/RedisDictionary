using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoSmart.Redis;
using StackExchange.Redis;

namespace Tests
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void SetAndRetrieve()
        {
            var stringDictionary = new RDictionary<string, string>("stringDictionary");

            stringDictionary["answer"] = "42";

            Assert.AreEqual("42", stringDictionary["answer"]);
        }
    }
}
