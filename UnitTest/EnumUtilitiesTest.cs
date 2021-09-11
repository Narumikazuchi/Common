using Microsoft.VisualStudio.TestTools.UnitTesting;
using Narumikazuchi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class EnumUtilitiesTest
    {
        [TestMethod]
        public void ValidatorTrue()
        {
            Assert.IsTrue(EnumValidator.IsDefined(ConsoleColor.Green));
        }

        [TestMethod]
        public void ValidatorFalse()
        {
            Assert.IsFalse(EnumValidator.IsDefined((ConsoleColor)128));
        }

        [TestMethod]
        public void EnumerateValues()
        {
            IEnumerable<ConsoleColor> values = EnumEnumerator.EnumerateValues<ConsoleColor>();
            ConsoleColor[] all = new ConsoleColor[]
            {
                ConsoleColor.Black,
                ConsoleColor.DarkBlue,
                ConsoleColor.DarkGreen,
                ConsoleColor.DarkCyan,
                ConsoleColor.DarkRed,
                ConsoleColor.DarkMagenta,
                ConsoleColor.DarkYellow,
                ConsoleColor.Gray,
                ConsoleColor.DarkGray,
                ConsoleColor.Blue,
                ConsoleColor.Green,
                ConsoleColor.Cyan,
                ConsoleColor.Red,
                ConsoleColor.Magenta,
                ConsoleColor.Yellow,
                ConsoleColor.White
            };
            Assert.IsTrue(values.SequenceEqual(all));
        }

        [TestMethod]
        public void EnumerateNonFlags()
        {
            IEnumerable<ConsoleColor> values = EnumEnumerator.EnumerateFlags(ConsoleColor.Yellow);
            Assert.IsFalse(values.Any());
        }

        [TestMethod]
        public void EnumerateFlags()
        {
            IEnumerable<AttributeTargets> values = EnumEnumerator.EnumerateFlags(AttributeTargets.Class | AttributeTargets.Event | AttributeTargets.Method);
            AttributeTargets[] all = new AttributeTargets[]
            {
                AttributeTargets.Class,
                AttributeTargets.Method,
                AttributeTargets.Event
            };
            Assert.IsTrue(values.SequenceEqual(all));
        }
    }
}
