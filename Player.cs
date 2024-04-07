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
        public int Lives { get; set; }
        public int Damage = 1;
        public List<string> Items { get; private set; }
        private List<string> availableItems = new List<string> { "Sör", "Elsősegély doboz", "Nagyító", "Kézi fűrész" };

        public Player(string name, int maxLives)
        {
            Name = name;
            Lives = maxLives;
            Items = new List<string>();
        }

        public bool ShotAt(Player enemy) //ha azt választja hogy a másikat lőjje
        {
            if (Gun.Shoot())
            {
                Lives -= enemy.Damage;
                enemy.Damage = 1;
                return Lives > 0;
            }
            else
            {
                Damage = 1;
                return true;
            }
            
        }
        public bool ShootSelf() //ha önmagát akarja lőni
        {
            if (Gun.Shoot())
            {
                Lives -= Damage;
                Damage = 1;
                return Lives > 0;
            }
            else
            {
                Damage = 1;
                return true;
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
        public void ChooseItem(string item) //ez a tárgy kivákastása, ezt kell menüben meghívni
        {
            Items.Remove(item);
            Console.WriteLine($"{Name} elhasznált egy {item}t.");
            UseItem(item);
        }
        private void UseItem(string item) //ez megoldja az effecteket amiket az itemek adnak
        {
            switch (item)
            {
                case "Sör":
                    Console.WriteLine($"A következő töltény el lett távolítva a fegyverből. \nA lövedék {Gun.RemoveLastBullet()} volt.");
                    break;
                case "Elsősegély doboz":
                    Lives++;
                    break;
                case "Nagyító":
                    Gun.NextBullet();
                    break;
                case "Kézi fűrész":
                    Damage = 2;
                    break;
            }
        }
    }
}
