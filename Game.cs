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
        public static void Round(Player player, AI ai) //round során minden leadott lövés után az AInak a CurLive és CurBlankben meg kell adni mennyi maradt még melyikből
        {
            int roundCount = 1;
            Gun.LoadBullets();
            player.GetItems();
            ai.GetItems();

            

            while (player.Lives > 0 && ai.Lives > 0 && Gun.Bullets.Count > 0)// ez így még lehet nem jó ha meghal az AI akkor szerintem kifagy amikor ő jönne de nem volt kedvem egy függvényben megcsinálni ugyhogy ezt is meg kell még :(
            {
                Console.WriteLine($"A fegyverben lévő lövedékek:\n");
                WriteBullets();
                Thread.Sleep(7000);
                Console.Clear();
                Console.Write($"\nA(z) {roundCount}. kör következik");
                Thread.Sleep(5000);
                PlayerTurn(player, ai); // a menüket leteszteltem jól működnek de most így nincsenek meghívva
                if (ai.Lives > 0)
                {
                    AITurn(player, ai);
                }
                else Console.WriteLine("Nyertél??"); // ez lehet megoldás a fenti commentre de asdasdasdjasnfjasbefitbugsweritgbsipdubgsidzbgpiusdbs
                roundCount++;
                

            }
        }

        public static void WriteBullets()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"\t{Gun.LiveNum} db éles");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\t{Gun.BlankNum} db vaktöltény");
            Console.ForegroundColor = ConsoleColor.White;
        }

        //public static void UseItems(Player player)
        //{

        //}
        static void PlayerTurn(Player player, AI ai)
        {
            int v;

            Console.ResetColor();
            v = Menu.MenuRajzol(new string[] { "Shoot self", "Shoot opponent", "Use item" }); // Akarunk e kilépés vagy vissza gombot?
            switch (v)
            {
                case 0:
                    Console.Clear();
                    Console.WriteLine(ai.ShotAt(ai));
                    break;
                case 1:
                    Console.Clear();
                    Console.WriteLine(player.ShootSelf(ai));// Valamiért most nem működik az agyam és esélytelen hogy ezt most megcsináljam de megbeszéljük aztán holnap meglesz vagy ha akarod beírhatod a függvényeket
                    break;
                case 2:
                    GetItemsList(player);
                    break;
            }
            Console.ResetColor();
        }


        static void GetItemsList(Player player)
        {
            List<string> items = new List<string>();
            items = player.Items;
            int v;
            Console.ResetColor(); // egy keveset gondolkodtam azon hogy hogyan tudjuk a menübe a player tényleges itemjeit berakni de végül nem jött össze(legegyszerűbb ha csak 3 item van/többet kap a játékos és akkor a menü fix és nem random) de ez is megoldható normálisan
            v = Menu.MenuRajzol(new string[] { items[0], items[1], items[2] }); // itt lehetne a vissza gomb
            switch (v)
            {
                case 0:
                    Console.Clear();
                    Console.WriteLine(items[0]);
                    player.ChooseItem(items[0]);
                    break;
                case 1:
                    Console.Clear();
                    Console.WriteLine("used item:", items[1]);
                    player.ChooseItem(items[1]);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("used item:", items[2]);
                    player.ChooseItem(items[2]);
                    break;
            }
            Console.ResetColor();
            
        }

        static void AITurn(Player player, AI ai)
        {

            ai.PullTrigger(player);

        }
    }
}