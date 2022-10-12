using System;
using System.Collections.Generic;
using System.Text;

namespace FairLootRNG
{
    class FairLootTable : IFairLootTable
    {
        private List<IItem> items;
        
        private IFairLootController fairLootController;
        private Random rng;

        public double WeightSum => GetWeightSum();

        public FairLootTable(IFairLootController controller)
        {
            fairLootController = controller;
            rng = new Random();
        }

        public FairLootTable(IFairLootController controller, List<IItem> items)
        {
            fairLootController = controller;
            Items = items;
            rng = new Random();
        }

        public List<IItem> Items
        {
            get => items;
            set
            {
                if (items == value) return;
                items = value;
            }
        }
        public IItem Roll(double magicFindOverride)
        {
            if (items == null) throw new NullReferenceException("Field `items` was null when trying to roll an item");
            if (items.Count == 0) throw new IndexOutOfRangeException("Trying to roll on an empty table");
            var finalMFValue = (magicFindOverride > 0 ? magicFindOverride : fairLootController.MagicFindValue) + fairLootController.BadLuckValue;
            var finalWeightMultiValue = 1 + finalMFValue;
            var maximumRoll = GetWeightSum(finalMFValue);
            var roll = rng.NextDouble() * maximumRoll;
            int id = 0;
            while (roll > items[id].Weight * finalWeightMultiValue && id + 1 < items.Count)
            {
                roll -= items[id].Weight * finalWeightMultiValue;
                id++;
            }
            fairLootController.ScoreCorrection(roll, maximumRoll);
            return items[id];
        }

        public IItem Roll()
        {
            return Roll(fairLootController.MagicFindValue);
        }

        public IItem Roll(int magicFindOverride)
        {
            return Roll(magicFindOverride * 0.01);
        }

        public IItem Roll(List<IItem> items, double magicFindOverride)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (items.Count == 0) throw new IndexOutOfRangeException("Trying to roll on an empty table");
            var finalMFValue = (magicFindOverride > 0 ? magicFindOverride : fairLootController.MagicFindValue) + fairLootController.BadLuckValue;
            var finalWeightMultiValue = 1 + finalMFValue;
            var maximumRoll = GetWeightSum(items, finalMFValue);
            var roll = rng.NextDouble() * maximumRoll;
            int id = 0; 
            while (roll > items[id].Weight * finalWeightMultiValue && id + 1 < items.Count)
            {
                roll -= items[id].Weight * finalWeightMultiValue;
                id++;
            }
            fairLootController.ScoreCorrection(roll, maximumRoll);
            return items[id];
        }

        public IItem Roll(List<IItem> items)
        {
            return Roll(items, fairLootController.MagicFindValue);
        }

        public IItem Roll(List<IItem> items, int magicFindOverride)
        {
            return Roll(items, magicFindOverride * 0.01);
        }

        public List<IItem> RollMultiple(int rolls, double magicFindOverride)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (items.Count == 0) throw new IndexOutOfRangeException("Trying to roll on an empty table");
            var rolled = new List<IItem>();
            var finalMFValue = (magicFindOverride > 0 ? magicFindOverride : fairLootController.MagicFindValue) + fairLootController.BadLuckValue;
            var maximumRoll = GetWeightSum(finalMFValue);
            Console.WriteLine($"[FairLootTable] rolling {rolls} times with MF = {finalMFValue}, maximum roll = {maximumRoll}");
            for (int i = 0; i < rolls; i++)
            {
                var roll = rng.NextDouble() * maximumRoll;
                int id = 0;
                while (roll > items[id].Weight * (1 + items[id].MagicFindMultiplier * finalMFValue) && id + 1 < items.Count)
                {
                    roll -= items[id].Weight * (1 + items[id].MagicFindMultiplier * finalMFValue);
                    id++;
                }
                //fairLootController.ScoreCorrection(roll, maximumRoll);
                rolled.Add(items[id]);
            }
            return rolled;
        }

        public List<IItem> RollMultiple(int rolls)
        {
            return RollMultiple(rolls, fairLootController.MagicFindValue);
        }

        public List<IItem> RollMultiple(int rolls, int magicFindOverride)
        {
            return RollMultiple(rolls, magicFindOverride * 0.01);
        }

        public List<IItem> RollMultiple(List<IItem> items, int rolls, double magicFindOverride)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (items.Count == 0) throw new IndexOutOfRangeException("Trying to roll on an empty table");
            var finalMFValue = (magicFindOverride > 0 ? magicFindOverride : fairLootController.MagicFindValue) + fairLootController.BadLuckValue;
            var finalWeightMultiValue = 1 + finalMFValue;
            var maximumRoll = GetWeightSum(items, finalMFValue);
            var rolled = new List<IItem>();
            for (int i = 0; i < rolls; i++)
            {
                var roll = rng.NextDouble() * maximumRoll;
                int id = 0;
                while (roll > items[id].Weight * finalWeightMultiValue && id + 1 < items.Count)
                {
                    roll -= items[id].Weight * finalWeightMultiValue;
                    id++;
                }
                fairLootController.ScoreCorrection(roll, maximumRoll);
                rolled.Add(items[id]);
            }
            return rolled;
        }

        public List<IItem> RollMultiple(List<IItem> items, int rolls)
        {
            return RollMultiple(items, rolls, fairLootController.MagicFindValue);
        }

        public List<IItem> RollMultiple(List<IItem> items, int rolls, int magicFindOverride)
        {
            return RollMultiple(items, rolls, magicFindOverride * 0.01);
        }

        public double GetWeightSum(double magicFind = 0, double badLuckMagicFind = 0)
        {
            if (items == null) return 0;
            double sum = 0;
            foreach(var item in items)
            {
                sum += item.Weight * (1 + (magicFind + badLuckMagicFind) * item.MagicFindMultiplier);
            }
            return sum;
        }
        public double GetWeightSum(List<IItem> items, double magicFind = 0, double badLuckMagicFind = 0)
        {
            if (items == null) return 0;
            double sum = 0;
            foreach (var item in items)
            {
                sum += item.Weight * (1 + (magicFind + badLuckMagicFind) * item.MagicFindMultiplier);
            }
            return sum;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("FairLootTable:");
            stringBuilder.AppendLine("{");
            var weightSum = WeightSum;
            for(int i = 0; i < items.Count; i++)
            {
                stringBuilder.AppendLine($"\t{items[i]}, chance: {items[i].Weight / weightSum * 100:0.00}%");
            }
            stringBuilder.AppendLine($"\tWeight Sum: {weightSum}");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        public string ToString(double MF)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("FairLootTable:");
            stringBuilder.AppendLine("{");
            var weightSum = GetWeightSum(MF);
            var adjustedMF = 1 + MF;
            for (int i = 0; i < items.Count; i++)
            {
                var weight = items[i].Weight + items[i].Weight * MF * items[i].MagicFindMultiplier;
                stringBuilder.AppendLine($"\t[{items[i].Name}]: Weight = {weight:000.000}, Value = {items[i].MagicFindMultiplier:00}, chance: {weight / weightSum * 100:0.00}%");
            }
            stringBuilder.AppendLine($"\tWeight Sum: {weightSum}");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
    }
}
