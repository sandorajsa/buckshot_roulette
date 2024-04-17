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
        public static int LiveNum { get; set; } //folyamatosan v�ltoznak
        public static int BlankNum { get; set; }
        public static int LiveAtStart { get; private set; } //maradnak ugyanazok, a goly�k eredeti ar�nya
        public static int BlankAtStart { get; private set; }
        public static int NumOfBullets { get {  return Bullets.Count; } }
        public static bool LastBullet { get { return Bullets[0]; } }


        public static void LoadBullets() //minden k�r elej�n felt�lti a fegyvert
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

        public static void RandomizeBullets() //randomiz�lja a sorrendet, hogy az els� 2 ne mindig true majd false legyen (ami ugye az�rt kell hogy ne lehessen csak true vagy csak false a fegyverben)
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

        public static bool Shoot(Player ai) //ez maga a l�v�s, azt adja vissza hogy a goly� �les volt e vagy vak
        {
            bool liveOrNot = Bullets[Bullets.Count - 1];
            if (liveOrNot)
                LiveNum--;
            else
                BlankNum--;
            Bullets.RemoveAt(Bullets.Count - 1);
            return liveOrNot;
        }

        public static string RemoveLastBullet() //ez kiszedi az utols� goly�t a fegyverb�l �s stringben visszaadja milyen volt (itemekhez kell)
        {
            bool last = Bullets[Bullets.Count - 1];
            Bullets.RemoveAt(Bullets.Count - 1);
            if (last)
            {
                LiveNum--;
                return "�les";
            }

            else
            {
                BlankNum--;
                return "vakt�lt�ny";
            }
        }

        public static void NextBulletString() //ez ki�rja milyen t�pus� az utols� l�ved�k de nem veszi ki (nagy�t�hoz kell)
        {
            if (Bullets[Bullets.Count - 1])
                Console.WriteLine("A k�vetkez� l�ved�k �les.");
            else
                Console.WriteLine("A k�vetkez� l�ved�k vakt�lt�ny.");
        }
        public static bool NextBullet()
        {
            return Bullets[Bullets.Count - 1];
        }
    }
}