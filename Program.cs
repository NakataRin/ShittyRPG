using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TestingThings
{
    class Program
    {
        public static int killCount = 0;
        public static int magicLevel;
        public static int staminaLevel;
        public static int skills;
        public static int hp = 100;
        public static bool dead = false;
        public static bool knowAboutHeal = false;
        public static string name = "";

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; //Я пишу это лишь для того, чтобы ебаный Русский язык нормально работал сука!
            Console.InputEncoding = Encoding.Unicode; //^
            string userInput;

            Console.WriteLine("Привет! Я - консольная симуляция достаточно посредственной RPG-игры в этом достаточно посредственном мире...");
            Console.WriteLine("Для начала - введи мне свое имя");
            
            name = Console.ReadLine();

            Console.WriteLine($"Хорошо, {name}, давай начнем");
            Console.WriteLine("Набери \"Начать\" и мы подберем тебе стартовый набор... всего.");

            userInput = Console.ReadLine();

            if (userInput == "Начать" || userInput == "начать")
            {
                Random random = new Random();
                magicLevel = random.Next(1, 3);
                staminaLevel = random.Next(1, 3);
                skills = random.Next(1, 3);

                Console.WriteLine($"Хорошо, ты - демон.\nТвоя демоническая энергия {magicLevel}-го уровня.\nТвоя выносливость {staminaLevel}-го уровня.\nТвои навыки {skills}-го уровня.");
            }
            else //For stupids
            {
                Console.WriteLine($"You shoud've write \"Начать\" or \"начать\" instead of {userInput}...\nNow i'll close myself.");
                Console.ReadKey();
                Environment.Exit(0);
            }

            while (dead == false)
            {

                Console.WriteLine("Ну, что ты теперь будешь делать? У тебя четыре варианта:\n1 - Тренировки (Попытаться улучшить свои навыки)\n2 - Сражение (Найти кого-нибудь для убийства)\n3 - Исследования (Попытаться исследовать что-либо)\n4 - Статистика (Что ты из себя представляешь)");

                userInput = Console.ReadLine();

                while (userInput != "1" && userInput != "2" && userInput != "3" && userInput != "4")
                {
                    Console.WriteLine("Напиши \"1\", \"2\", \"3\" или \"4\"...");
                    userInput = Console.ReadLine();
                }

                switch (userInput)
                {
                    case "1":
                        Training(skills);
                        break;
                    case "2":
                        Fight(false);
                        break;
                    case "3":
                        Learning(knowAboutHeal);
                        break;
                    case "4":
                        CheckStats();
                        break;
                }
            }
            //Just to wait a little
            Console.ReadKey();
        }

        private static void CheckStats()
        {
            Console.WriteLine($"Тебя зовут - {name}\nТвоя демоническая энергия {magicLevel}-го уровня.\nТвоя выносливость {staminaLevel}-го уровня.\nТвои навыки {skills}-го уровня.\nТы убил {killCount} врагов.");
            if (knowAboutHeal) Console.WriteLine("Ты знаешь про лечение.");
        }

        private static bool Learning(bool knowAboutHeal)
        {
            if (knowAboutHeal == true)
            {
                Console.WriteLine("Ты уже знаешь про лечение, на данный момент больше учить нечего.");
                return knowAboutHeal;
            }

            Random randomChance = new Random();
            int actualChance = randomChance.Next(1, 100);

            if (actualChance <= 15)
            {
                Console.WriteLine("Поздравляю, теперь ты знаешь, как лечить себя!");
                knowAboutHeal = true;
                return knowAboutHeal;
            }
            else if (actualChance > 95)
            {
                Console.WriteLine("Похоже, во время обучения на тебя напали!");
                Fight(true);
                return knowAboutHeal;
            }
            else
            {
                Console.WriteLine("К сожалению, ты ничего не выучил... Может, повезет в другой раз.");
                return knowAboutHeal;
            }
        }

        private static void Fight(bool forced)
        {
            Random randomChance = new Random();
            int actualChance = randomChance.Next(1, 100);
            
            if (actualChance <= 50 || forced)
            {
                bool magicHight = false;
                bool staminaHight = false;
                bool skillsHight = false;
                Enemy enemy = new Enemy();
                
                Console.WriteLine($"Ты встречаешь противника. Это демон.\nИмя: {enemy.name}.\nУровень демонической энергии - {enemy.magicLevel}.\nУровень выносливости - {enemy.staminaLevel}.\nУровень навыков - {enemy.skills}.\nУдачи!");
                Console.WriteLine("Ты совершаешь удар по противнику...");
                Console.ReadKey();

                if (magicLevel > enemy.magicLevel) magicHight = true;
                if (staminaLevel > enemy.staminaLevel) staminaHight = true;
                if (skills > enemy.skills) skillsHight = true;

                if (!magicHight) Console.WriteLine("Противник заблокировал твою атаку.");
                else if (magicHight && !staminaHight) Console.WriteLine("Противник увернулся от твоей атаки.");
                else if (magicHight && staminaHight && !skillsHight) Console.WriteLine("Противник узнал формулу твоей атаки и свел ее на нет.");
                else if (magicHight && staminaHight && skillsHight)
                {
                    enemy.hp -= 50;
                    Console.WriteLine("Твоя атака попала по врагу. Тем не менее, ты его не убил, и теперь враг пытается сбежать.\nТебе нужно иметь хорошие физические данные, чтобы остановить его!");
                    if (skillsHight)
                    {
                        Console.WriteLine("Ты успешно остановил его и смог убить!");
                        killCount++;
                    }
                    else Console.WriteLine("Хоть ты и старался, но враг удрал. Может, повезет в другой раз.");
                    return;
                }

                Console.WriteLine("Враг атакует тебя...");
                Console.ReadKey();
                Console.WriteLine("Враг попал по тебе, нанеся достаточно большой урон, тем не менее, ты все еще можешь попытаться сбежать.");
                Console.ReadKey();
                if (staminaHight)
                {
                    Console.WriteLine("Тебе повезло, что противник оказался медленне тебя. Тебе удалось сбежать!");
                    return;
                }
                else
                {
                    Console.WriteLine("Хоть ты и был медленнее, тебе кое-как удалось сбежать.\nНо, с такими ранениями, ты вряд-ли уйдешь далеко...");
                    Console.ReadKey();
                    if (knowAboutHeal)
                    {
                        Console.WriteLine("Тебе повезло, что ты узнал, как лечить себя, иначе ты бы умер.\n Ты продолжишь жить, но тебе стоит заново поучиться навыкам лечения, без этого в следующий такой раз ты умрешь.");
                        knowAboutHeal = false;
                        return;
                    }
                    else
                    {
                        dead = true;
                        Console.WriteLine("К сожалению, твоя жизнь окончена...\nМир демонов достаточно жестокий (и скучный), и теперь ты еще раз убедился в этом\nТем не менее, ты можешь попробовать начать заново.");
                        Console.WriteLine($"Кстати, ты убил {killCount} врагов.");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }

            }
            else if (actualChance > 50 && !forced)
            {
                Console.WriteLine("Но никто не пришёл.");
            }
        }

        private static int Training(int skills)
        {
            Random randomChance = new Random();
            int actualChance = randomChance.Next(1, 100);
            if (skills == 3)
            {
                Console.WriteLine("Ты не можешь подняться выше, для простого демона это - потолок.");
                return skills;
            }
            if (actualChance > 95)
            {
                Console.WriteLine("Похоже, во время тренировки на тебя напали!");
                Fight(true);
                return skills;
            }
            else if (actualChance <= 15)
            {
                Console.WriteLine("Поздравляю, ты улучшил свои навыки!");
                if (skills == 1) skills++;
                else if (skills == 2) skills++;
                return skills;
            }
            else
            {
                Console.WriteLine("Потратив достаточно времени, ты, к сожалению, не узнал ничего...\nВозможно, тебе повезет в другой раз.");
                return skills;
            }
        }
    }
    class Enemy
    {
        static Random random = new Random();
        static int i = random.Next(1, 21);
        static string[] availableNames = {"Yui", "Rin", "Sakura", "Yuina", "Yua", "Koharu", "An", "Tsumugi", "Riko", "Mei", "Yuuma", "Hinata", "Aoi", "Minato", "Souma", "Haruto", "Souta", "Asahi", "Gaku", "Yuuto", "Yuito"};

        public string name = availableNames[i];
        public int magicLevel = random.Next(1, 3);
        public int staminaLevel = random.Next(1, 3);
        public int skills = random.Next(1, 3);
        public int hp = 100;
    }
}