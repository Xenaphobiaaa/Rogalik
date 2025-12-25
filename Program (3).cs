using System;

namespace Learnings
{
    internal class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {

            Console.Write("Введите своё имя: ");
            string name = Console.ReadLine();
            Weapon playerWeapon = GenerateWeapon(20, 40);
            Aid playerAid = GenerateAid();

            Player player = new Player(name, 100, playerWeapon, playerAid);

            Enemy currentEnemy = GenerateEnemy();
            Console.WriteLine($"О могучий {name}, ты вооружен {playerWeapon.name} ({playerWeapon.damage}) и аптечкой {playerAid.name}  ({playerAid.aidAmount})");

            int command = 0;
            while (command != 4 && player.currentHP > 0)
            {
                Console.WriteLine($"Перед тобой стоит {currentEnemy.name}, у него в руках {currentEnemy.weapon.name} ({currentEnemy.weapon.damage})");
                Console.WriteLine("------------------------------");
                Console.WriteLine("Твои действия?");
                Console.WriteLine("1. Атака");
                Console.WriteLine("2. Исцеление");
                Console.WriteLine("3. Пропуск хода");
                Console.WriteLine("4. Конец");

                Console.WriteLine("------------------------------");
                string line = Console.ReadLine();
                if (line != "")
                {
                    command = Convert.ToInt32(line);
                }
                else
                    command = 0;

                if (command == 1)
                {
                    player.Damage(currentEnemy);
                    if (currentEnemy.currentHP <= 0)
                    {
                        player.score++;
                        Console.WriteLine($"Ты одолел {currentEnemy.name}! Твой счёт вырос и равен {player.score} Новый враг на подходе...");
                        currentEnemy = GenerateEnemy();
                        player.aid = GenerateAid();
                        continue; 
                }
                else if(command == 2)
                {
                    player.Heal();
                }
                else if (command == 3)
                {
                    Console.WriteLine("Пропуск хода...");
                }
                else if (command == 4)
                {
                    break; 
                }

                currentEnemy.Damage(player);
                if (player.currentHP <= 0)
                {
                    Console.WriteLine($"{player.name} пал в бою...");
                    continue; 
                }
                
            }
            Console.ReadLine();
        }
        static Enemy GenerateEnemy()
        {
            string[] names = new string[] { "Зомбде", "Скелет", "Маслёнок" };
            string name = names[rnd.Next(1, 3)];
            Weapon weapon = GenerateWeapon(1, 20);
            int maxHp = rnd.Next(1, 100);
            return new Enemy(name, maxHp, weapon);
        }

        static Weapon GenerateWeapon(int minDmg, int maxDmg)
        {
            string[] names = new string[] { "Катана", "Рапира", "Меч" };
            int damage = rnd.Next(minDmg, maxDmg);
            int durability = rnd.Next(1, 20);
            string name = names[rnd.Next(1, 3)];
            return new Weapon(name, damage, durability);
        }
        static Aid GenerateAid()
        {
            string[] names = new string[] { "Аптечка", "Бинт", "Мазь" };
            int healAmount = rnd.Next(1, 100);
            string name = names[rnd.Next(1, 3)];
            return new Aid(healAmount, name);
        }
        class Creature
        {
            public string name;
            public int currentHP;
            public int maxHP;
            public Weapon weapon;

            public Creature(string name, int maxHP, Weapon weapon)
            {
                this.name = name;
                this.maxHP = maxHP;
                currentHP = maxHP;
                this.weapon = weapon;
            }
            public void Damage(Creature creature)
            {
                creature.currentHP -= weapon.damage;
                weapon.durability -= 1;

                if (creature.currentHP <= 0)
                {
                    creature.currentHP = 0;
                }
                Console.WriteLine($"Существо {name} ударил {creature.name} на {weapon.damage} и у него осталось {creature.currentHP}hp");
            }
        }

        class Weapon
        {
            public string name;
            public int damage;
            public int durability;

            public Weapon(string name, int damage, int durability)
            {
                this.name = name;
                this.damage = damage;
                this.durability = durability;
            }
        }
        class Aid
        {
            public int aidAmount;
            public string name;

            public Aid(int aidAmount, string name)
            {
                this.aidAmount = aidAmount;
                this.name = name;
            }
        }

        class Player : Creature
        {
            public Aid aid; 
            public int score = 0;
            public Player(string name, int maxHP, Weapon weapon, Aid aid) : base(name, maxHP, weapon)
            {
                this.aid = aid;
                score = 0;
            }

            public void Heal()
            {
                currentHP += aid.aidAmount;    
                Console.WriteLine($"{name} использует {aid.name} и восстанавливает {aid.aidAmount}hp. Всего:" + currentHP);
                aid = null;
            }
        }


        class Enemy : Creature
        {
            public Enemy(string name, int maxHP, Weapon weapon) : base(name, maxHP, weapon)
            {
            }
        }

    }


}
