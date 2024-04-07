using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace buckshot_roulette
{
    static class Game
    {
        public static void Round(Player player, AI ai, List<bool> bullets)
        {
            Gun.LoadBullets();
            player.GetItems();
            ai.GetItems();

            Console.WriteLine($"A fegyverben lévő lövedékek:\n");
            WriteBullets(bullets);
            Thread.Sleep(100);

            while (player.Lives > 0 && ai.Lives > 0 && bullets.Count > 0)
            {
                //ide kéne a kérdés hogy meghúzza e a ravaszt
                //3 opció: lője magát, lője az ellenfelet, használjon tárgyakat
                //tárgyak: mutassa az összes elérhető tárgyat és lehessen választani
            }
        }

        public static void WriteBullets(List<bool> bullets)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"\t{Gun.LiveNum} db éles");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\t{Gun.BlankNum} db vaktöltény");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void UseItems(Player player)
        {

        }
    }
}