using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buckshot_roulette
{
    static class Gun
    {
        public static List<bool> Bullets = new List<bool>();
        public static int LiveNum { get; set; } //folyamatosan változnak
        public static int BlankNum { get; set; }
        public static int LiveAtStart { get; private set; } //maradnak ugyanazok, a golyók eredeti aránya
        public static int BlankAtStart { get; private set; }
        public static int NumOfBullets { get {  return Bullets.Count; } }
        public static bool LastBullet { get { return Bullets[0]; } }


        public static void LoadBullets() //minden kör elején feltölti a fegyvert
        {
            Random r = new Random();
            int bulletNum = r.Next(7, 9);
            Bullets.Clear();
            Bullets.Add(true);
            Bullets.Add(false);
            LiveNum = 1;
            BlankNum = 1;
            LiveAtStart = 0;
            BlankAtStart = 0;
            for (int i = 0; i < bulletNum - 2; i++)
            {
                int choice = r.Next(0, 2);
                if (choice == 0 && Math.Abs(LiveNum - BlankNum) < 2)
                {
                    Bullets.Add(true);
                    LiveNum++;
                }
                else if (choice == 1 && Math.Abs(BlankNum - LiveNum) < 2)
                {
                    Bullets.Add(false);
                    BlankNum++;
                }
            }
            LiveAtStart += LiveNum;
            BlankAtStart += BlankNum;
            RandomizeBullets();
            //foreach (var item in Bullets)
            //{
            //    Console.WriteLine(item);
            //}
        }

        public static void RandomizeBullets() //randomizálja a sorrendet, hogy az elsõ 2 ne mindig true majd false legyen (ami ugye azért kell hogy ne lehessen csak true vagy csak false a fegyverben)
        {
            Random r = new Random();
            for (int i = Bullets.Count - 1; i > 0; i--)
            {
                int j = r.Next(i + 1);
                bool choosen = Bullets[i];
                Bullets[i] = Bullets[j];
                Bullets[j] = choosen;
            }
        }

        public static bool Shoot(Player ai) //ez maga a lövés, azt adja vissza hogy a golyó éles volt e vagy vak
        {
            bool liveOrNot = Bullets[Bullets.Count - 1];
            if (liveOrNot)
                LiveNum--;
            else
                BlankNum--;
            Bullets.RemoveAt(Bullets.Count - 1);
            return liveOrNot;
        }

        public static string RemoveLastBullet() //ez kiszedi az utolsó golyót a fegyverbõl és stringben visszaadja milyen volt (itemekhez kell)
        {
            bool last = Bullets[Bullets.Count - 1];
            Bullets.RemoveAt(Bullets.Count - 1);
            if (last)
            {
                LiveNum--;
                return "éles";
            }

            else
            {
                BlankNum--;
                return "vaktöltény";
            }
        }

        public static void NextBulletString() //ez kiírja milyen típusú az utolsó lövedék de nem veszi ki (nagyítóhoz kell)
        {
            if (Bullets[Bullets.Count - 1])
                Console.WriteLine("A következõ lövedék éles.");
            else
                Console.WriteLine("A következõ lövedék vaktöltény.");
        }
        public static bool NextBullet()
        {
            return Bullets[Bullets.Count - 1];
        }
    }
}