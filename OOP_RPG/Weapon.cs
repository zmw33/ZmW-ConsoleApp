using System;
namespace OOP_RPG
{
    public class Weapon : IItem
    {
        public string Name { get; set; }
        public int Strength { get; set; }
        public int OriginalValue { get; set; }
        public int ResellValue { get; set; }

        public Weapon() { }

        public Weapon(string name, int strength)
        {
            this.Name = name;
            this.Strength = strength;
        }

        public Weapon(string name, int strength, int originalValue, int resellValue)
        {
            this.Name = name;
            this.Strength = strength;
            this.OriginalValue = originalValue;
            this.ResellValue = resellValue;
        }

    }
}