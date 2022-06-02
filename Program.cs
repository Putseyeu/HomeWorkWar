using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkWarMy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Создадим первый взвод");
            Platoon platoonOne = new Platoon();
            Console.WriteLine("Создадим второй взвод");
            Platoon platoonTwo = new Platoon();
            Batle batle = new Batle();
            batle.DealingDamage(platoonOne, platoonTwo);
            Console.ReadKey();
        }
    }

    class Batle
    {
        private Soldier _soldierAttacking;
        private Soldier _soldierDefensive;

        public void DealingDamage(Platoon platoonOne, Platoon platoonTwo)
        {
            while (platoonOne.GetCount() > 0 && platoonTwo.GetCount() > 0)
            {
                _soldierAttacking = platoonOne.GetSoldier();
                _soldierDefensive = platoonTwo.GetSoldier();
                _soldierDefensive.TakesDamage(_soldierDefensive.Healts, _soldierAttacking.Damage, _soldierDefensive.Armor);
                _soldierAttacking.TakesDamage(_soldierAttacking.Healts, _soldierDefensive.Damage, _soldierAttacking.Armor);

                if (_soldierAttacking.Healts < 0)
                {
                    platoonOne.DeleteSolder();
                }

                if (_soldierDefensive.Healts < 0)
                {
                    platoonTwo.DeleteSolder();
                }
            }

            ShowBattleResult(platoonOne, platoonTwo);
        }

        private void ShowBattleResult(Platoon platoonOne, Platoon platoonTwo)
        {
            Console.WriteLine("Бой закончен пора определить победителя!");
            if (platoonOne.GetCount() == 0)
            {
                Console.WriteLine($"Солдаты страны {platoonOne.Name} потерпели поражение.");
            }

            if (platoonTwo.GetCount() == 0)
            {
                Console.WriteLine($"Солдаты страны {platoonTwo.Name} потерпели поражение.");
            }

            if (platoonOne.GetCount() == 0 && platoonTwo.GetCount() == 0)
            {
                Console.WriteLine("Погибли все !");
            }
        }
    }

    class Platoon
    {
        private List<Soldier> _soldiers = new List<Soldier>();

        public string Name { get; private set; }

        public Platoon()
        {
            CreateNewPlatoon(_soldiers);
            CreateNameCountry();
        }

        public void DeleteSolder()
        {
            _soldiers.RemoveAt(0);
        }

        public Soldier GetSoldier()
        {
            Soldier soldier = _soldiers[0];
            return soldier;
        }

        public int GetCount()
        {
            int countSoldiers = _soldiers.Count;
            return countSoldiers;
        }

        private void CreateNameCountry()
        {
            Console.WriteLine("Укажите название страны!");
            Name = Console.ReadLine();
        }

        private void CreateNewPlatoon(List<Soldier> soldier)
        {
            int numberOfSoldiers = 30;
            for (int i = 0; i < numberOfSoldiers; i++)
            {
                soldier.Add(new Soldier());
            }
        }
    }

    class Soldier
    {
        private Random _random = new Random();

        public int Healts { get; private set; }
        public int Damage { get; private set; }
        public int Armor { get; private set; }

        public Soldier()
        {
            CreateHealth();
            CreateDamage();
            CreateArmor();
        }

        public int TakesDamage(int health, int damage, int armor)
        {
            health -= damage - armor;
            Healts = health;
            return Healts;
        }

        private void CreateHealth()
        {
            int minHealth = 90;
            int maxHealth = 120;
            Healts = _random.Next(minHealth, maxHealth);
        }

        private void CreateDamage()
        {
            int minDamage = 10;
            int maxDamage = 25;
            Damage = _random.Next(minDamage, maxDamage);
        }

        private void CreateArmor()
        {
            int minArmor = 1;
            int maxArmor = 10;
            Armor = _random.Next(minArmor, maxArmor);
        }
    }
}
