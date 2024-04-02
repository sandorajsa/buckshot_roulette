using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buckshot_roulette
{
    class Gun
    {
        public List<bool> Bullets { get; private set; }
        public int LiveNum { get; private set; }
        public int BlankNum { get; private set; }


        private void LoadBullets()
        {
            Random r = new Random();
            int bulletNum = r.Next(3, 6);
            Bullets = new List<bool>();
            LiveNum = 0;
            BlankNum = 0;
            for (int i = 0; i < bulletNum; i++)
            {
                int choice = r.Next(0, 2);
                if (choice == 0)
                {
                    Bullets.Add(true);
                    LiveNum++;
                }
                else
                {
                    Bullets.Add(false);
                    BlankNum++;
                }  
            }
            //foreach (var item in bullets)
            //{
            //    Console.WriteLine(item);
            //}
        }

        public bool Shoot()
        {
            
        }
    }
}