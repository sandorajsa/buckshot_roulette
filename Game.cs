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

            while (player.Lives > 0 && ai.Lives > 0 && bullets.Count > 0)// ez így még lehet nem jó ha meghal az AI akkor szerintem kifagy amikor ő jönne de nem volt kedvem egy függvényben megcsinálni ugyhogy ezt is meg kell még :(
            {
                PlayerTurn(); // a menüket leteszteltem jól működnek de most így nincsenek meghívva
                if (ai.Lives > 0)
                {
                    AITurn();
                }
                else Console.WriteLine("Nyertél??"); // ez lehet megoldás a fenti commentre de asdasdasdjasnfjasbefitbugsweritgbsipdubgsidzbgpiusdbs

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

        public static void UseItems(Player player)
        {

        }
    }
}