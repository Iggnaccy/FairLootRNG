using System;
using System.Collections.Generic;
using System.Text;

namespace FairLootRNG
{
    class Item : IItem
    {
        internal double weight;
        public double Weight => weight;

        internal double magicFindMultiplier;
        public double MagicFindMultiplier => magicFindMultiplier;

        internal string name;
        public string Name => name;

        public override string ToString()
        {
            return $"[{name}]: Weight = {Weight:000.000}, Value = {MagicFindMultiplier:00}";
        }
    }
}
