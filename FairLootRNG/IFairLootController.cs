using System;
using System.Collections.Generic;
using System.Text;

namespace FairLootRNG
{
    interface IFairLootController
    {
        public int BadLuckScore { get; }
        public double BadLuckValue { get; }
        public double MagicFindValue { get; }
        public void ScoreCorrection(double roll, double max);
    }
}
