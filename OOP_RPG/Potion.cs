using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    public class Potion : IItem
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int OriginalValue { get; set; }
        public int ResellValue { get; set; }

        public Potion() { }

        public Potion(string name, int hp)
        {
            this.Name = name;
            this.HP = hp;
        }

        public Potion(string name, int hp, int originalValue, int resellValue)
        {
            this.Name = name;
            this.HP = hp;
            this.OriginalValue = originalValue;
            this.ResellValue = resellValue;
        }
    }
}
