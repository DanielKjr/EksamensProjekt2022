using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamensProjekt2022
{
    public class SurvivalAspect : Component
    {
        private int health;
        private int hunger;
        private int energy;
        private int maxHunger = 50;
        private int maxEnergy = 50;
        private int maxHealth;

        private bool playerAspects;
        private float hungerDamageCountDown = 2;
        private float hungerCountDown = 10;
        public event EventHandler DeathEvent;

        public int CurrentHealth
        {
            get { return health; }
            set
            {
                health = value;
                if (health < 0)
                {
                    health = 0;
                }

                if (health > maxHealth)
                {
                    health = maxHealth;
                }

                if (health == 0)
                {
                    OnDeathEvent();
                }

            }
        }

        public int CurrentEnergy
        {
            get { return energy; }
            set
            {
                energy = value;

                if (energy < 0)
                {
                    energy = 0;
                }

                if (energy > maxEnergy)
                {
                    health = maxEnergy;
                }
                if (energy == 0)
                {
                    //change movespeed or something, not implemented
                }
            }
        }

        public int CurrentHunger
        {
            get { return hunger; }
            set
            {
                hunger = value;

                if (hunger < 0)
                {
                    hunger = 0;
                }
                if (hunger > maxHunger)
                {
                    hunger = maxHunger;
                }

            }
        }

        public bool IsAlive
        {
            get
            {
                return health > 0;
            }
        }

        public int MaxHealth
        {
            get { return maxHealth; }
            private set { maxHealth = value; }
        }


        public SurvivalAspect(int health, int maxHealth, int hunger, int energy)
        {
            this.health = health;
            this.maxHealth = maxHealth;
            this.hunger = hunger;
            this.energy = energy;
            playerAspects = true;
        }
        public SurvivalAspect(int health, int maxHealth)
        {
            this.health = health;
            this.maxHealth = maxHealth;
        }

        protected virtual void OnDeathEvent()
        {
            DeathEvent?.Invoke(this.GameObject, new EventArgs());
        }

        public void Heal(int healing)
        {
            if (healing > 0)
            {
                CurrentHealth += healing;
            }

            if (!IsAlive)
                return;
        }

        public void TakeDamage(int dmg)
        {
            CurrentHealth -= dmg;
        }

        public override void Update(GameTime gameTime)
        {
            hungerDamageCountDown -= GameWorld.DeltaTime;
            hungerCountDown -= GameWorld.DeltaTime;

     
            if (playerAspects && CurrentHunger == 50 && hungerDamageCountDown <= 0)
            {

                TakeDamage(2);
                hungerDamageCountDown = 20;


            }
           
            if (playerAspects && CurrentHunger < 50 && hungerCountDown <= 0)
            {

                hungerCountDown = 20;
                CurrentHunger++;
            }

        }
    }
}
