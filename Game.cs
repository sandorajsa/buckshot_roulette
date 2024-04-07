using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace buckshot_roulette
{
    static class Game //ha úgy van lehetne egy default függvény ami az összes többi classban is meghív egy default függvényt ami mindent alaphelyzetbe állít minden kör elején (csak ötlet)
    {
        public static void Round(Player player, AI ai, List<bool> bullets) //round során minden leadott lövés után az AInak a CurLive és CurBlankben meg kell adni mennyi maradt még melyikből
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
                //folyamatosan vizsgálni kell hogy meghalt e már valaki és van e még elég golyó
            }
        }

        public static void WriteBullets(List<bool> bullets)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"\t{Gun.LiveAtStart} db éles");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\t{Gun.BlankAtStart} db vaktöltény");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}