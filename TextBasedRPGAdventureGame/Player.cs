using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPGAdventureGame
{
    class Player
    {
        public string playerClass { get; set; }
        public int playerHealth { get; set; }
        public int playerWeapon { get; set; }
        public int playerAgility { get; set; }
        public int playerLevel { get; set; }
        public int playerScore { get; set; }

        public Player()
        {}

        public Player(string pClass, int pHealth, int pWeapon, int pAgility, int pLevel, int pScore)
        {
            playerClass = pClass;
            playerHealth = pHealth;
            playerWeapon = pWeapon;
            playerAgility = pAgility;
            playerLevel = pLevel;
            playerScore = pScore;
        }

        public void printPlayerInfo ()
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("You have chosen to be a {0}.", playerClass.ToLower());
            Console.WriteLine("The class has the following starting stats:");
            Console.WriteLine("Health = {0}", playerHealth);
            Console.WriteLine("Weapon damage = {0}", playerWeapon);
            Console.WriteLine("Agility = {0}", playerAgility);
            Console.WriteLine("-------------------------------------------");
        }

        public static void scoreDisplay (int pScore)
        {
            if (pScore < 8)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Your score is at {0}. You need 8 to unlock the door to the Demon King", pScore);
                Console.ResetColor();
            }
            else if (pScore >= 8)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Your score is at {0}. You have enough to open the door to the Demon King", pScore);
                Console.ResetColor();
            }
        }

        public static int gainExp (int eExp)
        {
            int expPool = 0;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You gained {0} exp", eExp);
            Console.ResetColor();
            expPool += eExp;
            return expPool;
        }

        public static bool gainLevel (ref int pLevel, ref int expPool)
        {
            bool ifLevel = false;
            if ((expPool >= 10) && (pLevel < 2))
            { pLevel++; expPool = 0; Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("You gained a level! You are level {0}", pLevel); Console.ResetColor(); return ifLevel = true; }
            else if ((expPool >= 20) && (pLevel < 3))
            { pLevel++; expPool = 0; Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("You gained a level! You are level {0}", pLevel); Console.ResetColor(); return ifLevel = true; }
            else if ((expPool >= 40) && (pLevel < 4))
            { pLevel++; expPool = 0; Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("You gained a level! You are level {0}", pLevel); Console.ResetColor(); return ifLevel = true; }
            else if ((expPool >= 80) && (pLevel < 5))
            { pLevel++; expPool = 0; Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("You gained a level! You are level {0}", pLevel); Console.ResetColor(); return ifLevel = true; }
            else if ((expPool >= 120) && (pLevel < 6))
            { pLevel++; expPool = 0; Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("You gained a level! You are level {0}", pLevel); Console.ResetColor(); return ifLevel = true; }
            else if ((expPool >= 160) && (pLevel < 7))
            { pLevel++; expPool = 0; Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("You gained a level! You are level {0}", pLevel); Console.ResetColor(); return ifLevel = true; }
            else if ((expPool >= 200) && (pLevel < 8))
            { pLevel++; expPool = 0; Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("You gained a level! You are level {0}", pLevel); Console.ResetColor(); return ifLevel = true; }
            else if ((expPool >= 300) && (pLevel < 9))
            { pLevel++; expPool = 0; Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("You gained a level! You are level {0}", pLevel); Console.ResetColor(); return ifLevel = true; }
            else if ((expPool >= 400) && (pLevel < 10))
            { pLevel++; expPool = 0; Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("You gained a level! You are level {0}", pLevel); Console.ResetColor(); return ifLevel = true; }
            return ifLevel;
        }

        public static void addStatsUponLevel(ref int pHealth, ref int pAgility, ref int pWeapon, bool ifLvl, string pClass, int pLevel)
        {
            if (ifLvl == true)
            {
                if (pClass == "WARRIOR")
                { pHealth += 50; pAgility += 5; pWeapon += 5; }
                else if (pClass == "TROJAN")
                { pHealth += 25; pAgility += 5; pWeapon += 10; }
                else if (pClass == "NINJA")
                { pHealth += 10; pAgility += 5; pWeapon += 20; }
            }
            if ((ifLvl == true) && (pLevel == 10)) // huge bonus to fight boss
            {
                if (pClass == "WARRIOR")
                { pHealth += 800; pWeapon += 45; }
                else if (pClass == "TROJAN")
                { pHealth += 600; pWeapon += 50; }
                else if (pClass == "NINJA")
                { pHealth += 400; pWeapon += 100; }
            }
        }

        public static void statDisplay (int pHealth, int pAgility, int pWeapon, int pLevel, string pClass)
        {
            if (pLevel == 10)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You feel a sudden surge of power... (press any key to continue)");
                Console.ReadKey(true);
                Console.WriteLine("Congratulations! You have regained all your skills and levels as a master {0} before entering the dimension.", pClass);
                Console.WriteLine("You are definately ready to take on the Demon King! Good Luck!");
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You have {0} hp, {1} agility, and {2} weapon damage now.", pHealth, pAgility, pWeapon);
            Console.ResetColor();
        }

        public static void maxLevelReminder (int pLevel)
        {
            if (pLevel >= 10)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Your level is maxed out. If you have enough score, go fight the demon king.");
                Console.ResetColor();
            }
        }
    }

}
