using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;

namespace FairLootRNG
{
    class FairLootController : IFairLootController
    {
        private NameValueCollection config;
        private readonly int maxBadLuckScore;
        private readonly int minimumDeviation;
        private readonly int maximumDeviation;
        private readonly int maximumScoreChange;
        private readonly double maximumBoostValue;
        int range => maximumDeviation - minimumDeviation;

        public FairLootController()
        {
            config = ConfigurationManager.AppSettings;
            maxBadLuckScore = int.Parse(config.Get("BadLuckMaxScore"));
            minimumDeviation = int.Parse(config.Get("BadLuckScoreDeviation"));
            maximumDeviation = int.Parse(config.Get("BadLuckMaxDeviation"));
            maximumScoreChange = int.Parse(config.Get("BadLuckMaxScoreChange"));
            maximumBoostValue = double.Parse(config.Get("BadLuckValue")) / 100;
        }

        private int badLuckScore;

        private double magicFindValue;

        public int BadLuckScore
        {
            get => badLuckScore;
            set
            {
                if(badLuckScore != value)
                    badLuckScore = Math.Max(0, value);
            }
        }

        public double BadLuckValue => 0; // Lerp(0, maximumBoostValue, (double)BadLuckScore / maxBadLuckScore);

        public double MagicFindValue
        {
            get => magicFindValue;
            set
            {
                if (magicFindValue != value)
                    magicFindValue = value;
            }
        }

        private double Lerp(double min, double max, double value)
        {
            value = Math.Clamp(value, 0, 1);
            return min + (max - min) * value;
        }

        private int GetScoreChangeForValue(double deviation)
        {
            if (deviation < minimumDeviation) return 0;
            if (deviation >= maximumDeviation) return maximumScoreChange;
            var value = (deviation - minimumDeviation) / (maximumDeviation - minimumDeviation);
            var lerp = Lerp(0, range, value);
            int scoreChange = (int)(lerp * maximumScoreChange / range);
            return scoreChange;
        }

        public void ScoreCorrection(double value, double maxValue)
        {
            return;
            var percent = 100 * value / maxValue;
            var deviation = Math.Abs(percent - 50);
            var scoreChange = GetScoreChangeForValue(deviation);
            if (percent >= 50) scoreChange = -scoreChange;
            BadLuckScore += scoreChange;
        }
    }
}
