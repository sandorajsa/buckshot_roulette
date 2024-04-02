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
        public bool Lives { get; set; }

        public Player(string name, int maxLives)
        {
            Name = name;
            Lives = maxLives;
        }

        public virtual bool PullTrigger(Gun gun)
        {
            Console.WriteLine($"{Name} meghúzza a ravaszt...");
            if (gun.Shoot())
            {
                Lives--;
                Console.WriteLine($"A lövedék éles volt. {Name} hátralévő életei: {Lives}");
                return Lives > 0;
            }
            else
            {
                Console.WriteLine($"A fegyver elsült. {Name} hátralévő életei: {Lives}");
                return true;
            }
        }
    }
}
