using System;
using System.Collections.Generic;

namespace OOP_RPG
{
    public class Game
    {
        public Hero Hero { get; set; }
        public Shop Shop { get; set; }

        public Game()
        {

            Shop = new Shop(this);
            Hero = new Hero(this, Shop);
        }

        public void Start()
        {
            Console.WriteLine("Welcome hero!");
            Console.Write("Please enter your name: ");
            this.Hero.Name = Console.ReadLine();
            Console.WriteLine("Hello " + Hero.Name);
            //Console.WriteLine("Hello {hero.Name}");      v.4.6

            this.Main();
        }

        public void Main()
        {

            Console.WriteLine("");
            Console.WriteLine("Please choose an option by entering a number.");
            Console.WriteLine("1. View Stats");
            Console.WriteLine("2. View Inventory");
            Console.WriteLine("3. Fight Monster");
            Console.WriteLine("4. Go To The Shop");
            Console.WriteLine("5. Equip Weapon");
            Console.WriteLine("6. Equip Armor");
            Console.WriteLine("7. Sip Some Sizzurp");
            Console.WriteLine("8. QUIT");
            Console.WriteLine("");
            Console.Write("Enter your selection: ");
            var input = Console.ReadLine();

            if (input == "1")
            {
                this.Stats();
            }
            else if (input == "2")
            {
                this.Inventory();
            }
            else if (input == "3")
            {
                this.Fight();
            }
            else if (input == "4")
            {
                Shop.Menu();
            }
            else if (input == "5")
            {
                Hero.EquipWeapon();
            }
            else if (input == "6")
            {
                Hero.EquipArmor();
            }
            else if (input == "7")
            {
                Hero.EquipPotion();
            }
            else if (input == "8")
            {
                Environment.Exit(0);
            }
            else
            {
                this.Main();
            }

            #region Switch Statement
            //switch (input) {
            ////switch on value of input
            //    case "1":
            //        Stats();
            //        break;
            //    case "2":
            //        Inventory();
            //        break;
            //    case "3":
            //        Fight();
            //        break;
            //    case "4":
            //        shop.Menu();
            //        break;
            //    case "5":
            //    default:
            //        return;
            //}
            #endregion
        }


        public void Stats()
        {
            Hero.ShowStats();
            Console.WriteLine("");
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadKey();
            this.Main();
        }

        public void Inventory()
        {
            Hero.ShowInventory();
            Console.WriteLine("");
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadKey();
            this.Main();
        }

        public void Fight()
        {
            var myMonster = new Monster("Steven Seagull", 8, 10, 15, 5, 5);
            myMonster = new Monster("Jean - Claude Van Danimal", 10, 12, 20, 7, 10);
            myMonster = new Monster("Rocky Balboaconstrictor", 15, 12, 30, 9, 15);
            myMonster = new Monster("Woodchuck Norris", 20, 15, 40, 11, 20);
            var fight = new Fight(Hero, this, myMonster);

            fight.Start();
        }
    }
}