using System;
using System.Collections.Generic;

namespace OOP_RPG
{
    public class Fight
    {
        List<Monster> Monsters { get; set; }
        public Monster Monster { get; set; }

        public Game game { get; set; }
        public Hero hero { get; set; }
        public Shop shop { get; set; }

        public Fight(Hero hero, Game game, Monster monster)
        {
            this.Monsters = new List<Monster>();
            this.Monster = monster;
            this.hero = hero;
            this.shop = shop;
            this.game = game;
            this.AddMonster("Steven Seagull", 8, 10, 15, 5, 5);
            this.AddMonster("Jean-Claude Van Danimal", 10, 12, 20, 7, 10);
            this.AddMonster("Rocky Balboaconstrictor", 15, 12, 30, 9, 15);
            this.AddMonster("Woodchuck Norris", 20, 15, 40, 11, 20);
        }

        public void AddMonster(string name, int strength, int defense, int hp, int spd, int gld)
        {
            var monster = new Monster();
            monster.Name = name;
            monster.Strength = strength;
            monster.Defense = defense;
            monster.OriginalHP = hp;
            monster.CurrentHP = hp;
            monster.Speed = spd;
            monster.Gold = gld;
            this.Monsters.Add(monster);
        }



        public void Start()
        {
            #region other ways to fight 
            //This is how we are fighting the first monster in the list

            //default: fight first monster
            //var monster = this.Monsters[0];

            //fight last monster
            //var monster = this.Monsters[this.Monsters.Count - 1];

            //fight 2nd monster
            //var monster = this.Monsters[1];

            //first monster with less than 20 hitpoints
            //var monster = this.Monsters.FirstOrDefault(m => m.CurrentHP < 20);

            //first monster with strength of at least 11
            //var monster = this.Monsters.FirstOrDefault(m => m.Strength >= 11);
            #endregion
            //random monster - how do i generate a random monster
            var random = new Random();
            var monster = this.Monsters[random.Next(0, this.Monsters.Count)];
            this.Monster = this.Monsters[random.Next(0, this.Monsters.Count)];

            Console.WriteLine("You've encountered a " + Monster.Name + "! " + Monster.Strength + " Strength/" + Monster.Defense + " Defense/" +
            Monster.CurrentHP + " HP. What will you do?");
            Console.WriteLine("1. Fight");
            Console.WriteLine("2. Go Back");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    this.HeroTurn(Monster);
                    break;
                case "2":
                    game.Main();
                    break;
            }
            game.Main();
        }

        public void Again(Monster monster)
        {
            var Monster = monster;
            Console.WriteLine("");
            Console.WriteLine($"{monster.Name} has {monster.CurrentHP} HPs. Fight again?");
            Console.WriteLine($"You currently have {game.Hero.CurrentHP} HPs.");
            Console.WriteLine("1. Fight");
            Console.WriteLine("2. Run");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    HeroTurn(monster);
                    break;
                case "2":
                    Run(monster);
                    break;
                default:
                    game.Main();
                    break;
            }
        }

        public void HeroTurn(Monster monster)
        {

            var compare = hero.Strength - monster.Defense;
            int damage;

            if (compare <= 0)
            {
                damage = 1;
                monster.CurrentHP -= damage;
            }
            else
            {
                damage = compare;
                monster.CurrentHP -= damage;
            }
            Console.WriteLine("You did " + damage + " damage!");

            if (monster.CurrentHP <= 0)
            {
                this.Win(monster);
            }
            else
            {
                this.MonsterTurn(monster);
            }

        }

        public void MonsterTurn(Monster monster)
        {

            int damage;
            var compare = monster.Strength - hero.Defense;
            if (compare <= 0)
            {
                damage = 1;
                hero.CurrentHP -= damage;
            }
            else
            {
                damage = compare;
                hero.CurrentHP -= damage;
            }
            Console.WriteLine(monster.Name + " does " + damage + " damage!");
            if (hero.CurrentHP <= 0)
            {
                this.Lose();
            }
            else
            {
                Again(monster);
            }
        }

        public void Win(Monster monster)
        {

            Console.WriteLine(monster.Name + " has been defeated! You win the battle!");
            //I want to add the monster's gold to the hero's gold
            this.hero.Gold += this.Monster.Gold;
            Console.WriteLine($"You earned {this.Monster.Gold} pieces of gold and now have {this.hero.Gold} gold pieces.");
            game.Main();
        }

        public void Lose()
        {
            Console.WriteLine("You've been defeated! :( GAME OVER.");
            Console.ReadKey();
            game.Main();
        }

        public void Run(Monster monster)
        {
            if (game.Hero.Speed >= monster.Speed)
            {
                Console.WriteLine($"You escaped the {monster.Name}!");
                Console.ReadKey();
                game.Main();
            }
            else
            {
                Console.WriteLine($"You did not escape the {monster.Name}!");
                Console.ReadKey();
                Lose();
            }
        }
    }

}