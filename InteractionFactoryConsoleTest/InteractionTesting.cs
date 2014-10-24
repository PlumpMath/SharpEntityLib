using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpEntity;

namespace InteractionFactoryConsoleTest
{
    class InteractionTesting
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            NPC cpu = new NPC();
            
            while(true)
            {
                player = new Player();
                cpu = new NPC();
                Console.Clear();

                //TestSpeakInteraction(player, cpu);
                //TestGiveInteraction(player, cpu);
                TestSellInteraction(player, cpu);

                Console.Read();
            }
        }
        static void DisplayBeingItems(Being being)
        {
            Console.WriteLine(being.Name + " items:");

            foreach (IItem t_item in being.getInventory.getInventoryItemList)
            {
                if (t_item != null)
                {
                    Console.WriteLine(t_item.ID + "  " + t_item.Name + "  " + t_item.Cost + " gold  " + "Rarity: " + t_item.Rarity.ToString("#00.#0") + " %");
                }
            }
            Console.WriteLine(being.WalletValue() + " gold");
            Console.WriteLine();
        }
        static void TestSpeakInteraction(Being source, Being destination)
        {
            //Create and perform interaction
            Interaction talking = new Interaction(source, destination);
            String t_string = "";

            while (t_string != null)
            {
                t_string = talking.Speak();
                Console.WriteLine(t_string);
            }
        }
        static void TestGiveInteraction(Being source, Being destination)
        {
            IItem t_item = ItemFactory.MakeItem(Constants.ItemType.Gloves, "fancy gloves", 1, Constants.BeingClassType.BlackMage);

            source.getInventory.Add(t_item);

            //Display items before interaction
            DisplayBeingItems(source);
            DisplayBeingItems(destination);

            //Create and perform interaction
            new Interaction(source, destination).Give(t_item);

            //Display items after interaction
            DisplayBeingItems(source);
            DisplayBeingItems(destination);
        }
        static void TestSellInteraction(Being source, Being destination)
        {
            IItem t_item = ItemFactory.MakeItem(Constants.ItemType.Helm, "Cool Helmet", 50, Constants.BeingClassType.BlackMage);

            source.getInventory.Add(t_item);
            destination.AddGold(10000);

            //Display items before interaction
            DisplayBeingItems(source);
            DisplayBeingItems(destination);

            //Create and perform interaction
            Console.WriteLine("Attempting to sell " + t_item.Name + " from " + source.Name + " to " + destination.Name + ".");

            if (new Interaction(source, destination).Sell(t_item))
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Failed!");
            }

            Console.WriteLine();

            //Display items after interaction
            DisplayBeingItems(source);
            DisplayBeingItems(destination);
        }
    }
}