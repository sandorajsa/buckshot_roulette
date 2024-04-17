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
                {
                    enemy.ShotAt(this);
                    Console.WriteLine($"\nAz ellenfél rád lőtt ÉLES tölténnyel!");
                }
                    
                else
                {
                    ShootSelf(this);
                    Console.WriteLine($"\nAz ellenfél önmagára lőtt VAK tölténnyel!");
                }   
            }
            else
            {
                bool shootSelf = ShouldShootSelf(enemy);
                if (shootSelf)
                {
                    bool bullet = ShootSelf(this);
                    if (bullet)
                        Console.WriteLine($"\nAz ellenfél önmagára lőtt ÉLES tölténnyel!");
                    else 
                        Console.WriteLine($"\nAz ellenfél önmagára lőtt VAK tölténnyel!");
                    Thread.Sleep(2000);
                }
                else
                {
                    bool bullet = enemy.ShotAt(this);
                    if (bullet)
                        Console.WriteLine($"\nAz ellenfél rád lőtt ÉLES tölténnyel!");
                    else 
                        Console.WriteLine($"\nAz ellenfél rád lőtt VAK tölténnyel!");
                    Thread.Sleep(2000);
                }
                
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
            else if (Damage == 2)
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
                    if (Gun.NumOfBullets - 1 > 0)
                        Console.WriteLine($"A következő töltény el lett távolítva a fegyverből. \nA lövedék {Gun.RemoveLastBullet()} volt.");
                    return null;
                case "Elsősegély doboz":
                    if (Lives < MaxLives)
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
