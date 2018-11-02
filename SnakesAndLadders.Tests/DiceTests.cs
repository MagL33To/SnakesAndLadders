using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SnakesAndLadders.GameClasses;
using SnakesAndLadders.Interfaces;

namespace SnakesAndLadders.Tests
{
    [TestFixture]
    public class DiceTests
    {
        private IDice _dice;

        [Test]
        public void RollDice_ReturnsValueBetween1And6()
        {
            _dice = new D6();

            var results = new List<int>();

            for (var i = 0; i < 100; i++)
            {
                results.Add(_dice.RollDice());
            }
            
            Assert.IsTrue(results.All(r => r > 0 && r < 7));
        }

        [Test]
        public void RollDice_ReturnsDifferentValues()
        {
            _dice = new D6();

            var results = new List<int>();

            for (var i = 0; i < 100; i++)
            {
                results.Add(_dice.RollDice());
            }

            var distinct = results.Distinct();

            Assert.IsTrue(distinct.Count() > 1);
        }
    }
}