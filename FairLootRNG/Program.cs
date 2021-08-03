using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FairLootRNG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            FairLootController controller = new FairLootController();
            List<IItem> items = new List<IItem>();
            Random rng = new Random();

            Console.WriteLine("Creating random table of size 50");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for(int i = 0; i < 20; i++)
            {
                items.Add(new Item { value = i/2, weight = (20 - i) * 50, name = $"Item #{i:00}" });
            }
            Console.WriteLine($"Created random table in {sw.Elapsed.TotalSeconds}s");
            sw.Stop();
            sw.Reset();
            Console.WriteLine("Creating Loot Table");
            sw.Start();
            FairLootTable lootTable = new FairLootTable(controller, items);
            Console.WriteLine($"Created loot table in {sw.Elapsed.TotalSeconds}s");
            Console.WriteLine($"Table:\n{lootTable}");
            Console.WriteLine($"Table at 20% MF:\n{lootTable.ToString(.2)}");
            Console.WriteLine($"Table at 100% MF:\n{lootTable.ToString(1)}");
            sw.Stop();
            double averageValue = 0.0, averageValue20 = 0, averageValue100 = 0;
            double weightSumDiv = 1.0 / lootTable.WeightSum, weightSumDiv20 = 1.0 / lootTable.GetWeightSum(0.2), weightSumDiv100 = 1.0 / lootTable.GetWeightSum(1);
            foreach(var item in lootTable.Items)
            {
                averageValue += item.Value * item.Weight * weightSumDiv;
                averageValue20 += item.Value * item.Weight * (1 + .2 * item.Value) * weightSumDiv20;
                averageValue100 += item.Value * item.Weight * (1 + 1 * item.Value) * weightSumDiv100;
            }
            Console.WriteLine($"Average value in table: {averageValue:0.000} at 0% MF");
            Console.WriteLine($"Average value in table: {averageValue20:0.000} at 20% MF");
            Console.WriteLine($"Average value in table: {averageValue100:0.000} at 100% MF");
            sw.Reset();
            var rolls = new List<IItem>();
            /*
            Console.WriteLine("Rolling random items 50 times");
            sw.Start();
            for(int i = 0; i < 50; i++)
            {
                var roll = lootTable.Roll();
                //Console.WriteLine($"Rolled {roll}. Controller values: BLS = {controller.BadLuckScore}, BLV = {controller.BadLuckValue:0.00}, MFV = {controller.MagicFindValue:0.00}");
                rolls.Add(roll);
            }
            rolls.Sort((x, y) => (int)(x.Value - y.Value));
            Console.WriteLine($"Rolls:\n{TableToString(rolls, lootTable)}");
            Console.WriteLine($"Items rolled in {sw.Elapsed.TotalSeconds}");
            sw.Stop();
            sw.Reset();
            */
            for (int i = 0; i < 3; i++)
            {
                sw.Start();
                controller.MagicFindValue = i * 0.2;
                Console.WriteLine($"Rolling random items 1000000 times. Magic find = {controller.MagicFindValue * 100:0.00}%");
                rolls = lootTable.RollMultiple(1000000);
                //rolls.Sort((x, y) => (int)(x.Value - y.Value));
                double rollSumF = 0;
                foreach (var roll in rolls)
                    rollSumF += roll.Value;
                Console.WriteLine($"Rolls had a total value of {rollSumF}, average: {rollSumF / 1000000.0:0.000}");
                //Console.WriteLine($"Rolls:\n{TableToString(rolls, lootTable)}");
                //Console.WriteLine($"Items rolled in {sw.Elapsed.TotalSeconds}");
                sw.Stop();
                sw.Reset();
            }
            sw.Start();
            controller.MagicFindValue = 1;
            Console.WriteLine($"Rolling random items 1000000 times. Magic find = {controller.MagicFindValue * 100:0.00}%");
            rolls = lootTable.RollMultiple(1000000);
            //rolls.Sort((x, y) => (int)(x.Value - y.Value));
            double rollSum = 0;
            foreach (var roll in rolls)
                rollSum += roll.Value;
            Console.WriteLine($"Rolls had a total value of {rollSum}, average: {rollSum / 1000000.0:0.000}");
            //Console.WriteLine($"Rolls:\n{TableToString(rolls, lootTable)}");
            //Console.WriteLine($"Items rolled in {sw.Elapsed.TotalSeconds}");
            sw.Stop();
            sw.Reset();
        }

        private static string TableToString(List<IItem> items, FairLootTable table)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("RolledTable:");
            stringBuilder.AppendLine("{");
            for (int i = 0; i < items.Count; i++)
            {
                stringBuilder.AppendLine($"\t{items[i]}, chance: {items[i].Weight / table.WeightSum * 100:0.00}%");
            }
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
    }
}
