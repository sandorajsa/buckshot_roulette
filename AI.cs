using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buckshot_roulette
{
    class AI : Player
    {
        public AIPlayer(string name, int maxLives) : base('Az ellenfél', maxLives)
        {
        }

        //public override bool PullTrigger(Gun gun)
        //{
        //    bool decision = WhoToShoot();
        //    if (decision)
        //    {
        //        return base.PullTrigger(gun);
        //    }
        //}

        private bool WhoToShoot(Gun gun) //true = self, false = player
        {
            int liveBullets = gun.LiveNum;
            int blankBullets = gun.BlankNum;

            if (liveBullets > blankBullets)
                return false;
            else
                return true;
        }
    }
}
