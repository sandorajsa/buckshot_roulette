using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buckshot_roulette
{
    internal static class Game
    {
        public static List<bool> bullets = new List<bool>();

        public static void Round()
        {
            Random r = new Random();
            int bulletNum = r.Next(3, 6);
            int liveNum = 0;
            for (int i = 0; i < bulletNum; i++)
            {
                int choice = r.Next(0, 2);
                if (choice == 0)
                {
                    bullets.Add(true);
                    liveNum++;
                } 
                else
                    bullets.Add(false);
            }
            foreach (var item in bullets)
            {
                Console.WriteLine(item);
            }
        }
    }
}
