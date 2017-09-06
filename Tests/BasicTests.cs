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

        [TestMethod]
        public void IDisposable()
        {
            using (var dict = new RDictionary<string, int>("intValues"))
            {
                dict["42"] = 42;
            }
        }

        [TestMethod]
        public void SerializerRequiredException()
        {
            Assert.ThrowsException<SerializerRequiredException>(() =>
            {
                var dict = new RDictionary<string, CustomType>("rdict");
            });

            Assert.ThrowsException<SerializerRequiredException>(() =>
            {
                var dict = new RDictionary<CustomType, string>("rdict");
            });
        }

        [TestMethod]
        public void BasicSerialization()
        {
            var dict = new RDictionary<int, int>("intDict");
            dict[42] = 42;

            Assert.AreEqual(42, dict[42]);
        }

        [TestMethod]
        public void ExternalSerializer()
        {
            var dict = new RDictionary<int, CustomType, CustomSerializer, CustomSerializer>("intDict");

            var key = new CustomType
            {
                Value = 42
            };

            dict[42] = key;

            Assert.AreEqual(42, dict[42].Value);
        }
    }
}
