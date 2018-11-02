using System;
using SnakesAndLadders.Interfaces;

namespace SnakesAndLadders.GameClasses
{
    public class D6 : IDice
    {
        private static readonly Random Rand;

        static D6()
        {
            Rand = new Random();
        }

        public int RollDice()
        {
            return Rand.Next(1, 7);
        }
    }
}