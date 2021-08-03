using System;
using System.Collections.Generic;
using System.Text;

namespace FairLootRNG
{
    interface IItem
    {
        public double Weight { get; }
        public double Value { get; }
        public string Name { get; }
    }
}
