using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buckshot_roulette
{
    class Player
    {
        public string Name { get; private set; }
        public int Points { get; set; } = 0;
        public int Lives { get; set; }
        public int MaxLives { get; set; }
        public int Damage = 1;
        public List<string> Items { get; private set; }
        private List<string> availableItems = new List<string> { "Sör", "Elsősegély doboz", "Nagyító", "Kézi fűrész" };

        public Player(string name, int maxLives)
        {
            Name = name;
            Lives = maxLives;
            MaxLives = maxLives;
            Items = new List<string>();
        }

        public bool ShotAt(Player enemy) //ha azt választja hogy a másikat lőjje
        {
            if (Gun.Shoot(enemy))
            {
                Lives -= enemy.Damage;
                enemy.Damage = 1;
                enemy.Points += 100;
                return true;
            }
            else
            {
                enemy.Damage = 1;
                return false;
            }
            
        }
        public bool ShootSelf(Player enemy) //ha önmagát akarja lőni
        {
            if (Gun.Shoot(enemy))
            {
                Lives -= Damage;
                Damage = 1;
                return true;
            }
            else
            {
                Damage = 1;
                Points += 200;
                return false;
            }
        }
        public void GetItems() //ez alapból minden round elején meghívandó és ad 3 random itemet
        {
            Items.Clear();
            Random r = new Random();
            while (Items.Count < 4)
            {
                int i = r.Next(availableItems.Count);
                Items.Add(availableItems[i]);
            }
        }
        public virtual void ChooseItem(string item) //ez a tárgy kiválasztása, ezt kell menüben meghívni
        {
            Items.Remove(item);
            Console.WriteLine($"{Name} elhasznált egy {item}t.");
            UseItem(item);
        }
        protected void UseItem(string item) //ez megoldja az effecteket amiket az itemek adnak
        {
            switch (item)
            {
                case "Sör":
                    if (Gun.NumOfBullets - 1 > 0)
                        Console.WriteLine($"A következő töltény el lett távolítva a fegyverből. \nA lövedék {Gun.RemoveLastBullet()} volt.");
                    break;
                case "Elsősegély doboz":
                    if (Lives < MaxLives)
                    {
                        Lives++;
                        Console.WriteLine($"Visszaszerzett egy életet!");
                    }
                        
                    break;
                case "Nagyító":
                    Gun.NextBulletString();
                    break;
                case "Kézi fűrész":
                    Console.WriteLine($"Megduplázta a sebzését!");
                    Damage = 2;
                    break;
            }
        }
    }
}
