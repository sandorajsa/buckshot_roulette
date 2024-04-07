using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buckshot_roulette
{
    class AI : Player
    {
        private int LiveAtStart = Gun.LiveAtStart;
        private int BlankAtStart = Gun.BlankAtStart;
        private int CurLive {  get; set; }
        private int CurBlank { get; set; }

        public AI(string name, int maxLives) : base("Az ellenfél", maxLives)
        {
        }

        //mindig ezt kell meghívni minden körben, mivel ez hív meg minden másik függvényt
        private bool WhoToShoot() //true = self, false = player //ez az ahol az AI eldönti hogy önmagát vagypedig a másikat lőjje bizonyos faktorok alapján pl: golyók aránya, maradék életek száma
        {
            if (Gun.LiveNum > Gun.BlankNum)
                return false;
            else if (Gun.BlankNum > Gun.LiveNum)
                return true;
            else
                return false; //másikat lövi, mivel nem biztos benne
        }

        
    }
}
