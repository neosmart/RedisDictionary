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

        [TestMethod]
        public void MultipleValues()
        {
            var stringDictionary = new RDictionary<string, string>("stringDictionary");

            stringDictionary["correct"] = "42";
            stringDictionary["incorrect"] = "43";

            Assert.AreEqual("42", stringDictionary["correct"]);
            Assert.AreNotEqual("42", stringDictionary["incorrect"]);
        }

        [TestMethod]
        public void MultipleInstancesDifferentKeys()
        {
            var s1 = new RDictionary<string, string>("s1");
            var s2 = new RDictionary<string, string>("s2");

            s1["name"] = "s1";
            s2["name"] = "s2";

            Assert.AreNotEqual(s1["name"], s2["name"]);
            Assert.AreEqual("s1", s1["name"]);
            Assert.AreEqual("s2", s2["name"]);
        }

        [TestMethod]
        public void MultipleInstancesSameKey()
        {
            var s1 = new RDictionary<string, string>("s1");
            var s2 = new RDictionary<string, string>("s1");

            s1["name"] = "s1";

            Assert.AreEqual(s1["name"], s2["name"]);
        }
    }
}
