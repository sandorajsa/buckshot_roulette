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
            Console.Write($"\t{Gun.LiveNum} db éles");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\t{Gun.BlankNum} db vaktöltény");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void UseItems(Player player)
        {

        }

        static void PlayerTurn()
        {
            int v;

            Console.ResetColor();
            v = Menu.MenuRajzol(new string[] { "Shoot self", "Shoot opponent", "Use item" }); // Akarunk e kilépés vagy vissza gombot?
            switch (v)
            {
                case 0:
                    Console.Clear();
                    Console.WriteLine("ai.shotat függvény"); ;
                    break;
                case 1:
                    Console.Clear();
                    Console.WriteLine("player.shootself függvény");// Valamiért most nem működik az agyam és esélytelen hogy ezt most megcsináljam de megbeszéljük aztán holnap meglesz vagy ha akarod beírhatod a függvényeket
                    break;
                case 2:
                    GetItemsList();
                    break;
            }
            Console.ResetColor();
        }

   
        static void GetItemsList()
        {
            int v;
            Console.ResetColor(); // egy keveset gondolkodtam azon hogy hogyan tudjuk a menübe a player tényleges itemjeit berakni de végül nem jött össze(legegyszerűbb ha csak 3 item van/többet kap a játékos és akkor a menü fix és nem random) de ez is megoldható normálisan
            v = Menu.MenuRajzol(new string[] { "Item 1", "Item 2", "Item 3" }); // itt lehetne a vissza gomb
            switch (v)
            {
                case 0:
                    Console.Clear();
                    Console.WriteLine("Used item 1");
                    break;
                case 1:
                    Console.Clear();
                    Console.WriteLine("used item 2");
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("used item 3");
                    break;
            }
            Console.ResetColor();
        }

        static void AITurn()
        {
            
            Console.WriteLine("\nai.whotoshoot"); // ja
        }
    }
}