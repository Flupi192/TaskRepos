using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection.Emit;

namespace TestTask
{
    public class Creature
    {

        [Range(1, 30)]
        private int Attack { get; set; }
        [Range(1, 30)]
        private int Defense { get; set; }        
        private int Health { get; set; }
        private int MaxHealth { get; set; }
        private int Damage { get; set; }
        private int MinDamage { get; set; }
        private int MaxDamage { get; set; }

        public Creature(int attack, int defense, int health, int minDamage, int maxDamage, int maxHealth)
        {
            Attack = attack;
            Defense = defense;
            Health = health;
            MaxHealth = maxHealth;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                throw new ArgumentException("Урон не может быть отрицательным.");
            }

            Health -= damage;

            if (Health <= 0)
            {
                throw new InvalidOperationException("Существо умерло.");
            }
        }

        public bool AttackCreature(Creature target)
        {
            int attackModifier = CalculateAttackModifier(target); 

            if (IsAttackSuccessful(attackModifier))
            {
                int damage = CalculateDamage(); 
                target.TakeDamage(damage); 
                return true; 
            }

            return false; 
        }

        private bool IsAttackSuccessful(int attackModifier)
        {
            Random random = new Random();
            int diceRoll = random.Next(1, 7);

            return diceRoll == 5 || diceRoll == 6;
        }

        protected int CalculateAttackModifier(Creature target)
        {
            int modifierAttack = Attack - Defense + 1;
            return modifierAttack > 0 ? modifierAttack : 1;
        }

        private int CalculateDamage()
        {
            Random random = new Random();
            int damage = random.Next(MinDamage, MaxDamage + 1); 
            return damage;
        }

        public void Heal()
        {
            if(Health > 0)
            {
                int maxHealAmount = (int)(MaxHealth * 0.3);
                int healAmount = Math.Min(maxHealAmount, 4);
                Health = Math.Min(Health + healAmount, MaxHealth);
            }
        }

        public class Player: Creature
        {
            public Player(int attack, int defense, int health, int minDamage, int maxDamage, int maxHealth)
            : base(attack, defense, health, minDamage, maxDamage, maxHealth)
            {
                //Дополнительные характеристики  
            }
        }

        public class Monster : Creature
        {
            public Monster(int attack, int defense, int health, int minDamage, int maxDamage, int maxHealth)
            : base(attack, defense, health, minDamage, maxDamage, maxHealth)
            {
                //Дополнительные характеристики
            }
        }
    }
}