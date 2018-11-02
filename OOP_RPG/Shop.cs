using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    public class Shop
    {
        public List<Armor> ArmorsList { get; set; }
        public List<Weapon> WeaponsList { get; set; }
        public List<Potion> PotionsList { get; set; }

        private Dictionary<string, object> ItemCatalog { get; set; }

        public Game Game { get; set; }

        public Shop(Game game)
        {
            this.ArmorsList = new List<Armor>();
            this.WeaponsList = new List<Weapon>();
            this.PotionsList = new List<Potion>();
            this.ItemCatalog = new Dictionary<string, object>();
            this.Game = game;

            StockShop();
        }

        private void StockShop()
        {
            //Add weapons to weapons list
            for (var loop = 1; loop <= 5; loop++)
            {
                WeaponsList.Add(new Weapon("Wooden Stick", 1, 3, 1));
                WeaponsList.Add(new Weapon("Kung Fu Fist", 3, 5, 3));
                WeaponsList.Add(new Weapon("ACME Boxing Glove", 5, 10, 5));
                WeaponsList.Add(new Weapon("Roundhouse Boots", 8, 18, 8));
            }

            //Add armor to armors list
            for (var loop = 1; loop <= 5; loop++)
            {
                ArmorsList.Add(new Armor("Leather Vest", 1, 2, 1));
                ArmorsList.Add(new Armor("Padded Shield", 2, 5, 3));
                ArmorsList.Add(new Armor("Shock Absorbing Robe", 5, 9, 5));
                ArmorsList.Add(new Armor("Chuck Norris' Beard", 7, 15, 8));

            }

            //Add potions to potions list
            for (var loop = 1; loop <= 5; loop++)
            {
                PotionsList.Add(new Potion("Sizzurp", 5, 5, 2));
                PotionsList.Add(new Potion("Super Sizzurp", 10, 10, 5));
                PotionsList.Add(new Potion("Sizzurp de Saints", 30, 30, 13));
            }
        }

        public void Menu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the shop! What would you like to do?");
            Console.WriteLine("1. Sell Item");
            Console.WriteLine("2. Buy Item");
            Console.WriteLine("3. Return To Main Menu");

            Console.WriteLine("");
            Console.WriteLine("Enter your selection: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                //switch on value of input
                case "1":
                    Sell2();
                    break;
                case "2":
                    ShowInventory();
                    Buy();
                    break;
                case "3":
                    Main();
                    break;
                default:
                    Game.Main();
                    break;

            }
        }

        void ShowInventory()
        {
            Console.WriteLine($"Here is what we have in stock as of today, {DateTime.Now}");
            ItemCatalog.Clear();
            ShopListWeapons();
            ShopListArmors();
            ShopListPotions();

        }

        void Buy()
        {
            var selection = "";
            while (string.IsNullOrEmpty(selection))
            {
                Console.WriteLine("");
                Console.WriteLine("What Would you like to purchase? ");
                selection = Console.ReadLine();
            }

            if (!ItemCatalog.ContainsKey(selection))
            {
                Console.WriteLine("");
                Console.WriteLine("That selection is not valid please try again.");
                Buy();
            }

            if (VerifyFunds(selection))
            {
                FinalizeBuy(selection);
            }

            this.Menu();
        }



        void FinalizeBuy(string selection)
        {
            switch (selection.Substring(0, 1))
            {
                case "a":
                    var armor = (Armor)ItemCatalog[selection];
                    Game.Hero.Gold -= armor.OriginalValue;
                    Game.Hero.ArmorsBag.Add(armor);
                    this.ArmorsList.Remove(armor);
                    break;
                case "w":
                    var weapon = (Weapon)ItemCatalog[selection];
                    Game.Hero.Gold -= weapon.OriginalValue;
                    Game.Hero.WeaponsBag.Add(weapon);
                    this.WeaponsList.Remove(weapon);
                    break;
                case "p":
                    var potion = (Potion)ItemCatalog[selection];
                    Game.Hero.Gold -= potion.OriginalValue;
                    Game.Hero.PotionsBag.Add(potion);
                    this.PotionsList.Remove(potion);
                    break;
            }
            Menu();
        }


        bool VerifyFunds(string selection)
        {
            switch (selection.Substring(0, 1))
            {
                case "a":
                    var armor = (Armor)ItemCatalog[selection];
                    return Game.Hero.Gold >= armor.OriginalValue;
                case "w":
                    var weapon = (Weapon)ItemCatalog[selection];
                    return Game.Hero.Gold >= weapon.OriginalValue;
                case "p":
                    var potion = (Potion)ItemCatalog[selection];
                    return Game.Hero.Gold >= potion.OriginalValue;
                default:
                    return false;
            }
        }

        void Main()
        {
            Game.Main();
        }

        void ShopListWeapons()
        {
            var count = 1;
            Console.WriteLine("---Weapons For Sale---");
            foreach (var weapon in WeaponsList.OrderBy(x => x.Name))
            {
                Console.WriteLine($"w{count} {weapon.Name}, {weapon.OriginalValue}, {weapon.ResellValue}");
                ItemCatalog.Add($"w{count}", weapon);
                count++;
            }
            Console.WriteLine("");
        }

        void ShopListArmors()
        {
            var count = 1;
            Console.WriteLine("---Armors For Sale---");
            foreach (var armor in ArmorsList.OrderBy(x => x.Name))
            {
                Console.WriteLine($"a{count} {armor.Name}, {armor.OriginalValue}, {armor.ResellValue}");
                ItemCatalog.Add($"a{count}", armor);
                count++;
            }
            Console.WriteLine("");
        }

        void ShopListPotions()
        {
            var count = 1;
            Console.WriteLine("---Potions For Sale---");
            foreach (var potion in PotionsList.OrderBy(x => x.Name))
            {
                Console.WriteLine($"p{count} {potion.Name}, {potion.OriginalValue}, {potion.ResellValue}");
                ItemCatalog.Add($"p{count}", potion);
                count++;
            }
            Console.WriteLine("");
        }

        public void Sell2()
        {
            Console.Clear();
            Game.Hero.ItemsToSell();
            Console.WriteLine("");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Sell Item");
            Console.WriteLine("2. Shop Menu");

            Console.Write("Enter your selection: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Game.Hero.Sell();
                    break;
                case "2":
                    Menu();
                    break;
                default:
                    this.Game.Main();
                    break;
            }
        }
    }
}
