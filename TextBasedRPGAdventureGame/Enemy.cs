using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPGAdventureGame
{
    class Enemy
    {
        public string enemyName { get; set; }
        public int enemyHealth { get; set; }
        public int enemyWeapon { get; set; }  // damage
        public int enemyAttack { get; set; } // accuracy
        public int enemyExp { get; set; }

        public Enemy()
        {
            enemyName = "GOBLIN";
            enemyHealth = 50;
            enemyWeapon = 5;
            enemyAttack = 60;
            enemyExp = 10;
        }

        public Enemy(string eName, int eHealth, int eWeapon, int eAttack, int eExp)
        {
            enemyName = eName;
            enemyHealth = eHealth;
            enemyWeapon = eWeapon;
            enemyAttack = eAttack;
            enemyExp = eExp;
        }
    }

}
