using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TextBasedRPGAdventureGame
{
    class Program
    {
        // to auto maximize console window at all times
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE); // to auto maximize console window

            Player Warrior = new Player("WARRIOR", 200, 10, 10, 1, 0); // high health, lower weapon damage and agility (agility increases chance to dodge enemy attack)
            Player Trojan = new Player("TROJAN", 100, 10, 20, 1, 0); // moderate health, lower weapon damage and moderate agility
            Player Ninja = new Player("NINJA", 50, 20, 40, 1, 0); // low health, high weapon damage and high agility
            // Check out function addStatsUponLevel to see how stats increase per level. different classes get different bonuses
            // Check out gainLevel function in player class to see experience points table and level
            // To beat the game fast, use ninja and train to level 5 on goblins/ogres before tackling the demons. 
            Enemy Goblin = new Enemy();
            Enemy Ogre = new Enemy("OGRE", 80, 10, 70, 15);
            Enemy LDemon = new Enemy("LESSER DEMON", 320, 30, 80, 30);
            Enemy GDemon = new Enemy("GREATER DEMON", 400, 40, 97, 40);
            Enemy DemonKing = new Enemy("DEMON KING", 800, 100, 97, 100);
            string playerClass = ""; int playerHealth = 0; int playerWeapon = 0; int playerAgility = 0; int playerExp = 0; bool ifplayerLevel; int playerLevel = 1; int Score = 0;
            int loop = 0;
            // menu
            while (loop == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  _                                    _                  _                       __                 ");
                Console.WriteLine(" /  |_  ._ _  ._  o  _ |  _   _    _ _|_   _|_ |_   _    | \\  _  ._ _   _  ._    (_  |  _.     _  ._ ");
                Console.WriteLine(" \\_ | | | (_) | | | (_ | (/_ _>   (_) |     |_ | | (/_   |_/ (/_ | | | (_) | |   __) | (_| \\/ (/_ |  ");
                Console.WriteLine("                                                                                           /         ");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("                .                                                                .");
                Console.WriteLine("               / \\                                                              / \\");
                Console.WriteLine("               | |                                                              | |");
                Console.WriteLine("               | |                                                              | |");
                Console.WriteLine("               | |                                                              | |");
                Console.WriteLine("               |.|                                                              |.|");
                Console.WriteLine("               |.|                                                              |.|");
                Console.WriteLine("               |:|                                                              |:|");
                Console.WriteLine("               |:|                                                              |:|");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("             `--8--'                                                          `--8--'");
                Console.WriteLine("                8                         1 - Start Game                         8");
                Console.WriteLine("                O                         2 - Quit                               O");
                Console.ResetColor();
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Enter a choice: 1 or 2");
                string selection = Console.ReadLine();
                if (selection == "1")
                { loop = 1; }
                else if (selection == "2")
                { Environment.Exit(0); }
                Console.Clear();
            }

            loop = 0;
            // choose class
            while (loop == 0)
            {
                Console.WriteLine("Please choose your class:");
                Console.WriteLine("Warrior");
                Console.WriteLine("Trojan");
                Console.WriteLine("Ninja");
                Console.WriteLine("Type out your choice and press Enter:");
                playerClass = Console.ReadLine().ToUpper();
                if (playerClass == Warrior.playerClass)
                {
                    Warrior.printPlayerInfo(); // prints out info
                    Console.WriteLine("Are you sure you want to choose this class? Type yes or no");
                    string choice = Console.ReadLine().ToUpper();
                    if (choice == "YES" || choice == "Y")
                    { Console.Clear(); loop = 1; } // to get out of selection screen and start game
                    else if (choice == "NO" || choice == "N")
                    { loop = 0; }
                    playerHealth = Warrior.playerHealth; // player stats initialized differently dependning on class chosen
                    playerWeapon = Warrior.playerWeapon;
                    playerAgility = Warrior.playerAgility;
                }
                else if (playerClass == Trojan.playerClass)
                {
                    Trojan.printPlayerInfo();
                    Console.WriteLine("Are you sure you want to choose this class? Type yes or no");
                    string choice = Console.ReadLine().ToUpper();
                    if (choice == "YES" || choice == "Y")
                    { Console.Clear(); loop = 1; }
                    else if (choice == "NO" || choice == "N")
                    { loop = 0; }
                    playerHealth = Trojan.playerHealth;
                    playerWeapon = Trojan.playerWeapon;
                    playerAgility = Trojan.playerAgility;
                }
                else if (playerClass == Ninja.playerClass)
                {
                    Ninja.printPlayerInfo();
                    Console.WriteLine("Are you sure you want to choose this class? Type yes or no");
                    string choice = Console.ReadLine().ToUpper();
                    if (choice == "YES" || choice == "Y")
                    { Console.Clear(); loop = 1; }
                    else if (choice == "NO" || choice == "N")
                    { loop = 0; }
                    playerHealth = Ninja.playerHealth;
                    playerWeapon = Ninja.playerWeapon;
                    playerAgility = Ninja.playerAgility;
                }
                else { Console.WriteLine("Invalid choice, please try again."); }

            }
            loop = 0;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                   _.--.    .--._");
            Console.WriteLine("                 .\"  .\"      \".  \".");
            Console.WriteLine("                ;  .\"    /\\    \".  ;");
            Console.WriteLine("                ;  '._,-/  \\-,_.`  ;");
            Console.WriteLine("                \\  ,`  / /\\ \\  `,  /");
            Console.WriteLine("                 \\/    \\/  \\/    \\/");
            Console.WriteLine("                 ,=_    \\/\\/    _=,");
            Console.WriteLine("                 |  \"_   \\/   _\"  |");
            Console.WriteLine("                 |_   '\"-..-\"'   _|");
            Console.WriteLine("                 | \" -.      .- \" |");
            Console.WriteLine("                 |    \"\\    /\"    |");
            Console.WriteLine("                 |      |  |      |");
            Console.WriteLine("         ___     |      |  |      |     ___");
            Console.WriteLine("     _,-\", \",    '_     |  |     _'   , \", \"-,_");
            Console.WriteLine("   _(  \\  \\   \\\"=--\" -. |  | .- \"--=\"/   /  /  )_");
            Console.WriteLine(" ,\"  \\  \\  \\   \\      \"-'--'-\"      /   /  /  /  \".");
            Console.WriteLine("!     \\  \\  \\   \\                  /   /  /  /     !");
            Console.WriteLine(":      \\  \\  \\   \\                /   /  /  /      :");
            Console.WriteLine("Welcome Hero to the underground lair of the Demon King. You have been sent by the Kingdom of Falador to exterminate the Demon King");
            Console.WriteLine("who has been wreaking havok upon the citizens of Earth for milleniums. You have proven your skills time and time again as a master {0}.", playerClass);
            Console.WriteLine("Unfortunately when our mages transported you into the alternate dimension where the Demon King resides, all your skills and levels were lost.");
            Console.WriteLine("Luckily, you still have all your equipment and weapons on you but you must train yourself before fighting the Demon King.");
            Console.WriteLine("Our crystal ball indicates that there are many monsters nearby that you can slay for Exp. Also, in order to get through the big door to the Demon King,");
            Console.WriteLine("you must obtain 8 score points to unlock it so you can fight the Demon King. You can increase your score by slaying GREATER DEMONS");
            Console.WriteLine("which give 1 point each. But be careful, they are the toughest bunch out of all the monsters and you probably should train yourself before fighting them.");
            Console.WriteLine("If you die, you will be revived back to the starting location but you will lose Exp each time you die. So try not to die.");
            Console.WriteLine("Good luck on your quest master {0}, we are counting on you!", playerClass);
            Console.ResetColor();
            SeparateLine(); //draws line on console window
            Console.WriteLine("...(press any key to continue)...");
            Console.ReadKey(true);
            Console.Clear();
            while (loop == 0)
            {
                loop = 0; // note
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("             ,      ,  ");
                Console.WriteLine("            /(.-\"\"-.)\\");
                Console.WriteLine("        |\\  \\/      \\/  /|");
                Console.WriteLine("        | \\ / =.  .= \\ / |");
                Console.WriteLine("        \\( \\   o\\/o   / )/");
                Console.WriteLine("         \\_, '-/  \\-' ,_/");
                Console.WriteLine("           /   \\__/   \\");
                Console.WriteLine("           \\ \\__/\\__/ /");
                Console.WriteLine("         ___\\ \\|--|/ /___");
                Console.WriteLine("       /`    \\      /    `\\");
                Console.WriteLine("      /       '----'       \\");
                Console.WriteLine("You are in a den filled with goblins that you can fight. Would you like to fight goblins or run past them to the next area?");
                Console.ResetColor();
                Console.WriteLine("Enter a choice: 1 to Fight goblins or 2 to Run past them");
                SeparateLine();
                string choice1 = Console.ReadLine();
                if (choice1 == "1")
                {
                    loop = 1;
                    //fighting goblin
                    int enemyHealth = Goblin.enemyHealth; int enemyWeapon = Goblin.enemyWeapon; int enemyAttack = Goblin.enemyAttack; int enemyExp = Goblin.enemyExp; string enemyName = Goblin.enemyName;
                    while (loop == 1)
                    {
                        Console.WriteLine("Fighting a goblin. Enter a choice: 1 to attack or 0 to run away");
                        string choice11 = Console.ReadLine();
                        if (choice11 == "1")
                        {
                            addDamageToEnemy(ref enemyHealth, enemyName, playerWeapon);
                            addDamageToPlayer(ref playerHealth, enemyName, enemyWeapon, playerAgility, enemyAttack);
                            if (enemyHealth <= 0) // if enemy is dead
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("{0} is dead!", enemyName);
                                Console.ResetColor();
                                resetHealth(playerClass, ref playerHealth, playerLevel); // regenerate to full health upon kill.Check function at bottom of page.
                                playerExp += Player.gainExp(enemyExp); // gain exp
                                ifplayerLevel = Player.gainLevel(ref playerLevel, ref playerExp); //if enough exp reached, level increased by 1 and returns true that we just gained a level
                                Player.addStatsUponLevel(ref playerHealth, ref playerAgility, ref playerWeapon, ifplayerLevel, playerClass, playerLevel); // upon gaining a level being true, upgrades health, agility and weapon damage
                                if (ifplayerLevel == true)
                                { Player.statDisplay(playerHealth, playerAgility, playerWeapon, playerLevel, playerClass); } // displays new stats to user
                                Player.maxLevelReminder(playerLevel);
                                SeparateLine();
                                loop = 0;
                            }
                            else if (playerHealth <= 0) // if player dies
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("You died. Reviving back to starting location");
                                Console.ResetColor();
                                resetHealth(playerClass, ref playerHealth, playerLevel);
                                playerExp = 0; // reduces exp pool to 0 but still stay same level.
                                loop = 0;
                            }
                        }
                        else if (choice11 == "0") // if player runs away. start back.
                        {
                            resetHealth(playerClass, ref playerHealth, playerLevel);
                            loop = 0;
                        }
                    }//while loop 1 end
                }
                if (choice1 == "2")
                {
                    loop = 2;
                    while (loop == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("            __,='`````'=/__");
                        Console.WriteLine("           '//  (o) \\(o) \\ `'         _,-,");
                        Console.WriteLine("           //|     ,_)   (`\\      ,-'`_,-\\");
                        Console.WriteLine("         ,-~~~\\  `'==='  /-,      \\==```` \\__");
                        Console.WriteLine("        /        `----'     `\\     \\       \\/");
                        Console.WriteLine("     ,-`                  ,   \\  ,.-\\       \\");
                        Console.WriteLine("    /      ,               \\,-`\\`_,-`\\_,..--'\\");
                        Console.WriteLine("   ,`    ,/,              ,>,   )     \\--`````\\");
                        Console.WriteLine("   (      `\\`---'`  `-,-'`_,<   \\      \\_,.--'`");
                        Console.WriteLine("    `.      `--. _,-'`_,-`  |    \\");
                        Console.WriteLine("     [`-.___   <`_,-'`------(    /");
                        Console.WriteLine("     (`` _,-\\   \\ --`````````|--`");
                        Console.WriteLine("      >-`_,-`\\,-` ,          |");
                        Console.WriteLine("    <`_,'     ,  /\\          /");
                        Console.WriteLine("     `  \\/\\,-/ `/  \\/`\\_/V\\_/");
                        Console.WriteLine("        (  ._. )    ( .__. )");
                        Console.WriteLine("        |      |    |      |");
                        Console.WriteLine("         \\,---_|    |_---./");
                        Console.WriteLine("         ooOO(_)    (_)OOoo");
                        Console.WriteLine("You stumble into a lair filled with ogres. Would you like to fight ogres, run back and fight goblins, or go on to the next area?");
                        Console.ResetColor();
                        Console.WriteLine("Enter a choice: 1 to fight ogres, 2 to go back to goblins or 3 to move ahead");
                        SeparateLine();
                        string choice2 = Console.ReadLine();
                        // fighting ogre
                        if (choice2 == "1")
                        {
                            loop = 3;
                            int enemyHealth = Ogre.enemyHealth; int enemyWeapon = Ogre.enemyWeapon; int enemyAttack = Ogre.enemyAttack; int enemyExp = Ogre.enemyExp; string enemyName = Ogre.enemyName;
                            while (loop == 3)
                            {
                                Console.WriteLine("Fighting an ogre. Enter a choice: 1 to attack or 0 to run away");
                                string choice22 = Console.ReadLine();
                                if (choice22 == "1")
                                {
                                    addDamageToEnemy(ref enemyHealth, enemyName, playerWeapon);
                                    addDamageToPlayer(ref playerHealth, enemyName, enemyWeapon, playerAgility, enemyAttack);
                                    if (enemyHealth <= 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("{0} is dead!", enemyName);
                                        Console.ResetColor();
                                        resetHealth(playerClass, ref playerHealth, playerLevel);
                                        playerExp += Player.gainExp(enemyExp);
                                        ifplayerLevel = Player.gainLevel(ref playerLevel, ref playerExp);
                                        Player.addStatsUponLevel(ref playerHealth, ref playerAgility, ref playerWeapon, ifplayerLevel, playerClass, playerLevel);
                                        if (ifplayerLevel == true)
                                        { Player.statDisplay(playerHealth, playerAgility, playerWeapon, playerLevel, playerClass); }
                                        Player.maxLevelReminder(playerLevel);
                                        SeparateLine();
                                        loop = 2;
                                    }
                                    else if (playerHealth <= 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("You died. Reviving back to starting location");
                                        Console.ResetColor();
                                        resetHealth(playerClass, ref playerHealth, playerLevel);
                                        playerExp = 0;
                                        loop = 0;
                                    }
                                }
                                else if (choice22 == "0")
                                {
                                    resetHealth(playerClass, ref playerHealth, playerLevel);
                                    loop = 2;
                                }
                            }//while loop 3 end
                        }
                        if (choice2 == "2")
                        {
                            loop = 0;
                        }
                        if (choice2 == "3")
                        {
                            loop = 4;
                            while(loop == 4)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("          (                      )");
                                Console.WriteLine("          |\\    _,--------._    / |");
                                Console.WriteLine("          | `.,'            `. /  |");
                                Console.WriteLine("          `  '              ,-'   '");
                                Console.WriteLine("           \\/_         _   (     /");
                                Console.WriteLine("          (,-.`.    ,',-.`. `__,'");
                                Console.WriteLine("           |/#\\ ),-','#\\`= ,'.` |");
                                Console.WriteLine("           `._/)  -'.\\_,'   ) ))|");
                                Console.WriteLine("           /  (_.)\\     .   -'//");
                                Console.WriteLine("          (  /\\____/\\    ) )`'\\");
                                Console.WriteLine("           \\ |V----V||  ' ,    \\");
                                Console.WriteLine("            |`- -- -'   ,'   \\  \\      _____");
                                Console.WriteLine("     ___    |         .'    \\ \\  `._,-'     `-");
                                Console.WriteLine("        `.__,`---^---'       \\ ` -'");
                                Console.WriteLine("           -.______  \\ . /  ______,-");
                                Console.WriteLine("                   `.     ,' ");
                                Console.WriteLine("You reached the pit of the lesser demons! Would you like to fight lesser demons, run back to ogres, or continue ahead?");
                                Console.ResetColor();
                                Console.WriteLine("Enter a choice: 1 to fight lesser demons, 2 to go back to ogres, or 3 to move ahead.");
                                SeparateLine();
                                string choice3 = Console.ReadLine();
                                // fighting lesser demon
                                if (choice3 == "1")
                                {
                                    loop = 5;
                                    int enemyHealth = LDemon.enemyHealth; int enemyWeapon = LDemon.enemyWeapon; int enemyAttack = LDemon.enemyAttack; int enemyExp = LDemon.enemyExp; string enemyName = LDemon.enemyName;
                                    while (loop == 5)
                                    {
                                        Console.WriteLine("Fighting a lesser demon. Enter a choice: 1 to attack or 0 to run away");
                                        string choice33 = Console.ReadLine();
                                        if (choice33 == "1")
                                        {
                                            addDamageToEnemy(ref enemyHealth, enemyName, playerWeapon);
                                            addDamageToPlayer(ref playerHealth, enemyName, enemyWeapon, playerAgility, enemyAttack);
                                            if (enemyHealth <= 0)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                Console.WriteLine("{0} is dead!", enemyName);
                                                Console.ResetColor();
                                                resetHealth(playerClass, ref playerHealth, playerLevel);
                                                playerExp += Player.gainExp(enemyExp);
                                                ifplayerLevel = Player.gainLevel(ref playerLevel, ref playerExp);
                                                Player.addStatsUponLevel(ref playerHealth, ref playerAgility, ref playerWeapon, ifplayerLevel, playerClass, playerLevel);
                                                if (ifplayerLevel == true)
                                                { Player.statDisplay(playerHealth, playerAgility, playerWeapon, playerLevel, playerClass); }
                                                Player.maxLevelReminder(playerLevel);
                                                SeparateLine();
                                                loop = 4;
                                            }
                                            else if (playerHealth <= 0)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                Console.WriteLine("You died. Reviving back to starting location");
                                                Console.ResetColor();
                                                SeparateLine();
                                                resetHealth(playerClass, ref playerHealth, playerLevel);
                                                playerExp = 0;
                                                loop = 0;
                                            }
                                        }
                                        else if (choice33 == "0")
                                        {
                                            resetHealth(playerClass, ref playerHealth, playerLevel);
                                            loop = 4;
                                        }
                                    } // while loop 5 end
                                }
                                if (choice3 == "2")
                                {
                                    loop = 2;
                                }
                                if (choice3 == "3") // go to greater demons
                                {
                                    loop = 6;
                                    while (loop == 6)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("                                              ,--,  ,.-.");
                                        Console.WriteLine("                ,                   \\,       '-,-`,'-.' | ._");
                                        Console.WriteLine("               /|           \\    ,   |\\         }  )/  / `-,',");
                                        Console.WriteLine("               [ '          |\\  /|   | |        /  \\|  |/`  ,`");
                                        Console.WriteLine("               | |       ,.`  `,` `, | |  _,...(   (      _',");
                                        Console.WriteLine("               \\  \\  __ ,-` `  ,  , `/ |,'      Y     (   \\_L\\");
                                        Console.WriteLine("                \\  \\_\\,``,   ` , ,  /  |         )         _,/");
                                        Console.WriteLine("                 \\  '  `  ,_ _`_,-,<._.<        /         /");
                                        Console.WriteLine("                  ', `>.,`  `  `   ,., |_      |         /");
                                        Console.WriteLine("                    \\/`  `,   `   ,`  | /__,.-`    _,   `\\");
                                        Console.WriteLine("                -,-..\\  _  \\  `  /  ,  / `._) _,-\\`       \\");
                                        Console.WriteLine("                 \\_,,.) /\\    ` /  / ) (-,, ``    ,        |");
                                        Console.WriteLine("                ,` )  | \\_\\       '-`  |  `(               \\");
                                        Console.WriteLine("               /  /```(   , --, ,' \\   |`<`    ,            |");
                                        Console.WriteLine("              /  /_,--`\\   <\\  V /> ,` )<_/)  | \\      _____)");
                                        Console.WriteLine("        ,-, ,`   `   (_,\\ \\    |   /) / __/  /   `----`");
                                        Console.WriteLine("       (-, \\           ) \\ ('_.-._)/ /,`    /");
                                        Console.WriteLine("       | /  `          `/ \\ V    V, /`     /");
                                        Console.WriteLine("    ,--\\(        ,     <_/`\\      ||      /");
                                        Console.WriteLine("   (   ,``-     \\/|         \\-A.A-`|     /");
                                        Console.WriteLine("  ,>,_ )_,..(    )\\          -,,_-`  _--`");
                                        Console.WriteLine(" (_ \\|`   _,/_  /  \\_            ,--`");
                                        Console.WriteLine("  \\( `   <.,../`     `-.._   _,-`");
                                        Console.WriteLine("   `                      ```");
                                        Console.WriteLine("You reached a lava pit surrounded by greater demons! Up ahead, you notice an ominous door that probaly leads to the demon king!");
                                        Console.ResetColor();
                                        Console.WriteLine("What would you like to do? Enter a choice: 1 to fight greater demons, 2 to go back to lesser demons, or 3 to proceed to the big door.");
                                        SeparateLine();
                                        string choice4 = Console.ReadLine();
                                        if (choice4 == "1") // choose to fight greater demons
                                        {
                                            loop = 7;
                                            int enemyHealth = GDemon.enemyHealth; int enemyWeapon = GDemon.enemyWeapon; int enemyAttack = GDemon.enemyAttack; int enemyExp = GDemon.enemyExp; string enemyName = GDemon.enemyName;
                                            while (loop == 7) 
                                            {
                                                Console.WriteLine("Fighting a greater demon. Enter a choice: 1 to attack or 0 to run away");
                                                string choice44 = Console.ReadLine();
                                                if (choice44 == "1")
                                                {
                                                    addDamageToEnemy(ref enemyHealth, enemyName, playerWeapon);
                                                    addDamageToPlayer(ref playerHealth, enemyName, enemyWeapon, playerAgility, enemyAttack);
                                                    if (enemyHealth <= 0)
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                                        Console.WriteLine("{0} is dead!", enemyName);
                                                        Console.ResetColor();
                                                        Score++;
                                                        Player.scoreDisplay(Score);
                                                        resetHealth(playerClass, ref playerHealth, playerLevel);
                                                        playerExp += Player.gainExp(enemyExp);
                                                        ifplayerLevel = Player.gainLevel(ref playerLevel, ref playerExp);
                                                        Player.addStatsUponLevel(ref playerHealth, ref playerAgility, ref playerWeapon, ifplayerLevel, playerClass, playerLevel);     
                                                        if (ifplayerLevel == true)
                                                        { Player.statDisplay(playerHealth, playerAgility, playerWeapon, playerLevel, playerClass); }
                                                        Player.maxLevelReminder(playerLevel);
                                                        SeparateLine();
                                                        loop = 6;
                                                    }
                                                    else if (playerHealth <= 0)
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                                        Console.WriteLine("You died. Reviving back to starting location");
                                                        Console.ResetColor();
                                                        SeparateLine();
                                                        resetHealth(playerClass, ref playerHealth, playerLevel);
                                                        playerExp = 0;
                                                        loop = 0;
                                                    }
                                                }
                                                else if (choice44 == "0")
                                                {
                                                    resetHealth(playerClass, ref playerHealth, playerLevel);
                                                    loop = 6;
                                                }
                                            }
                                        }
                                        if (choice4 == "2") // go back to lesser demons
                                        {
                                            loop = 4;
                                        }
                                        if (choice4 == "3") // go to door
                                        {
                                            loop = 8;
                                            while (loop == 8)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                                Console.WriteLine("                          ^                       ^");
                                                Console.WriteLine("                         (_)                     (_)");
                                                Console.WriteLine("  /\\    /\\    /\\    /\\   /X\\                     /X\\   /\\    /\\    /\\    /\\");
                                                Console.WriteLine("  XX____XX____XX____XX___XXX  _________________  XXX___XX____XX____XX____XX");
                                                Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXX |.................| XXXXXXXXXXXXXXXXXXXXXXXXXX");
                                                Console.WriteLine("  XX    XX    XX    XX   XXX |.               .| XXX   XX    XX    XX    XX");
                                                Console.WriteLine("  XX    XX    XX    XX   XX/#|:      ___      .| XXX   XX    XX    XX    XX");
                                                Console.WriteLine("  XX    XX |\\_XX __/|X   XX\\#|:.....I I I......| XXX   X|\\__ XX_/| XX    XX");
                                                Console.WriteLine("  XX    XX \\ _ \\/ _ /X   XXX |.    .I_I_I.    .| XXX   X\\ _ \\/ _ / XX    XX");
                                                Console.WriteLine("  XX    XX  (I)\\/(I)XX   XXX |.       .       .|_XXX   XX(I)\\/(I)  XX    XX");
                                                Console.WriteLine("  XX    XX  \\/(oo)\\/XX   XXX |...............:(==)XX   XX\\/(oo)\\/  XX    XX");
                                                Console.WriteLine("  XX    XX   |V,,V| XX   XXX |.       .       .| XXX   XX |V,,V|   XX    XX");
                                                Console.WriteLine("  XX____XX  /|A^^A|\\XX __XXX |.       .       .| XXX_ _XX/|A^^A|\\  XX____XX");
                                                Console.WriteLine("  XXXXXX/ \\/  TTTT  \\/\\XXXXX |.................| XXXXX/\\/  TTTT  \\/ \\XXXXXX");
                                                Console.WriteLine("  XX  X/    (  ||  )   \\ XX/#|:       .       .| XXX /   (  ||  )    \\X  XX");
                                                Console.WriteLine("  XX_ X_\\    \\ || /   /__XX\\#|:       .       .| XXX__\\   \\ || /    /_X _XX");
                                                Console.WriteLine("  XXX/   \\   \\||||/  /   \\XX |.................| XX/   \\  \\||||/   /   \\XXX");
                                                Console.WriteLine("  XX/   oooo_/||||\\_ooo   \\X |_________________| X/   ooo_/||||\\_oooo   \\XX");
                                                Console.WriteLine("   /         oooooo        \\                     /        oooooo         \\");
                                                Console.WriteLine("  /_________________________\\                   /_________________________\\");
                                                Console.WriteLine("  |_________________________|                   |_________________________|");
                                                Console.WriteLine("   |___I___I___I___I__I___|                       |___I__I___I___I___I___|");
                                                Console.WriteLine("   |_I___I___I___I__I___I_|                       |_I__I___I___I___I___I_|");
                                                Console.ResetColor();
                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                Console.WriteLine("                              Enter, Ye Who Dare");
                                                Console.ResetColor();
                                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                                Console.WriteLine("You reached the big door to the Demon King! Would you like to try and open it or go back to greater demons?");
                                                Console.ResetColor();
                                                Console.WriteLine("Enter a choice: 1 to open or 2 to go back to greater demons.");
                                                SeparateLine();
                                                string choice5 = Console.ReadLine();
                                                if (choice5 == "1")
                                                {
                                                    if (Score >= 8) // choice to go in and fight demon king. no escape.
                                                    {
                                                        Console.WriteLine("As you go in, the door suddenly locks behind you! You hear a loud roar.");
                                                        Console.WriteLine(" ...(press any key to continue)... ");
                                                        Console.ReadKey(true);
                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                        Console.WriteLine("");
                                                        Console.WriteLine("");
                                                        Console.WriteLine("                            ,-.");
                                                        Console.WriteLine("       ___,---.__          /'|`\\          __,---,___");
                                                        Console.WriteLine("    ,-'    \\`    `-.____,-'  |  `-.____,-'    //    `-.");
                                                        Console.WriteLine("  ,'        |           ~'\\     /`~           |        `.");
                                                        Console.WriteLine(" /      ___//              `. ,'          ,  , \\___      \\");
                                                        Console.WriteLine("|    ,-'   `-.__   _         |        ,    __,-'   `-.    |");
                                                        Console.WriteLine("|   /          /\\_  `   .    |    ,      _/\\          \\   |");
                                                        Console.WriteLine("\\  |           \\ \\`-.___ \\   |   / ___,-'/ /           |  /");
                                                        Console.WriteLine(" \\  \\           | `._   `\\\\  |  //'   _,' |           /  /");
                                                        Console.WriteLine("  `-.\\         /'  _ `---'' , . ``---' _  `\\         /,-'");
                                                        Console.WriteLine("     ``       /     \\    ,='/ \\`=.    /     \\       ''");
                                                        Console.WriteLine("             |__   /|\\_,--.,-.--,--._/|\\   __|");
                                                        Console.WriteLine("             /  `./  \\\\`\\ |  |  | /,//' \\,'  \\");
                                                        Console.WriteLine("            /   /     ||--+--|--+-/-|     \\   \\");
                                                        Console.WriteLine("           |   |     /'\\_\\_\\ | /_/_/`\\     |   |");
                                                        Console.WriteLine("            \\   \\__, \\_     `~'     _/ .__/   /");
                                                        Console.WriteLine("             `-._,-'   `-._______,-'   `-._,-'");
                                                        Console.WriteLine("");
                                                        Console.WriteLine("");
                                                        Console.WriteLine("DEMONKING: RoOOoooaaaaarrrr!!!!!!!");
                                                        Console.WriteLine("DEMONKING: You puny human! Do you think you can defeat me?! The King of Demons! Prepare to Die! MuHaHahA!");
                                                        Console.ResetColor();
                                                        loop = 9;
                                                        int enemyHealth = DemonKing.enemyHealth; int enemyWeapon = DemonKing.enemyWeapon; int enemyAttack = DemonKing.enemyAttack; int enemyExp = DemonKing.enemyExp; string enemyName = DemonKing.enemyName;
                                                        // fight demon king
                                                        while (loop == 9)
                                                        {
                                                            Console.WriteLine("Enter a choice: 1 to attack or 0 to surrender and die");
                                                            string choice55 = Console.ReadLine();
                                                            if (choice55 == "1")
                                                            {
                                                                addDamageToEnemy(ref enemyHealth, enemyName, playerWeapon);
                                                                addDamageToPlayerBoss(ref playerHealth, enemyName, enemyWeapon, playerAgility, enemyAttack);
                                                                if (enemyHealth <= 0)
                                                                {
                                                                    SeparateLine();
                                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                                    Console.WriteLine("DEMONKING: Impossible... how can a mere human defeat me, the king of demons?! Hear this {0}, I shall be revived in the future!", playerClass);
                                                                    Console.WriteLine("DEMONKING: And when I do come back, I will destroy you and all humans on Earth! This will not be the last of me!!!");
                                                                    Console.ResetColor();
                                                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                    Console.WriteLine("{0} is dead!", enemyName);
                                                                    Console.ResetColor();
                                                                    SeparateLine();
                                                                    // game is done now, end credit
                                                                    Console.WriteLine(" ...(press any key to continue)... ");
                                                                    Console.ReadKey(true);
                                                                    Console.Clear();
                                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                                    Console.WriteLine("Congratulations Master {0}! You have defeated the Demon King! The Kingdom of Falador and the world owes you the umost", playerClass);
                                                                    Console.WriteLine("gratitute. Without you, this would not have been possible. Our mages will return you to Earth right away. We shall throw");
                                                                    Console.WriteLine("the most grandest celebration upon your return.");
                                                                    Console.WriteLine("...(press any key to continue)...");
                                                                    Console.ResetColor();
                                                                    Console.ReadKey(true);
                                                                    Console.WriteLine("Thank you for playing my game! I hope you enjoyed it. Tune in for a possible sequal to Chronicles of the Demon Slayer!");
                                                                    Console.WriteLine("                   - Nijastan K.");
                                                                    Console.WriteLine("                   - Programmer and Story Writer");
                                                                    Console.ReadLine();
                                                                    Environment.Exit(0);
                                                                }
                                                                else if (playerHealth <= 0)
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                    Console.WriteLine("You died. Reviving back to starting location. (hint: At level 10, you will regain all your powers...)");
                                                                    Console.ResetColor();
                                                                    SeparateLine();
                                                                    resetHealth(playerClass, ref playerHealth, playerLevel);
                                                                    playerExp = 0;
                                                                    loop = 0;
                                                                }
                                                            }
                                                            else if(choice55 == "0")
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                Console.WriteLine("The Demon King impales you with its mighty horns and you die. Reviving back to starting location");
                                                                Console.ResetColor();
                                                                SeparateLine();
                                                                resetHealth(playerClass, ref playerHealth, playerLevel);
                                                                playerExp = 0;
                                                                loop = 0;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                        Console.WriteLine("You don't have enough score to open the door. Fight more greater demons to increase your score");
                                                        Console.ResetColor();
                                                    }
                                                }
                                                if (choice5 == "2")
                                                { loop = 6; }
                                            } // while loop 8 end
                                        }
                                    } // while loop 6 end
                                }
                            } // while loop 4 end
                        }
                    }// while loop 2 end
                }
            }// while loop 0 end
            Console.ReadLine();
        }

        //Reset health after battle or dying
        public static void resetHealth(string pClass, ref int pHealth, int pLevel)
        {
            if (pClass == "WARRIOR")
            { pHealth = 200 + 50 * (pLevel - 1); } // makes sure to include new health at the different levels
            else if (pClass == "TROJAN")
            { pHealth = 100 + 25 * (pLevel - 1); }
            else if (pClass == "NINJA")
            { pHealth = 50 + 10 * (pLevel - 1); }

            if ((pClass == "WARRIOR")&&(pLevel == 10)) // when at max level
            { pHealth = (pHealth = 200 + 50 * (pLevel - 1)) + 800; }
            else if ((pClass == "TROJAN")&&(pLevel == 10))
            { pHealth = (pHealth = 100 + 25 * (pLevel - 1)) + 600; }
            else if ((pClass == "NINJA")&&(pLevel == 10))
            { pHealth = (pHealth = 50 + 10 * (pLevel - 1)) + 400; }
        }

        //Damage to enemy
        public static void addDamageToEnemy(ref int eHealth, string eName, int pDamage)
        {
            eHealth -= pDamage;
            if (eHealth <= 0)
            {
                Console.WriteLine("You did {0} damage to {1}. {1} has 0 hp left.", pDamage, eName);
            }
            if (eHealth > 0)
            {
                Console.WriteLine("You did {0} damage to {1}. {1} has {2} hp left.", pDamage, eName, eHealth);
            }
        }

        //Damage to player. Incorporates chance to miss or hit.
        public static void addDamageToPlayer(ref int pHealth, string eName, int eDamage, int pAgility, int eAttack)
        {
            Random rand = new Random();
            int rand1 = rand.Next(pAgility, 101);
            if (eAttack <= rand1)
            {
                Console.WriteLine("{0} missed", eName);
            }
            else if (eAttack > rand1)
            {
                pHealth -= eDamage;
                if (pHealth <= 0)
                {
                    Console.WriteLine("{0} did {1} damage. You have 0 hp left.", eName, eDamage);
                }
                if (pHealth > 0)
                {
                    Console.WriteLine("{0} did {1} damage. You have {2} hp left.", eName, eDamage, pHealth);
                }
            }
        }
        //Boss (demon king) damage to player. Also include special attack that has 50% chance of activating for additional damage on player.
        public static void addDamageToPlayerBoss(ref int pHealth, string eName, int eDamage, int pAgility, int eAttack)
        {
            Random rand = new Random();
            int rand1 = rand.Next(pAgility, 101);
            int rand2 = rand.Next(0, 100);
            if (eAttack <= rand1)
            {
                Console.WriteLine("{0} missed", eName);
            }
            else if (eAttack > rand1)
            {
                pHealth -= eDamage;
                if (pHealth <= 0)
                {
                    Console.WriteLine("{0} did {1} damage. You have 0 hp left.", eName, eDamage); // to make sure it doesn't say you have a negative hp left
                }
                if (pHealth > 0)
                {
                    Console.WriteLine("{0} did {1} damage. You have {2} hp left.", eName, eDamage, pHealth);
                }
                if (rand2 < 100)
                {
                    pHealth -= 50;
                    if (pHealth <= 0)
                    {
                        Console.WriteLine("{0}  in a fit of rage charged at you before you can counter and did 50 more damage. You have 0 hp left.", eName); // to avoid negative hp output
                    }
                    if (pHealth > 0)
                    {
                        Console.WriteLine("The {0} in a fit of rage charged at you before you can counter and did 50 more damage. You have {1} hp left.", eName, pHealth);
                    }
                }
            }
        }

        public static void SeparateLine()
        { Console.WriteLine("-------------------------------------------"); }

    }
}

