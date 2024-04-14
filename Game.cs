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

            

            while (player.Lives > 0 && ai.Lives > 0 && Gun.Bullets.Count > 0)
            {
                Console.WriteLine($"A fegyverben lévő lövedékek:\n");
                WriteBullets();
                Thread.Sleep(8000);
                Console.Clear();
                Console.Write($"\nA(z) {roundCount}. kör következik");
                Thread.Sleep(5000);
                PlayerTurn(player, ai);
                if (ai.Lives > 0)
                {
                    AITurn(player, ai);
                }
                else Console.WriteLine("Nyertél??");

                Console.WriteLine($"\n{player.Name}-nek {player.Lives} élete maradt, emellett {player.Points} pontja van.");
                Console.WriteLine($"\n{ai.Name}-nak {ai.Lives} élete maradt, emellett {ai.Points} pontja van.");
                roundCount++;
                Thread.Sleep(1000);
                

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
            v = Menu.MenuRajzol(new string[] { "Fegyver magadra fogása", "Lőjj az ellenfeledre", "Kacatok használata" }); // Akarunk e kilépés vagy vissza gombot?
            switch (v)
            {
                case 0:
                    Console.Clear();
                    ai.ShotAt(ai);
                    Console.WriteLine($"{player.Name} önmagára lőtt!");
                    Thread.Sleep(1000);
                    break;
                case 1:
                    Console.Clear();
                    player.ShootSelf(ai);// Valamiért most nem működik az agyam és esélytelen hogy ezt most megcsináljam de megbeszéljük aztán holnap meglesz vagy ha akarod beírhatod a függvényeket
                    Console.WriteLine($"{player.Name} ellenfelére lőtt!");
                    Thread.Sleep(1000);
                    break;
                case 2:
                    GetItemsList(player, ai);
                    break;
            }
            Console.ResetColor();
        }


        static void GetItemsList(Player player, AI ai)
        {
            List<string> items = new List<string>();
            items = player.Items;
            int v;
            Console.ResetColor();
            v = Menu.MenuRajzol(new string[] { items[0], items[1], items[2] }); // itt lehetne a vissza gomb
            switch (v)
            {
                case 0:
                    Console.Clear();
                    player.ChooseItem(items[0]);
                    AfterItem(player, ai);
                    break;
                case 1:
                    Console.Clear();
                    player.ChooseItem(items[1]);
                    AfterItem(player, ai);
                    break;
                case 2:
                    Console.Clear();
                    player.ChooseItem(items[2]);
                    AfterItem(player, ai);
                    break;
            }
            Console.ResetColor();
            
        }

        static void AITurn(Player player, AI ai)
        {

            ai.PullTrigger(player);

        }

        static void AfterItem(Player player, AI ai)
        {
            int v ;
            v = Menu.MenuRajzol(new string[] { "Fegyver magadra fogása", "Lőjj az ellenfeledre"});
            switch (v)
            {
                case 0:
                    Console.Clear();
                    ai.ShotAt(ai);
                    Console.WriteLine($"{player.Name} önmagára lőtt!");
                    Thread.Sleep(1000);
                    break;
                case 1:
                    Console.Clear();
                    player.ShootSelf(ai);
                    Console.WriteLine($"{player.Name} ellenfelére lőtt!");
                    Thread.Sleep(1000);
                    break;
            }
            Console.ResetColor();
        }
    }
}