using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buckshot_roulette
{
    class AI : Player
    {
        private static int LiveAtStart = Gun.LiveAtStart;
        private static int BlankAtStart = Gun.BlankAtStart;

        public AI(string name, int maxLives) : base("Az ellenfél", maxLives)
        {
        }

        //mindig ezt kell meghívni minden körben, mivel ez hív meg minden másik függvényt
        public void PullTrigger(Player enemy)
        {
            bool? nextBullet = null;
            nextBullet = ChooseItem();
            if (Gun.NumOfBullets == 1)
                nextBullet = Gun.LastBullet;
            if (nextBullet != null)
            {
                if (nextBullet == true)
                    enemy.ShotAt(this);
                else
                    ShootSelf(this);
            }
            else
            {
                bool shootSelf = ShouldShootSelf(enemy);
                if (shootSelf)
                    ShootSelf(this);
                else
                    enemy.ShotAt(this);
            }
        }

        private bool ShouldShootSelf(Player enemy) //eldönti kit lőjjön le bizonyos tények alapján
        {
            Random r = new Random();
            if (Gun.LiveNum > Gun.BlankNum)
                if (Lives > 3 && enemy.Lives < Lives + 2)
                    return r.Next(2) == 0;
                else
                    return false;
            else if (Gun.LiveNum < Gun.BlankNum)
                if (Lives == 1 || enemy.Lives > Lives + 2)
                    return false;
                else
                    return true;
            else
                return r.Next(2) == 0;
        }

        public bool? ChooseItem()
        {
            Random r = new Random();
            if (r.Next(2) == 0 && Items.Count > 0)
            {
                string item = Items[r.Next(Items.Count)];
                Console.WriteLine($"{Name} elhasznált egy {item}t.");
                bool? nextBullet = UseItem(item);
                Items.Remove(item);
                return nextBullet;
            }
            return null;
        }

        private new bool? UseItem(string item)
        {
            switch (item)
            {
                case "Sör":
                    Console.WriteLine($"A következő töltény el lett távolítva a fegyverből. \nA lövedék {Gun.RemoveLastBullet()} volt.");
                    return null;
                case "Elsősegély doboz":
                    Lives++;
                    return null;
                case "Nagyító":
                    return Gun.NextBullet();
                case "Kézi fűrész":
                    Damage = 2;
                    return null;
            }
            return null;
        }
    }
}
