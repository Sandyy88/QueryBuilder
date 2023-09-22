using QueryBuilderP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilderP
{
    internal class Pokemon : IClassModel
    {
        public int Id { get; set; }
        public int DexNumber { get; set; } = ' ';
        public string Name { get; set; } = string.Empty;
        public string Form { get; set; } = string.Empty;
        public string Type1 { get; set; } = string.Empty;
        public string Type2 { get; set; } = string.Empty;
        public int Total { get; set; } = ' ';
        public int HP { get; set; } = ' ';
        public int Attack { get; set; } = ' '; 
        public int Defense { get; set; } = ' ';
        public int SpecialAttack { get; set; } = ' ';
        public int SpecialDefense { get; set; } = ' ';
        public int Speed { get; set; } = ' ';
        public int Generation { get; set; } = ' ';
        private static int lastId = 1; //used for the incrementing 

        public Pokemon()
        { 
        }

        public Pokemon(int dexNumber, string name, string Form, string type1, string type2, int total, int hp, int attack, int defense, int specialAttack, int specialdefense, int speed, int generation)
        {
            Id = lastId++; //used for the incrementing 
            this.DexNumber = dexNumber;
            this.Name = name;
            this.Form = Form;
            this.Type1 = type1;
            this.Type2 = type2;
            this.Total = total;
            this.HP = hp;
            this.Attack = attack;
            this.Defense = defense;
            this.SpecialAttack = specialAttack;
            this.SpecialDefense = specialdefense;
            this.Speed = speed;
            this.Generation = generation;
        }

        public override string ToString()
        {
            return $"Id: {Id} DexNumber: {DexNumber} Name: {Name} ";
        }

    }
}
