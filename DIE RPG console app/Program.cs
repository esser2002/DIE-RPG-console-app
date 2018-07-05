using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DIE_RPG_console_app
{
    class Program
    {
        public const int readTime = 1000;
        //default should be 1000

        public int roundCounter;
        System.Random rnd = new System.Random();
        public int orcsSlain;
        public int testVar;
        public string currentGroupType;

        public void IncreaseVar()
        {
            testVar = +1;
        }

        //private int hp;
        //private int attack;
        //private int defence;
        //private int dam;
        public class Weapon
        {
            public string Name;
            public int Dam;
            public int AttackMod;
            public int DefenceMod;

            public Weapon(int dam, int attmod, int defmod, string name)
            {
                Dam = dam;
                AttackMod = attmod;
                DefenceMod = defmod;
                Name = name;
            }

            public string WeaponInfo()
            {
                return ("Name: " + Name + ". Damage: " + Dam);

            }



        }

        public class OffHand
        {
            public string Name;
            public int DefenceBonus;
            public int DefenceMod;
            public string Type;


            public OffHand(int defbonus, int defencemod, string name, string type)
            {
                DefenceBonus = defbonus;
                DefenceMod = defencemod;
                Name = name;
                Type = type;
            }
        }

        public class Item
        {
            //public interface IActiveEffect { };       
        }

        public class Warrior
        {
            public string Name;
            public int Hp;
            public int AttackTrue;
            public int DefenceTrue;
            public int AttackEffective;
            public int DefenceEffective;
            public int Dam;
            public Weapon EquippedWeapon;
            public OffHand EquippedOffHand;
            public List<Item> Inventory;

            public Warrior(int hp, int attack, int defence, string name, Weapon weapon, OffHand offhand)
            {
                Hp = hp;
                AttackTrue = attack;
                DefenceTrue = defence;
                Name = name;
                EquippedWeapon = weapon;
                EquippedOffHand = offhand;

                UpdateStats();
            }
            public void UpdateStats()
            {
                AttackEffective = AttackTrue + EquippedWeapon.AttackMod;
                DefenceEffective = DefenceTrue + EquippedWeapon.DefenceMod + EquippedOffHand.DefenceMod;
                Dam = EquippedWeapon.Dam;
            }

            public void TakeDamage(int dam)
            {
                Hp -= dam;
            }

            public int CalcAttack(System.Random rnd)
            {
                return (rnd.Next(1, AttackEffective + 1));
            }

            public int CalcDefence(System.Random rnd)
            {
                return (rnd.Next(1, DefenceEffective + 1) + EquippedOffHand.DefenceBonus);
            }

            public int CalcDefenceMultibleOpponents(System.Random rnd, int enemies)
            {
                return ((rnd.Next(1, DefenceEffective + 1) / enemies) + EquippedOffHand.DefenceBonus);
            }

            public int CalcDamage(System.Random rnd)
            {
                return (rnd.Next(0, Dam + 1) + Dam);
            }

            public bool Alive()
            {
                if (Hp > 0)
                    return (true);
                else
                    return (false);
            }

            public override string ToString()
            {
                return Name + " HP: " + Hp;
            }
        }

        static void Main(string[] args)
        {
            Program knutnutnutknut = new Program();
            knutnutnutknut.Run();
        }

        public void Run()
        {
            Weapon Axe = new Weapon(4, 0, 0, "axe");
            OffHand none = new OffHand(0, 0, "nothing", "NONE");
            OffHand shield = new OffHand(1, 3, "shield", "SHIELD");
            Warrior player = new Warrior(10, 10, 10, "Fjeldulf", Axe, shield);
            Warrior Gundar = new Warrior(10, 5, 10, "Gundar", Axe, shield);

            Warrior orc1 = new Warrior(5, 10, 5, "Orc", Axe, none);

            List<Warrior> orcs = new List<Warrior>();
            for (int i = 0; i < 3; i++)
            {
                orcs.Add(new Warrior(5, 5, 5, "Orc", Axe, none));
            }

            GroupCombatStart(player, orcs);
            Sleep(2);
            PrintNewline(player.Name + " died after slaying " + orcsSlain + " orcs");
            // for (int i = 0; 0 < player.Hp; i++)
            // {
            //     Warrior orci = new 
            // }
            //CombatStart(player, orc1);        
            //CombatStart(player, Gundar);


            
            //string t = Console.ReadLine();            
            Console.ReadLine();
        }



        public void CombatStart(Warrior w1, Warrior w2)
        {
            PrintNewline("The combat begins!");
            roundCounter = 0;
            Sleep(1);
            CombatOverCheck(w1, w2, true);
            

        }

        // The body of combat rounds. Ends in CombatOverCheck()
        public void CombatRound(Warrior w1, Warrior w2)
        {
            roundCounter += 1;
            int attackValueW1 = w1.CalcAttack(rnd);
            int attackValueW2 = w2.CalcAttack(rnd);
            int defenceValueW1 = w1.CalcDefence(rnd);
            int defenceValueW2 = w2.CalcDefence(rnd);
            //PrintNewline(w1.Name + attackValueW1 + defenceValueW1);
            //PrintNewline(w2.Name + attackValueW2 + defenceValueW2);
            if (attackValueW1 > defenceValueW2)
            {
                int currentDam = w1.CalcDamage(rnd);
                w2.TakeDamage(currentDam);
                PrintHitAttack(w1, w2, currentDam);
            }
            else
            {
                PrintParry(w1, w2);
            }

            Sleep(2);
            if (attackValueW2 > defenceValueW1)
            {
                int currentDam = w2.CalcDamage(rnd);
                w1.TakeDamage(currentDam);
                PrintHitAttack(w2, w1, currentDam);
            }
            else
            {
                PrintParry(w2, w1);
            }

            Sleep(2);
            CombatOverCheck(w1, w2, false);

        }

        public void CombatOverCheck(Warrior w1, Warrior w2, bool firstRound)
        {
            PrintNewline(w1.Name + " has " + w1.Hp + " HP left");
            PrintNewline(w2.Name + " has " + w2.Hp + "HP left");
            Sleep(3);
            if (w1.Alive() && w2.Alive())
            {
                if (!firstRound)
                {
                    PrintNewline("Combat carries on");
                    Sleep(2);
                }
                CombatRound(w1, w2);
            }
            else
            {
                if (!w1.Alive() && !w2.Alive())
                {
                    PrintNewline(w1.Name + " and " + w2.Name + " are both dead");
                    Sleep(1.5);
                    PrintNewline("Nobody is Victorious");
                }
                else
                {
                    if (!w1.Alive())
                    {
                        PrintNewline(w1.Name + " has been slain");
                        Sleep(1.5);
                        PrintNewline(w2.Name + " is victorious");
                    }
                    else
                    {
                        PrintNewline(w2.Name + " has been slain");
                        Sleep(1.5);
                        PrintNewline(w1.Name + " is victorious");
                    }
                }
            }
        }

        public void GroupCombatStart(Warrior w1, List<Warrior> group)
        {
            PrintNewline("The combat begins!");
            roundCounter = 0;
            Sleep(1);
            GroupCombatOverCheck(w1, group, true);
        }

        public void GroupCombatRound(Warrior w1, List<Warrior> group)
        {
            roundCounter += 1;
            int attackValueW1 = w1.CalcAttack(rnd);
            int defenceValueG1 = group[0].CalcDefence(rnd);
            if (attackValueW1 > defenceValueG1)
            {
                int currentDam = w1.CalcDamage(rnd);
                group[0].TakeDamage(currentDam);
                PrintHitAttack(w1, group[0], currentDam);

            }
            else
                PrintParry(w1, group[0]);
            Sleep(2);
            for (int i = 0; i < group.Count; i++)
            {
                if (group[0].CalcAttack(rnd) > w1.CalcDefenceMultibleOpponents(rnd, group.Count))
                {                    
                    int currentDam = group[0].CalcDamage(rnd);
                    w1.TakeDamage(currentDam);
                    PrintHitAttack(group[i], w1, currentDam);
                }
                else
                    PrintParry(group[i], w1);
                Sleep(2);
            }
            //Console.WriteLine("b4 dead members are removed");
            RemoveDeadMembers(group);
            //Console.WriteLine("dead members removed. " + group.Count + " members left");
            GroupCombatOverCheck(w1, group, false);

        }

        public void GroupCombatOverCheck(Warrior w1, List<Warrior> group, bool firstround)
        {
            if (GroupAlive(group))
            {
                currentGroupType = group[0].Name;
            }
            PrintNewline(w1.Name + " has " + w1.Hp + " HP left");
            for (int i = 0; i < group.Count; i++)
            {
                PrintNewline(group[i].Name + " has " + group[i].Hp + " HP left");
            }
            Sleep(group.Count/2 + 1);
            if (w1.Alive() && GroupAlive(group))
            {
                if (!firstround)
                {
                    PrintNewline("Combat carries on");
                }
                GroupCombatRound(w1, group);
            }
            else
            {
                if (!w1.Alive() && !GroupAlive(group))
                {
                    PrintNewline("All combetants have been slain");
                    Sleep(1.5);
                    PrintNewline("Nobody is victorious");
                }
                else
                {
                    if (!w1.Alive())
                    {
                        PrintNewline(w1.Name + " has been slain");
                        Sleep(1.5);
                        PrintNewline(group[0].Name + " and his group is victorious");
                    }
                    else
                    {
                        PrintNewline(currentGroupType + " and his group has been slain");
                        Sleep(1.5);
                        PrintNewline(w1.Name + " is victorious");
                    }
                }
            }
        }
        public void PrintNewline(string newText)
        {
            Console.WriteLine(newText);
        }

        public void PrintHitAttack(Warrior attacker, Warrior defender, int currentDam)
        {
            PrintNewline(attacker.Name + " strikes " + defender.Name + " with his " + attacker.EquippedWeapon.Name + " for " + currentDam + " points of damage");
        }

        public void PrintParry(Warrior attacker, Warrior defender)
        {
            if (ShieldWeild(defender.EquippedOffHand))
            {
                PrintNewline(attacker.Name + "'s attack is blocked by " + defender.Name + "'s " + defender.EquippedOffHand.Name);
            }
            else
            {
                PrintNewline(attacker.Name + "'s " + attacker.EquippedWeapon.Name + " is parried by " + defender.Name);
            }
        }

        public bool GroupAlive(List<Warrior> group)
        {
            //Console.WriteLine("groupAlive triggered");
            int livecount = 0;
            for (int i = 0; i < group.Count; i++)
            {
                if (group[i].Alive())
                {
                    livecount += 1;
                }
            }
            if (livecount == 0)
            {
                return false;

            }
            else
                return true;
        }

        public bool ShieldWeild(OffHand offHand)
        {
            if (offHand.Type == "SHIELD")
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
        public void RemoveDeadMembers(List<Warrior> group)
        {
            List<int> whomsveToRemove = new List<int>();
            for (int i = group.Count - 1; i >= 0; i--)
            {
                if (!group[i].Alive())
                {
                    orcsSlain += 1;
                    whomsveToRemove.Add(i);
                    PrintNewline(group[i].Name + " has been slain");
                    Sleep(1.5);
                }

            }
            for (int i = 0; i < whomsveToRemove.Count; i++)
            {
                group.RemoveAt(whomsveToRemove[i]);
            }
        }

        public void Sleep(double seconds)
        {
            Thread.Sleep((int)(seconds * readTime));
        }

    }
}
