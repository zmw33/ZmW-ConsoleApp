using System;
namespace OOP_RPG
{
    public class Armor : IItem
    {
        public string Name { get; set; }
        public int Defense { get; set; }
        public int OriginalValue { get; set; }
        public int ResellValue { get; set; }

        public Armor() { }

        public Armor(string name, int defense)
        {
            this.Name = name;
            this.Defense = defense;
        }

        public Armor(string name, int defense, int originalValue, int resellValue)
        {
            this.Name = name;
            this.Defense = defense;
            this.OriginalValue = originalValue;
            this.ResellValue = resellValue;
        }
    }
}