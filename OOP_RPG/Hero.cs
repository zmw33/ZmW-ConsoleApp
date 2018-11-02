using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG
{
    public class Hero
    {
        private Dictionary<string, object> UserItemCatalog { get; set; }
        public Game Game { get; set; }
        public Shop Shop { get; set; }
        /*This is a Constructor.
        When we create a new object from our Hero class, the instance of this class, our hero, has:
        an empty List that has to contain instances of the Armor class,
        an empty List that has to contain instance of the Weapon class,
        stats of the "int" data type, including an intial strength and defense,
        original hitpoints that are going to be the same as the current hitpoints.
        */
        //constructor v has SAME NAME as class
        //public Hero() = public class Hero
        public Hero(Game game, Shop shop)
        {
            Game = game;
            Shop = shop;

            this.ArmorsBag = new List<Armor>();
            this.WeaponsBag = new List<Weapon>();
            this.Strength = 10;
            this.Defense = 10;
            this.OriginalHP = 30;
            this.CurrentHP = 30;
            this.Speed = 10;
            this.Gold = 50;
            this.PotionsBag = new List<Potion>();
            this.UserItemCatalog = new Dictionary<string, object>();
        }

        //^^^public hero has this.propertyName

        // These are the Properties of our Class.
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int OriginalHP { get; set; }
        public int CurrentHP { get; set; }
        public Weapon EquippedWeapon { get; set; }
        public Armor EquippedArmor { get; set; }
        public int Gold { get; set; }
        public Potion EquippedPotion { get; set; }
        public int Speed { get; set; }

        public List<Armor> ArmorsBag { get; set; }
        public List<Weapon> WeaponsBag { get; set; }
        public List<Potion> PotionsBag { get; set; }

        //These are the Methods (functions?) of our Class.
        public void ShowStats()
        {
            Console.Clear();

            Console.WriteLine("*****" + this.Name + "*****");
            Console.WriteLine("Strength: " + this.Strength);
            Console.WriteLine("Defense: " + this.Defense);
            Console.WriteLine("Hitpoints: " + this.CurrentHP + "/" + this.OriginalHP);
            Console.WriteLine("Speed: " + this.Speed);
            Console.WriteLine("Gold: " + this.Gold);
        }

        public void ShowInventory()
        {
            Console.Clear();

            Console.WriteLine("*****  INVENTORY ******");
            Console.WriteLine("Weapon(s): ");
            foreach (var w in this.WeaponsBag)
            {
                Console.WriteLine(w.Name + " of " + w.Strength + " Strength");
            }
            Console.WriteLine("");
            Console.WriteLine("Armor(s): ");
            foreach (var a in this.ArmorsBag)
            {
                Console.WriteLine(a.Name + " of " + a.Defense + " Defense");
            }
            Console.WriteLine("");
            Console.WriteLine("Potion(s): ");
            foreach (var p in this.PotionsBag)
            {
                Console.WriteLine(p.Name + " of " + p.HP + " Hit Points");
            }
        }

        public void ItemsToSell()
        {
            UserItemCatalog.Clear();

            ListArmor();
            ListWeapons();
            ListPotions();
        }

        public void ListArmor()
        {
            Console.WriteLine("");
            Console.WriteLine("*****Armor*****");
            var count = 1;
            foreach (var a in this.ArmorsBag)
            {
                Console.WriteLine($"a{count}: {a.Name}, {a.ResellValue} gold");

                UserItemCatalog.Add($"a{count}", a);
                count++;
            }

        }

        public void ListPotions()
        {
            Console.WriteLine("");
            Console.WriteLine("*****Potions*****");
            var count = 1;
            foreach (var p in this.PotionsBag)
            {
                Console.WriteLine($"p{count}: {p.Name}, {p.ResellValue} gold");

                UserItemCatalog.Add($"p{count}", p);
                count++;
            }

        }

        public void ListWeapons()
        {
            Console.WriteLine("*****Weapons*****");
            var count = 1;
            foreach (var w in this.WeaponsBag)
            {
                Console.WriteLine($"w{count}:{w.Name}, {w.ResellValue} gold");

                UserItemCatalog.Add($"w{count}", w);
                count++;
            }

        }

        public void Sell()
        {
            if (UserItemCatalog.Count == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("You have no items to sell");
                Console.ReadKey();
                Shop.Menu();
            }

            var selection = "";
            while (string.IsNullOrEmpty(selection))
            {
                Console.WriteLine("");
                Console.Write("What would you like to sell? ");

                selection = Console.ReadLine();
            }

            if (!UserItemCatalog.ContainsKey(selection))
            {
                Console.WriteLine("");
                Console.WriteLine("Please provide a valid selection.");
                Sell();
            }
            //I need to know what type of object the user has selected so I will look
            //at the first letter of the key (a, w, p)

            switch (selection.Substring(0, 1))
            {
                case "a":
                    var armor = (Armor)UserItemCatalog[selection];
                    ArmorsBag.Remove(armor);
                    Gold += armor.ResellValue;
                    Shop.ArmorsList.Add(armor);
                    break;
                case "w":
                    var weapon = (Weapon)UserItemCatalog[selection];
                    WeaponsBag.Remove(weapon);
                    Gold += weapon.ResellValue;
                    Shop.WeaponsList.Add(weapon);
                    break;
                case "p":
                    var potion = (Potion)UserItemCatalog[selection];
                    PotionsBag.Remove(potion);
                    Gold += potion.ResellValue;
                    Shop.PotionsList.Add(potion);
                    break;
                default:
                    Shop.Menu();
                    break;                    
            }            
        }

        public void EquipWeapon()
        {
            UserItemCatalog.Clear();
            Console.WriteLine("");
            Console.WriteLine("*****Weapons*****");
            var count = 1;
            foreach (var weapon in this.WeaponsBag)
            {
                Console.WriteLine($"{count}: {weapon.Name} with {weapon.Strength} strength");

                UserItemCatalog.Add($"{count}", weapon);
                count++;
            }

            if (UserItemCatalog.Count == 0)
            {
                Console.WriteLine("You have no weapons to use");
                Console.ReadKey();
                Shop.Menu();
            }

            var selection = "";
            while (string.IsNullOrEmpty(selection))
            {
                Console.WriteLine("");
                Console.Write("Make a selection: ");

                selection = Console.ReadLine();
            }

            var intselection = Convert.ToInt32(selection);
            if (intselection < 1 || intselection > UserItemCatalog.Count)
            {
                Console.WriteLine("Please make a valid selection");
                EquipWeapon();
                Console.ReadKey();
            }
            else
            {
                var weapon = (Weapon)UserItemCatalog[selection];
                WeaponsBag.Remove(weapon);
                EquippedWeapon = weapon;
                Strength += weapon.Strength;
                Game.Main();
            }
        }

        public void EquipArmor()
        {
            UserItemCatalog.Clear();
            Console.WriteLine("");
            Console.WriteLine("*****Armor*****");
            var count = 1;
            foreach (var armor in this.ArmorsBag)
            {
                Console.WriteLine($"{count}: {armor.Name} with {armor.Defense} defense");

                UserItemCatalog.Add($"{count}", armor);
                count++;
            }

            if (UserItemCatalog.Count == 0)
            {
                Console.WriteLine("You have no armor to equip");
                Console.ReadKey();
                Shop.Menu();
            }

            var selection = "";
            while (string.IsNullOrEmpty(selection))
            {
                Console.WriteLine("");
                Console.Write("Make a selection: ");

                selection = Console.ReadLine();
            }

            var intselection = Convert.ToInt32(selection);
            if (intselection < 1 || intselection > UserItemCatalog.Count)
            {
                Console.WriteLine("Please make a valid selection");
                EquipArmor();
                Console.ReadKey();
            }
            else
            {
                var armor = (Armor)UserItemCatalog[selection];
                ArmorsBag.Remove(armor);
                EquippedArmor = armor;
                Defense += armor.Defense;
                Game.Main();
            }
        }

        public void EquipPotion()
        {
            UserItemCatalog.Clear();
            Console.WriteLine("");
            Console.WriteLine("*****Potions*****");
            var count = 1;
            foreach (var potion in this.PotionsBag)
            {
                Console.WriteLine($"{count}: {potion.Name} with {potion.HP} hit points");

                UserItemCatalog.Add($"{count}", potion);
                count++;
            }

            if (UserItemCatalog.Count == 0)
            {
                Console.WriteLine("You have no potions to drink");
                Console.ReadKey();
                Shop.Menu();
            }

            var selection = "";
            while (string.IsNullOrEmpty(selection))
            {
                Console.WriteLine("");
                Console.Write("Make a selection: ");

                selection = Console.ReadLine();
            }

            var intselection = Convert.ToInt32(selection);
            if (intselection < 1 || intselection > UserItemCatalog.Count)
            {
                Console.WriteLine("Please make a valid selection");
                EquipPotion();
                Console.ReadKey();
            }
            else
            {
                var potion = (Potion)UserItemCatalog[selection];
                PotionsBag.Remove(potion);
                EquippedPotion = potion;
                CurrentHP += potion.HP;
                Game.Main();
            }
        }
    }
}
