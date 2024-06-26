﻿using System;
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
            Console.WriteLine($"A fegyverben lévő lövedékek:\n");
            WriteBullets();



            while (player.Lives > 0 && ai.Lives > 0 && Gun.Bullets.Count > 0)
            {
                
                
                Thread.Sleep(3000);
                Console.Clear();
                Console.Write($"\nA(z) {roundCount}. kör következik");
                Thread.Sleep(3000);
                PlayerTurn(player, ai);
                Console.WriteLine($"\n{player.Name}-nek {player.Lives} élete maradt, emellett {player.Points} pontja van.");
                Console.WriteLine($"\n{ai.Name}-nak {ai.Lives} élete maradt, emellett {ai.Points} pontja van.");
                Thread.Sleep(5000);
                Console.Clear();


                if (ai.Lives > 0 && player.Lives > 0 && Gun.NumOfBullets > 0)
                {
                    AITurn(player, ai);
                    Console.WriteLine($"\n{player.Name}-nek {player.Lives} élete maradt, emellett {player.Points} pontja van.");
                    Console.WriteLine($"\n{ai.Name}-nak {ai.Lives} élete maradt, emellett {ai.Points} pontja van.");
                    Thread.Sleep(6000);
                    Console.Clear();

                }
                if (ai.Lives <= 0 || player.Lives <= 0 || Gun.NumOfBullets <= 0)
                    GameEnd(ai, player);

                roundCount++;
                Thread.Sleep(2000);
                Console.Clear();

            }
            //newGame(player, ai);
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
            v = Menu.MenuRajzol(new string[] { "Fegyver magadra fogása", "Lőjj az ellenfeledre", "Tárgyak használata" }); // Akarunk e kilépés vagy vissza gombot?
            switch (v)
            {
                case 0:
                    Console.Clear();
                    bool bullet = player.ShootSelf(ai); 
                    if(bullet)
                    {
                        Console.WriteLine($"\n{player.Name} önmagára lőtt ÉLES tölténnyel!");
                    }else Console.WriteLine($"\n{player.Name} önmagára lőtt VAK tölténnyel!");

                    Thread.Sleep(3300);
                    break;
                case 1:
                    Console.Clear();
                    bullet = ai.ShotAt(player);// Valamiért most nem működik az agyam és esélytelen hogy ezt most megcsináljam de megbeszéljük aztán holnap meglesz vagy ha akarod beírhatod a függvényeket
                    if (bullet)
                    {
                        Console.WriteLine($"\n{player.Name} ellenfelére lőtt ÉLES tölténnyel!");
                    }
                    else Console.WriteLine($"\n{player.Name} ellenfelére lőtt VAK tölténnyel!");
                    Thread.Sleep(3300);
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
            v = Menu.MenuRajzol(new string[] { items[0], items[1], items[2] , "Mégse"}); // itt lehetne a vissza gomb
            switch (v)
            {
                case 0:
                    Console.Clear();
                    player.ChooseItem(items[0]);
                    Thread.Sleep(6500);
                    AfterItem(player, ai);
                    break;
                case 1:
                    Console.Clear();
                    player.ChooseItem(items[1]);
                    Thread.Sleep(6500);
                    AfterItem(player, ai);
                    break;
                case 2:
                    Console.Clear();
                    player.ChooseItem(items[2]);
                    Thread.Sleep(6500);
                    AfterItem(player, ai);
                    break;
                case 3:
                    Console.Clear();
                    PlayerTurn(player,ai);
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
                    bool bullet = player.ShootSelf(ai);
                    if (bullet)
                    {
                        Console.WriteLine($"\n{player.Name} önmagára lőtt ÉLES tölténnyel!");
                    }
                    else Console.WriteLine($"\n{player.Name} önmagára lőtt VAK tölténnyel!");
                    Thread.Sleep(3300);
                    break;
                case 1:
                    Console.Clear();
                    bullet = ai.ShotAt(player);
                    if (bullet)
                    {
                        Console.WriteLine($"\n{player.Name} ellenfelére lőtt ÉLES tölténnyel!");
                    }
                    else Console.WriteLine($"\n{player.Name} ellenfelére lőtt VAK tölténnyel!");
                    Thread.Sleep(3300);
                    break;
            }
            Console.ResetColor();
        }

        static void GameEnd(AI ai, Player player)
        {
            if (ai.Lives <= 0)
            {
                Console.WriteLine($"\n{ai.Name} meghalt.\n{player.Name} nyert! Pontjai: {player.Points}");
                Thread.Sleep(1500);
            }
            else if (player.Lives <= 0)
            {
                Console.WriteLine($"\n{player.Name} meghalt.\n{ai.Name} nyert! Pontjai: {ai.Points}");
                Thread.Sleep(1500);

            }
            else
            {
                Console.WriteLine($"\nElfogytak a lövedékek a fegyverből.\nJátékosok pontjai: \n\t{player.Name} - {player.Points} pont\n\t{ai.Name} - {ai.Points} pont");
                Thread.Sleep(1500);
                if (player.Points > ai.Points)
                {
                    Console.WriteLine($"{player.Name} nyert");
                    Thread.Sleep(1500);
                }
                    
                else if (ai.Points > player.Points)
                {
                    Console.WriteLine($"{ai.Name} nyert");
                    Thread.Sleep(1500);
                }

                else
                {
                    Console.WriteLine("A játék döntetlen");
                    Thread.Sleep(1500);
                }
                    
            }
        }
        //static void newGame(Player player, AI ai)
        // {
        //    int v;
        //    Console.ResetColor();
        //    v = Menu.MenuRajzol(new string[] { "Új játék", "Kilépés"});
        //    switch (v)
        //    {
        //        case 0:
        //            Console.Clear();
        //            Round(player, ai);
        //            Thread.Sleep(4000);
        //            break;
        //        case 1:
        //            break;
        //    }
        //    Console.ResetColor();
        //}
    }
}