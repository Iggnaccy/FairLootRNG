using System;
using System.Collections.Generic;
using System.Text;

namespace FairLootRNG
{
    class Item : IItem
    {
        internal double weight;
        public double Weight => weight;

        internal double value;
        public double Value => value;

        internal string name;
        public string Name => name;

        public override string ToString()
        {
            return $"[{name}]: Weight = {Weight:000.000}, Value = {Value:00}";
        }
    }
}
