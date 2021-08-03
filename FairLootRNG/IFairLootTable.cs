using System;
using System.Collections.Generic;
using System.Text;

namespace FairLootRNG
{
    interface IFairLootTable
    {
        /// <summary>
        /// Default list of items to use in rolls
        /// </summary>
        public List<IItem> Items { get; }
        
        /// <summary>
        /// Rolls an item from this <c>IFairLootTable.Items</c> using the current bad luck prevention and magic find
        /// </summary>
        /// <remarks>For multiple rolls use RollMultiple instead</remarks>
        /// <returns>The rolled item</returns>
        public IItem Roll();
        /// <summary>
        /// Rolls an item from this <c>IFairLootTable.Items</c> using the current bad luck prevention and the magic find supplied
        /// </summary>
        /// <param name="magicFindOverride">Magic find value</param>
        /// <returns>The rolled item</returns>
        public IItem Roll(double magicFindOverride);
        /// <summary>
        /// Rolls an item from this <c>IFairLootTable.Items</c> using the current bad luck prevention and the magic find supplied
        /// </summary>
        /// <param name="magicFindOverride">Magic find value in percentages</param>
        /// <remarks>For multiple rolls use RollMultiple instead</remarks>
        /// <returns>The rolled item</returns>
        public IItem Roll(int magicFindOverride);
        /// <summary>
        /// Rolls an item from supplied <c>items</c> using the current bad luck prevention and magic find
        /// </summary>
        /// <param name="items">List of items to roll from</param>
        /// <remarks>For multiple rolls use RollMultiple instead</remarks>
        /// <returns>The rolled item</returns>
        public IItem Roll(List<IItem> items);
        /// <summary>
        /// Rolls an item from supplied <c>items</c> using the current bad luck prevention and the magic find supplied
        /// </summary>
        /// <param name="items">List of items to roll from</param>
        /// <param name="magicFindOverride">Magic find value</param>
        /// <remarks>For multiple rolls use RollMultiple instead</remarks>
        /// <returns>The rolled item</returns>
        public IItem Roll(List<IItem> items, double magicFindOverride);
        /// <summary>
        /// Rolls an item from supplied <c>items</c> using the current bad luck prevention and the magic find supplied
        /// </summary>
        /// <param name="items">List of items to roll from</param>
        /// <param name="magicFindOverride">Magic find value in percentages</param>
        /// <remarks>For multiple rolls use RollMultiple instead</remarks>
        /// <returns>The rolled item</returns>
        public IItem Roll(List<IItem> items, int magicFindOverride);
        /// <summary>
        /// Rolls <c>rolls</c> items from this <c>IFairLootTable.Items</c> using the current bad luck prevention and magic find
        /// </summary>
        /// <param name="rolls">The amount of times to roll</param>
        /// <returns>List with the items rolled</returns>
        public List<IItem> RollMultiple(int rolls);
        /// <summary>
        /// Rolls <c>rolls</c> items from this <c>IFairLootTable.Items</c> using the current bad luck prevention and the magic find supplied
        /// </summary>
        /// <param name="rolls">The amount of times to roll</param>
        /// <param name="magicFindOverride">Magic find value in percentages</param>
        /// <returns>List with the items rolled</returns>
        public List<IItem> RollMultiple(int rolls, int magicFindOverride);
        /// <summary>
        /// Rolls <c>rolls</c> items from this <c>IFairLootTable.Items</c> using the current bad luck prevention and the magic find supplied
        /// </summary>
        /// <param name="rolls">The amount of times to roll</param>
        /// <param name="magicFindOverride">Magic find value</param>
        /// <returns>List with the items rolled</returns>
        public List<IItem> RollMultiple(int rolls, double magicFindOverride);
        /// <summary>
        /// Rolls <c>rolls</c> items from this <c>IFairLootTable.Items</c> using the current bad luck prevention and magic find
        /// </summary>
        /// <param name="items">List of items to roll from</param>
        /// <param name="rolls">The amount of times to roll</param>
        /// <returns>List with the items rolled</returns>
        public List<IItem> RollMultiple(List<IItem> items, int rolls);
        /// <summary>
        /// Rolls <c>rolls</c> items from this <c>IFairLootTable.Items</c> using the current bad luck prevention and the magic find supplied
        /// </summary>
        /// <param name="items">List of items to roll from</param>
        /// <param name="rolls">The amount of times to roll</param>
        /// <param name="magicFindOverride">Magic find value in percentages</param>
        /// <returns>List with the items rolled</returns>
        public List<IItem> RollMultiple(List<IItem> items, int rolls, int magicFindOverride);
        /// <summary>
        /// Rolls <c>rolls</c> items from this <c>IFairLootTable.Items</c> using the current bad luck prevention and the magic find supplied
        /// </summary>
        /// <param name="items">List of items to roll from</param>
        /// <param name="rolls">The amount of times to roll</param>
        /// <param name="magicFindOverride">Magic find value</param>
        /// <returns>List with the items rolled</returns>
        public List<IItem> RollMultiple(List<IItem> items, int rolls, double magicFindOverride);
    }
}
