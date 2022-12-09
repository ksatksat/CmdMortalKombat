using System;
using static System.Console;

namespace MortalCombatConsole
{
    class Game
    {
        public static Player player;
        public static Enemy enemy;
        public static Illustrator illustrator;
        public static PlayerName playerName;
        public static void Main()
        {
            illustrator = new Illustrator();
            playerName = new PlayerName();
            player = new Player();
            enemy = new Enemy();
            illustrator.DrawingIntro();
            playerName.SetPlayerName();
            RunGame();
        }
        public static void RunGame()
        {
            bool endOfAnyonesHealth = false;
            bool isPlayerBlockHigh = false;
            bool isEnemyBlockHigh = false;
            bool isEnemyAttackHigh;
            bool isPlayerHighAttack;
            int enemyDamageCounter;
            int playerDamageCounter;
            int enemyHealthCounter = 100;
            int playerHealthCounter = 100;
            bool isEnemyFinishHimShowed = false;
            bool isPlayerFinishHimShowed = false;
            while (endOfAnyonesHealth != true)  
            {
                (playerDamageCounter, isPlayerHighAttack) = MakeStep();
                isPlayerBlockHigh = player.Block();
                (enemyDamageCounter, isEnemyAttackHigh) = EnemyMakesStep();
                isEnemyBlockHigh = enemy.Block();
                //check for player blocking success
                if (isPlayerBlockHigh == true && isEnemyAttackHigh != true
                    || isPlayerBlockHigh != true && isEnemyAttackHigh == true)
                {
                    playerHealthCounter -= enemyDamageCounter;
                }
                else
                {
                    WriteLine($"{PlayerName.player_Name} BLOCKED MOTARO's Hit Attempt");
                }
                //check for enemy blocking success
                if (isEnemyBlockHigh == true && isPlayerHighAttack != true
                    || isEnemyBlockHigh != true && isPlayerHighAttack == true)
                {
                    enemyHealthCounter -= playerDamageCounter;
                }
                else
                {
                    WriteLine("MOTARO BLOCKED your Hit Attempt");
                }
                //results of one step
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"MOTARO Health Is: {enemyHealthCounter}");
                WriteLine($"{PlayerName.player_Name} Health Is: {playerHealthCounter}");
                ForegroundColor = ConsoleColor.White;
                //draw FINISH HIM
                if (enemyHealthCounter <= 10 && isEnemyFinishHimShowed == false)
                {
                    illustrator.DrawingFinishHimText();
                    isEnemyFinishHimShowed = true;
                }
                if (playerHealthCounter <= 10 && isPlayerFinishHimShowed == false)
                {
                    illustrator.DrawingFinishHimText();
                    isPlayerFinishHimShowed = true;
                }
                //check if fighter lose
                if (enemyHealthCounter <= 0 && endOfAnyonesHealth == false)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine($"                {PlayerName.player_Name} WINS!");
                    ForegroundColor = ConsoleColor.White;
                    illustrator.DrawPlayerWin();
                    if (enemyHealthCounter <= 0 && playerHealthCounter == 100)
                    {
                        illustrator.DrawFlawlessVictory();
                    }
                    endOfAnyonesHealth = true;
                    illustrator.DrawingOutro();
                }
                if (playerHealthCounter <= 0 && endOfAnyonesHealth == false)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("                 MOTARO WINS!");
                    ForegroundColor = ConsoleColor.White;
                    illustrator.DrawEnemyWin();
                    if (playerHealthCounter <= 0 && enemyHealthCounter == 100)
                    {
                        illustrator.DrawFlawlessVictory();
                    }
                    endOfAnyonesHealth = true;
                    illustrator.DrawingOutro();
                }
            } 
        }
        static int playerDamage;
        static bool isPlayerHighAttack;
        static int enemyDamage;
        static bool isEnemyAttackHigh;
        public static (int, bool) MakeStep()
        {
            (playerDamage, isPlayerHighAttack) = player.Attack();
            return (playerDamage, isPlayerHighAttack);
        }
        public static (int, bool) EnemyMakesStep()
        {
            (enemyDamage, isEnemyAttackHigh) = enemy.Attack();
            return (enemyDamage, isEnemyAttackHigh);
        }
    }
    public class PlayerName
    {
        public static string player_Name;
        public void SetPlayerName()
        {
            try
            {
                WriteLine("*******************************************************************");
                WriteLine("Welcome to the dark world of MORTAL-COMBAT" +
                    "\nChoose your name fighter! Write your name and press enter...");
                WriteLine("*******************************************************************");
                ForegroundColor = ConsoleColor.Cyan;
                player_Name = ReadLine();
                WriteLine($"Your name is: {player_Name}");
                ForegroundColor = ConsoleColor.White;
                WriteLine("*******************************************************************");
                ForegroundColor = ConsoleColor.Magenta;
                WriteLine("Your Enemy is a GOD of underworld!" +
                    "\nHis name is MOTARO !");
                ForegroundColor = ConsoleColor.White;
                WriteLine("*******************************************************************");
                ForegroundColor = ConsoleColor.Red;
                WriteLine("                 FIGHT !!!");
                ForegroundColor = ConsoleColor.White;
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }
    }
    class Player
    {
        Random randomKick;
        bool isPlayerHighAttack;
        int isKicked;
        int damage;
        string type;
        (string t, string h) attackTypeCollector;
        string highOrLow;
        string punchOrKickChoice;
        string highOrLowChoice;
        bool isHighBlock;
        public (int, bool) Attack()
        {
            isPlayerHighAttack = false;
            randomKick = new Random();
            isKicked = 1;
            isKicked = randomKick.Next(1, 3);
            damage = 0;
            type = "";
            highOrLow = "";
            punchOrKickChoice = $"\nChoose Attack Type {PlayerName.player_Name}. " +
                $"\nPress Up Arrow To Punch... " +
                "\nOr Press Down Arrow To Kick";
            WriteLine(punchOrKickChoice);
            WriteLine("############################################################");
            if (ReadKey().Key == ConsoleKey.UpArrow)//input here
            {
                ForegroundColor = ConsoleColor.Cyan;
                WriteLine("PUNCH");
                ForegroundColor = ConsoleColor.White;
                WriteLine("############################################################");
                type = "PUNCH";
            }
            else if (ReadKey().Key == ConsoleKey.DownArrow)//input here
            {
                ForegroundColor = ConsoleColor.Cyan;
                WriteLine("KICK");
                ForegroundColor = ConsoleColor.White;
                WriteLine("############################################################");
                type = "KICK";
            }
            highOrLowChoice = $"\nChoose Attack Type {PlayerName.player_Name}. " +
                $"\nPress Up Arrow To High Attack... " +
                "\nOr Press Down Arrow To Low Attack";
            WriteLine(highOrLowChoice);
            WriteLine("############################################################");
            if (ReadKey().Key == ConsoleKey.UpArrow)//input here
            {
                ForegroundColor = ConsoleColor.Cyan;
                WriteLine("HIGH ATTACK");
                ForegroundColor = ConsoleColor.White;
                WriteLine("############################################################");
                highOrLow = "HIGH ATTACK";
                isPlayerHighAttack = true;
            }
            else if (ReadKey().Key == ConsoleKey.DownArrow)//input here
            {
                ForegroundColor = ConsoleColor.Cyan;
                WriteLine("LOW ATTACK");
                ForegroundColor = ConsoleColor.White;
                WriteLine("############################################################");
                highOrLow = "LOW ATTACK";
            }
            attackTypeCollector = (type, highOrLow);
            WriteLine($"{PlayerName.player_Name} Your Attack Type Is: {attackTypeCollector.t}, " +
                $"{attackTypeCollector.h}");
            switch (attackTypeCollector)
            {
                case ("PUNCH", "HIGH ATTACK"):
                    damage = 20;
                        break;
                case ("PUNCH", "LOW ATTACK"):
                    damage = 5;
                    break;
                case ("KICK", "HIGH ATTACK"):
                    if (isKicked == 1)
                    {
                        damage = 35;
                    }
                    else
                    {
                        damage = 0;
                        WriteLine($"{PlayerName.player_Name} Attack is Missed. Sorry!");
                    }
                    break;
                case ("KICK", "LOW ATTACK"):
                    if (isKicked == 1)
                    {
                        damage = 15;
                    }
                    else
                    {
                        damage = 0;
                        WriteLine($"{PlayerName.player_Name} Attack is Missed. Sorry!");
                    }
                    break;
            }
            return (damage, isPlayerHighAttack);
        }
        public bool Block()
        {
            isHighBlock = false;
            WriteLine("############################################################");
            WriteLine($"{PlayerName.player_Name} Choose Block Type. " +
                $"\nPress Up Arrow to High Block. Press Down Arrow to Low Block.");
            if (ReadKey().Key == ConsoleKey.UpArrow)//input here
            {
                WriteLine("############################################################");
                ForegroundColor = ConsoleColor.Cyan;
                WriteLine("HIGH BLOCK");
                ForegroundColor = ConsoleColor.White;
                WriteLine("############################################################");
                WriteLine($"{PlayerName.player_Name} Block is: HIGH BLOCK.");
                isHighBlock = true;
            }
            else if (ReadKey().Key == ConsoleKey.DownArrow)//input here
            {
                WriteLine("############################################################");
                ForegroundColor = ConsoleColor.Cyan;
                WriteLine("LOW BLOCK");
                ForegroundColor = ConsoleColor.White;
                WriteLine("############################################################");
                WriteLine($"{PlayerName.player_Name} Block is: LOW BLOCK.");
                isHighBlock = false;
            }
            return isHighBlock;
        }
    }
    class Enemy
    {
        Random randomAttack;
        Random randomKick;
        Random randomBlock;
        public bool isEnemyHighAttack;
        int isKicked;
        int damage;
        int enemyAttackType;
        bool isHighBlock;
        public (int, bool) Attack()
        {
            isEnemyHighAttack = false;
            randomAttack = new Random();
            randomKick = new Random();
            isKicked = 1;
            isKicked = randomKick.Next(1, 3);
            damage = 0;
            enemyAttackType = randomAttack.Next(1, 5);
            switch (enemyAttackType)
            {
                case 1:
                    WriteLine("############################################################");
                    ForegroundColor = ConsoleColor.Magenta;
                    WriteLine("MOTARO attack type is : LOW PUNCH");
                    ForegroundColor = ConsoleColor.White;
                    WriteLine("############################################################");
                    damage = 5;
                    break;
                case 2:
                    WriteLine("############################################################");
                    ForegroundColor = ConsoleColor.Magenta;
                    WriteLine("MOTARO attack type is : HIGH PUNCH");
                    ForegroundColor = ConsoleColor.White;
                    WriteLine("############################################################");
                    damage = 20;
                    isEnemyHighAttack = true;
                    break;
                case 3:
                    WriteLine("############################################################");
                    ForegroundColor = ConsoleColor.Magenta;
                    WriteLine("MOTARO attack type is : LOW KICK");
                    ForegroundColor = ConsoleColor.White;
                    WriteLine("############################################################");
                    if (isKicked == 1)
                    {
                        damage = 15;
                    }
                    else
                    {
                        damage = 0;
                        WriteLine("You Are Lucky! Motaro Attack Missed!");
                    }
                    break;
                case 4:
                    WriteLine("############################################################");
                    ForegroundColor = ConsoleColor.Magenta;
                    WriteLine("MOTARO attack type is : HIGH KICK");
                    ForegroundColor = ConsoleColor.White;
                    WriteLine("############################################################");
                    if (isKicked == 1)
                    {
                        damage = 35;
                    }
                    else
                    {
                        damage = 0;
                        WriteLine("You Are Lucky! Motaro Attack is Missed!");
                    }
                    isEnemyHighAttack = true;
                    break;
            }
            return (damage, isEnemyHighAttack);
        }
        public bool Block()
        {
            isHighBlock = false;
            randomBlock = new Random();
            if (randomBlock.Next(1, 3) == 1)
            {
                isHighBlock = true;
                WriteLine("MOTARO Block Type is: HIGH BLOCK");
            }
            else
            {
                WriteLine("MOTARO Block Type is: LOW BLOCK");
            }
            return isHighBlock;
        }
    }
    class Illustrator
    {
        int frequency;
        int duration;
        public void DrawingIntro()
        {
            WriteLine("                                   Press any key to start...");
            ReadLine();
            ForegroundColor = ConsoleColor.Red;
            WriteLine(@"
            
                   *                                                           
                 (  `              )     (      (                  )         ) 
                 )\))(      (   ( /(   ) )\     )\         )    ( /(    ) ( /( 
                ((_)()\  (  )(  )\()| /(((_)  (((_)  (    (     )\())( /( )\())
                (_()((_) )\(()\(_))/)(_))_    )\___  )\   )\  '((_)\ )(_)|_))/ 
                |  \/  |((_)((_) |_((_)_| |  ((/ __|((_)_((_)) | |(_|(_)_| |_  
                | |\/| / _ \ '_|  _/ _` | |   | (__/ _ \ '  \()| '_ Y _` |  _| 
                |_|  |_\___/_|  \__\__,_|_|    \___\___/_|_|_| |_.__|__,_|\__| 
                                                                           
      _____                      _        _______        _      _____                      
     / ____|                    | |      |__   __|      | |    / ____|                     
    | |     ___  _ __  ___  ___ | | ___     | | _____  _| |_  | |  __  __ _ _ __ ___   ___ 
    | |    / _ \| '_ \/ __|/ _ \| |/ _ \    | |/ _ \ \/ / __| | | |_ |/ _` | '_ ` _ \ / _ \
    | |___| (_) | | | \__ \ (_) | |  __/    | |  __/>  <| |_  | |__| | (_| | | | | | |  __/
     \_____\___/|_| |_|___/\___/|_|\___|    |_|\___/_/\_\\__|  \_____|\__,_|_| |_| |_|\___|
                                                                                           
           ");
            ForegroundColor = ConsoleColor.White;
            WriteLine("                       10.04.2021 DopustimVladimir and BulBulSup present");
            WriteLine("                                   Press any key to start...");
            PlayIntroMusic();
            ReadLine();
        }
        void PlayIntroMusic()
        {
            frequency = 80;
            duration = 1800;
            Beep(frequency, duration);//it is working only on Windows, you can delete this
        }
        public void DrawingFinishHimText()//not using that yet
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine(@"
            ____ _ _  _ _ ____ _  _    _  _ _ _  _   /
            |___ | |\ | | [__  |__|    |__| | |\/|  / 
            |    | | \| | ___] |  |    |  | | |  | .  
                                                      
            ");
            ForegroundColor = ConsoleColor.White;
            PlayIntroMusic();
        }
        public void DrawPlayerWin()
        {
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine(@"


          )      )          (                            (                     (        )      )  (        ____
   (   ( /(   ( /(  (       )\ )    (       *   )        )\ )    (       *   ) )\ )  ( /(   ( /(  )\ )    |   /
   )\  )\())  )\()) )\ )   (()/(    )\    ` )  /(    (  (()/(    )\    ` )  /((()/(  )\())  )\())(()/(    |  / 
 (((_)((_)\  ((_)\ (()/(    /(_))((((_)(   ( )(_))   )\  /(_))((((_)(   ( )(_))/(_))((_)\  ((_)\  /(_))   | /  
 )\___  ((_)  _((_) /(_))_ (_))   )\ _ )\ (_(_()) _ ((_)(_))   )\ _ )\ (_(_())(_))    ((_)  _((_)(_))     |/   
((/ __|/ _ \ | \| |(_)) __|| _ \  (_)_\(_)|_   _|| | | || |    (_)_\(_)|_   _||_ _|  / _ \ | \| |/ __|   (     
 | (__| (_) || .` |  | (_ ||   /   / _ \    | |  | |_| || |__   / _ \    | |   | |  | (_) || .` |\__ \   )\    
  \___|\___/ |_|\_|   \___||_|_\  /_/ \_\   |_|   \___/ |____| /_/ \_\   |_|  |___|  \___/ |_|\_||___/  ((_)   
                                                                                                               
                              __   __  _____  _     _      _  _  _ _____ __   _        /
                                \_/   |     | |     |      |  |  |   |   | \  |       / 
                                 |    |_____| |_____|      |__|__| __|__ |  \_|      . 
            ");
            ForegroundColor = ConsoleColor.White;
            PlayIntroMusic();
        }
        public void DrawEnemyWin()
        {
            ForegroundColor = ConsoleColor.Magenta;
            WriteLine(@"
             (        )            *              )      )       )    )             ____
             )\ )  ( /(   (      (  `          ( /(   ( /(    ( /( ( /(            |   /
            (()/(  )\())  )\     )\))(   (     )\())  )\())   )\()))\())     (     |  / 
             /(_))((_)\((((_)(  ((_)()\  )\   ((_)\  ((_)\   ((_)\((_)\      )\    | /  
            (_))   _((_))\ _ )\ (_()((_)((_)    ((_)  _((_) __ ((_) ((_)  _ ((_)   |/   
            / __| | || |(_)_\(_)|  \/  || __|  / _ \ | \| | \ \ / // _ \ | | | |  (     
            \__ \ | __ | / _ \  | |\/| || _|  | (_) || .` |  \ V /| (_) || |_| |  )\    
            |___/ |_||_|/_/ \_\ |_|  |_||___|  \___/ |_|\_|   |_|  \___/  \___/  ((_)   
                                                                                        
             _______ __   _ _______ _______ __   __      _  _  _ _____ __   _ _______        /
             |______ | \  | |______ |  |  |   \_/        |  |  |   |   | \  | |______       / 
             |______ |  \_| |______ |  |  |    |         |__|__| __|__ |  \_| ______|      .  
                                                                                              
            ");
            ForegroundColor = ConsoleColor.White;
            PlayIntroMusic();
        }
        public void DrawFlawlessVictory()
        {
            ForegroundColor = ConsoleColor.Green;
            WriteLine(@"
             (     (                        (          (    (    
             )\ )  )\ )    (      (  (      )\ )       )\ ) )\ ) 
            (()/( (()/(    )\     )\))(   '(()/(  (   (()/((()/( 
             /(_)) /(_))((((_)(  ((_)()\ )  /(_)) )\   /(_))/(_))
            (_))_|(_))   )\ _ )\ _(())\_)()(_))  ((_) (_)) (_))  
            | |_  | |    (_)_\(_)\ \((_)/ /| |   | __|/ __|/ __| 
            | __| | |__   / _ \   \ \/\/ / | |__ | _| \__ \\__ \ 
            |_|   |____| /_/ \_\   \_/\_/  |____||___||___/|___/ 
                                                                 
            
             _    _ _____ _______ _______  _____   ______ __   __        /
              \  /    |   |          |    |     | |_____/   \_/         / 
               \/   __|__ |_____     |    |_____| |    \_    |         .  
                                                                           
            ");
            ForegroundColor = ConsoleColor.White;
            PlayIntroMusic();
        }

        public void DrawingOutro()
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine(@"
            
                   *                                                           
                 (  `              )     (      (                  )         ) 
                 )\))(      (   ( /(   ) )\     )\         )    ( /(    ) ( /( 
                ((_)()\  (  )(  )\()| /(((_)  (((_)  (    (     )\())( /( )\())
                (_()((_) )\(()\(_))/)(_))_    )\___  )\   )\  '((_)\ )(_)|_))/ 
                |  \/  |((_)((_) |_((_)_| |  ((/ __|((_)_((_)) | |(_|(_)_| |_  
                | |\/| / _ \ '_|  _/ _` | |   | (__/ _ \ '  \()| '_ Y _` |  _| 
                |_|  |_\___/_|  \__\__,_|_|    \___\___/_|_|_| |_.__|__,_|\__| 
                                                                           
      _____                      _        _______        _      _____                      
     / ____|                    | |      |__   __|      | |    / ____|                     
    | |     ___  _ __  ___  ___ | | ___     | | _____  _| |_  | |  __  __ _ _ __ ___   ___ 
    | |    / _ \| '_ \/ __|/ _ \| |/ _ \    | |/ _ \ \/ / __| | | |_ |/ _` | '_ ` _ \ / _ \
    | |___| (_) | | | \__ \ (_) | |  __/    | |  __/>  <| |_  | |__| | (_| | | | | | |  __/
     \_____\___/|_| |_|___/\___/|_|\___|    |_|\___/_/\_\\__|  \_____|\__,_|_| |_| |_|\___|
                                                                                           
           ");
            ForegroundColor = ConsoleColor.White;
            WriteLine("                       10.04.2021 DopustimVladimir and BulBulSup present");
            WriteLine("                                   Thank you for playing!");
            PlayIntroMusic();
            ReadLine();
        }
    }
}
namespace PseudoCode
{
    class Action
    {
        public static void Punch()
        {
            System.Console.WriteLine("Punching");
        }
        public static void Kick()
        {
            System.Console.WriteLine("Kicking");
        }
        public static void Uppercut()
        {
            System.Console.WriteLine("Uppercutting");
        }
        public static void Shoot()
        {
            System.Console.WriteLine("Shooting");
        }
        public static void Idle()
        {
            System.Console.WriteLine("Do nothing");
        }
    }
    class CallMethods
    {
        public void Call()
        {
            Action.Punch();
            Action.Kick();
            Action.Uppercut();
            Action.Shoot();
            Action.Idle();
        }
    }
}